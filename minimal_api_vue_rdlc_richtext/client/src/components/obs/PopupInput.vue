<template>
  <span>{{ props.name }}</span>
  <div class="path-section">
    <span class="tw-whitespace-nowrap tw-pr-1 tw-font-bold"> 項目選択 Item </span>
    <div class="tw-w-4/5">
      <select-box :rows="listOfInput" :display-props="['label']" display-text="label" v-model="selectInput" />
    </div>
    <div class="tw-pl-1">
      <button data-v-122660fd="" @click="navigateItem(NAVIGATE_VERTICAL.UP)" class="mini-btn tw-block">
        <i data-v-122660fd="" class="q-icon notranslate material-icons -tw-rotate-90" aria-hidden="true"
          role="presentation">play_arrow</i>
      </button>
      <button data-v-122660fd="" @click="navigateItem(NAVIGATE_VERTICAL.DOWN)" class="mini-btn tw-block"><i
          data-v-122660fd="" class="q-icon notranslate material-icons tw-rotate-90" aria-hidden="true"
          role="presentation">play_arrow</i></button>
    </div>
  </div>
  <div class="function-section tw-py-1">
    <div class="tw-flex tw-items-center tw-justify-between">
      <div class="function-text tw-whitespace-pre-line">{{ $t('popupInput.description') }}</div>
      <div class="tw-flex">
        <button class="btn tw-mx-1" @click="applyHandler()">適用<br />Apply</button>
        <button class="btn close" @click="closePopup">閉じる<br />Close</button>
      </div>
      <div class="function-text tw-ml-3 tw-mr-1 tw-whitespace-nowrap">
        <b>右欄表示<br/>Display</b>
      </div>
      <div class="tw-flex">
        <button :class="{ 'selected-mode-button': INPUT_DISPLAY_MODE.NORMAL === currentMode }" class="btn tw-mr-1"
          @click="changeMode(INPUT_DISPLAY_MODE.NORMAL)">通常<br />Normal</button>
        <button :class="{ 'selected-mode-button': INPUT_DISPLAY_MODE.CONTEXT === currentMode }" class="btn tw-mr-1"
          @click="changeMode(INPUT_DISPLAY_MODE.CONTEXT)">前後<br />Context</button>
        <button  v-if="!props.isComment" :class="{ 'selected-mode-button': INPUT_DISPLAY_MODE.LANGUAGE === currentMode }" class="btn tw-mr-1"
          @click="changeMode(INPUT_DISPLAY_MODE.LANGUAGE)">訳文<br />Language</button>
        <button v-else :class="{ 'selected-mode-button': INPUT_DISPLAY_MODE.ENTRY === currentMode }" class="btn tw-mr-1"
        @click="changeMode(INPUT_DISPLAY_MODE.ENTRY)">入力項目<br />Entry</button>
        <button :class="{ 'selected-mode-button': INPUT_DISPLAY_MODE.COMMENT === currentMode }" class="btn" :disabled="props.isComment"
          @click="changeMode(INPUT_DISPLAY_MODE.COMMENT)">コメント<br />Comment</button>
      </div>
    </div>
  </div>
  <div class="tw-flex " :class="{'tw-flex-row-reverse': props.isComment}">
    <div class="tw-flex-1" :class="{ 'tw-mr-2': currentMode != INPUT_DISPLAY_MODE.NORMAL && !props.isComment,' tw-ml-2': props.isComment }">
      <div class="tw-font-bold" >{{!props.isComment ? '入力項目 Entry' : 'コメント Comment'}}</div>
      <template v-if="!props.isComment">
        <template  v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.TITLE">
          <rich-text v-if="isEnglish"
          v-model="obsClone.titleTrans" height="412px" class="text-area"
          id="editor" bounds="#editor"></rich-text>
          <rich-text v-else-if="!isEnglish"
          v-model="obsClone.title" height="412px" class="text-area"
          id="editor" bounds="#editor"></rich-text>
        </template>
        <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.SCOPE">
          <rich-text v-if="isEnglish"
          v-model="obsClone.scopeTrans" height="412px" class="text-area"
          id="editor" bounds="#editor"></rich-text>
          <rich-text v-else-if="!isEnglish"
          v-model="obsClone.scope" height="412px" class="text-area"
          id="editor" bounds="#editor"></rich-text>
        </template>
        <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.FACT">
          <rich-text v-if="isEnglish"
        v-model="factClone[selectInput.index].factTrans" height="412px" class="text-area"
        id="editor" bounds="#editor"></rich-text>
        <rich-text v-if="!isEnglish"
        v-model="factClone[selectInput.index].fact" height="412px" class="text-area"
        id="editor" bounds="#editor"></rich-text>
        </template>
        <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION" >
          <rich-text v-if="isEnglish"
          v-model="conclusionClone[selectInput.index].conclusionTrans" height="412px" class="text-area"
          id="editor" bounds="#editor"></rich-text>
          <rich-text v-if="!isEnglish"
          v-model="conclusionClone[selectInput.index].conclusion" height="412px" class="text-area"
          id="editor" bounds="#editor"></rich-text>
        </template>
      </template>
      <template v-else>
        <rich-text v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.SCOPE"
        v-model="obsClone.scopeComment" height="412px" class="text-area tw-ml-2"
        id="editor" bounds="#editor"></rich-text>
        <rich-text v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.FACT"
        v-model="factClone[selectInput.index].comment" height="412px" class="text-area tw-ml-2"
        id="editor" bounds="#editor"></rich-text>
        <rich-text v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION"
        v-model="conclusionClone[selectInput.index].comment" height="412px" class="text-area tw-ml-2"
        id="editor" bounds="#editor"></rich-text>
        <rich-text v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.GENERAL_COMMENT"
        v-model="obsClone.generalComment" height="412px" class="text-area tw-ml-2"
        id="editor" bounds="#editor"></rich-text>
      </template>
    </div>
    <div class="tw-flex-1" v-show="currentMode === INPUT_DISPLAY_MODE.CONTEXT">
      <div>
        <div class="tw-font-bold tw-whitespace-pre-line">
          {{listOfInput[listOfInput.indexOf(selectInput) -1] ? "▲" + listOfInput[listOfInput.indexOf(selectInput) -1]?.label : "&nbsp;"}}
        </div>
        <template v-if="listOfInput[listOfInput.indexOf(selectInput) -1]?.type === TYPE_INPUT_AS_FIRST_LETTER.TITLE">
          <rich-text v-if="!props.isComment" disabled height="188px" v-model="obsClone.title" class="text-area !tw-cursor-default !tw-opacity-100"></rich-text>
          <rich-text v-else disabled height="188px" v-model="obsClone.title"
          class="text-area !tw-cursor-default !tw-opacity-100"></rich-text>
        </template>
        <template v-else-if="listOfInput[listOfInput.indexOf(selectInput) -1]?.type === TYPE_INPUT_AS_FIRST_LETTER.SCOPE">
          <rich-text v-if="!props.isComment" height="188px" class="text-area !tw-cursor-default !tw-opacity-100" v-model="obsClone.scope"
            disabled></rich-text>
          <rich-text v-else height="188px" class="text-area !tw-cursor-default !tw-opacity-100" v-model="obsClone.scopeComment"
          disabled></rich-text>
        </template>
        <template  v-else-if="listOfInput[listOfInput.indexOf(selectInput) -1]?.type === TYPE_INPUT_AS_FIRST_LETTER.FACT">
          <rich-text height="188px" v-if="!props.isComment"
          class="text-area !tw-cursor-default !tw-opacity-100" v-model="factClone[listOfInput[listOfInput.indexOf(selectInput) -1].index].fact"
          disabled></rich-text>
          <rich-text height="188px" v-else
          class="text-area !tw-cursor-default !tw-opacity-100" v-model="factClone[listOfInput[listOfInput.indexOf(selectInput) -1].index].comment"
          disabled></rich-text>
        </template>
        <template v-else-if="listOfInput[listOfInput.indexOf(selectInput) -1]?.type === TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION">
          <rich-text  height="188px" v-if="!props.isComment"
          class="text-area !tw-cursor-default !tw-opacity-100" v-model="conclusionClone[listOfInput[listOfInput.indexOf(selectInput) -1].index].conclusion"
          disabled></rich-text>
          <rich-text  height="188px" v-else
          class="text-area !tw-cursor-default !tw-opacity-100" v-model="conclusionClone[listOfInput[listOfInput.indexOf(selectInput) -1].index].comment"
          disabled></rich-text>
        </template>
        <div v-else class="tw-h-[188px] tw-p-[4px] tw-border tw-border-[#cccccc] tw-rounded-[6px] tw-bg-[#efefef4d]"></div>
      </div>
      <div>
        <div class="tw-font-bold tw-whitespace-pre-line">
          {{listOfInput[listOfInput.indexOf(selectInput) + 1] ? "▼" + listOfInput[listOfInput.indexOf(selectInput) + 1]?.label : "&nbsp;"}}
        </div>
        <template v-if="listOfInput[listOfInput.indexOf(selectInput) + 1]?.type === TYPE_INPUT_AS_FIRST_LETTER.TITLE">
          <rich-text v-if="!props.isComment" disabled height="200px" v-model="obsClone.title" class="text-area !tw-cursor200pxult !tw-opacity-100"></rich-text>
          <rich-text v-else disabled height="200px" v-model="obsClone.title"
          class="text-area !tw-cursor-default !tw-opacity-100"></rich-text>
        </template>
        <template v-else-if="listOfInput[listOfInput.indexOf(selectInput) + 1]?.type === TYPE_INPUT_AS_FIRST_LETTER.SCOPE">
          <rich-text v-if="!props.isComment" height="200px" class="text-area !tw-cursor-default !tw-opacity-100" v-model="obsClone.scope"
            disabled></rich-text>
          <rich-text v-else height="200px" class="text-area !tw-cursor-default !tw-opacity-100" v-model="obsClone.scopeComment"
          disabled></rich-text>
        </template>
        <template  v-else-if="listOfInput[listOfInput.indexOf(selectInput) + 1]?.type === TYPE_INPUT_AS_FIRST_LETTER.FACT">
          <rich-text height="200px" v-if="!props.isComment"
          class="text-area !tw-cursor-default !tw-opacity-100" v-model="factClone[listOfInput[listOfInput.indexOf(selectInput) + 1].index].fact"
          disabled></rich-text>
          <rich-text height="200px" v-else
          class="text-area !tw-cursor-default !tw-opacity-100" v-model="factClone[listOfInput[listOfInput.indexOf(selectInput) + 1].index].comment"
          disabled></rich-text>
        </template>
        <template v-else-if="listOfInput[listOfInput.indexOf(selectInput) + 1]?.type === TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION">
          <rich-text  height="200px" v-if="!props.isComment"
          class="text-area !tw-cursor-default !tw-opacity-100" v-model="conclusionClone[listOfInput[listOfInput.indexOf(selectInput) + 1].index].conclusion"
          disabled></rich-text>
          <rich-text  height="200px" v-else
          class="text-area !tw-cursor-default !tw-opacity-100" v-model="conclusionClone[listOfInput[listOfInput.indexOf(selectInput) + 1].index].comment"
          disabled></rich-text>
        </template>
        <template v-else-if="listOfInput[listOfInput.indexOf(selectInput) + 1]?.type === TYPE_INPUT_AS_FIRST_LETTER.GENERAL_COMMENT">
          <rich-text  height="200px"
          class="text-area !tw-cursor-default !tw-opacity-100" v-model="obsClone.generalComment"
          disabled></rich-text>
        </template>
        <div v-else class="tw-h-[200px] tw-p-[4px] tw-border tw-border-[#cccccc] tw-rounded-[6px] tw-bg-[#efefef4d]"></div>
      </div>
    </div>
    <div class="tw-flex-1" v-if="currentMode === INPUT_DISPLAY_MODE.COMMENT">
      <div>
        <div class="tw-font-bold">コメント Comment</div>
        <rich-text v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.TITLE"
        :disabled="true" height="412px" class="text-area"
        id="editor" bounds="#editor"></rich-text>
        <rich-text v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.SCOPE" :disabled="true"
        v-model="obsClone.scopeComment" height="412px" class="text-area"
        id="editor" bounds="#editor"></rich-text>
        <rich-text v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.FACT" :disabled="true"
        v-model="factClone[selectInput.index].comment" height="412px" class="text-area"
        id="editor" bounds="#editor"></rich-text>
        <rich-text v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION" :disabled="true"
        v-model="conclusionClone[selectInput.index].comment" height="412px" class="text-area"
        id="editor" bounds="#editor"></rich-text>
      </div>
    </div>
    <div class="tw-flex-1" v-if="currentMode === INPUT_DISPLAY_MODE.LANGUAGE">
      <div >
        <div class="tw-font-bold">訳文 Language</div>
        <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.TITLE"
        >
        <rich-text v-if="isEnglish"
        v-model="obsClone.title" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
        <rich-text v-if="!isEnglish"
        v-model="obsClone.titleTrans" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
      </template>
      <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.SCOPE">
        <rich-text v-if="isEnglish"
        v-model="obsClone.scope" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
        <rich-text v-if="!isEnglish"
        v-model="obsClone.scopeTrans" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
      </template>
      <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.FACT">
        <rich-text v-if="isEnglish"
        v-model="factClone[selectInput.index].fact" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
        <rich-text v-if="!isEnglish"
        v-model="factClone[selectInput.index].factTrans" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
      </template >
      <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION">
        <rich-text v-if="isEnglish"
        v-model="conclusionClone[selectInput.index].conclusion" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
        <rich-text v-if="!isEnglish"
        v-model="conclusionClone[selectInput.index].conclusionTrans" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
      </template>
      <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.GENERAL_COMMENT">
        <div class="tw-h-[412px] tw-p-[4px] tw-border tw-border-[#cccccc] tw-rounded-[6px] tw-bg-[#efefef4d]"></div>
      </template>
      </div>
    </div>
    <div class="tw-flex-1" v-if="currentMode === INPUT_DISPLAY_MODE.ENTRY">
      <div >
        <div class="tw-font-bold">入力項目 Entry</div>
        <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.TITLE"
        >
        <rich-text v-if="!isEnglish"
        v-model="obsClone.title" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
        <rich-text v-if="isEnglish"
        v-model="obsClone.titleTrans" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
      </template>
      <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.SCOPE">
        <rich-text v-if="!isEnglish"
        v-model="obsClone.scope" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
        <rich-text v-if="isEnglish"
        v-model="obsClone.scopeTrans" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
      </template>
      <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.FACT">
        <rich-text v-if="!isEnglish"
        v-model="factClone[selectInput.index].fact" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
        <rich-text v-if="isEnglish"
        v-model="factClone[selectInput.index].factTrans" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
      </template >
      <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION">
        <rich-text v-if="!isEnglish"
        v-model="conclusionClone[selectInput.index].conclusion" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
        <rich-text v-if="isEnglish"
        v-model="conclusionClone[selectInput.index].conclusionTrans" height="412px" class="text-area"
        id="editor" bounds="#editor" :disabled="true"></rich-text>
      </template>
      <template v-if="selectInput.type === TYPE_INPUT_AS_FIRST_LETTER.GENERAL_COMMENT">
        <div class="tw-h-[412px] tw-p-[4px] tw-border tw-border-[#cccccc] tw-rounded-[6px] tw-bg-[#efefef4d]"></div>
      </template>
      </div>
    </div>
  </div>
