<template>
  <teleport to="#header">
    <div class="sub-header tw-flex">
      <div class="tw-w-1/2 tw-mt-1 tw-mb-3 tw-mr-4">
        <fieldset class="tw-w-full">
          <legend class="tw-px-3">{{ $t('obsInput.other') }}</legend>
          <div>
            <div class="tw-flex tw-justify-between">
              <button class="btn" @click="clickSaveHandler" :class="{ disabled: readonly }">{{ $t('obsInput.save') }}</button>
              <button class="btn" @click="clickRevUpSaveHandler" :class="{ disabled: readonly }">{{ $t('obsInput.saveNew') }}</button>
              <button class="btn delete" @click="clickDeleteHandler" :class="{ disabled: readonly }">{{ $t('obsInput.delete') }}</button>
              <button class="btn" @click="clickPrintHandler">{{ $t('obsInput.print') }}</button>
              <button class="btn" @click="clickOutputWordHandler">{{ $t('obsInput.word') }}</button>
              <button class="btn" @click="clickChangeLanguageHandler">{{ $t('obsInput.language') }}</button>
              <button class="btn" :class="{ disabled: readonly }">{{ $t('obsInput.spell') }}</button>
              <button class="btn">{{ $t('obsInput.manual') }}</button>
            </div>
            <div class="tw-mt-1 tw-flex tw-justify-between tw-items-center">
              <div class="tw-space-x-4">
                <label for="obs-input-num" class="tw-font-semibold tw-ml-0.5">{{ $t('obsInput.num') }}:</label>
                <span>{{ obs.num }}</span>
              </div>
              <div class="tw-space-x-4 tw-flex tw-items-center tw-mr-0.5">
                <label for="obs-input-rev">Rev.</label>
                <select-box :rows="revisions" display-text="revisions" :disabled="readonly && previewMode" :display-props="['revisions']"
                  v-model="rev" binding-prop="num" @change="changeUpdateRevHandler">
                </select-box>
              </div>
            </div>
          </div>
        </fieldset>
      </div>
      <div class="tw-w-1/2 tw-space-x-4 tw-mt-1 tw-mb-3">
        <fieldset>
          <legend class="tw-px-3">{{ $t('obsInput.state') }}</legend>
          <div class="tw-table tw-m-auto tw-space-y-3">
            <button class="btn" :class="{ disabled: readonly }" @click="clickEditorHandler">{{ $t('obsInput.editor') }}</button>
            <div class="tw-text-left">
              <label for="">{{ $t('obsInput.editor') }}: {{ obs.editor }}</label>
            </div>
          </div>
        </fieldset>
        <fieldset>
          <legend class="tw-px-3">{{ $t('obsInput.report') }}、{{ $t('obsInput.recognition') }}</legend>
          <div class="tw-table tw-space-y-3">
            <div class="tw-space-x-2">
              <button class="btn" :class="{ disabled: readonly }">{{ $t('obsInput.report') }}</button>
              <button class="btn" :class="{ disabled: readonly }">{{ $t('obsInput.recognition') }}</button>
              <button class="btn" :class="{ disabled: readonly }">{{ $t('obsInput.reject') }}</button>
            </div>
            <div class="tw-text-left">
              <label for="">{{ $t('obsInput.nextAs') }}: {{ nextApprover }}</label>
            </div>
          </div>
        </fieldset>
        <fieldset>
          <legend class="tw-px-3">{{ $t('obsInput.transStatus') }}</legend>
          <div class="tw-table tw-space-y-3">
            <div class="tw-space-x-2">
              <button class="btn tw-w-[140px]" :class="{ disabled: readonly }" @click="clickTransReqHandler">{{ $t('obsInput.transReq') }}</button>
              <button class="btn tw-w-[140px]" @click="clickTransHistHandler">{{ $t('obsInput.transHis') }}</button>
            </div>
            <div class="tw-flex">
              <div
                class="tw-border tw-border-r-0 tw-border-solid tw-border-gray-300 tw-w-1/2 tw-h-[25px] tw-text-center tw-pl-1">
                {{ obs.transDeadline }}
              </div>
              <div class="tw-border tw-border-solid tw-border-gray-300 tw-w-1/2 tw-text-center tw-h-[25px] tw-pl-1">
                {{ transState }}
              </div>
            </div>
          </div>
        </fieldset>
      </div>
    </div>
  </teleport>
  <div class="q-pa-md tw-space-y-3">
    <div class="tw-w-full tw-flex">
      <div class="tw-w-7/12">
        <div class="tw-text-center tw-font-semibold tw-text-xl tw-h-[40px]">OBSERVATION SHEET 観察シート</div>
        <div class="tw-flex tw-justify-between tw-h-[30px] tw-mb-[10px]">
          <div>
            <label class="tw-font-semibold tw-inline-block tw-w-[150px] tw-text-lg">プラント名 Plant:</label>
            <span>{{ obs.plantName }}</span>
          </div>
          <div>
            <label class="tw-font-semibold tw-inline-block tw-w-[170px] tw-text-lg">{{ $t('obsInput.area') }}:</label>
            <span>{{ obs.fields }}</span>
          </div>
        </div>
      </div>
      <div class="tw-w-1/12"></div>
      <div class="tw-w-4/12 tw-px-3">
        <div class="tw-flex tw-space-x-5">
          <div class="tw-space-x-3 tw-w-[360px] tw-flex tw-items-center">
            <label for="obs-input-obs-target">{{ $t('obsInput.activity') }}</label>
            <select-box :rows="obsTargets" v-model="obs.observationTarget" binding-prop="target" :disabled="readonly"
              :display-props="['targetName']" display-text="targetName" class="tw-flex-auto tw-inline-block">
            </select-box>
          </div>
          <div class="tw-flex tw-items-center">
            <label>{{ $t('obsInput.owner') }}：</label>
            <span>{{ app.isDisplayEnglish ? user?.romaName : user?.name }}</span>
          </div>
        </div>
        <div class="tw-flex tw-space-x-5">
          <div class="tw-space-x-3 tw-w-[360px]">
            <q-checkbox v-model="obs.packageExclude" :disable="readonly" :label="$t('obsInput.excludePkg')" size="sm" />
          </div>
          <div class="tw-space-x-3">
            <q-checkbox v-model="obs.finalVer" :disable="readonly" :label="$t('obsInput.finalized')" size="sm" />
          </div>
        </div>
      </div>
    </div>
    <div class="tw-w-full tw-flex">
      <div class="tw-w-[150px] tw-font-semibold tw-text-lg">タイトル Title</div>
      <div class="tw-flex-auto">
        <rich-text v-if="app.isDisplayEnglish" v-model="obs.titleTrans" @dblclick="dbClickTitleHandler"
          height="65px" :disabled="readonly">
        </rich-text>
        <rich-text v-else v-model="obs.title" @dblclick="dbClickTitleHandler" height="65px" :disabled="readonly">
        </rich-text>
      </div>
    </div>
    <!-- Begin SCOPE -->
    <div class="tw-space-y-3">
      <div class="tw-flex tw-items-end tw-justify-between">
        <div class="tw-font-semibold tw-text-lg">1. 範囲 SCOPE</div>
        <div class="tw-flex tw-space-x-2">
          <div><button class="btn" @click="clickToggleCommentHandler">{{ showComment ? $t('obsInput.hideComment') : $t('obsInput.showComment') }}</button></div>
          <div v-if="!readonly && showComment"><button class="btn" @click="clickClearCommentHandler">{{ $t('obsInput.clear') }}</button></div>
        </div>
      </div>
      <div class="tw-space-y-2">
        <div class="tw-min-h-[65px] tw-flex">
          <rich-text v-if="app.isDisplayEnglish" height="auto" v-model="obs.scopeTrans"
            @dblclick="dbClickScopeHandler" resize :disabled="readonly"></rich-text>
          <rich-text v-else height="auto" v-model="obs.scope" @dblclick="dbClickScopeHandler" resize
            :disabled="readonly"></rich-text>
        </div>
        <div v-if="showComment" class="tw-flex tw-ml-[50px] tw-space-x-5">
          <div class="tw-font-semibold tw-text-lg">{{ $t('obsInput.commentTitle') }}</div>
          <div class="tw-flex-auto tw-min-h-[35px]">
            <rich-text v-model="obs.scopeComment" @dblclick="dbClickScopeCmtHandler" resize
              :disabled="readonly"></rich-text>
          </div>
        </div>
      </div>
    </div>
    <!-- End SCOPE -->
    <!-- Begin Fact -->
    <div class="tw-space-y-3">
      <div class="tw-flex">
        <div class="tw-flex tw-items-end tw-w-3/5">
          <div class="tw-font-semibold tw-text-lg">2.観察事実 FACTS</div>
        </div>
        <div class="tw-flex tw-w-2/5">
          <div class="tw-space-x-3 tw-flex tw-w-[280px]">
            <label class="tw-flex tw-items-center tw-whitespace-nowrap">{{ $t('obsInput.valComp') }}</label>
            <button class="btn" :class="{ disabled: readonly }" @click="clickCheckAllHandler">{{ $t('obsInput.allCheck') }}</button>
            <button class="btn" :class="{ disabled: readonly }" @click="clickUnCheckHandler">{{ $t('obsInput.allClear') }}</button>
          </div>
          <div class="tw-flex tw-items-center tw-justify-center tw-flex-auto">
            <label>{{ !pocMode ? $t('obsInput.relatedArea') : $t('obsInput.relatedPoc') }}</label>
          </div>
        </div>
      </div>
      <div ref="factsRef" :key="factsKey" class="tw-space-y-2">
        <div class="tw-rounded-lg tw-p-2 tw-border tw-border-gray-400 tw-border-dashed tw-space-y-2 fact-item"
          tabindex="-1" v-for="(item, index) in facts" :key="item">
          <div class="tw-flex tw-w-full">
            <div class="tw-flex tw-w-3/5">
              <div class="tw-w-[40px] tw-flex tw-flex-col tw-space-y-2 tw-mt-2">
                <div class="tw-h-[40px]">
                  <q-btn dense :disable="readonly" push icon="swap_vert" color="primary" @click="clickSwapFactHandler(index)" />
                </div>
                <div class="tw-h-[40px]">
                  <q-btn disable round color="blue-grey" size="sm" :label="index + 1" class="handle"></q-btn>
                </div>
              </div>
              <rich-text v-if="app.isDisplayEnglish" v-model="item.factTrans" resize
                @dblclick="dbClickFactHandler(index)" :disabled="readonly"></rich-text>
              <rich-text v-else v-model="item.fact" resize @dblclick="dbClickFactHandler(index)"
                :disabled="readonly"></rich-text>
            </div>
            <div class="tw-flex tw-ml-4 tw-pr-2 tw-w-2/5">
              <div class="tw-flex tw-space-x-1 tw-mt-2 tw-w-[320px]">
                <div class="tw-flex tw-flex-col tw-space-y-2 tw-w-[100px]">
                  <button class="btn" @click="clickPhotoHandler(index)"
                    :class="{ 'tw-text-red-600': hasPhoto(item) }">{{ $t('obsInput.photo') }}</button>
                  <button class="btn" @click="clickTransferHandler(item)" :class="{ disabled: readonly }">{{ $t('obsInput.transfer') }}</button>
                </div>
                <div class="tw-w-[190px] tw-pl-2">
                  <div class="tw-space-x-2 tw-text-center">
                    <q-checkbox v-model="item.valComp" :label="$t('obsInput.valComp')" color="cyan" keep-color size="sm"
                      :disable="readonly || item.plusNeutralDelta === PLUS_NEUTRAL_DELTA.NEUTRAL" />
                  </div>
                  <div class="tw-flex tw-mx-2 tw-justify-between">
                    <div class="tw-text-center tw-space-y-1">
                      <q-radio keep-color v-model="item.plusNeutralDelta" :val="PLUS_NEUTRAL_DELTA.PLUS" size="sm"
                        class="tw-flex-col-reverse" color="teal" label="Plus" :disable="readonly" />
                    </div>
                    <div class="tw-text-center tw-space-y-1">
                      <q-radio keep-color v-model="item.plusNeutralDelta" :val="PLUS_NEUTRAL_DELTA.NEUTRAL" size="sm"
                        class="tw-flex-col-reverse" color="deep-orange" label="Neutral" :disable="readonly"
                        @update:model-value="item.valComp = false" />
                    </div>
                    <div class="tw-text-center tw-space-y-1">
                      <q-radio keep-color v-model="item.plusNeutralDelta" :val="PLUS_NEUTRAL_DELTA.DELTA" size="sm"
                        class="tw-flex-col-reverse" color="negative" label="Delta" :disable="readonly" />
                    </div>
                  </div>
                </div>
              </div>
              <div class="tw-flex-auto">
                <div v-if="!pocMode" class="tw-grid tw-grid-cols-1 tw-my-1">
                  <div class="tw-w-full">
                    <select-box v-model="item.offerFields" :rows="fields" :options-width="'300px'"
                      :display-props="['fields', app.isDisplayEnglish ? 'fieldsNameEn' : 'fieldsName']" filter
                      :disabled="readonly" display-text="fields" binding-prop="fields" multiple>
                    </select-box>
                  </div>
                </div>
                <div v-else class="tw-grid tw-grid-cols-1 tw-gap-3 tw-my-1">
                  <div class="tw-w-full">
                    <select-box v-model="item.poCs" :rows="pocs" :options-width="'350px'"
                      :display-props="['code', app.isDisplayEnglish ? 'nameEn' : 'name']" filter
                      :disabled="readonly" display-text="code" binding-prop="code" multiple>
                    </select-box>
                  </div>
                  <div class="tw-w-full">
                    <select-box v-model="item.safetyCultures" :rows="safetyCultures" :options-width="'350px'"
                      :display-props="['code', app.isDisplayEnglish ? 'nameEn' : 'name']" filter
                      :disabled="readonly" display-text="code" binding-prop="code" multiple>
                    </select-box>
                  </div>
                </div>
              </div>
            </div>
            <div class="tw-w-[40px] tw-flex tw-items-end tw-flex-col tw-space-y-2 tw-mt-2">
              <div class="tw-h-[40px]">
                <q-btn dense :disable="readonly" push icon="add" color="secondary" @click="clickAddFactHandler(index)" />
              </div>
              <div class="tw-h-[40px]">
                <q-btn dense :disable="readonly" push color="red" icon="remove" @click="clickRemoveFactHandler(index)" />
              </div>
            </div>
          </div>
          <div v-if="showComment" class="tw-flex tw-ml-[40px] tw-space-x-5">
            <div class="tw-font-semibold tw-text-lg">{{ $t('obsInput.commentTitle') }}</div>
            <div class="tw-flex-auto tw-min-h-[35px]">
              <rich-text v-model="item.comment" resize @dblclick="dbClickFactCmtHandler(index)"
                :disabled="readonly"></rich-text>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- End Fact -->
    <!-- Begin Conclusion -->
    <div class="tw-space-y-3">
      <div class="tw-font-semibold tw-text-lg">3. 結論 Conclusions</div>
      <div ref="conclusionRef" :key="conclusionsKey" class="tw-space-y-2">
        <div class="tw-rounded-lg tw-p-2 tw-border tw-border-gray-400 tw-border-dashed tw-space-y-2 conclusion-item"
          tabindex="-1" v-for="(item, index) in conclusions" :key="item">
          <div class="tw-flex tw-w-full">
            <div class="tw-flex tw-w-3/5">
              <div class="tw-w-[40px] tw-flex tw-flex-col tw-space-y-2 tw-mt-2">
                <div class="tw-h-[40px]">
                  <q-btn dense :disable="readonly" push icon="swap_vert" color="primary"
                    @click="clickSwapConclusionHandler(index)" />
                </div>
                <div class="tw-h-[40px]">
                  <q-btn disable round color="blue-grey" size="sm" :label="index + 1" class="handle"></q-btn>
                </div>
              </div>
              <rich-text v-if="app.isDisplayEnglish" resize v-model="item.conclusionTrans"
                @dblclick="dbClickConclusionHandler(index)" :disabled="readonly"></rich-text>
              <rich-text v-else resize v-model="item.conclusion" @dblclick="dbClickConclusionHandler(index)"
                :disabled="readonly"></rich-text>
            </div>
            <div class="tw-ml-4 tw-pr-2 tw-w-2/5">
              <div class="tw-space-x-2 tw-mt-2">
                <button class="btn tw-w-[100px]" :class="{ disabled: readonly }" @click="clickRelatedFactHandler(item)"> {{$t('obsInput.relatedFacts') }}</button>
                <span v-if="item.connectFact">
                  {{ connectFactByFactSort(item.connectFact) }}</span>
              </div>
            </div>
            <div class="tw-w-[40px] tw-flex tw-items-end tw-flex-col tw-space-y-2 tw-mt-2">
              <div class="tw-h-[40px]">
                <q-btn dense :disable="readonly" push icon="add" color="secondary"
                  @click="clickAddConclusionHandler(index)" />
              </div>
              <div class="tw-h-[40px]">
                <q-btn dense :disable="readonly" push color="red" icon="remove"
                  @click="clickRemoveConclusionHandler(index)" />
              </div>
            </div>
          </div>
          <div v-if="showComment" class="tw-flex tw-ml-[40px] tw-space-x-5">
            <div class="tw-font-semibold tw-text-lg">{{ $t('obsInput.commentTitle') }}</div>
            <div class="tw-flex-auto tw-min-h-[35px]">
              <rich-text v-model="item.comment" resize @dblclick="dbClickConclusionCmtHandler(index)"
                :disabled="readonly"></rich-text>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- End Conclusion -->
    <div v-if="showComment" class="tw-flex tw-ml-[50px] tw-space-x-5">
      <div class="tw-font-semibold tw-text-lg">{{ $t('obsInput.jdcTitle') }}</div>
      <div class="tw-flex-auto tw-min-h-[35px]">
        <rich-text v-model="obs.generalComment" resize :disabled="readonly" @dblclick="dbClickGeneralCmtHandler"></rich-text>
      </div>
    </div>
  </div>

