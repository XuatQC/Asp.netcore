<template>
  <div class="container">
      <div class="tw-whitespace-pre-line tw-pb-6">
        {{ title }}
      </div>
      <div class="tw-p-2">
          <span class="tw-pb-2">翻訳依頼 Request time limit</span>
          <select
              name="request time limit"
              v-model="currentValue.requestTimeLimitation"
              id="request_time_limit"
              :size="requestTimeLimitations.length"
              class="select-box"
          >
              <option
                v-for="requestTime in requestTimeLimitations"
                :value="requestTime"
                :key="requestTime"
                @dblclick="acceptButtonHandler"
              >
                  {{ requestTime.label }}
              </option>
          </select>
          <div class="tw-pt-4">
              <div class="tw-pb-1">言語選択 Language choice</div>
              <select-box :rows="languageChoice" v-model="currentValue.languageChoice" binding-prop="value" :display-props="['label']" display-text="label"/>
          </div>
      </div>
      <div class="btn-container">
          <button @click="acceptButtonHandler" class="btn tw-mr-12">OK</button>
          <button @click="closePopupButtonHandler(currentValue)" class="btn close">{{ $t('cancel') }}</button>
      </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import dialog from 'utilities/dialog';
import { MODAL, MSG, LANGUAGE } from 'helpers/constant';
import {
  REQUEST_FROM_SCREEN, LANG_TRANSFER, FORM_NAME, DIALOG_RETURN_VAL,
} from 'helpers/enum';
import { useAppStore } from 'stores/app-store';
import { useAuthStore } from 'stores/auth-store';
import { useI18n } from 'vue-i18n';
import {
  createTranslateData, loadTranslateDataToWktTranslate,
  outputWord, outputWordProcessing,
} from 'utilities/common';
import { sprintf } from 'sprintf-js';
import obsService from 'services/obs.service';
import { REQUEST_TIME_LIMITATION_OPTION } from 'helpers/options';
import SelectBox from 'components/common/SelectBox.vue';

// 1) ======= INITIALIZATION ========

const appStore = useAppStore();
const athStore = useAuthStore();
const { locale } = useI18n();

// 2) ======= VARIABLE REF ========

const props = defineProps({
  requestTimeLimitation: {
    type: Object,
  },
  from: {
    type: Number,
  },
  translateStatus: {
    type: Boolean,
  },
  data: {
    type: Object,
  },
  screenName: {
    type: String,
  },
  saveObs: {
    type: Function,
  },
});

const title = ref('');
const requestTimeLimitations = ref([]);
const languageChoice = ref([]);
const currentValue = ref({
  requestTimeLimitation: null,
  languageChoice: null,
});
const obs = ref({});
const pattern = ref();

// 3) ======= METHOD/FUNCTION ========

// translateデータ一覧を取得し、確認ポップアップを表示する
const confirmTranslatedData = async ([parentKind]) => {
  const confirmedTransDataRes = await outputWord({
    parentKind,
    plantNameParam: props.data.plantName,
    kind: props.data.kinds,
    field: props.data.fields,
    individual: athStore.initial,
    serialNumber: props.data.serialNum,
    revision: props.data.revisions,
    engEdition: props.data.englishEdition,
  });
  const confirmTransDataPayload = confirmedTransDataRes.payload;
  const confirmedTranslateData = [];
  if (confirmTransDataPayload.length > 0) {
    for await (const translatedData of confirmTransDataPayload) {
      const isConfirm = await dialog.showMessage(MODAL.TITLE_INFO, sprintf(
        MSG.TRANS_CONFIRM_PRINT,
        {
          plantName: translatedData.plantName,
          num: translatedData.num,
        },
      ));
      if (isConfirm === DIALOG_RETURN_VAL.OK) {
        confirmedTranslateData.push(translatedData);
      }
    }
    const isCompletedPrint = await outputWordProcessing(confirmedTranslateData);
    return isCompletedPrint;
  }
  return false;
};

// 翻訳依頼中のOBSの値を更新する
const updateTransReqOBS = async () => {
  const currentOBS = await obsService.getByNum(obs.value.num);
  if (currentOBS) {
    currentOBS.transDeadline = currentValue.value.requestTimeLimitation.label;
    currentOBS.transLang = currentValue.value.languageChoice;
    currentOBS.transStateReq = true;
    // OBSの値を変更するAPIを呼び出す
    await obsService.update(currentOBS);
  }
};

