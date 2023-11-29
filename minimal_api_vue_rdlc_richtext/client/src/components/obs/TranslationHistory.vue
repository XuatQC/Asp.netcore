<template>
  <div class="">
      <div class="function-bar">
          <div class="tw-flex">
              <div class="tw-font-medium tw-mr-4 tw-whitespace-nowrap tw-flex tw-items-center">{{t('translationHistory.item')}}</div>
              <div class="">
                  <select-box :rows="topicToBeSelected" bindingProp="name" :displayProps = "['name']" displayText="name" v-model="selectedValue.selectedItem" class="tw-w-48"
                  ></select-box>
              </div>
          </div>
          <div class="tw-flex tw-ml-6 tw-items-center">
              <span class="tw-font-medium tw-mr-1"> Rev. </span>
              <div class="tw-border tw-p-1">
                  <div class="tw-flex">
                      <q-radio keep-color v-model="selectedValue.revValue" val="0" size="sm"
                       :label="t('translationHistory.optionOnly')"  />
                      <q-radio keep-color v-model="selectedValue.revValue" val="1" size="sm"
                     :label="t('translationHistory.optionAll')"  />
                  </div>
              </div>
          </div>
          <button class="btn tw-block tw-ml-auto tw-mx-4 close" @click="closePopup">{{t('translationHistory.close')}}</button>
      </div>
      <div class="tw-min-h-[400px]">
        <s-table :columns="headerTable" :rows="filteredValue">
          <template #col1="{ context }">{{ context.item }}</template>
          <template #col2="{ context }">Rev.{{ context.revisions }}</template>
          <template #col3="{ context }"><rich-text v-model="context.japanese" plain></rich-text></template>
          <template #col4="{ context }"><rich-text v-model="context.english" plain></rich-text></template>
          <template #col5="{ context }">{{ context.translated }}</template>
        </s-table>
      </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue';
import STable from 'components/common/STable.vue';
import dialog from 'utilities/dialog';
import { OBS_INPUT_LABEL, TRANS_HIST_ITEM } from 'helpers/enum';
import { useI18n } from 'vue-i18n';
import wktTransHistoryService from 'services/wkt-trans-history.service';
import { TRAN_FULL_TEXT } from 'helpers/constant';
import RichText from 'components/common/RichText.vue';
import SelectBox from 'components/common/SelectBox.vue';
// 1) ======= INITIALIZATION ========
const { t } = useI18n();
const props = defineProps({
  wktTranHisCondition: {
    type: Object,
  },
  lang: {
    type: Object,
  },
});
const wktTransHistoryRequest = {
  plantName: props.wktTranHisCondition.plantName,
  kinds: props.wktTranHisCondition.kinds,
  fields: props.wktTranHisCondition.fields,
  partId: props.wktTranHisCondition.partId,
  serialNum: props.wktTranHisCondition.serialNum,
  revisions: props.wktTranHisCondition.revisions,
  englishEdition: props.wktTranHisCondition.language,
  displayLanguage: props.lang.code,
};
// 2) ======= VARIABLE REF ========
const translateHistories = ref([]);
const condition = {
  translated: '',
};

const selectedValue = ref({
  selectedItem: '',
  revValue: '0',
});

const topicToBeSelected = ref([
  {
    id: -1,
    value: '',
    name: '',
  },
  {
    id: 1,
    value: TRANS_HIST_ITEM.TITLE,
    name: OBS_INPUT_LABEL.TITLE,
  },
  {
    id: 2,
    value: TRANS_HIST_ITEM.SCOPE,
    name: OBS_INPUT_LABEL.SCOPE,
  },
  {
    id: 3,
    value: TRANS_HIST_ITEM.FACT,
    name: OBS_INPUT_LABEL.FACT,
  },
  {
    id: 4,
    value: TRANS_HIST_ITEM.CONCLUSION,
    name: OBS_INPUT_LABEL.CONCLUSION,
  },
]);

const headerTable = ref([
  {
    key: 'col1',
    label: t('translationHistory.table.itemCol'),
    colClass: 'tw-w-[240px]',
  },
  {
    key: 'col2',
    label: t('translationHistory.table.revCol'),
    colClass: 'tw-w-[100px]',

  },
  {
    key: 'col3',
    label: t('translationHistory.table.japaneseCol'),
    colClass: 'tw-w-[200px]',

  },
  {
    key: 'col4',
    label: t('translationHistory.table.englishCol'),
    colClass: 'tw-w-[200px]',

  },
  {
    key: 'col5',
    label: t('translationHistory.table.transCol'),
    colClass: 'tw-w-[150px]',
  },
]);

const filteredValue = computed(() => {
  if (selectedValue.value.selectedItem === '') {
    if (+selectedValue.value.revValue === 0) {
      return translateHistories.value.filter((instance) => instance.translated === TRAN_FULL_TEXT);
    }
  } else {
    if (+selectedValue.value.revValue === 0) {
      return translateHistories.value.filter((instance) => instance.translated === TRAN_FULL_TEXT
    && instance.item.slice(0, selectedValue.value.selectedItem.length) === selectedValue.value.selectedItem);
    }
    return translateHistories.value.filter((instance) => instance.item.slice(0, selectedValue.value.selectedItem.length) === selectedValue.value.selectedItem);
  }
  return translateHistories.value;
});

// 3) ======= METHOD/FUNCTION ========

// ポップアップを閉じるイベント
const closePopup = (value) => {
  if (value != null) dialog.hide(value);
  else dialog.hide();
};

// データロードイベント
const loadData = async () => {
  await wktTransHistoryService.delAll();
  await wktTransHistoryService.processData(wktTransHistoryRequest);
  translateHistories.value = await wktTransHistoryService.list(condition);
};
// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(async () => {
  await loadData();
});
</script>

<style scoped>
.function-bar {
@apply tw-flex tw-items-center tw-pb-2;
}
</style>