</template>

<script setup>
// ======= Libraries =======
import {
  ref, onMounted, nextTick, defineAsyncComponent, computed, watch,
} from 'vue';
import {
  TYPE_INPUT_AS_FIRST_LETTER,
  PLUS_NEUTRAL_DELTA, DIVISION, OBS_INPUT_TAG, DIALOG_RETURN_VAL, REQUEST_FROM_SCREEN, FORM_NAME, PRINT_MODE, NUMBER_AS_BOOL,
} from 'helpers/enum';
import { useRouter, onBeforeRouteLeave } from 'vue-router';
import { cloneDeep, isEqual, isNumber } from 'lodash';
import { changeLanguage } from 'utilities/language';
import Sortable from 'sortablejs';
import { sprintf } from 'sprintf-js';
import { useI18n } from 'vue-i18n';

// ======= Components =======
import RichText from 'components/common/RichText.vue';
import SelectBox from 'components/common/SelectBox.vue';

// ======= Services =======
import obsService from 'services/obs.service';
import peerReviewService from 'services/peer-review.service';
import obsFactService from 'services/obs-fact.service';
import obsConclusionService from 'services/obs-conclusion.service';
import obsAttachService from 'services/obs-attach.service';
import activityService from 'services/activity.service';
import fieldService from 'services/field.service';
import userService from 'services/user.service';
import pocService from 'services/poc.service';
import safetyCultureService from 'services/safety-culture.service';

