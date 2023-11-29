<template>
    <teleport to="#header">
    <div class="tw-flex sub-header">
      <fieldset class="tw-w-[70%] tw-border tw-flex tw-p-2 tw-items-center tw-ml-2 tw-pl-[30px]">
        <legend class="tw-text-center tw-px-4">{{ $t('obsList.search') }}</legend>
        <div class="tw-flex tw-h-5/6 tw-space-x-10 tw-w-full">
          <div class="tw-w-[30%] tw-space-y-1">
            <div class="tw-flex tw-h-1/2">
              <label for="obs-list-field" class="tw-w-[85px] tw-inline-flex tw-items-center">{{ $t('obsList.area') }}</label>
              <select-box
                class="tw-flex-auto"
                v-model="condition.fields"
                blank-first
                :rows="fields"
                :display-props="['fields', 'fieldsName']"
                binding-prop="fields"
                display-text="fields"
                options-width="220px"
                :disabled="app.pocMode">
              </select-box>
            </div>
            <div class="tw-flex tw-h-1/2">
              <label for="obs-list-initial" class="tw-w-[85px] tw-inline-flex tw-items-center">{{ $t('obsList.initial') }}</label>
              <select-box
                class="tw-flex-auto"
                v-model="condition.partId"
                :rows="initials"
                :display-props="['initial', 'name']"
                binding-prop="initial"
                options-width="220px"
                display-text="initial">
              </select-box>
            </div>
          </div>
          <div class="tw-w-[50%] tw-space-y-1">
            <div class="tw-flex tw-h-1/2">
              <label for="obs-lits-free-word" class="tw-w-[100px] tw-inline-flex tw-items-center">{{ $t('obsList.keyword') }}</label>
              <q-input v-model="condition.freeWord" outlined dense class="tw-flex-auto tw-text-base"></q-input>
            </div>
            <div class="tw-flex tw-h-1/2">
              <label for="obs-lits-language" class="tw-w-[100px] tw-inline-flex tw-items-center">{{ $t('obsList.language') }}</label>
              <div class="tw-flex tw-justify-between tw-flex-auto">
                <select-box
                  class="tw-w-[250px]"
                  v-model="condition.language"
                  :rows="langs"
                  :display-props="['name']"
                  binding-prop="id"
                  display-text="name">
                </select-box>
                <div class="tw-flex tw-items-center">
                  <q-checkbox v-model="condition.chkTL" dense left-label :label="$t('obsList.TLRecog')" />
                </div>
              </div>
            </div>
          </div>
          <div class="tw-w-[20%]">
            <div class="tw-h-1/2 tw-items-center tw-flex tw-space-x-3">
              <button class="btn" @click="clickSearchHandler">{{ $t('obsList.search') }}</button>
              <button class="btn" @click="clickClearHandler">{{ $t('obsList.clear') }}</button>
            </div>
          </div>
        </div>
      </fieldset>
      <fieldset class="tw-justify-center tw-items-center tw-flex tw-border tw-p-3 tw-ml-3">
        <legend class="tw-px-4 tw-text-center">{{ $t('obsList.other') }}</legend>
          <div class="tw-flex-col tw-flex tw-space-y-1 tw-pb-[25px] tw-mr-3">
            <button class="btn add" @click="clickCreateNewHandler">{{ $t('obsList.createNew') }}</button>
          </div>
          <div class="tw-flex-col tw-flex tw-space-y-1 tw-pb-[25px]">
            <button class="btn" @click="clickOutputWordHandler">{{ $t('obsList.word') }}</button>
        </div>
      </fieldset>
    </div>
  </teleport>
  <div class="q-pa-md">
    <s-table :columns="columns" :rows="obsDisp">
      <template #col1="{ context }">{{ context.lNum }}</template>
      <template #col2="{ context }">{{ context.lTitleText }}</template>
      <template #col3="{ context }">{{ context.tlApprovalStatus }}</template>
      <template #col4="{ context }">
          <q-icon name="check_circle" color="light-green-14" v-if="context.approvalStatePr" size="20px"></q-icon>
      </template>
      <template #col5="{ context }">{{ context.editor }}</template>
      <template #col6="{ context }">{{ context.valStatus }}</template>
      <template #col8="{ context }">
        <div class="tw-flex tw-justify-center" v-if="context.transStateReq" >
          {{ context.transDeadline }}
        </div>
      </template>
      <template #col9="{ context }"><span v-if="context.lastUpdate">{{ dayjs(context.lastUpdate).format('YYYY/MM/DD HH:mm') }}</span></template>
      <template #col10="{ context }">
          <q-icon name="check_circle" color="light-green-14" v-if="context.finalVer" size="20px"></q-icon>
      </template>
      <template #col11="{ context }">
          <q-icon name="check_circle" color="light-green-14" v-if="context.packageExclude" size="20px"></q-icon>
      </template>
    </s-table>
  </div>
</template>

<script setup>
// ======= Libraries =======
import {
  ref, reactive, onMounted, computed,
} from 'vue';
import { useRouter } from 'vue-router';
import { useI18n } from 'vue-i18n';
import dayjs from 'dayjs';

// ======= Components =======
import STable from 'components/common/STable.vue';
import SelectBox from 'components/common/SelectBox.vue';

// ======= Services =======
import obsService from 'services/obs.service';
import fieldService from 'services/field.service';
import reviewMemberService from 'services/review-member.service';