</template>

<script setup>
import {
  ref, onMounted, computed,
} from 'vue';
import dialog from 'utilities/dialog';
import {
  NAVIGATE_VERTICAL, INPUT_DISPLAY_MODE, TYPE_INPUT_AS_FIRST_LETTER, DIALOG_RETURN_VAL,
} from 'helpers/enum';
import { cloneDeep, isEqual } from 'lodash';
import RichText from 'components/common/RichText.vue';
import {
  CONFIRM_DIALOG, LANGUAGE, MODAL, MSG,
} from 'helpers/constant';
import SelectBox from 'components/common/SelectBox.vue';
import { useAppStore } from 'stores/app-store';
// 1) ======= INITIALIZATION ========
const appStore = useAppStore();
const props = defineProps({
  name: String,
  obs: {
    type: Object,
    required: true,
  },
  facts: {
    type: Array,
    default: () => [],
  },
  conclusions: {
    type: Array,
    default: () => [],
  },
  selected: {
    type: Object,
    default: () => ({
      type: TYPE_INPUT_AS_FIRST_LETTER.TITLE,
      index: 0,
    }),
  },
  mode: {
    type: String,
    default: INPUT_DISPLAY_MODE.NORMAL,
  },
  isComment: {
    type: Boolean,
    default: false,
  },
  applyAction: {
    type: Function,
  },
});

