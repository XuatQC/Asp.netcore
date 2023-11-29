<template>
  <form action="javascript:;" class="tw-flex tw-justify-center">
    <div class="tw-my-4 tw-space-y-3">
      <div class="tw-flex tw-pb-1">
        <div class="tw-w-[200px] tw-leading-4">接続先<br /><span class="tw-whitespace-nowrap">Connection destination</span></div>
        <div class="tw-w-[220px]">
          <select-box :rows='connectionPlaces' :displayProps="['label']" displayText="label" v-model="info.connectionId"/>
        </div>
      </div>
      <div class="tw-flex tw-pb-1">
        <div class="tw-w-[200px] tw-leading-4">ピアレビュー番号<br />Peer Review No</div>
        <div class="tw-w-[220px]">
          <select-box :rows='peerReviews' :displayProps="['plantName', 'reviewName']" v-model="info.peerReviewId"
            optionsWidth="300px" displayText="plantName"/>
        </div>
      </div>
      <div class="tw-flex">
        <div class="tw-w-[200px] tw-leading-4">イニシャル入力<br />3 initials</div>
        <div class="tw-w-[220px]">
          <q-input outlined v-model="info.initial" dense ref="initial" :disable="!isConnected"/>
        </div>
      </div>
      <div class="tw-flex">
        <div class="tw-w-[200px] tw-leading-4">パスワード入力<br />Password</div>
        <div class="tw-w-[220px]">
          <q-input outlined v-model="info.password" dense ref="password" @keydown.enter="login" type="password" :disable="!isConnected"/>
        </div>
      </div>
      <div class="tw-flex tw-justify-center tw-space-x-4 !tw-mt-7">
        <div class="tw-w-1/2 tw-text-right">
          <button class="btn" :class="{ 'disabled': isConnected }" @click="connect"
            :disabled="isConnected">接続<br />Connect</button>
        </div>
        <div class="tw-w-1/2">
          <div class="tw-w-3/5 tw-text-center">
            <button class="btn" @click="login" :class="{ 'disabled': !isConnected }"
              :disabled="!isConnected">ログイン<br />Login</button>
          </div>
        </div>
      </div>
    </div>
  </form>
</template>

<script setup>
import {
  ref, reactive, onMounted, computed,
} from 'vue';
import { useRouter } from 'vue-router';
import dialog from 'utilities/dialog';
import AuthService from 'services/auth.service';
import { MSG, MODAL } from 'helpers/constant';
import peerReviewService from 'services/peer-review.service';
import { FIELD_ERROR_LOGIN } from 'helpers/enum.js';
import SharedFolderService from 'services/shared-folder.service';
import DirectoryService from 'services/directory.service';
import SelectBox from 'components/common/SelectBox.vue';
import { useAppStore } from 'stores/app-store';
import { CONNECTION_PLACE_OPTION } from 'helpers/options';
import { changeLanguage } from 'utilities/language';
import { SHARE_FOLDER_NAME, SHARE_FOLDER_PATH } from 'helpers/enum';
import { sprintf } from 'sprintf-js';

// 1) ======= INITIALIZATION ========
const router = useRouter();
const appStore = useAppStore();

// 2) ======= VARIABLE REF ========
const peerReviews = ref([]);
const initial = ref();
const password = ref();

const connectionPlaces = ref([]);
const info = reactive({
  peerReviewId: null,
  connectionId: null,
  initial: null,
  password: null,
});

const selectedConnection = computed(() => connectionPlaces.value.find((i) => i === info
  .connectionId));

const selectedPeerReview = computed(() => peerReviews.value.find((i) => i === info.peerReviewId));

const isConnected = ref(false);

// 3) ======= METHOD/FUNCTION ========
// API呼び出し前にイニシャルとパスワードの値を確認する
const validate = () => {
  if (!selectedPeerReview.value || !info.initial || !info.password) {
    dialog.showMessage(
      MSG.LOGIN_REQUIRED_HEADER,
      MSG.LOGIN_REQUIRED_MSG,
    ).then(() => {
      if (!info.initial) {
        initial.value.focus();
      } else if (!info.password) {
        password.value.focus();
      }
    });
    return false;
  }
  return true;
};

// 言語変更確認
const changeDisplayLanguage = () => {
  const { Lang } = appStore.peerReviewMaster;
  changeLanguage({ code: Lang });
};

// ピアレビューマスタの値をストレージに設定する
const setPeerReviewMaster = async () => {
  //  レビューメンバーの値を保存する
  const peerReviewMaster = {
    EvalArea: '',
    Lang: '',
    blnAD: false,
    blnTL: false,
    blnJudge: false,
    blnTrans: false,
  };
  const peerReviewMasterRes = await peerReviewService.getPeerReviewMaster({
    PlantName: selectedPeerReview.value.plantName,
    Initial: info.initial,
    IsExistsField: true,
  });
  const peerReviewMasterPayload = peerReviewMasterRes.payload[0];
  peerReviewMaster.EvalArea = peerReviewMasterPayload?.respnsArea ?? '';
  peerReviewMaster.Lang = peerReviewMasterPayload?.language ?? '';
  peerReviewMaster.blnAD = peerReviewMasterPayload?.coordinator ?? '';
  peerReviewMaster.blnTL = peerReviewMasterPayload?.tl ?? '';
  peerReviewMaster.blnJudge = peerReviewMasterPayload?.judge ?? '';
  peerReviewMaster.blnTrans = peerReviewMasterPayload?.trans ?? '';
  appStore.setPeerReviewMaster(peerReviewMaster);
  changeDisplayLanguage();
};