// ======= Utilities =======
import dialog from 'utilities/dialog';
import { createFact, createConclusion } from 'helpers/objectFactory';
import { useLoadingStore } from 'stores/loading-store';
import { useAppStore } from 'stores/app-store';
import {
  MSG, MODAL, LANGUAGE, CONFIRM_DIALOG,
} from 'helpers/constant';

// 1) ======= INITIALIZATION ========
const RecognizerSelection = defineAsyncComponent(() => import('components/obs/RecognizerSelection.vue'));
const TransferOBSFact = defineAsyncComponent(() => import('components/obs/TransferOBSFact.vue'));
const PhotographSelection = defineAsyncComponent(() => import('components/obs/PhotographSelection.vue'));
const FactSelection = defineAsyncComponent(() => import('components/obs/FactSelection.vue'));
const TranslateRequest = defineAsyncComponent(() => import('components/obs/TranslateRequest.vue'));
const TranslationHistory = defineAsyncComponent(() => import('components/obs/TranslationHistory.vue'));
const PopupInput = defineAsyncComponent(() => import('components/obs/PopupInput.vue'));
const OBSChanged = defineAsyncComponent(() => import('components/messages/OBSChanged.vue'));
const ForwardFactChanged = defineAsyncComponent(() => import('components/messages/ForwardFactChanged.vue'));
const FactChanged = defineAsyncComponent(() => import('components/messages/FactChanged.vue'));
const ReArrange = defineAsyncComponent(() => import('components/obs/ReArrange.vue'));
const OutputOption = defineAsyncComponent(() => import('components/obs/OutputOption.vue'));

