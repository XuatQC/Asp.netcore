<template>
  <div v-if="props.plain">{{ plain }}</div>
  <div v-else-if="props.rich" ref="editor"></div>
  <div v-else
    ref="editor" class="rich-text ql-snow"
    :class="[props.class, {'resize' : props.resize}]"
    :style="{ height: props.height }">
  </div>
</template>

<script setup>
import {
  ref, onMounted, nextTick, watch, computed,
} from 'vue';
import Quill from 'quill';
import 'quill/dist/quill.core.css';
import 'quill/dist/quill.bubble.css';
import 'quill/dist/quill.snow.css';

// 1) ======= INITIALIZATION ========
const props = defineProps({
  modelValue: {
    type: String,
    default: '',
  },
  disabled: {
    type: Boolean,
    default: false,
  },
  class: {
    type: String,
    default: '',
  },
  height: {
    type: String,
    default: '100%',
  },
  bounds: {
    type: String,
  },
  plain: {
    type: Boolean,
  },
  rich: {
    type: Boolean,
  },
  resize: {
    type: Boolean,
    default: false,
  },
});

const emits = defineEmits(['update:modelValue', 'dblclick']);

// 2) ======= VARIABLE REF ========
const editor = ref(null);
const quill = ref(null);

// htmlなしの表示テキスト
const plain = computed(() => {
  const element = document.createElement('div');
  element.innerHTML = props.modelValue;
  return element.textContent || element.innerText;
});

// 3) ======= METHOD/FUNCTION ========
// rgb色が含まれるhtmlをhex色に変える
const rgbToHex = (value) => {
  const getNumberString = ($num, radix) => `0${Number($num).toString(radix)}`;
  return value.replace(
    /\brgb\s*\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*\)/g,
    ($0, $1, $2, $3) => `#${getNumberString($1, 16).slice(-2)}${getNumberString($2, 16).slice(-2)}${getNumberString($3, 16).slice(-2)}`,
  );
};

// richTextとplainTextのparentComponentを返す
const update = () => {
  const richText = rgbToHex(quill.value.root.innerHTML).replace('<div><br></div>', '');
  emits('update:modelValue', richText.length > 0 ? richText : null);
};

const createRichText = () => {
  // blockタグをpタグからdivタグに変更する
  const block = Quill.import('blots/block');
  block.tagName = 'DIV';
  Quill.register(block, true);
  // フォントサイズ登録
  const size = Quill.import('attributors/style/size');
  size.whitelist = ['8px', '10px', '12px', '14px', false, '18px', '24px', '26px'];
  Quill.register(size, true);
  // 文字フォント登録
  const font = Quill.import('formats/font');
  font.whitelist = ['notoSansJP', 'timesNewRoman', 'arial', 'roboto'];
  Quill.register(font, true);
  // ツールバーの設定
  const toolbarOptions = [
    [{ font: font.whitelist }],
    [{ size: size.whitelist }],
    [{ direction: '' }, { direction: 'rtl' }],
    [{ indent: '+1' }, { indent: '-1' }],
    ['bold', 'italic', 'underline'],
    [{ color: [] }, { background: [] }],
    [{ align: '' }, { align: 'center' }, { align: 'right' }],
    [{ list: 'ordered' }, { list: 'bullet' }],
  ];
  // richtext作成
  quill.value = new Quill(editor.value, {
    modules: {
      toolbar: toolbarOptions,
    },
    theme: 'bubble',
    bounds: props.bounds ?? 'body',
  });
  nextTick(() => {
    // コンポーネントの内容、状態を設定する
    quill.value.root.innerHTML = props.modelValue;
    quill.value.enable(!props.disabled);
    // richtextとの操作イベント登録
    quill.value.root.addEventListener('dblclick', () => {
      update();
      emits('dblclick');
    });
    quill.value.root.addEventListener('blur', (e) => {
      if (e.relatedTarget?.closest('.ql-tooltip')) return;
      update();
    });
  });
};

// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(() => {
  if (props.rich) editor.value.innerHTML = props.modelValue;
  else if (!props.plain) createRichText();
});

// 外側から変更された場合はrichtextの内容を更新する
watch(
  () => props.modelValue,
  (newContent) => {
    if (props.rich) editor.value.innerHTML = newContent;
    else if (!props.plain && quill.value) quill.value.root.innerHTML = newContent;
  },
  { deep: true },
);

// richtextの状態を更新する
watch(
  () => props.disabled,
  (newValue) => {
    if (quill.value) quill.value.enable(!newValue);
  },
);
</script>