// 2) ======= VARIABLE REF ==========
const currentMode = ref(INPUT_DISPLAY_MODE.NORMAL);
const selectInput = ref({});
const listOfInput = ref([]);
const obsClone = ref({});
const factClone = ref([]);
const conclusionClone = ref([]);
const unchangeObsClone = ref({});
const unchangeFactClone = ref({});
const unchangeConclusionClone = ref({});
// 3) ======= METHOD/FUNCTION ========

const isEnglish = computed(() => appStore.language.code === LANGUAGE.ENGLISH_FIRST_LETTER);
// obs,fact,conclusionの値をコピーする
const getCloneOfInputs = () => {
  obsClone.value = cloneDeep(props.obs);
  factClone.value = cloneDeep(props.facts);
  conclusionClone.value = cloneDeep(props.conclusions);
};

// 変化チェックのため、obs,fact,conclusionの値をコピーする
const getUnchangeCloneOfInput = () => {
  unchangeObsClone.value = cloneDeep(obsClone.value);
  unchangeFactClone.value = cloneDeep(factClone.value);
  unchangeConclusionClone.value = cloneDeep(conclusionClone.value);
};
// '適用'ボタン押下イベント
const applyHandler = () => {
  props.applyAction(obsClone.value, factClone.value, conclusionClone.value);
  getUnchangeCloneOfInput();
};