const router = useRouter();
const loading = useLoadingStore();
const app = useAppStore();
const { t } = useI18n();

// 2) ======= VARIABLE REF ========
// 画面で表示するOBSデータ
const obs = ref({});
// 変化チェック用の元々OBSデータ
const obsClone = ref(null);
// プレビューの場合
const readonly = ref(false);
// 選択されているリビジョン
const rev = ref(null);
// 観察対象一覧
const obsTargets = ref([]);
// PO&C一覧
const pocs = ref([]);
// field一覧
const fields = ref([]);
// リビジョン一覧
const revisions = ref([]);
// PO&Cを表示した場合
const pocMode = ref(false);
// 画面で表示するfactデータ
const facts = ref([]);
// 変化チェック用の元々factデータ
const factsClone = ref([]);
// 画面で表示するconclusionデータ
const conclusions = ref([]);
// 変化チェック用の元々conclusionデータ
const conclusionsClone = ref([]);
// 写真データ
const photos = ref([]);
// 安全文化データ
const safetyCultures = ref([]);
// コメント表示モード
const showComment = ref(false);

const factsKey = ref(0);
const conclusionsKey = ref(0);
const factsSortable = ref(null);
const conclusionSortable = ref(null);
const factsRef = ref(null);
const conclusionRef = ref(null);
const user = ref({});
const skipCheckChange = ref(false);
const tag = ref(null);

const previewMode = computed(() => router.currentRoute.value.name === 'obs-preview');

// 次承認者
const nextApprover = computed(() => {
  if (obs.value?.approvalState1 && obs.value?.approvalState1Id?.length > 0) {
    return obs.value.approvalState1Id;
  }
  if (obs.value?.approvalState2 && obs.value?.approvalState2Id?.length > 0) {
    return obs.value.approvalState2Id;
  }
  if (obs.value?.approvalState3 && obs.value?.approvalState3Id?.length > 0) {
    return obs.value.approvalState3Id;
  }
  if (obs.value?.approvalStateTl && obs.value?.approvalStateTlId?.length > 0) {
    return obs.value.approvalStateTlId;
  }
  return null;
});

// 翻訳状況
const transState = computed(() => {
  if (obs.value.transSituation === '未出力') {
    if (obs.value.transCancel === '取消済') {
      return t('obsInput.transState.canceled');
    }
    return t('obsInput.transState.noOutput');
  }
  if (obs.value.transSituation === '出力済') {
    if (obs.value.transCancel === '取消済') {
      return t('obsInput.transState.canceled');
    }
    if (obs.value.transCancel === '取消依頼中') {
      return t('obsInput.transState.cancelReq');
    }
    if (obs.value.transCancel === '翻訳継続') {
      return t('obsInput.transState.transContinued');
    }
    return t('obsInput.transState.outputted');
  }
  if (obs.value.transSituation === '取込済') {
    return t('obsInput.transState.imported');
  }
  if (obs.value.pastTranslatedRev?.length > 0) {
    if (obs.value.pastTranslatedStatus) {
      return `r${obs.value.pastTranslatedRev} ${t('obsInput.transState.imported')}`;
    }
    return `r${obs.value.pastTranslatedRev} ${t('obsInput.transState.transReq')}`;
  }
  const outputLabel = t('obsInput.transState.noOutput');
  return obs.value.transDeadline ? outputLabel : '';
});

// 3) ======= METHOD/FUNCTION ========
// factに写真の存在チェック
const hasPhoto = (fact) => fact.obsAttachs?.some((photo) => photo?.size > 0 && !photo.deleteFlag);

// factに入力済みか確認
const isFactInputted = (fact) => fact.fact?.length > 0 || fact.factTrans?.length > 0 || fact.comment?.length > 0 || fact.partTrans || fact.valComp || hasPhoto(fact.factNum);

// factのPO&Cを入力済みか確認
const isPocFilled = (fact) => fact.poCs?.length > 0 || fact.safetyCultures?.length > 0;

// factの分野を入力済みか確認
const isFieldFilled = (fact) => fact.offerFields?.length > 0;

// 画面上の変化が有無か確認
const isDataChanged = () => !isEqual(obs.value, obsClone.value)
  || !isEqual(facts.value, factsClone.value)
  || !isEqual(conclusions.value, conclusionsClone.value);

// factに変化があった時に警告を表示する
const isFactChanged = async () => {
  if (!isEqual(facts.value, factsClone.value) && tag.value !== OBS_INPUT_TAG.ADD) {
    const result = await dialog.showContent(
      MODAL.TITLE_WARNING,
      FactChanged,
      { showFooter: true, buttons: [MODAL.YES, MODAL.NO] },
    );
      // いいえを選んだ時に処理を中止する
    if (result === MODAL.NO) return false;
  }
  // 処理続行
  return true;
};

// factが未入力の場合に警告を表示する
const showFactWarning = async () => {
  let factFilled = true;
  for (const fact of facts.value) {
    if (isFactInputted(fact)) {
      if ((pocMode.value && !isPocFilled(fact)) || (!pocMode.value && !isFieldFilled(fact))) {
        factFilled = false;
        break;
      }
    }
  }
  if (!factFilled) {
    await dialog.showMessage(MODAL.TITLE_INFO, pocMode.value ? MSG.RELATED_POC_NOT_FILLED : MSG.RELATED_AREA_NOT_FILLED);
  }
};

// facts一覧の順番を元に、conclusionにあるconnectFactの値を表示する
const connectFactByFactSort = (connectFact) => {
  let connectFactByFactSortString = '';
  const connectFactList = connectFact.split(',').map(Number);
  for (let index = 0; index < facts.value.length; index++) {
    const factValue = facts.value[index];
    if (connectFactList.includes(factValue.factNum)) {
      if (connectFactByFactSortString.length > 0) connectFactByFactSortString += ',';
      connectFactByFactSortString += `(${index + 1})`;
    }
  }
  return connectFactByFactSortString;
};

