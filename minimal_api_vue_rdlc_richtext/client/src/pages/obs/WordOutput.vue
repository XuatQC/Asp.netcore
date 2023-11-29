<template>
  <teleport to="#header">
    <div class="sub-header tw-space-y-2">
      <div class="tw-text-center tw-font-semibold tw-text-xl tw-mb-3">
        {{ from === 'ADMenu' ? $t('wordOutput.fromADMenu.title') : $t('wordOutput.title') }}
      </div>
      <div class="tw-space-x-8">
        <button class="btn" @click="clickSortHandler">{{ $t('wordOutput.sort') }}</button>
        <button class="!tw-px-1 btn" @click="clickOutputHandler">{{ $t('wordOutput.create') }}</button>
        <button class="btn tw-leading-4" @click="clickCheckAllHandler">{{ $t('wordOutput.checkAll.on') }}</button>
        <button class="btn tw-leading-4" @click="clickUnCheckAllHandler">{{ $t('wordOutput.checkAll.off') }}</button>
        <fieldset>
          <legend class="tw-px-3">{{ $t('wordOutput.textFormat.label') }}</legend>
          <div class="tw-space-x-6 tw-text-left">
            <div class="tw-inline-block tw-space-x-2">
              <q-radio v-model="charFormat" :val="true" id="char-format-yes" :label="$t('wordOutput.textFormat.rich')" size="xs"/>
            </div>
            <div class="tw-inline-block tw-space-x-2">
              <q-radio v-model="charFormat" :val="false" id="char-format-no" :label="$t('wordOutput.textFormat.clear')" size="xs"/>
            </div>
          </div>
        </fieldset>
        <fieldset :disabled="tag === TAG.SOI">
          <legend class="tw-px-3">{{ $t('wordOutput.photo.label') }}</legend>
          <div class="tw-space-x-6 tw-text-left">
            <div class="tw-inline-block tw-space-x-2">
              <q-radio v-model="printImage" :val="true" id="print-image-yes" :label="$t('wordOutput.photo.withPhoto')" size="xs"/>
            </div>
            <div class="tw-inline-block tw-space-x-2">
              <q-radio v-model="printImage" :val="false" id="print-image-no" :label="$t('wordOutput.photo.withoutPhoto')" size="xs"/>
            </div>
          </div>
        </fieldset>
        <div class="tw-inline-block" v-if="from === 'ADMenu'">
          (*1) 出力は、日本語（和文）のみ<br />
          (*2) ｢最終パッケージに含めない｣をチェックしたデータは含まず
        </div>
      </div>
      <div class="tw-space-x-8">
        <span class="tw-text-lg tw-align-bottom">{{ $t('wordOutput.orderTitle') }}</span>
      </div>
    </div>
  </teleport>
  <div class="q-pa-md">
    <s-table :columns="columns" :rows="rows">
      <template #col1="{ context }"><q-checkbox v-model="context.output" dense size="sm"></q-checkbox></template>
      <template #col2="{ context }">{{ context.beforeSort }}</template>
      <template #col3="{ context }"><input class="tw-text-center" type="text"
          v-model.number="context.afterSort" /></template>
      <template #col4="{ context }">{{ context.num }}</template>
      <template #col5="{ context }"><rich-text :model-value="context.title" plain></rich-text></template>
      <template #col6="{ context }">{{ context.fields }}</template>
      <template #col7="{ context }"><q-icon name="check_circle" color="light-green-14" v-if="context.packageExclude" size="20px"></q-icon></template>
    </s-table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import STable from 'components/common/STable.vue';
import { useI18n } from 'vue-i18n';
import { TAG } from 'helpers/enum';
import { MODAL, MSG } from 'helpers/constant';
import dialog from 'utilities/dialog';
import wkWordListService from 'services/wk-word-list.service';
import RichText from 'components/common/RichText.vue';

// 1) ======= INITIALIZATION ========
const router = useRouter();
const { t } = useI18n();

// 2) ======= VARIABLE REF ========
const rows = ref([]);
const charFormat = ref(false);
const printImage = ref(false);
const tag = ref(null);
const from = ref(null);
const columns = ref([
  {
    key: 'col1', label: t('wordOutput.table.target'), headerClass: 'tw-w-[10%]', colClass: 'tw-text-center',
  },
  {
    key: 'col2', label: t('wordOutput.table.before'), colClass: 'tw-text-center', headerClass: 'tw-w-[10%]',
  },
  { key: 'col3', label: t('wordOutput.table.after'), headerClass: 'tw-w-[10%]' },
  { key: 'col4', label: t('wordOutput.table.number'), headerClass: 'tw-w-[20%]' },
  { key: 'col5', label: t('wordOutput.table.title'), headerClass: 'tw-w-[30%]' },
  { key: 'col6', label: t('wordOutput.table.area'), headerClass: 'tw-w-[10%]' },
  {
    key: 'col7', label: t('wordOutput.table.excludePkg'), headerClass: 'tw-w-[10%]', colClass: 'tw-text-center',
  },
]);

// 3) ======= METHOD/FUNCTION ========
// 11.3
// 並び替え
const clickSortHandler = () => {
  rows.value.sort((a, b) => {
    if (!a.afterSort && !b.afterSort) return 0;
    if (a.afterSort && !b.afterSort) return -1;
    if (!a.afterSort && b.afterSort) return 1;
    return a.afterSort - b.afterSort;
  });
  const sortedRow = rows.value;
  for (let index = 0; index < rows.value.length; index++) {
    sortedRow[index].afterSort = index + 1;
  }
  rows.value = sortedRow;
};

// 11.3
// Wordファイルに出力する
const clickOutputHandler = async () => {
  if (rows.value.length === 0) return;
  let message;
  switch (tag.value) {
    case TAG.GFA:
      message = MSG.GFA_WORD_CREATE;
      break;
    case TAG.PFA:
      message = MSG.PFA_WORD_CREATE;
      break;
    case TAG.AFI:
      message = MSG.AFI_WORD_CREATE;
      break;
    case TAG.BP:
      message = MSG.BP_WORD_CREATE;
      break;
    case TAG.OBS:
      message = MSG.OBS_WORD_CREATE;
      break;
    case TAG.PD:
      message = MSG.PD_WORD_CREATE;
      break;
    case TAG.STR:
      message = MSG.STR_WORD_CREATE;
      break;
    case TAG.SOI:
      message = MSG.SOI_WORD_CREATE;
      break;
    default:
      return;
  }
  await dialog.showConfirm(MODAL.TITLE_WARNING, message);
};

// 11.5
// 全チェック
const clickCheckAllHandler = () => {
  for (const row of rows.value) {
    row.output = true;
  }
};

// 11.6
// 全解除
const clickUnCheckAllHandler = () => {
  for (const row of rows.value) {
    row.output = false;
  }
};

// 4) ======= VUEJS LIFECYCLE ========
// コンポーネント初期後
onMounted(async () => {
  tag.value = router.currentRoute.value.query.sheet;
  from.value = router.currentRoute.value.query.from;
  // 11.1 印刷対象一覧のデータ読み込み
  rows.value = await wkWordListService.list(from.value, tag.value);
  for (let index = 0; index < rows.value.length; index++) {
    rows.value[index].output = true;
    rows.value[index].beforeSort = index + 1;
    rows.value[index].afterSort = null;
  }
});
</script>

<style scoped>
fieldset {
  border: 1px solid gray;
  display: inline;
  text-align: center;
  min-width: 250px;
}

.btn {
  min-height: 40px;
  line-height: 1rem;
  min-width: 100px;
  padding: 0;
}
</style>
