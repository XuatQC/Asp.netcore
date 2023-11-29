<template>
  <div class="tw-w-full">
    <div class="tw-mb-1">
      <div class="tw-font-bold">事実の紐づけ Related Facts</div>
      <div>紐づける事実を選択してください。※複数選択可</div>
      <div>Please choose Related Facts. ※ multiple choice</div>
    </div>
    <div class="table-multi-checker tw-select-none">
      <table class="checker-table" aria-describedby="checkerTable">
        <tr>
          <th class="tw-w-12">No</th>
          <th>事実</th>
        </tr>
        <tr v-for="(facts, index) in relatedFacts" :key="index.id" @click="selectItem(facts)" :class="{
          'checker-row-selected': facts.selected,
          'checker-row': !facts.selected
        }">
          <td class="tw-pr-2">({{ index + 1 }})</td>
          <td class="tw-w-full tw-pl-1">
            <rich-text v-if="isEnglish" v-model="facts.factTrans" plain></rich-text>
            <rich-text v-else v-model="facts.fact" plain></rich-text>
          </td>
        </tr>
        <template v-if="rowPlaceHolderOffset > 0">
          <tr class="" v-for="index in (rowPlaceHolderOffset)" :key="index">
            <td :class="{
              '!tw-border-y-0': index != rowPlaceHolderOffset,
              '!tw-border-t-0': index === rowPlaceHolderOffset
            }">&nbsp;</td>
            <td :class="{
              '!tw-border-y-0':
                index != rowPlaceHolderOffset,
              '!tw-border-t-0': index === rowPlaceHolderOffset
            }">&nbsp;</td>
          </tr>
        </template>
      </table>
    </div>
    <div class="button-container">
      <button class="btn tw-mx-8" @click="okButtonHandler(selectedFact)">OK</button>
      <button class="btn close" @click="closePopup()">{{ $t('cancel') }}</button>
    </div>
  </div>
</template>

<script setup>
import {
  computed, onMounted, ref,
} from 'vue';
import dialog from 'utilities/dialog';
import { SELECT_BOX_PLACEHOLDER } from 'helpers/enum';
import RichText from 'components/common/RichText.vue';
import cloneDeep from 'lodash/cloneDeep';
import { useAppStore } from 'stores/app-store';
import { LANGUAGE } from 'helpers/constant';

// 1) ======= INITIALIZATION ========
const props = defineProps({
  relatedFact: {
    type: Array,
    default: () => [],
  },
  conclusion: {
    type: Object,
    default: () => {},
  },
});
const appStore = useAppStore();

// 2) ======= VARIABLE REF ========
const relatedFacts = ref([]);
const currentConclusion = ref({});
const selectedFact = computed(() => relatedFacts.value.filter((value) => value.selected === true));
const isEnglish = computed(() => appStore.language.code === LANGUAGE.ENGLISH_FIRST_LETTER);
const rowPlaceHolderOffset = computed(() => SELECT_BOX_PLACEHOLDER.ROWS_OF_PLACEHOLDER - relatedFacts.value.length);

// 3) ======= METHOD/FUNCTION ========
// ポップアップを閉じるイベント
const closePopup = () => {
  dialog.hide();
};

// connectFactにfactNumが含まれるなら、selected = trueとセットする
const getListOfConnectFact = (connectFact) => {
  if (!connectFact) return;
  const connectFactList = connectFact.split(',').map(Number);
  for (const connectionFact of connectFactList) {
    const relatedFact = relatedFacts.value.find((x) => x.factNum === connectionFact);
    if (relatedFact) relatedFact.selected = true;
  }
};

// 選択されているfactの値をstringに変換する
const getConnectFactAsString = (facts) => {
  if (!facts) return '';
  let factNumString = '';
  for (const fact of facts) {
    factNumString += (fact.factNum + (fact === facts[facts.length - 1] ? '' : ','));
  }
  return factNumString;
};

// OKボタン押下イベント
const okButtonHandler = (facts) => {
  const connectedFact = getConnectFactAsString(facts);
  currentConclusion.value.connectFact = connectedFact;
  closePopup();
};

// relatedFactの1つ項目を選択するイベント
const selectItem = (currentRow) => {
  currentRow.selected = !currentRow.selected;
};

// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(() => {
  relatedFacts.value = cloneDeep(props.relatedFact);
  currentConclusion.value = props.conclusion;
  getListOfConnectFact(currentConclusion.value.connectFact);
});
</script>

<style scoped>
th,
td {
  @apply tw-border;
}

.checker-table {
  width: 30vw;
}

.button-container {
  @apply tw-mt-2 tw-flex tw-justify-center;
}

.checker-row {
  cursor: pointer;
}

.checker-row:hover {
  @apply tw-bg-slate-100;
}

.checker-row-selected {
  @apply tw-bg-slate-500 tw-text-slate-100;
}
</style>
