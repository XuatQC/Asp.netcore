<template>
  <teleport to="#header">
    <q-header elevated>
      <q-toolbar>
        <q-img src="/foot_rm.png" fit="contain" width="203px" height="61px" class="tw tw-mr-3" />
        <q-breadcrumbs class="text-black" active-color="primary">
          <template #separator>
            <q-icon size="1.5em" name="chevron_right" color="primary" />
          </template>
          <q-breadcrumbs-el v-for="item in breadcrumbs" class="tw-cursor-pointer" :key="item" :label="item.meta.title" @click="routerMove(item)" />
          <q-breadcrumbs-el :label="current" class="tw-cursor-default" />
        </q-breadcrumbs>
        <q-space />
        <div class="tw-flex tw-items-center">
          <div class="tw-text-black tw-space-x-3 tw-mr-10 tw-flex tw-justify-center tw-items-center">
          <label>{{ $t('theme.label') }}</label>
          <select-box :rows="themeList" :displayProps="['label']" class="tw-w-[140px]" binding-prop="value"
            displayText="label" @change="changeThemeHandler" v-model="selectedTheme"/>
        </div>
        <div v-if="router.currentRoute.value.fullPath == '/main-menu'" class="tw-text-black tw-w-[324px] tw-mr-10" >
          <PlantChange/>
        </div>
      </div>
        <q-btn-dropdown flat no-caps size="1rem" :label="userNameByLang" color="black" dropdown-icon="menu">
          <q-list>
            <q-item clickable v-close-popup :to="{ name: 'main-menu' }">
              <q-item-section>
                <q-item-label>{{ $t('mainHeader.mainMenu') }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item clickable v-close-popup @click="clickChangePasswordHandler">
              <q-item-section>
                <q-item-label>{{ $t('mainHeader.changePassword') }}</q-item-label>
              </q-item-section>
            </q-item>
            <q-item clickable v-close-popup @click="logout">
              <q-item-section>
                <q-item-label>{{ $t('mainHeader.logOut') }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </q-btn-dropdown>
      </q-toolbar>
    </q-header>
  </teleport>
</template>

<script setup>
import { computed, onMounted, ref } from 'vue';
import { storeToRefs } from 'pinia';
import { useRouter } from 'vue-router';
import { useAuthStore } from 'stores/auth-store';
import AuthService from 'services/auth.service';
import PlantChange from 'components/PlantChange.vue';
import SelectBox from 'components/common/SelectBox.vue';
import dialog from 'utilities/dialog';
import ChangePassword from 'components/ChangePassword.vue';
import { useI18n } from 'vue-i18n';
import { useAppStore } from 'stores/app-store';
import { LANGUAGE } from 'helpers/constant';

// 1) ======= INITIALIZATION ========
const auth = useAuthStore();
const { user } = storeToRefs(auth);
const router = useRouter();
const { t, locale } = useI18n();
const app = useAppStore();

// 2) ======= VARIABLE REF ========
const selectedTheme = ref('');

const themeList = computed(() => [
  {
    value: '', label: '',
  },
  {
    value: 'theme1', label: t('theme.purple'),
  },
  {
    value: 'theme2', label: t('theme.blueGreen'),
  },
  {
    value: 'theme3', label: t('theme.pink'),
  },
  {
    value: 'theme4', label: t('theme.orange'),
  },
]);

const breadcrumbs = computed(() => {
  const flows = router.currentRoute.value.meta.breadcrumbs;
  const result = [];
  for (const flow of flows) {
    result.push(router.getRoutes().find((x) => x.name === flow));
  }
  return result;
});

const userNameByLang = computed(() => (locale.value === LANGUAGE.ENGLISH_CODE ? user.value?.romaName : user.value?.name));

// 現在ページのパンくずリスト
const current = computed(() => {
  let result = '';
  result = router.currentRoute.value.meta.title;
  // プレビューの場合にパンくずリストにバージョンを追加する
  if (router.currentRoute.value.name === 'obs-preview') {
    result += `(Ver.${router.currentRoute.value.query.ver})`;
  }
  return result;
});

// 3) ======= METHOD/FUNCTION ========
// ログアウトしログイン画面に遷移する
const logout = () => {
  AuthService.logout();
  router.push({ name: 'login' });
};

// パンくずリストで押下時にルーターを変える
const routerMove = (item) => {
  if (router.currentRoute.value.name === 'obs-preview' && item.name === 'obs-input') {
    router.back();
  } else router.push({ name: item.name });
};

// テーマ変更処理
const changeThemeHandler = (value) => {
  document.body.classList.remove('theme1', 'theme2', 'theme3', 'theme4');
  if (value !== '') document.body.classList.add(value);
  app.theme.className = value;
};

// パスワード変更画面を開く
const clickChangePasswordHandler = () => {
  dialog.showContent('Field Notes - Password Change', ChangePassword, {
    maxWidth: '45vw',
  });
};
onMounted(() => {
  document.body.classList.remove('theme1', 'theme2', 'theme3', 'theme4');
  if (app.theme.className !== '') document.body.classList.add(app.theme.className);
  selectedTheme.value = app.theme.className;
});
</script>

<style scoped>
header {
  background-color: #edf4f0;
  position: sticky;
  top: 0;
}
</style>
