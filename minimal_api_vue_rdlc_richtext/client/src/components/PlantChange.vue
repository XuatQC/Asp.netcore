<template>
  <div class="tw-w-full">
    <div class="tw-flex tw-w-full">
      <div class="tw-mr-2">
        <div>
          <span class="tw-whitespace-nowrap">ピアレビュー番号</span>
          <br />
          <span class="tw-whitespace-nowrap"> Peer Preview No </span>
        </div>
      </div>
      <div class="tw-flex tw-items-center tw-w-full">
        <select-box class="tw-w-full" :rows="peerPreviewNo" :displayProps="['plantName', 'reviewName']" displayText="plantName"
          v-model="selectedValue" @change="changeEventHandler" optionsWidth="300px" />
      </div>
    </div>
  </div>
</template>

<script setup>

import { ref, onMounted } from 'vue';
import dialog from 'utilities/dialog';
import peerReviewService from 'services/peer-review.service';
import { useAuthStore } from 'stores/auth-store';
import { MSG, MODAL } from 'helpers/constant';
import { useAppStore } from 'stores/app-store';
import SharedFolderService from 'services/shared-folder.service';
import SelectBox from 'components/common/SelectBox.vue';
import { SHARE_FOLDER_NAME, SHARE_FOLDER_PATH } from 'helpers/enum';
import { sprintf } from 'sprintf-js';

// 1) ======= INITIALIZATION ========
const auth = useAuthStore();
const app = useAppStore();

// 2) ======= VARIABLE REF ========
const selectedValue = ref(null);
const peerPreviewNo = ref([]);
const oldValue = ref(null);

// 3) ======= METHOD/FUNCTION ========
const getDirectory = async (wordFolder, photoLiveFolder, photoPastFolder, docPutTrans, docInTrans, docInTransFin) => {
  const folderStore = {
    ...app.sharedFolder,
    wordFolder: wordFolder?.length > 0 ? wordFolder : sprintf(SHARE_FOLDER_PATH.WORD, { plantName: app.plant.plantName }),
    photoLiveFolder: photoLiveFolder?.length > 0 ? photoLiveFolder : sprintf(SHARE_FOLDER_PATH.PHOTO_LIVE, { plantName: app.plant.plantName }),
    photoPastFolder: photoPastFolder?.length > 0 ? photoPastFolder : sprintf(SHARE_FOLDER_PATH.PHOTO_PAST, { plantName: app.plant.plantName }),
    docPutTrans: docPutTrans?.length > 0 ? docPutTrans : sprintf(SHARE_FOLDER_PATH.PUT_TRANS, { plantName: app.plant.plantName }),
    docInTrans: docInTrans?.length > 0 ? docInTrans : sprintf(SHARE_FOLDER_PATH.IN_TRANS, { plantName: app.plant.plantName }),
    docInTransFin: docInTransFin?.length > 0 ? docInTransFin : sprintf(SHARE_FOLDER_PATH.IN_TRANS_FINAL, { plantName: app.plant.plantName }),
  };
  return folderStore;
};

// 該当値をストレージフォルダに保存する
const setFolderStore = async (sharedFolderResponse) => {
  let errorMsg = '';
  let wordFolder = '';
  let photoLiveFolder = '';
  let photoPastFolder = '';
  let docPutTrans = '';
  let docInTrans = '';
  let docInTransFin = '';
  for (const sharedFolder of sharedFolderResponse.payload) {
    if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.WORD, { connection: app.connectionPlace.connectionPlaceString })) {
      wordFolder = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.PHOTO_LIVE, { connection: app.connectionPlace.connectionPlaceString })) {
      photoLiveFolder = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.PHOTO_PAST, { connection: app.connectionPlace.connectionPlaceString })) {
      photoPastFolder = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.PUT_TRANS, { connection: app.connectionPlace.connectionPlaceString })) {
      docPutTrans = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.IN_TRANS, { connection: app.connectionPlace.connectionPlaceString })) {
      docInTrans = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.IN_TRANS_FINAL, { connection: app.connectionPlace.connectionPlaceString })) {
      docInTransFin = sharedFolder.path;
    }
  }
  if (wordFolder === '') {
    errorMsg += MSG.ERR_MSG_WORDFOLDER;
  }
  if (photoLiveFolder === '') {
    errorMsg += MSG.ERR_MSG_PHOTOLIVE;
  }
  if (photoPastFolder === '') {
    errorMsg += MSG.ERR_MSG_PHOTOPAST;
  }
  if (docPutTrans === '') {
    errorMsg += MSG.ERR_MSG_DOC_PUT_TRANS;
  }
  if (docPutTrans === '') {
    errorMsg += MSG.ERR_MSG_DOC_IN_TRANS;
  }
  if (docInTransFin === '') {
    errorMsg += MSG.ERR_MSG_DOC_IN_TRANS_FIN;
  }

  const folderStore = await getDirectory(wordFolder, photoLiveFolder, photoPastFolder, docPutTrans, docInTrans, docInTransFin);

  app.setFolderStore(folderStore);

  if (errorMsg !== '') {
    await dialog.showMessage(MODAL.TITLE_INFO, `${errorMsg}\n${MSG.ERR_REQUIRED_CONTACT}`);
  }
};

// 'OK'ボタン押下イベント
const changeEventHandler = async () => {
  const peerReviewRes = await peerReviewService.getPeerReviewMaster(
    {
      PlantName: selectedValue.value?.plantName ?? '',
      Initial: auth.initial,
      IsExistsField: true,
    },
  );
  if (peerReviewRes.payload.length === 0) {
    await dialog.showMessage(MODAL.TITLE_REQUIRED_SYNC, MSG.CANNOT_ACCESS_PLANT_NAME);
    selectedValue.value = oldValue.value;
  } else {
    oldValue.value = selectedValue.value;
    app.setPlant(selectedValue.value);

    const peerReviewMaster = {};
    const peerReviewPayload = peerReviewRes.payload[0];
    peerReviewMaster.EvalArea = peerReviewPayload?.respnsArea ?? '';
    peerReviewMaster.Lang = peerReviewPayload?.language ?? '';
    peerReviewMaster.blnAD = peerReviewPayload?.coordinator ?? '';
    peerReviewMaster.blnTL = peerReviewPayload?.tl ?? '';
    peerReviewMaster.blnJudge = peerReviewPayload?.judge ?? '';
    peerReviewMaster.blnTrans = peerReviewPayload?.trans ?? '';
    app.setPeerReviewMaster(peerReviewMaster);

    const sharedFolderResponse = await SharedFolderService.getSharedFolderList();
    setFolderStore(sharedFolderResponse);
  }
};

// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(async () => {
  const plantList = await peerReviewService.getByPlantNameAndHold();
  peerPreviewNo.value = plantList.payload;
  selectedValue.value = peerPreviewNo.value.find((x) => x.plantName === app.plantName);
  oldValue.value = selectedValue.value;
});

</script>
