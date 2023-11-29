<template>
  <span class="tw-font-bold tw-text-left tw-text-xl tw-flex tw-w-full tw-justify-center">編集者移行 Change Editor</span>
  <div class="tw-p-2">
      <span class="tw-text-left tw-font-medium">
          編集者を選択してください。<br />
          <span class="tw-text-sm">Please choose an Editor</span>
      </span>
      <div class="tw-flex tw-items-center">
          <span class="tw-mr-2">表示 Display</span>
          <div class="tw-border tw-p-1 tw-flex tw-items-center tw-mt-2">
              <div class="tw-flex tw-items-center tw-mr-4">
                  <q-radio val="default" name="display" v-model="display" label="Default"></q-radio>
              </div>
              <div class="tw-flex tw-items-center tw-mr-6">
                  <q-radio val="all" name="display" v-model="display" label="All"></q-radio>
              </div>
          </div>
      </div>
      <div class="">
          <div class="tw-pt-3 tw-h-[326px]">
              <table class="checker-table tw-w-full" aria-describedby="checkTable">
                  <tr
                      v-for="(value) in instanceListFilterByDisplay"
                      :key="value"
                      @click="selectCurrent(value)"
                      @dblclick="doubleClickTableRowHandler(value)"
                      :class="{
                          'checker-row-selected': value === selectedItem ?? null,
                          'checker-row': !value === selectedItem ?? null
                      }"
                  >
                      <th class="tw-pr-2 tw-text-left tw-font-normal">&nbsp;({{ value.initial }})</th>
                      <th class="tw-pr-2 tw-text-left tw-font-normal">&nbsp;{{ value.name }}</th>
                      <th class="tw-text-left tw-font-normal">&nbsp;{{ value.responseArea }}</th>
                  </tr>
              <template v-if="SELECT_BOX_PLACEHOLDER.ROWS_OF_PLACEHOLDER - instanceListFilterByDisplay.length > 0">
                  <tr class="" v-for="i in (SELECT_BOX_PLACEHOLDER.ROWS_OF_PLACEHOLDER - instanceListFilterByDisplay.length)" :key="i">
                  <td
                      :class="{'!tw-border-y-0': i != SELECT_BOX_PLACEHOLDER.ROWS_OF_PLACEHOLDER - instanceListFilterByDisplay.length,
                      '!tw-border-t-0': i === SELECT_BOX_PLACEHOLDER.ROWS_OF_PLACEHOLDER - instanceListFilterByDisplay.length}">&nbsp;</td>
                  <td
                      :class="{'!tw-border-y-0': i != SELECT_BOX_PLACEHOLDER.ROWS_OF_PLACEHOLDER - instanceListFilterByDisplay.length,
                      '!tw-border-t-0': i === SELECT_BOX_PLACEHOLDER.ROWS_OF_PLACEHOLDER - instanceListFilterByDisplay.length}">&nbsp;</td>
                  <td
                      :class="{'!tw-border-y-0': i != SELECT_BOX_PLACEHOLDER.ROWS_OF_PLACEHOLDER - instanceListFilterByDisplay.length,
                      '!tw-border-t-0': i === SELECT_BOX_PLACEHOLDER.ROWS_OF_PLACEHOLDER - instanceListFilterByDisplay.length}">&nbsp;</td>
                  </tr>
              </template>
              </table>
          </div>
      </div>
      <div class="tw-mt-2">
          <div class="tw-font-bold">編集者の移行後、保存と同期を行ってください。</div>
          <span class="tw-font-bold"
              >After editor's change, Please save and synchronize analysis data.</span>
      </div>
  </div>
  <div class="tw-flex tw-justify-center tw-mt-2">
      <button class="btn tw-mr-6" @click="acceptButtonHandler">OK</button>
      <button class="btn close" @click="closePopup">{{ $t('cancel') }}</button>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue';
import dialog from 'utilities/dialog';
import { SELECT_BOX_PLACEHOLDER, DIALOG_RETURN_VAL, EDITOR_SELECTION_DISPLAY } from 'helpers/enum';
import reviewMemberService from 'services/review-member.service';
import { useAppStore } from 'stores/app-store';

// 1) ======= INITIALIZATION ========
const appStore = useAppStore();
const currentResponseArea = appStore.peerReviewMaster.EvalArea;

// 2) ======= VARIABLE REF ========

const display = ref(EDITOR_SELECTION_DISPLAY.DEFAULT);
const instance = ref([]);
const selectedItem = ref(null);

// 3) ======= METHOD/FUNCTION ========

// ポップアップを閉じるイベント
const closePopup = () => {
  dialog.hide(DIALOG_RETURN_VAL.CANCEL);
};

// 編集者を選択するイベント
const selectCurrent = (value) => {
  selectedItem.value = value;
};

// 'OK'ボタン押下イベント
const acceptButtonHandler = () => {
  dialog.hide({ displayValue: display.value, selectedItems: selectedItem.value });
};

// 編集者の1行をダブルクリックするイベント
const doubleClickTableRowHandler = (value) => {
  selectCurrent(value);
  acceptButtonHandler();
};

const instanceListFilterByDisplay = computed(() => {
  if (display.value === EDITOR_SELECTION_DISPLAY.DEFAULT) {
    return instance.value.filter((x) => x.responseArea === currentResponseArea);
  }
  return instance.value;
});

// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(async () => {
  const { plantName } = appStore.plant;
  const instanceList = await reviewMemberService.getReviewMemberEditorByPlantName(plantName);
  instance.value = instanceList;
});

</script>

<style scoped>
.checker-table,
th,
td {
  @apply tw-border
}
td {
  width: calc(100% / 3);
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
