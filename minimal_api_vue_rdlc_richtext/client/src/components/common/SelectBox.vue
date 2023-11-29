<template>
  <q-select v-model="selectedValue" :options="optionFiltered" :display-value="displayText" color="gray"
    @update:model-value="selectRow" :multiple="props.multiple"
    :class="[props.class, 'select-box', { 'filter': props.filter }]" dense options-dense outlined @filter="filter"
    :use-input="props.filter" :option-disable="() => props.disabled">
    <template v-slot:option="{ itemProps, opt, toggleOption }">
      <q-item v-bind="itemProps">
        <q-item-section side v-if="props.multiple">
          <q-checkbox :model-value="selectedValue?.length > 0 && selectedValue.includes(opt)"
            @update:model-value="toggleOption(opt)"></q-checkbox>
        </q-item-section>
        <q-item-section>
          <q-item-label class="tw-my-2" :style="{ 'width': props.optionsWidth }">
            <div v-if="props.displayProps?.length > 1" class="tw-flex tw-items-center">
              <div class="tw-w-[30%]">{{ opt[displayProps[0]] }}</div>
              <div class="tw-w-[70%]">{{ opt[displayProps[1]] }}</div>
            </div>
            <div v-else-if="props.displayProps?.length > 0">
              {{ opt[props.displayProps[0]] }}
            </div>
            <div v-else>{{ opt }}</div>
          </q-item-label>
        </q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script setup>
import {
  ref, watchEffect, computed,
} from 'vue';

// 1) ======= INITIALIZATION ========
const props = defineProps({
  // セレクトボックスにバインディングされる値
  modelValue: [String, Number, Object],
  // 選択肢の値
  rows: Array,
  // 選択肢に表示する値
  displayProps: Array,
  // 選択後に表示する値
  displayText: String,
  // 選択肢の幅
  optionsWidth: String,
  // コンポーネントq-selectに入力するCss
  class: String,
  // 編集不可状態
  disabled: Boolean,
  // 先頭の選択肢に空行を追加する
  blankFirst: Boolean,
  // 検索のために入力許可する
  filter: Boolean,
  // セレクトボックスにバインディングされる属性
  bindingProp: String,
  // 複数選択可
  multiple: Boolean,
});

const emits = defineEmits(['update:modelValue', 'change']);

// 2) ======= VARIABLE REF ========
// 選択されている選択肢
const selectedValue = ref(null);

// props変換後の選択肢
const options = computed(() => {
  if (!props.rows) return [];
  if (props.blankFirst) {
    const blankOption = {};
    // 表示内容を元に空選択肢の属性を作成する
    for (const displayProp of props.displayProps) {
      blankOption[displayProp] = null;
    }
    const value = [blankOption];
    return value.concat(props.rows);
  }
  return props.rows;
});

// 検索後に表示する選択肢
const optionFiltered = ref(options.value);

// 選択後に表示される内容
const displayText = computed(() => {
  if (!selectedValue.value) return '';
  let result = props.displayText ? selectedValue.value[props.displayText] : selectedValue.value;
  // 複数選択可の場合、値は","区切りで表示する
  if (props.multiple) {
    result = selectedValue.value.map((x) => (props.displayText ? x[props.displayText] : x)).join(', ');
  }
  return result;
});

// 3) ======= METHOD/FUNCTION ========
// 1行の値を選択するイベント
const selectRow = (value) => {
  if (props.filter) document.activeElement.blur();
  let returnVal;
  // 複数選択可の場合
  if (props.multiple) {
    // 複数選択可の場合、返却値は","で区切る
    returnVal = props.bindingProp ? value.map((x) => x[props.bindingProp]).join(',') : value.join(',');
  } else returnVal = props.bindingProp ? value[props.bindingProp] : value;
  emits('update:modelValue', returnVal);
  emits('change', returnVal);
};

// 検索値を絞る
const filter = (val, update) => {
  const valLowerCase = val?.toLowerCase();
  update(() => {
    // 検索内容が入力されない場合、全て表示する
    if (!val || val === '') {
      optionFiltered.value = options.value;
      return;
    }
    // 複数属性を表示した場合
    if (props.displayProps?.length > 0) {
      optionFiltered.value = options.value.filter((x) => {
        // 表示中の属性をループし検索処理を行う
        for (let index = 0; index < props.displayProps?.length; index++) {
          // 検索値を小文字に変換する
          // 選択肢の値を小文字に変換する
          const propValLowerCase = x[props.displayProps[index]]?.toLowerCase();
          // 検索内容と一致した選択肢を検索する
          if (propValLowerCase?.includes(valLowerCase)) return true;
        }
        return false;
      });
      // 1つ属性が表示される場合
    } else {
      optionFiltered.value = options.value.filter((x) => {
        const propValLowerCase = x?.toLowerCase();
        return propValLowerCase?.includes(valLowerCase);
      });
    }
  });
};

// 4) ======= VUEJS LIFECYCLE ========
// セレクトボックスに値をバインディングする処理
watchEffect(
  () => {
    // 複数選択可の場合
    if (props.multiple && props.modelValue?.length > 0) {
      // ","区切られている文字列から選択値一覧を取得する
      const values = props.modelValue.split(',');
      selectedValue.value = [];
      // 選択肢一覧内の項目を検索し、配列を順番にプッシュする
      for (const element of values) {
        const option = options.value.find((x) => (props.bindingProp ? x[props.bindingProp] : x) === element);
        if (option) selectedValue.value.push(option);
      }
    } else if (props.bindingProp) {
      // 単一選択可の場合
      selectedValue.value = options.value.find((x) => (props.bindingProp ? x[props.bindingProp] : x) === props.modelValue);
    } else selectedValue.value = props.modelValue;
  },
);
</script>