// 結果を返す前に翻訳値を確認、セットする
const acceptHandler = async () => {
  if (currentValue.value.requestTimeLimitation === null) {
    await dialog.showMessage(MODAL.TITLE_INFO, MSG.ERR_REQUIRED_RECOGZER);
  }
  // OBS(title,fact,conclusions,...)の値を保存する
  await props.saveObs(false, false);
  // OBSテーブルの値を変更する
  await updateTransReqOBS();
  await dialog.showMessage(MODAL.TITLE_INFO, MSG.DATA_SAVED);
  const parentKind = props.data.kinds;
  let isAllowNextStep = await createTranslateData({
    parentKind,
    plantNameParam: props.data.plantName,
    kinds: props.data.kinds,
    fields: props.data.fields,
    individual: athStore.initial,
    serialNumber: props.data.serialNum,
    revision: props.data.revisions,
    engEdition: props.data.transLang,
  });

  if (isAllowNextStep) {
    isAllowNextStep = await loadTranslateDataToWktTranslate(props.data.plantName);
  }

  if (isAllowNextStep) {
    await confirmTranslatedData(parentKind);
  }
};

// 'OK'ボタン押下イベント
const acceptButtonHandler = async () => {
  await acceptHandler();
  dialog.hide({ currentValue: currentValue.value, dialogResult: DIALOG_RETURN_VAL.OK });
};

// OBS値をクリアする
const clearTranslateReq = () => {
  const dataVal = props.data;
  if (props.from === REQUEST_FROM_SCREEN.DEFAULT) {
    // 値のクリア
    dataVal.trans_state_req = false;
    dataVal.trans_req_date = null;
  } else if (props.from === REQUEST_FROM_SCREEN.OBS_LIST || props.from === REQUEST_FROM_SCREEN.OBS_INPUT) {
    if (props.screenName === FORM_NAME.WC || props.screenName === FORM_NAME.WC_EDIT) {
      // WC & WC_EDIT画面内の値
      dataVal.check112 = false;
    }
  }
};

// ポップアップを閉じるイベント
const closePopupButtonHandler = (value) => {
  clearTranslateReq();
  dialog.hide({ currentValue: value, dialogResult: DIALOG_RETURN_VAL.CANCEL });
};
const loadForm = async () => {
  // 引数を渡すか確認
  if (!props.from) {
    await dialog.showMessage(MODAL.TITLE_INFO, MSG.ERROR_IN_PROCESS);
    dialog.hide();
  } else {
    pattern.value = props.from;
  }
  // 値が一覧、入力画面と一致するか確認
  const requestedScreen = [REQUEST_FROM_SCREEN.DEFAULT, REQUEST_FROM_SCREEN.OBS_LIST, REQUEST_FROM_SCREEN.OBS_INPUT];
  if (requestedScreen.includes(pattern.value)) {
    title.value = `${MSG.TRANS_TITLE}\n${appStore.folderStore.docPutTrans}`;
  }
  if (!currentValue.value.requestTimeLimitation) {
    [currentValue.value.requestTimeLimitation] = requestTimeLimitations.value;
  }
  // 言語を確認しlangChoiciesに値をセットする
  if (locale.value === LANGUAGE.JAPAN_CODE) {
    languageChoice.value = [
      {
        label: LANG_TRANSFER.JP_ENGLISH_TO_JAPANESE,
        value: LANGUAGE.JAPAN_FIRST_LETTER,
      },
      {
        label: LANG_TRANSFER.JP_JAPANESE_TO_ENGLISH,
        value: LANGUAGE.ENGLISH_FIRST_LETTER,
      },
    ];
    currentValue.value.languageChoice = LANGUAGE.JAPAN_FIRST_LETTER;
  } else {
    languageChoice.value = [
      {
        label: LANG_TRANSFER.EN_JAPANESE_TO_ENGLISH,
        value: LANGUAGE.ENGLISH_FIRST_LETTER,
      },
      {
        label: LANG_TRANSFER.EN_ENGLISH_TO_JAPANESE,
        value: LANGUAGE.JAPAN_FIRST_LETTER,
      },
    ];
    currentValue.value.languageChoice = LANGUAGE.ENGLISH_FIRST_LETTER;
  }
};

// 4) ======= VUEJS LIFECYCLE ========

// コンポーネント初期後
onMounted(() => {
  requestTimeLimitations.value = REQUEST_TIME_LIMITATION_OPTION;
  currentValue.value.requestTimeLimitation = props.requestTimeLimitation;
  obs.value = props.data;
  loadForm();
});

</script>

<style scoped>

.select-box {
  @apply tw-border tw-w-full;
}

.btn-container {
  @apply tw-flex tw-justify-center tw-items-center tw-pt-10;
}
</style>