const checkChange = () => isEqual(obsClone.value, unchangeObsClone.value);

// ポップアップを閉じるイベント
const closePopup = async () => {
  if (!checkChange()) {
    const confirmValue = await dialog.showConfirm(MODAL.TITLE_INFO, MSG.DATA_CHANGED);
    if (confirmValue !== CONFIRM_DIALOG.NO) {
      dialog.hide(DIALOG_RETURN_VAL.CANCEL);
    }
  } else {
    dialog.hide();
  }
};

// directionを元に現在itemInputを移動する
const navigateItem = (direction) => {
  const indexOfSelectedInput = listOfInput.value.indexOf(selectInput.value);
  const theNextPosition = listOfInput.value[indexOfSelectedInput + (direction * -1)];
  if (theNextPosition) selectInput.value = theNextPosition;
};

// 4) ======= VUEJS LIFECYCLE ========

const getListOfInput = () => {
  if (!props.isComment) {
    listOfInput.value.push({
      type: TYPE_INPUT_AS_FIRST_LETTER.TITLE, index: 0, label: 'タイトル／Title',
    });
  }
  listOfInput.value.push(
    {
      type: TYPE_INPUT_AS_FIRST_LETTER.SCOPE, index: 0, label: '範囲／Scope',
    },
  );
  for (let index = 0; index < props.facts.length; index++) {
    listOfInput.value.push({
      type: TYPE_INPUT_AS_FIRST_LETTER.FACT, index, label: `事実／Facts(${(index + 1).toString().padStart(2, '0')})`,
    });
  }
  for (let index = 0; index < props.conclusions.length; index++) {
    listOfInput.value.push({
      type: TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION, index, label: `結論／Conclusions(${(index + 1).toString().padStart(2, '0')})`,
    });
  }
  if (props.isComment) {
    listOfInput.value.push({
      type: TYPE_INPUT_AS_FIRST_LETTER.GENERAL_COMMENT, index: 0, label: '全般／General Comments',
    });
  }
  [selectInput.value] = listOfInput.value;
};

// 表示モード変更
const changeMode = (modeName) => {
  currentMode.value = modeName;
};

// コンポーネント初期時に、渡されているprops「selected.type」を元にinputTypeを選択する
const setCurrentSelectedInputItem = () => {
  if (!props.selected) return;
  selectInput.value = listOfInput.value.find((x) => x.index === props.selected.index && x.type === props.selected.type);
  changeMode(props.mode);
};

// コンポーネント初期後
onMounted(() => {
  getListOfInput();
  getCloneOfInputs();
  getUnchangeCloneOfInput();
  setCurrentSelectedInputItem();
});

</script>

<style scoped>
.path-section {
  @apply tw-flex tw-items-center;
}

.function-text {
  max-width: 50%;
}

.text-area {
  @apply tw-overflow-hidden;
}

.selected-mode-button {
  @apply tw-bg-[#04b2d9];
  transition: color .15s ease-in-out, background-color .15s ease-in-out, border-color .15s ease-in-out, box-shadow .15s ease-in-out;
}</style>