// fact並び替え後の処理
const sortFactsHandler = (e) => {
  const item = facts.value[e.oldIndex];
  facts.value.splice(e.oldIndex, 1);
  facts.value.splice(e.newIndex, 0, item);
  factsKey.value++;
};

// conclusion並び替え後の処理
const sortConclusionsHandler = (e) => {
  const item = conclusions.value[e.oldIndex];
  conclusions.value.splice(e.oldIndex, 1);
  conclusions.value.splice(e.newIndex, 0, item);
  conclusionsKey.value++;
};

// factの位置並び替えポップアップを開く
const clickSwapFactHandler = async (index) => {
  const dispOrder = await dialog.showContent('観察事実の並び替え Sort FACTS', ReArrange, { params: { selectedIndex: index, maxIndex: facts.value.length } });
  if (!dispOrder) return;
  if (!isNumber(dispOrder.from) || !isNumber(dispOrder.to)
    || dispOrder.form < 1 || dispOrder.from > facts.value.length
    || dispOrder.to < 1 || dispOrder.to > facts.value.length) return;
  sortFactsHandler({ oldIndex: dispOrder.from - 1, newIndex: dispOrder.to - 1 });
};

// conclusionの位置並び替えポップアップを開く
const clickSwapConclusionHandler = async (index) => {
  const dispOrder = await dialog.showContent('結論の並び替え Sort Conclusions', ReArrange, { params: { selectedIndex: index, maxIndex: conclusions.value.length } });
  if (!dispOrder) return;
  if (!isNumber(dispOrder.from) || !isNumber(dispOrder.to)
    || dispOrder.form < 1 || dispOrder.from > conclusions.value.length
    || dispOrder.to < 1 || dispOrder.to > conclusions.value.length) return;
  sortConclusionsHandler({ oldIndex: dispOrder.from - 1, newIndex: dispOrder.to - 1 });
};

// factの項目を並び替えるためにドラッグ＆ドロップイベントを初期化する
const initFactsSort = () => {
  if (readonly.value) return;
  nextTick(() => {
    factsSortable.value = new Sortable(factsRef.value, {
      animation: 150,
      handle: '.handle',
      ghostClass: 'tw-bg-blue-100',
      onUpdate: sortFactsHandler,
    });
  });
};

// conclusionの項目を並び替えるためにドラッグ＆ドロップイベントを初期化する
const initConclusionsSort = () => {
  if (readonly.value) return;
  nextTick(() => {
    conclusionSortable.value = new Sortable(conclusionRef.value, {
      animation: 150,
      handle: '.handle',
      ghostClass: 'tw-bg-blue-100',
      onUpdate: sortConclusionsHandler,
    });
  });
};

// 画面初期化
const initObs = async () => {
  try {
    loading.showMulti();
    const obsNum = router.currentRoute.value.params.num;
    tag.value = router.currentRoute.value.query.tag;
    obs.value = await obsService.getOBSByNumber(obsNum);
    if (!obs.value) {
      router.push({ name: 'obs-list' });
      return;
    }
    readonly.value = previewMode.value || obs.value.transStateReq;

    const result = await Promise.all([
      peerReviewService.getByPlantName(obs.value.plantName),
      obsFactService.list(obs.value.num),
      obsConclusionService.list(obs.value.num),
      obsAttachService.list(obs.value.num),
      fieldService.get11(),
      pocService.getList(),
      activityService.list(),
      userService.getUserName(obs.value.plantName, obs.value.partId),
      obsService.getRevisionList(obs.value),
      safetyCultureService.getList(),
    ]);

    let peerReview;
    [peerReview, facts.value, conclusions.value, photos.value, fields.value, pocs.value, obsTargets.value, user.value, revisions.value, safetyCultures.value] = result;
    pocMode.value = peerReview.offerDivision === DIVISION.POC;
    // TODO process obs attach
    obsClone.value = cloneDeep(obs.value);
    factsClone.value = cloneDeep(facts.value);
    conclusionsClone.value = cloneDeep(conclusions.value);
    initFactsSort();
    initConclusionsSort();
  } catch {
    skipCheckChange.value = true;
    router.push({ name: 'obs-list' });
  } finally {
    loading.hideMulti();
  }
};

// OBS保存のため、オブジェクトformDataを作成する
const createSaveObsFormData = (isSaveNew) => {
  const saveObs = new FormData();
  saveObs.append('obs', JSON.stringify(obs.value));
  saveObs.append('isNewFlag', isSaveNew);
  saveObs.append('photoPath', `${app.folderStore.photoLiveFolder}`);
  for (const fact of facts.value) {
    saveObs.append('facts[]', JSON.stringify(fact));
  }
  for (const conclusion of conclusions.value) {
    saveObs.append('conclusions[]', JSON.stringify(conclusion));
  }
  return saveObs;
};

// OBSを新バージョンとして保存する
const saveNewHandler = async (msgFlg) => {
  const saveObs = createSaveObsFormData(true);
  // OBS新規保存用APIを呼び出す
  const result = await obsService.saveAll(saveObs);
  if (result?.length > 0) {
    if (msgFlg) { await dialog.showMessage(MODAL.TITLE_INFO, MSG.REVUP_SAVED); }
    // 保存したobsNumでパスを変える
    router.replace({ name: 'obs-input', params: { num: result }, query: { tag: tag.value } });
  } else {
    await dialog.showMessage(MODAL.TITLE_INFO, MSG.SAVE_FAILED);
  }
};

// 現在OBSを上書き保存する
const saveOverrideHandler = async (msgFlg, justSaveFlag = false) => {
  // OBS保存APIを呼び出す
  const saveObs = createSaveObsFormData(false);
  const result = await obsService.saveAll(saveObs);
  if (!justSaveFlag) {
    if (result?.length > 0) {
      if (msgFlg) { await dialog.showMessage(MODAL.TITLE_INFO, obs.value.transStateReq ? MSG.REVUP_SAVED : MSG.DATA_SAVED_TRANS_REQUEST); }
      // タグを確認しパスを変える
      if (tag.value === OBS_INPUT_TAG.ADD) router.replace({ name: 'obs-input', params: { num: result }, query: { tag: OBS_INPUT_TAG.EDIT } });
      else initObs();
    } else {
      await dialog.showMessage(MODAL.TITLE_INFO, MSG.SAVE_FAILED);
    }
  }
};

// 9.3
// 保存
const clickSaveHandler = async () => {
  if (readonly.value) return;
  if (!await isFactChanged()) return;
  await showFactWarning();
  await saveOverrideHandler(true);
};

