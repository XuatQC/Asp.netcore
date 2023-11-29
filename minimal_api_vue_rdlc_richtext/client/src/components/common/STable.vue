<template>
  <table class="s-table" aria-describedby="sTable">
    <thead>
      <tr>
        <th v-for="(col, cIndex) in props.columns" :key="col" :style="{ width: col.width }"
          :class="[col.headerClass, { 'selected': colIndex === cIndex }]">
          {{ col.label }}
        </th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(row, rIndex) in props.rows" :key="row" ref="rowsRef" @focusin="focusRow(row, rIndex)">
        <td v-for="(col, cIndex) in props.columns" :key="col" ref="colsRef" @focusin="focusColumn(cIndex)"
          @keydown="keyDown" @click="click(row, col)" tabindex="-1"
          :class="[{ selected: colIndex === cIndex && rowIndex === rIndex }, col.colClass]">
          <slot :name="col.key" :rowIndex="rIndex" :colIndex="cIndex" :context="row"></slot>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script setup>
import { ref } from 'vue';

// 1) ======= INITIALIZATION ========
const props = defineProps({
  columns: { type: Array, default: () => [] },
  rows: { type: Array, default: () => [] },
});

// 2) ======= VARIABLE REF ========
// 選択中の行
const selectedRow = ref(null);
// 選択中の行のindex位置
const rowIndex = ref(null);
// 選択中の列のindex位置
const colIndex = ref(null);
const rowsRef = ref([]);
const colsRef = ref([]);

// 3) ======= METHOD/FUNCTION ========
// フォーカスが当たっている行の値を変更するイベント
const focusRow = (row, rIndex) => {
  selectedRow.value = row;
  rowIndex.value = rIndex;
};

// フォーカスが当たっている列の値を変更するイベント
const focusColumn = (cIndex) => {
  colIndex.value = cIndex;
};

// １つ列を選択するイベント
const selectColumn = () => {
  selectedRow.value = props.rows[rowIndex.value];
  colsRef.value[rowIndex.value * props.columns.length + colIndex.value].focus();
};

// 選択中の列を左側に移動する
const moveLeft = () => {
  if (
    colIndex.value === null
    || rowIndex.value === null
    || (colIndex.value === 0 && rowIndex.value === 0)
  ) { return; }
  if (colIndex.value === 0) {
    colIndex.value = props.columns.length - 1;
    if (rowIndex.value !== 0) rowIndex.value--;
  } else colIndex.value--;
  selectColumn();
};

// 選択中の列を右側に移動する
const moveRight = () => {
  if (colIndex.value === props.columns.length - 1 && rowIndex.value === props.rows.length - 1) return;
  if (colIndex.value === props.columns.length - 1) {
    colIndex.value = 0;
    if (rowIndex.value !== props.rows.length - 1) rowIndex.value++;
  } else colIndex.value++;
  selectColumn();
};

// 選択中の列を上側に移動する
const moveUp = () => {
  if (rowIndex.value === 0) return;
  rowIndex.value--;
  selectColumn();
};

// 選択中の列を下側に移動する
const moveDown = () => {
  if (rowIndex.value === props.rows.length - 1) return;
  rowIndex.value++;
  selectColumn();
};

// 十字キーにイベントを付与する
const keyDown = (event) => {
  switch (event.key) {
    case 'ArrowLeft':
      moveLeft();
      event.preventDefault();
      break;
    case 'ArrowRight':
      moveRight();
      event.preventDefault();
      break;
    case 'ArrowUp':
      moveUp();
      event.preventDefault();
      break;
    case 'ArrowDown':
      moveDown();
      event.preventDefault();
      break;
    default:
      break;
  }
};

// クリック行動にイベントを付与する
const click = (row, col) => {
  if (col.onClick) col.onClick(row);
};
</script>
