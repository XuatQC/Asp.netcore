<template>
  <div class="tw-w-full">
    <div class="tw-font-bold tw-text-center">
      他のシートに転送 Transfer OBS Fact To Other Sheet
    </div>
    <div class="tw-border tw-mb-2">
      <div class="tw-grid tw-grid-cols-6 tw-gap-y-2 tw-m-2">
        <div class="tw-text-left ">番号<br/>Num</div>
        <div class="tw-col-span-5 tw-border tw-w-fit tw-pr-1" :class="{ 'tw-w-full': !fact?.num }">
          {{ props.obsNum }}
        </div>
        <div class="tw-text-left">観察事実<br />Facts</div>
        <div class="tw-col-span-5 tw-border tw-h-24">
          <rich-text v-if="isLanguageEnglish" v-model="fact.factTrans" rich></rich-text>
          <rich-text v-else v-model="fact.fact" rich></rich-text>
        </div>
      </div>
    </div>
    <div class="tw-border">
      <div class="tw-grid tw-grid-cols-4 tw-gap-y-2 tw-m-2">
        <div class="tw-text-left">シートの種類<br />Sheet Type</div>
        <div class="tw-col-span-3 flex items-center tw-w-full">
          <select-box
          class="tw-w-full"
          :rows="sheetTypes"
          v-model="sheetValue"
          binding-prop="value"
          :displayProps="['label']"
          display-text="label"
          />
        </div>
        <div class="tw-text-left">転送先番号<br />Transfer to No</div>
        <div class="tw-col-span-3 tw-w-full flex items-center">
          <select-box
          class="tw-w-[350px]"
          optionsWidth="650px"
          mode="column"
          :rows="computedForwarding"
          v-model="selectedForwardingNumber"
          binding-prop="id" :displayProps="['num','titlePlain']"
          display-text="num"
          />
        </div>
        <div class="tw-text-left">新しい番号<br />New No</div>
        <div class="tw-col-span-3 flex items-center">
          <button class="btn add tw-w-full" @click="newSheet()">{{ t('transferOBSFact.newSheet') }}</button>
        </div>
      </div>
    </div>
    <div class="btn-container">
      <button @click="acceptButtonHandler()" class="btn tw-mr-12">OK</button>
      <button @click="closePopup" class="btn close">{{t('cancel')}}</button>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref, computed } from 'vue';
import dialog from 'utilities/dialog';
import RichText from 'components/common/RichText.vue';
import {
  DM_DIVISION, SHEET_TYPE_NAME, TAG,
} from 'helpers/enum';
import { useAppStore } from 'stores/app-store';
import { LANGUAGE } from 'helpers/constant';
import SelectBox from 'components/common/SelectBox.vue';
import { useI18n } from 'vue-i18n';
import obsService from 'services/obs.service';

// 1) ======= INITIALIZATION ========

const { t } = useI18n();
const appStore = useAppStore();
const props = defineProps({
  fact: {
    type: Object,
    required: true,
  },
  obsNum: {
    type: String,
    required: true,
    default: '',
  },
});

// 2) ======= VARIABLE REF ========

const fact = ref({});
const sheetValue = ref({});
const sheetTypes = ref([]);
const forwardingNumbers = ref([]);
const selectedForwardingNumber = ref(0);

// forwardingNumbers内の全てオブジェクトにあるプロパティtitleのplain textを取得する
const computedForwarding = computed(() => forwardingNumbers.value.map((x) => {
  // 仮divタグを作成する
  const element = document.createElement('div');
  element.innerHTML = x.title;
  // titlePlainが含まれたxのクローン値を返す
  return { ...x, titlePlain: element.textContent || element.innerText };
}));

// 3) ======= METHOD/FUNCTION ========

// 英語が選択中か確認
const isLanguageEnglish = computed(() => appStore.language.code === LANGUAGE.ENGLISH_FIRST_LETTER);

// ポップアップを閉じるイベント
const closePopup = (value) => {
  if (value != null) dialog.hide(value);
  else dialog.hide();
};

// 'OK'ボタン押下イベント
const acceptButtonHandler = () => {
  dialog.hide(sheetValue.value);
};

// newSheetボタン押下イベント
const newSheet = () => {
  // TO DO
};

// 条件確認し、データをセレクトボックス「sheetType」に追加する
const getSheetType = async () => {
  const { dmDivision } = appStore.plant;
  if (dmDivision === DM_DIVISION.F) {
    sheetTypes.value.push({
      value: TAG.SOI, label: SHEET_TYPE_NAME.SOI,
    });
    sheetValue.value = sheetTypes.value[0].value;

    const { plantName } = appStore;
    const { EvalArea } = appStore.peerReviewMaster;

    // OBS GetNumのAPI呼び出し
    const obsNums = await obsService.getNumByPlantNameAndFields(plantName, EvalArea);
    forwardingNumbers.value = obsNums;
  } else if (dmDivision === DM_DIVISION.MAKER) {
    sheetTypes.value.push(
      {
        value: TAG.GFA, label: SHEET_TYPE_NAME.GFA,
      },
      {
        value: TAG.PFA, label: SHEET_TYPE_NAME.PFA,
      },
      {
        value: TAG.AFI, label: SHEET_TYPE_NAME.AFI,
      },
      {
        value: TAG.PD, label: SHEET_TYPE_NAME.PD,
      },
      {
        value: TAG.STR, label: SHEET_TYPE_NAME.STR,
      },
      {
        value: TAG.BP, label: SHEET_TYPE_NAME.BP,
      },
    );
  } else {
    sheetTypes.value.push(
      {
        value: TAG.GFA, label: SHEET_TYPE_NAME.GFA,
      },
      {
        value: TAG.PFA, label: SHEET_TYPE_NAME.PFA,
      },
      {
        value: TAG.AFI, label: SHEET_TYPE_NAME.AFI,
      },
      {
        value: TAG.STR, label: SHEET_TYPE_NAME.STR,
      },
    );
  }
};
// 4) ======= VUEJS LIFECYCLE ========

// コンポーネント初期後
onMounted(async () => {
  // セレクトボックスにポップアップ値をセットする
  getSheetType();
  fact.value = props.fact;
});
</script>

<style scoped>
.btn-container {
  @apply tw-flex tw-justify-center tw-items-center tw-pt-4;
}
</style>