// 9.4
// RevUp保存
const clickRevUpSaveHandler = async () => {
  if (readonly.value) return;
  const result = await dialog.showConfirm(MODAL.TITLE_WARNING, MSG.REVUP_SAVE_CONFIRM);
  if (result === CONFIRM_DIALOG.NO) return;
  if (!await isFactChanged()) return;
  await showFactWarning();
  await saveNewHandler(true);
};

// 9.5
// 削除ボタン押下イベント
const clickDeleteHandler = async () => {
  if (readonly.value) return;
  const result = await dialog.showConfirm(MODAL.TITLE_WARNING, MSG.DELETE_RECORD);
  if (result === CONFIRM_DIALOG.NO) return;
  const deleted = await obsService.deleteOBS(obs.value);
  if (deleted) {
    skipCheckChange.value = true;
    router.push({ name: 'obs-list' });
  }
};

// 9.6
// 印刷ボタン押下イベント
const clickPrintHandler = async () => {
  if (isDataChanged()) saveOverrideHandler(false);
  // TODO G_OBSワークテーブル変換
  await dialog.showContent('印刷オプション Print Options', OutputOption, {
    maxWidth: '60vw',
    params: {
      formName: FORM_NAME.OBS,
      printMode: PRINT_MODE.PRINT,
    },
  });
  // TODO open report
};

// ファイル保存パスを選ぶポップアップを開く
const downloadFileHandler = (fileBlob, fileName) => {
  const blobURL = URL.createObjectURL(fileBlob);
  const link = document.createElement('a');
  link.href = blobURL;
  link.download = fileName;
  document.body.appendChild(link);
  link.dispatchEvent(
    new MouseEvent('click', {
      bubbles: true,
      cancelable: true,
      view: window,
    }),
  );

  // Remove link from body
  document.body.removeChild(link);
};

// richtextのplainText値を取得する
const getPlainText = (richText) => {
  const element = document.createElement('div');
  element.innerHTML = richText;
  return element.textContent ? element.textContent : element.innerText;
};

// factによりmainPhotoを取得する
const getPhotoByFact = (index, photoPrinting) => {
  const representPhoto = facts.value[index].obsAttachs.find((x) => x.representPhotoFlag);
  if (photoPrinting === NUMBER_AS_BOOL.TRUE) {
    const fileName = representPhoto ? representPhoto.fileName : '';
    return fileName;
  }
  return '';
};

// 印刷選択肢によりfactInfoを取得する
const getFactInfo = (isShowRichText, photoPrinting) => {
  const factInfo = [];
  for (let index = 0; index < facts.value.length; index++) {
    factInfo[index] = {};
    factInfo[index].no = (index + 1);
    if (app.isDisplayEnglish) {
      factInfo[index].content = facts.value[index].factTrans;
      if (isShowRichText) {
        factInfo[index].content = getPlainText(facts.value[index].factTrans);
      }
    } else {
      factInfo[index].content = facts.value[index].fact;
      if (isShowRichText) {
        factInfo[index].content = getPlainText(facts.value[index].fact);
      }
    }
    const attachFileName = getPhotoByFact(index, photoPrinting);
    factInfo[index].imageUrl = attachFileName.length > 0 ? `${app.folderStore.photoLiveFolder}\\${attachFileName}` : '';
  }
  return factInfo;
};

// 印刷の値が英語の場合、conclusionInfoを取得する
const getConclusionEnglish = (isShowRichText, connectedFact, index, conclusionInfo) => {
  connectedFact = connectedFact ? `【Facts】${connectedFact}` : '';
  const plainTransText = getPlainText(conclusions.value[index].conclusionTrans);
  connectedFact = plainTransText === '' ? connectedFact : `<div>${connectedFact}</div>`;
  if (!isShowRichText) {
    if (plainTransText === '') {
      conclusionInfo[index].content = connectedFact;
    } else {
      conclusionInfo[index].content = (conclusions.value[index].conclusionTrans ? conclusions.value[index].conclusionTrans : '') + connectedFact;
    }
  } else {
    conclusionInfo[index].content = plainTransText + connectedFact;
  }
};

// 印刷の値がオリジナル版の場合、conclusionInfoを取得する
const getConclusionOriginalLang = (connectedFact, isShowRichText, index, conclusionInfo) => {
  connectedFact = connectedFact ? `【観察事実】${connectedFact}` : '';
  const plainOriginText = getPlainText(conclusions.value[index].conclusion);
  connectedFact = plainOriginText === '' ? connectedFact : `<div>${connectedFact}</div>`;

  if (!isShowRichText) {
    if (plainOriginText === '') {
      conclusionInfo[index].content = connectedFact;
    } else {
      conclusionInfo[index].content = (conclusions.value[index].conclusion ? conclusions.value[index].conclusion : '') + connectedFact;
    }
  } else {
    conclusionInfo[index].content = plainOriginText + connectedFact;
  }
};

// 印刷選択肢によりconclusionInfoを取得する
const getConclusionInfo = (isShowRichText) => {
  const conclusionInfo = [];
  for (let index = 0; index < conclusions.value.length; index++) {
    conclusionInfo[index] = {};
    conclusionInfo[index].no = (index + 1);
    const connectedFact = conclusions.value[index].connectFact ? connectFactByFactSort(conclusions.value[index].connectFact) : '';
    if (app.isDisplayEnglish) {
      getConclusionEnglish(isShowRichText, connectedFact, index, conclusionInfo);
    } else {
      getConclusionOriginalLang(connectedFact, isShowRichText, index, conclusionInfo);
    }
  }
  return conclusionInfo;
};

// 印刷選択肢と言語を元に、titleInfoとscopeInfoの値を取得する
const getObsTitleInfoAndScopeInfo = (isShowRichText) => {
  let titleValue = '';
  if (app.isDisplayEnglish) {
    titleValue = obs.value.titleTrans;
    if (isShowRichText) {
      titleValue = getPlainText(obs.value.titleTrans);
    }
  } else {
    titleValue = obs.value.title;
    if (isShowRichText) {
      titleValue = getPlainText(obs.value.title);
    }
  }

  let scopeValue = '';
  if (app.isDisplayEnglish) {
    scopeValue = obs.value.scopeTrans;
    if (isShowRichText) {
      scopeValue = getPlainText(obs.value.scopeTrans);
    }
  } else {
    scopeValue = obs.value.scope;
    if (isShowRichText) {
      scopeValue = getPlainText(obs.value.scope);
    }
  }
  return [titleValue, scopeValue];
};

