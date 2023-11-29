<template>
  <div class="tw-grid tw-grid-cols-3 tw-gap-2">
    <template v-for="iter in settingList" :key="iter" >
      <template v-if="iter.hidden === false">
        <div class="tw-flex tw-items-center">{{ iter.name }}</div>
        <div class="tw-flex items-center tw-col-span-2 tw-border tw-p-1 tw-pr-4 tw-flex-1">
        <div v-for="option in iter.options" :key="option.label" class="tw-flex tw-items-center tw-mr-10 tw-min-w-[56px]">
          <q-radio class="tw-mr-2" :val="option.value" :name="iter.name" v-model="settingValue[iter.varName]" :disable="iter.disabled" :label="option.label"/>
        </div>
      </div>
    </template>
    </template>
  </div>
  <div class="button-wrapper">
    <button class="btn" @click="acceptHandler">OK</button>
    <button class="btn close" @click="closePopup">{{ $t('cancel') }}</button>
  </div>
</template>
<script setup>
import dialog from 'utilities/dialog';
import { onMounted, ref } from 'vue';
import { useI18n } from 'vue-i18n';
import {
  PRINT_MODE, NUMBER_AS_BOOL, FORM_NAME, DIALOG_RETURN_VAL,
} from 'helpers/enum';

// 1) ======= INITIALIZATION ========
const { t } = useI18n();
const props = defineProps({
  formName: {
    type: String,
    required: true,
  },
  printMode: {
    type: String,
    required: true,
  },
  settingValue: {
    type: Object,
    default: () => ({
      characterFormat: NUMBER_AS_BOOL.FALSE,
      photoPrinting: NUMBER_AS_BOOL.FALSE,
      printComments: NUMBER_AS_BOOL.FALSE,
    }),
  },
});

// 2) ======= VARIABLE REF ========
const settingValue = ref(
  {
    characterFormat: NUMBER_AS_BOOL.FALSE,
    photoPrinting: NUMBER_AS_BOOL.FALSE,
    printComments: NUMBER_AS_BOOL.FALSE,
  },
);
const settingList = ref({
  characterFormat: {
    name: t('printOption.textFormat.name'),
    varName: 'characterFormat',
    hidden: false,
    disabled: false,
    options: [
      {
        value: NUMBER_AS_BOOL.TRUE,
        label: t('printOption.textFormat.rich'),
      },
      {
        value: NUMBER_AS_BOOL.FALSE,
        label: t('printOption.textFormat.clear'),
      },
    ],
  },
  photoPrinting: {
    name: t('printOption.photo'),
    varName: 'photoPrinting',
    hidden: false,
    disabled: false,
    options: [
      {
        value: NUMBER_AS_BOOL.TRUE,
        label: t('printOption.print'),
      },
      {
        value: NUMBER_AS_BOOL.FALSE,
        label: t('printOption.notPrint'),
      },
    ],
  },
  printComments: {
    name: t('printOption.comment'),
    varName: 'printComments',
    hidden: false,
    disabled: false,
    options: [
      {
        value: NUMBER_AS_BOOL.TRUE,
        label: t('printOption.print'),
      },
      {
        value: NUMBER_AS_BOOL.FALSE,
        label: t('printOption.notPrint'),
      },
    ],
  },
});

// 3) ======= METHOD/FUNCTION ========
// 現在ポップアップを閉じるイベント
const closePopup = () => {
  dialog.hide(DIALOG_RETURN_VAL.CANCEL);
};

//  承認ボタン押下イベント
const acceptHandler = () => {
  dialog.hide(settingValue.value);
};

// コンポーネントが初期化された時の表示値を取得する
const loadForm = () => {
  // settingListの選択肢を表示・非表示するために印刷モードを確認する
  if (props.printMode !== PRINT_MODE.PRINT && props.printMode !== PRINT_MODE.WORD && props.printMode !== PRINT_MODE.EXCEL) {
    return;
  }
  if (props.printMode === PRINT_MODE.WORD
  || props.printMode === PRINT_MODE.EXCEL) {
    // コメント印刷の選択肢を非表示する
    settingList.value.printComments.hidden = true;
    if (props.formName === FORM_NAME.SOI
     || props.formName === FORM_NAME.SOI_REFER) {
      // コメント印刷を読み込み専用で表示する
      settingValue.value.printComments = NUMBER_AS_BOOL.TRUE;
      settingList.value.printComments.disabled = true;
    } else if (props.formName === FORM_NAME.WC || props.formName === FORM_NAME.WC_EDIT || props.formName === FORM_NAME.WC_REFER) {
      settingValue.value.printComments = NUMBER_AS_BOOL.FALSE;
      settingList.value.printComments.disabled = true;
    } else {
      settingList.value.printComments.disabled = false;
    }
  }

  if (props.formName === FORM_NAME.ANALYSIS_WC) {
    settingValue.value.characterFormat = NUMBER_AS_BOOL.TRUE;
    settingList.value.characterFormat.disabled = true;
    settingValue.value.printComments = NUMBER_AS_BOOL.FALSE;
    settingList.value.printComments.disabled = true;
  } else if (props.formName === FORM_NAME.WC_LIST) {
    settingValue.value.characterFormat = NUMBER_AS_BOOL.TRUE;
    settingList.value.characterFormat.disabled = true;
  } else if (props.formName === FORM_NAME.OUTPUT_WC) {
    settingValue.value.characterFormat = NUMBER_AS_BOOL.TRUE;
    settingList.value.characterFormat.disabled = true;
    settingList.value.printComments.disabled = true;
  }
};
// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(() => {
  settingValue.value = props.settingValue;
  loadForm();
});
</script>

<style scoped>
.button-wrapper {
  @apply tw-flex tw-justify-around tw-pt-4;
}
</style>
