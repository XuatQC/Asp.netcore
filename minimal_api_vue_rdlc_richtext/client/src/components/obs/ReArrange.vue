<template>
  <q-card-section class="tw-flex tw-justify-center">
    <table aria-describedby="rearrange" class="tw-w-[200px] s-table rearrange">
      <thead>
        <tr>
          <th class="tw-border tw-border-black">変更前</th>
          <th class="tw-border tw-border-black">変更後</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td class="tw-border tw-border-black tw-pb-2 tw-text-center">
            <input type="number" min="1" :max = props.maxIndex class="!tw-pl-[10px] !tw-w-[45px] tw-text-center"
              @keypress="validate($event)" v-model.number="dispOrder.from" @blur="checkLogic()"/>
          </td>
          <td class="tw-border tw-border-black tw-pb-2 tw-text-center">
            <input type="number" min="1" :max = props.maxIndex class="!tw-pl-[10px] !tw-w-[45px] tw-text-center"
              @keypress="validate($event)" v-model.number="dispOrder.to" @blur="checkLogic()" />
          </td>
        </tr>
      </tbody>

    </table>
  </q-card-section>
  <q-card-section class="tw-text-center tw-space-x-4">
    <q-btn color="primary" class="tw-w-[105px]" @click="changed">OK</q-btn>
    <q-btn color="primary" @click="hide">{{ $t("cancel") }}</q-btn>
  </q-card-section>
</template>

<script setup>
import { onMounted, reactive, ref } from 'vue';
import dialog from 'utilities/dialog';
import { MSG, MODAL } from 'helpers/constant';

// 1) ======= INITIALIZATION ========
const props = defineProps({
  selectedIndex: Number,
  maxIndex: Number,
});

// 2) ======= VARIABLE REF ========
const dispOrder = reactive({
  from: props.selectedIndex + 1,
  to: props.selectedIndex + 1,
});
const oldFrom = ref();
const oldTo = ref();

// 3) ======= METHOD/FUNCTION ========
// 位置変更
const changed = () => dialog.hide(dispOrder);

// 位置変更のキャンセル
const hide = () => dialog.hide();

// 入力データの確認
const validate = (e) => {
  const charCode = (e.which) ? e.which : e.keyCode;
  if (e.target.value.length >= 2) {
    e.preventDefault();
  }
  if (charCode > 31 && (charCode < 48 || charCode > 57)) {
    e.preventDefault();
  }
  if (e.target.value > props.maxIndex) {
    e.preventDefault();
  }
};

// 入力値がソート番号より大きいことを確認します。
const checkLogic = () => {
  if (dispOrder.from > props.maxIndex || dispOrder.from.length === 0) {
    dialog.showMessage(MODAL.TITLE_INFO, `変更前 ${MSG.ERR_MSG_INPUT_VALID}`);
    dispOrder.from = oldFrom.value;
    return;
  }
  if (dispOrder.to > props.maxIndex || dispOrder.to.length === 0) {
    dialog.showMessage(MODAL.TITLE_INFO, `変更後 ${MSG.ERR_MSG_INPUT_VALID}`);
    dispOrder.to = oldTo.value;
    return;
  }
  oldFrom.value = dispOrder.from;
  oldTo.value = dispOrder.to;
};

// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(() => {
  oldFrom.value = dispOrder.from;
  oldTo.value = dispOrder.to;
});
</script>