// 9.7
// Word出力ボタン押下イベント
const clickOutputWordHandler = async () => {
  if (isDataChanged()) saveOverrideHandler(false);
  const outputOptions = await dialog.showContent('出力オプション Output Options', OutputOption, {
    maxWidth: '60vw',
    params: {
      formName: FORM_NAME.OBS,
      printMode: PRINT_MODE.WORD,
    },
  });
  const isShowRichText = outputOptions.characterFormat !== NUMBER_AS_BOOL.TRUE;
  if (outputOptions !== DIALOG_RETURN_VAL.CANCEL) {
    const factInfo = getFactInfo(isShowRichText, outputOptions.photoPrinting);
    const conclusionInfo = getConclusionInfo(isShowRichText);
    const [titleValue, scopeValue] = getObsTitleInfoAndScopeInfo(isShowRichText);
    const obsInfo = {
      number: obs.value.num,
      area: app.peerReviewMaster?.EvalArea,
      title: titleValue,
      plantName: obs.value.plantName,
      facts: factInfo,
      conclusions: conclusionInfo,
      scope: scopeValue,
      isEnglish: app.language.code === LANGUAGE.ENGLISH_FIRST_LETTER,
      path: app.folderStore.wordFolder,
    };

    const file = await obsService.outputWord(obsInfo);
    downloadFileHandler(file, `${obs.value.num}.docx`);
  }
};

// 9.11
// rev修正
const changeUpdateRevHandler = async () => {
  const revision = revisions.value.find((x) => x.num === rev.value);

  router.push({ name: 'obs-preview', params: { num: revision.num }, query: { ver: revision.revisions } });
};

// 9.17
// recognizerSelectorのポップアップを開く
const clickEditorHandler = async () => {
  await dialog.showContent('承認者選択 Recognizer Selection', RecognizerSelection, {
    headerLeft: true,
    maxWidth: '500px',
  });
};

// TransReqポップアップを開く前に値を確認して渡す
const checkToOpenTransReq = async () => {
  const result = await dialog.showContent('翻訳依頼 Translation request', TranslateRequest, {
    maxWidth: '650px',
    params: {
      requestTimeLimitation: obs.value.transTime,
      from: REQUEST_FROM_SCREEN.OBS_INPUT,
      screenName: FORM_NAME.OBS_INPUT,
      data: obs.value,
      saveObs: saveOverrideHandler,
    },
  });
  if (result.dialogResult === DIALOG_RETURN_VAL.OK) initObs();
};

// 9.18
// 翻訳依頼ポップアップを開く
const clickTransReqHandler = async () => {
  checkToOpenTransReq();
};

// 9.19
// 翻訳履歴ポップアップを開く
const clickTransHistHandler = () => {
  dialog.showContent('翻訳履歴 Translate History', TranslationHistory, {
    params: {
      wktTranHisCondition: obs.value,
      lang: app.language,
    },
    maxWidth: '100%',
    width: '1200px',
  });
};

const saveDataFromPopupInput = (obsInput, factInput, conclusionInput) => {
  obs.value = obsInput;
  facts.value = factInput;
  conclusions.value = conclusionInput;
};

// 9.20
// dbClick titleInput押下イベント
const dbClickTitleHandler = async () => {
  if (readonly.value) return;
  await dialog.showContent('ポップアップ入力 Pop-up input', PopupInput, {

    params: {
      selected: {
        type: TYPE_INPUT_AS_FIRST_LETTER.TITLE,
        index: 0,
      },
      obs: obs.value,
      facts: facts.value,
      conclusions: conclusions.value,
      applyAction: saveDataFromPopupInput,
    },
    maxWidth: '100%',
    headerLeft: true,
  });
};

// 9.21
// dbClick scopeInput押下イベント
const dbClickScopeHandler = async () => {
  if (readonly.value) return;
  await dialog.showContent('ポップアップ入力 Pop-up input', PopupInput, {
    params: {
      selected: {
        type: TYPE_INPUT_AS_FIRST_LETTER.SCOPE,
        index: 0,
      },
      obs: obs.value,
      facts: facts.value,
      conclusions: conclusions.value,
      applyAction: saveDataFromPopupInput,
    },
    maxWidth: '100%',
    headerLeft: true,
  });
};

// 9.22
// dbClick scopeInputComment押下イベント
const dbClickScopeCmtHandler = async () => {
  if (readonly.value) return;
  await dialog.showContent('ポップアップ入力 Pop-up input', PopupInput, {
    params: {
      selected: {
        type: TYPE_INPUT_AS_FIRST_LETTER.SCOPE,
        index: 0,
      },
      isComment: true,
      obs: obs.value,
      facts: facts.value,
      conclusions: conclusions.value,
      applyAction: saveDataFromPopupInput,
    },
    maxWidth: '100%',
    headerLeft: true,
  });
};

// 9.23
// dbClick factInput押下イベント
const dbClickFactHandler = async (index) => {
  if (readonly.value) return;
  await dialog.showContent('ポップアップ入力 Pop-up input', PopupInput, {
    params: {
      selected: {
        type: TYPE_INPUT_AS_FIRST_LETTER.FACT,
        index,
      },
      obs: obs.value,
      facts: facts.value,
      conclusions: conclusions.value,
      applyAction: saveDataFromPopupInput,
    },
    maxWidth: '100%',
    headerLeft: true,
  });
};

// 9.24
// dbClick factInputComment押下イベント
const dbClickFactCmtHandler = async (index) => {
  if (readonly.value) return;
  await dialog.showContent('ポップアップ入力 Pop-up input', PopupInput, {
    params: {
      selected: {
        type: TYPE_INPUT_AS_FIRST_LETTER.FACT,
        index,
      },
      isComment: true,
      obs: obs.value,
      facts: facts.value,
      conclusions: conclusions.value,
      applyAction: saveDataFromPopupInput,
    },
    maxWidth: '100%',
    headerLeft: true,
  });
};

// 9.27
// dbClick dbConclusion押下イベント
const dbClickConclusionHandler = async (index) => {
  if (readonly.value) return;
  await dialog.showContent('ポップアップ入力 Pop-up input', PopupInput, {
    params: {
      selected: {
        type: TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION,
        index,
      },
      obs: obs.value,
      facts: facts.value,
      conclusions: conclusions.value,
      applyAction: saveDataFromPopupInput,
    },
    maxWidth: '100%',
    headerLeft: true,
  });
};

// 9.28
// dbClick dbConclusionComment押下イベント
const dbClickConclusionCmtHandler = async (index) => {
  if (readonly.value) return;
  await dialog.showContent('ポップアップ入力 Pop-up input', PopupInput, {
    params: {
      selected: {
        type: TYPE_INPUT_AS_FIRST_LETTER.CONCLUSION,
        index,
      },
      isComment: true,
      obs: obs.value,
      facts: facts.value,
      conclusions: conclusions.value,
      applyAction: saveDataFromPopupInput,
    },
    maxWidth: '100%',
    headerLeft: true,
  });
};