// ======= Utilities =======
import dialog from 'utilities/dialog';
import { useAppStore } from 'stores/app-store';
import { langs, changeLanguage } from 'utilities/language';
import {
  MSG, MODAL, LANGUAGE, CONFIRM_DIALOG,
} from 'helpers/constant';
import { OBS_INPUT_TAG } from 'helpers/enum';
import { useLoadingStore } from 'stores/loading-store';
import { useAuthStore } from 'stores/auth-store';

// 1) ======= INITIALIZATION ========
const { t } = useI18n();
const router = useRouter();
const app = useAppStore();
const auth = useAuthStore();
const loading = useLoadingStore();

// 2) ======= VARIABLE REF ========
// 検索条件
const condition = reactive({
  fields: null,
  freeWord: null,
  partId: null,
  language: app.language.id,
  chkTL: false,
});

const rows = ref([]);
const fields = ref([]);
const initials = ref([]);

// 返却されているプロパティlTitleを変換し画面表示する
const obsDisp = computed(() => rows.value.map((x) => {
  // １つdivタグを作成する
  const element = document.createElement('div');
  element.innerHTML = x.lTitle;
  let lTitleText = element.textContent || element.innerText;
  if ((!lTitleText || lTitleText.length === 0) && app.language.id === LANGUAGE.ENGLISH_ID) {
    lTitleText = 'making';
  }
  // lTitleTextが含まれたxのクローン値を返す
  return { ...x, lTitleText };
}));

// 3) ======= METHOD/FUNCTION ========
// 8.3
// 新規追加ボタン押下イベント
const clickCreateNewHandler = async () => {
  const result = await dialog.showConfirm(MODAL.TITLE_INFO, MSG.NEW_RECORD);
  if (result === CONFIRM_DIALOG.NO) return;
  const newData = {
    plantName: app.plant.plantName,
    dmDivision: app.plant.dmDivision,
    partId: auth.initial,
    fields: app.peerReviewMaster.EvalArea,
    language: auth.user.language,
  };
  const obsNew = await obsService.addNew(newData);
  if (obsNew) {
    router.push({ name: 'obs-input', params: { num: obsNew.num }, query: { tag: OBS_INPUT_TAG.ADD } });
  }
};

// 8.4
// 検索ボタン押下イベント
const clickSearchHandler = async () => {
  rows.value = await obsService.getExtendList(condition);
  // システム言語をセレクトボックスにより変える
  changeLanguage({ id: condition.language === LANGUAGE.ORIGINAL_ID ? auth.lang : condition.language });
};

// 8.5
// クリアボタン押下イベント
const clickClearHandler = async () => {
  // 検索条件削除
  condition.fields = null;
  condition.freeWord = null;
  condition.partId = null;
  condition.language = auth.lang;
  condition.chkTL = false;
  rows.value = await obsService.getExtendClearList();
  // システム言語をデフォルトに変える
  changeLanguage({ id: condition.language });
};

// 8.6
// Word出力ボタン押下イベント
const clickOutputWordHandler = () => {
  router.push({ name: 'obs-word-output', query: { from: 'frm_OBS_List', sheet: 'OBS' } });
};

// 8.7, 8.8
// OBS1件の編集ボタン押下イベント
const clickEditHandler = async (row) => {
  if (row.plantName === null || row.lNum === null) return;
  if (await obsService.countByNum(row.plantName, row.lNum) === 0) {
    await dialog.showMessage(MODAL.TITLE_INFO, MSG.NO_DATA);
    return;
  }
  router.push({ name: 'obs-input', params: { num: row.num }, query: { tag: OBS_INPUT_TAG.EDIT } });
};

// ======== COMPUTED ==========
const columns = computed(() => [
  {
    key: 'col1', label: t('obsList.number'), onClick: clickEditHandler, colClass: 'tw-w-[230px]',
  },
  {
    key: 'col2', label: t('obsList.title'), onClick: clickEditHandler,
  },
  { key: 'col3', label: t('obsList.asStatus'), colClass: 'tw-text-center tw-w-[230px]' },
  { key: 'col4', label: t('obsList.checkReq'), colClass: 'tw-w-[120px] tw-text-center' },
  { key: 'col5', label: t('obsList.editor'), colClass: 'tw-w-[120px] tw-text-center' },
  { key: 'col6', label: t('obsList.valStatus'), colClass: 'tw-text-center tw-w-[120px]' },
  { key: 'col8', label: t('obsList.transReq'), colClass: 'tw-w-[150px] tw-text-center' },
  { key: 'col9', label: t('obsList.update'), colClass: 'tw-text-center tw-w-[180px]' },
  { key: 'col10', label: t('obsList.finalized'), colClass: 'tw-text-center tw-w-[120px]' },
  { key: 'col11', label: t('obsList.notPKG'), colClass: 'tw-text-center tw-w-[120px]' },
]);

// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(async () => {
  // 8.1
  try {
    loading.showMulti();
    const result = await Promise.all([
      fieldService.list(),
      reviewMemberService.list(),
      clickSearchHandler(),
    ]);
    [fields.value, initials.value] = result;
  } finally {
    loading.hideMulti();
  }
});
</script>

<style scoped>
.btn {
  min-height: 40px;
  line-height: 1rem;
  min-width: 100px;
}
legend{
  font-weight: 500;
}
</style>