const getDirectory = async (wordFolder, photoLiveFolder, photoPastFolder, docPutTrans, docInTrans, docInTransFin) => {
  const directory = await DirectoryService.getDirectory();
  const docPutLocal = sprintf(SHARE_FOLDER_PATH.PUT_LOCAL, { directory: directory.payload });
  const folderStore = {
    wordFolder: wordFolder?.length > 0 ? wordFolder : sprintf(SHARE_FOLDER_PATH.WORD, { plantName: selectedPeerReview.value.plantName }),
    photoLiveFolder: photoLiveFolder?.length > 0 ? photoLiveFolder : sprintf(SHARE_FOLDER_PATH.PHOTO_LIVE, { plantName: selectedPeerReview.value.plantName }),
    photoPastFolder: photoPastFolder?.length > 0 ? photoPastFolder : sprintf(SHARE_FOLDER_PATH.PHOTO_PAST, { plantName: selectedPeerReview.value.plantName }),
    docPutTrans: docPutTrans?.length > 0 ? docPutTrans : sprintf(SHARE_FOLDER_PATH.PUT_TRANS, { plantName: selectedPeerReview.value.plantName }),
    docInTrans: docInTrans?.length > 0 ? docInTrans : sprintf(SHARE_FOLDER_PATH.IN_TRANS, { plantName: selectedPeerReview.value.plantName }),
    docInTransFin: docInTransFin?.length > 0 ? docInTransFin : sprintf(SHARE_FOLDER_PATH.IN_TRANS_FINAL, { plantName: selectedPeerReview.value.plantName }),
    docPutLocal,
  };
  return folderStore;
};

// ローカル変数をセッションストレージに追加する
const setLocalValue = async () => {
  const sharedFolderResponse = await SharedFolderService.getSharedFolderList();

  let errorMsg = '';
  let wordFolder = '';
  let photoLiveFolder = '';
  let photoPastFolder = '';
  let docPutTrans = '';
  let docInTrans = '';
  let docInTransFin = '';

  for (const sharedFolder of sharedFolderResponse.payload) {
    if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.WORD, { connection: selectedConnection.value.label })) {
      wordFolder = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.PHOTO_LIVE, { connection: selectedConnection.value.label })) {
      photoLiveFolder = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.PHOTO_PAST, { connection: selectedConnection.value.label })) {
      photoPastFolder = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.PUT_TRANS, { connection: selectedConnection.value.label })) {
      docPutTrans = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.IN_TRANS, { connection: selectedConnection.value.label })) {
      docInTrans = sharedFolder.path;
    } else if (sharedFolder.dispName === sprintf(SHARE_FOLDER_NAME.IN_TRANS_FINAL, { connection: selectedConnection.value.label })) {
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
  appStore.setFolderStore(folderStore);
  if (errorMsg !== '') {
    await dialog.showMessage(MODAL.TITLE_INFO, `${errorMsg}\n${MSG.ERR_REQUIRED_CONTACT}`);
  }
  setPeerReviewMaster();
};

// 表示、ログイン確認
const login = async () => {
  if (validate()) {
    const loginResponse = await AuthService.login({ initial: info.initial, pass: info.password, plantName: selectedPeerReview.value.plantName });
    if (loginResponse.code === 400) {
      dialog.showMessage(MODAL.TITLE_INFO, loginResponse.message);
    } else if (loginResponse.payload.fieldError === null) {
      appStore.setPlant(selectedPeerReview.value);
      appStore.setConnectionPlace({ connectionPlaceString: selectedConnection.value.label });
      await setLocalValue();
      router.push({ name: 'main-menu' });
    } else if (loginResponse.payload.fieldError === FIELD_ERROR_LOGIN.INITIAL) {
      dialog.showMessage(MODAL.TITLE_INFO, MSG.VERIFY_INITIAL).then(() => {
        initial.value.focus();
      });
    } else if (loginResponse.payload.fieldError === FIELD_ERROR_LOGIN.PASSWORD) {
      dialog.showMessage(MODAL.TITLE_INFO, MSG.VERIFY_PASSWORD);
      password.value.focus();
    }
  }
};

// 値を接続するイベント
const connect = async () => {
  if (info.connection === null) {
    dialog.showMessage(
      MODAL.TITLE_WARNING,
      MSG.VERIFY_CONNECT_DESTINATION,
    );
    return;
  }
  const peerReviewResponse = await peerReviewService.getAll();
  peerReviews.value = peerReviewResponse.payload;
  isConnected.value = true;
};

// 4) ======= VUEJS LIFECYCLE ========

// コンポーネント初期後
onMounted(() => {
  connectionPlaces.value = CONNECTION_PLACE_OPTION;
  [info.connectionId] = connectionPlaces.value;
  connect();
});

</script>

<style scoped>
.btn {
  min-height: 40px;
  min-width: 120px;
}

input {
  height: 35px;
  width: 100%;
  border-radius: 5px;
  background-clip: text !important;
  -webkit-background-clip: text !important;
}

select {
  height: 35px;
  width: 100%;
  border-radius: 5px;
}</style>