// dbClick dbGeneralComment押下イベント
const dbClickGeneralCmtHandler = async () => {
  if (readonly.value) return;
  await dialog.showContent('ポップアップ入力 Pop-up input', PopupInput, {
    params: {
      selected: {
        type: TYPE_INPUT_AS_FIRST_LETTER.GENERAL_COMMENT,
        index: 0,
      },
      isComment: true,
      obs: obs.value,
      facts: facts.value,
      conclusions: conclusions.value,
      applyAction: saveDataFromPopupInput,
    },
    maxWidth: '100%',
    headerLeft: true,
  });
};

// 9.25
// transfer OBS Factポップアップを開く
const clickTransferHandler = async (item) => {
  // factの変化チェック
  if (!isEqual(facts.value, factsClone.value)) {
    const result = await dialog.showContent(
      MODAL.TITLE_INFO,
      ForwardFactChanged,
      { showFooter: true, buttons: [MODAL.YES, MODAL.NO] },
    );
    if (result === MODAL.NO) return;
    // 現在OBSの値も保存する
    await Promise.all([saveOverrideHandler(false), showFactWarning()]);
  }

  dialog.showContent('転送 Transfer OBS Fact', TransferOBSFact, {
    maxWidth: '100%',
    width: '624px',
    params: {
      fact: item,
      obsNum: obs.value.num,
    },
  });
};

// 9.26
// photographポップアップを表示する
const clickPhotoHandler = async (item) => {
  const currentFact = facts.value[item];
  if (!currentFact.obsAttachs) {
    currentFact.obsAttachs = [];
  }
  await dialog.showContent(
    '写真 Photograph',
    PhotographSelection,
    {
      maxWidth: 'none',
      params: {
        imageList: currentFact.obsAttachs,
        isReadOnly: readonly.value,
        photoNumSelected: item.photoNum,
        obs: obs.value,
        fact: currentFact,
      },
    },
  );
};

// 9.29
// RelatedFactポップアップを表示する
const clickRelatedFactHandler = async (conclusion) => {
  for (const fact of facts.value) {
    if (!isFactInputted(fact)) {
      dialog.showMessage(MODAL.TITLE_INFO, MSG.CANNOT_RELATION);
      return;
    }
  }
  await dialog.showContent('事実選択 Fact selection', FactSelection, {
    maxWidth: '60vw',
    params: {
      relatedFact: facts.value,
      conclusion,
    },
  });
};

// factを1件追加するイベント
const clickAddFactHandler = (index) => {
  if (facts.value.length >= 40) {
    dialog.showMessage(MODAL.TITLE_INFO, MSG.CANNOT_ADD);
    return;
  }
  const addedFact = createFact();
  let minFactNum = Math.min(...facts.value.map((item) => item.factNum)) - 1;
  if (minFactNum >= 0) {
    minFactNum = -1;
  }
  addedFact.factNum = minFactNum;
  facts.value.splice(index + 1, 0, addedFact);
};

const removeRelatedFact = (factNum) => {
  for (const conclusion of conclusions.value) {
    if (conclusion.connectFact?.length > 0) {
      let connectedFact = conclusion.connectFact.split(',');
      connectedFact = connectedFact.filter((x) => x !== `${factNum}`);
      conclusion.connectFact = connectedFact.join(',');
    }
  }
};

// factを1件削除するイベント
const clickRemoveFactHandler = async (index) => {
  const result = await dialog.showConfirm(MODAL.TITLE_WARNING, sprintf(MSG.DELETE_FACT, { num: [index + 1] }));
  if (result === CONFIRM_DIALOG.NO) return;
  removeRelatedFact(facts.value[index].factNum);
  facts.value.splice(index, 1);
  if (facts.value.length === 0) {
    facts.value.push(createFact());
  }
};

// conclusionを1件追加するイベント
const clickAddConclusionHandler = (index) => {
  if (conclusions.value.length >= 10) {
    dialog.showMessage(MODAL.TITLE_INFO, MSG.CANNOT_ADD);
    return;
  }
  conclusions.value.splice(index + 1, 0, createConclusion());
};

// conclusionを1件削除するイベント
const clickRemoveConclusionHandler = async (index) => {
  const result = await dialog.showConfirm(MODAL.TITLE_WARNING, sprintf(MSG.DELETE_CONCLUSION, { num: [index + 1] }));

  if (result === CONFIRM_DIALOG.NO) return;
  conclusions.value.splice(index, 1);
  if (conclusions.value.length === 0) {
    conclusions.value.push(createConclusion());
  }
};

// valCompの全チェック
const clickCheckAllHandler = () => {
  for (const fact of facts.value) {
    if (fact.plusNeutralDelta !== PLUS_NEUTRAL_DELTA.NEUTRAL) {
      fact.valComp = true;
    }
  }
};

// valCompの全クリア
const clickUnCheckHandler = () => {
  for (const fact of facts.value) {
    fact.valComp = false;
  }
};

// 言語切替押下イベント
const clickChangeLanguageHandler = () => {
  changeLanguage({ value: app.isDisplayEnglish ? LANGUAGE.JAPAN_CODE : LANGUAGE.ENGLISH_CODE });
};

// 全コメントクリア
const clickClearCommentHandler = () => {
  obs.value.scopeComment = null;
  for (const fact of facts.value) {
    fact.comment = null;
  }
  for (const conclusion of conclusions.value) {
    conclusion.comment = null;
  }
  obs.value.generalComment = null;
};

// コメント表示・非表示
const clickToggleCommentHandler = () => {
  showComment.value = !showComment.value;
};

// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(() => {
  // 9.1
  initObs();
});

// factをレンダリング後、factを並び替えるためにドラッグ＆ドロップ状態を再初期化する
watch(factsKey, () => {
  initFactsSort();
});

// conclusionsをレンダリング後、conclusionsを並び替えるためにドラッグ＆ドロップ状態を再初期化する
watch(conclusionsKey, () => {
  initConclusionsSort();
});

// ページを離脱する前に発生するイベント
onBeforeRouteLeave(async () => {
  if (skipCheckChange.value) return true;
  if (isDataChanged()) {
    const result = await dialog.showContent(
      MODAL.TITLE_WARNING,
      OBSChanged,
      {
        showFooter: true,
        buttons: [MODAL.YES, MODAL.NO, MODAL.CANCEL],
        isUseI18n: false,
      },

    );
      // キャンセルを押下した場合：閉じない
    if (result === MODAL.CANCEL) return false;
    // いいえを押下した場合
    if (result === MODAL.NO) return true;
    // はいを押下した場合：保存処理
    saveOverrideHandler(false, true);
  }
  return true;
});

</script>

<style scoped lang="scss">
.btn {
  min-height: 40px;
  line-height: 1rem;
  min-width: 100px;
}

fieldset {
  border: 1px solid gray;
  display: inline;
  height: 100%;
  text-align: center;
}

legend{
  font-weight: 500;
}
.handle {
  opacity: 1 !important;
  &:hover {
    opacity: 0.8 !important;
  }
  :deep(*) {
    cursor: pointer !important;
  }
}

</style>
