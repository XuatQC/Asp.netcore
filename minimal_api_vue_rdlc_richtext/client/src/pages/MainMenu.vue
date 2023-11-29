<template>
  <div class="tw-px-4 container">
    <div class="header">
      <h4 class="tw-pl-[32px] tw-text-4xl tw-w-full tw-text-center tw-font-bold">Field Notes Database
      </h4>
      <div class="tw-flex tw-ml-auto tw-absolute tw-right-0">
        <div class="tw-whitespace-nowrap">
          試験環境({{ connectionPlace}})
        </div>
      </div>
    </div>

    <div class="tw-grid tw-grid-cols-5 tw-pt-2">
      <div v-for="menuItem in menuList" :key="menuItem.name" class="column tw-border-[#9f9f9f] tw-rounded-[5px] tw-overflow-hidden" :class="{'tw-border': !menuItem.placeholder}">
        <div class="tw-bg-[#04b2d9] tw-text-white tw-h-[56px] tw-flex tw-items-center tw-justify-center" v-if="menuItem.name != ''"
        :class="{'tw-invisible': menuItem.placeholder}">
          <div class="tw-text-lg tw-text-center tw-whitespace-pre-line tw-font-extrabold">
            {{ menuItem.name }}
          </div>
        </div>
        <div class="tw-m-2" v-for="menuEvent in menuItem.items" :key="menuEvent.label">
          <button class="!tw-h-[60px] btn  tw-whitespace-pre-line tw-bg-[#0487d9]"
          :class="{ disabled: menuEvent.disabled,'tw-invisible':menuEvent.placeholder, 'close':menuEvent.isClose}" @click="menuEvent.event">
            {{ te(`mainMenu.${menuEvent.name}`) ? $t(`mainMenu.${menuEvent.name}`) : menuEvent.name }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import dialog from 'utilities/dialog';
import { MSG, MODAL, LANGUAGE } from 'helpers/constant';
import {
  onMounted, ref,
} from 'vue';
import { useRouter } from 'vue-router';
import { useI18n } from 'vue-i18n';
import SharedFolderService from 'services/shared-folder.service';
import { useAppStore } from 'stores/app-store';

// 1) ======= INITIALIZATION =========
const { t, te } = useI18n();
const appStore = useAppStore();
const currentPlant = appStore.plant;
const { language } = appStore;

const router = useRouter();

//  ======= Button handler =========

// ボタン押下時に発生したエラーをキャッチする
const errorClickButtonHandler = async () => {
  const isError = false;
  if (isError) {
    await dialog.showMessage(MODAL.TITLE_WARNING, MSG.ERROR_IN_PROCESS);
  }
};

// anaAFIボタン押下イベント
const clickAnaAFIHandler = async () => {
  await errorClickButtonHandler();
};

// AnaGFAボタン押下イベント
const clickAnaGFAHandler = async () => {
  await errorClickButtonHandler();
};

// AnaOBSボタン押下イベント
const clickAnaOBSHandler = async () => {
  await errorClickButtonHandler();
};

// AnaWCボタン押下イベント
const clickAnaWCHandler = async () => {
  await errorClickButtonHandler();
};

// OBSConclusionボタン押下イベント
const clickOBSConclusionHandler = async () => {
  await errorClickButtonHandler();
};

// WCボタン押下イベント
const clickWcHandler = async () => {
  await errorClickButtonHandler();
};

// OBSボタン押下イベント
const clickOBSHandler = async () => {
  router.push({ name: 'obs-list' });
};

// GFAボタン押下イベント
const clickGFAHandler = async () => {
  await errorClickButtonHandler();
};

// PFAボタン押下イベント
const clickPFAHandler = async () => {
  await errorClickButtonHandler();
};

// AFIボタン押下イベント
const clickAFIHandler = async () => {
  await errorClickButtonHandler();
};

// PDボタン押下イベント
const clickPDHandler = async () => {
  await errorClickButtonHandler();
};

// STRボタン押下イベント
const clickSTRHandler = async () => {
  await errorClickButtonHandler();
};

// BPボタン押下イベント
const clickBPHandler = async () => {
  await errorClickButtonHandler();
};

// PastDataSearchボタン押下イベント
const clickPastDataSearchHandler = async () => {
  await errorClickButtonHandler();
};

// TLCommentボタン押下イベント
const clickTLCommentHandler = async () => {
  await errorClickButtonHandler();
};

// AdMenuボタン押下イベント
const clickAdMenuHandler = async () => {
  await errorClickButtonHandler();
};

// TLOutLineボタン押下イベント
const clickTLOutLineHandler = async () => {
  await errorClickButtonHandler();
};

// 2) ======= VARIABLE REF ========
const isEnabledChangePR = ref(true);
const connectionPlace = ref('');
const menuList = ref([
  {
    name: t('mainMenu.lbl_dataEntry'),
    items: [
      {
        name: 'cmd_WC',
        disabled: false,
        event: clickWcHandler,
      },
      {
        name: 'cmd_OBS',
        disabled: false,
        event: clickOBSHandler,
      },
    ],
  },
  {
    name: t('mainMenu.lbl_analysis'),
    items: [
      {
        name: 'cmd_ANA_WC',
        disabled: false,
        event: clickAnaWCHandler,
      },
      {
        name: 'cmd_ANA_OBS',
        disabled: false,
        event: clickAnaOBSHandler,
      },
      {
        disabled: false,
        name: 'cmd_OBS_Conclusion',
        event: clickOBSConclusionHandler,
      },
      {
        name: 'cmd_ANA_GFA',
        disabled: false,
        event: clickAnaGFAHandler,
      },
      {
        name: 'cmd_ANA_AFI',
        disabled: false,
        event: clickAnaAFIHandler,
      },
      {
        name: 'cmd_SOI_Analysis',
        disabled: false,
        event: () => { },
      },
    ],
  },
  {
    name: t('mainMenu.lbl_afi'),
    items: [
      {
        name: 'cmd_GFA',
        disabled: false,
        event: clickGFAHandler,
      },
      {
        name: 'cmd_PFA',
        disabled: false,
        event: clickPFAHandler,
      },
      {
        name: 'cmd_AFI',
        disabled: false,
        event: clickAFIHandler,
      },
      {
        name: 'cmd_PD',
        disabled: false,
        label: t('mainMenu.cmd_PD'),
        event: clickPDHandler,
      },
      {
        name: 'cmd_STR',
        disabled: false,
        label: t('mainMenu.cmd_STR'),
        event: clickSTRHandler,
      },
      {
        label: t('mainMenu.cmd_BP'),
        name: 'cmd_BP',
        disabled: false,
        event: clickBPHandler,
      },
      {
        disabled: false,
        name: 'cmd_SOI_Entry',
        event: () => { },
      },
    ],
  },
  {
    name: t('mainMenu.lbl_reference'),
    items: [
      {
        name: '美浜ピアレビュー',
        disabled: false,
        event: () => {
        },
      },
      {
        name: 'Deltaのない観察結果',
        disabled: false,
        event: () => { },
      },
      {
        name: '事前入手資料',
        disabled: false,
        event: () => { },
      },
      {
        name: 'レビューワー作業用',
        disabled: false,
        event: () => { },
      },
      {
        name: '海外レビューワー用資料',
        disabled: false,
        event: () => { },
      },
    ],
  },
  {
    name: t('mainMenu.other'),
    items: [
      {
        name: 'cmd_TL_Comment',
        disabled: false,
        event: clickTLCommentHandler,
      },
      {
        name: 'TL Outline',
        disabled: false,
        event: clickTLOutLineHandler,
      },
      {
        name: 'cmd_ADMenu',
        disabled: false,
        event: clickAdMenuHandler,
        alwayEnabled: true,
      },
      {
        name: 'cmd_Translate',
        disabled: false,
        event: () => { },
      },
      {
        name: 'cmd_PastData_Search',
        disabled: false,
        event: clickPastDataSearchHandler,
        alwayEnabled: true,
      },
    ],
  },
]);

// 3) ======= METHOD/FUNCTION ========

// ボタンに無効な値をセットする
const setButtonsEnabled = (itemMenuKeyNames, isEnabled) => {
  for (const menuItem of menuList.value) {
    for (const menuButton of menuItem.items) {
      const currentButtonItem = menuButton;
      if (itemMenuKeyNames.includes(currentButtonItem.name)) {
        if (currentButtonItem.alwayEnabled) {
          currentButtonItem.disabled = true;
        } else {
          currentButtonItem.disabled = !isEnabled;
        }
      }
    }
  }
};

// メインメニューからロードするイベント
const formLoad = () => {
  const { dmDivision } = currentPlant;

  const currentPlantName = currentPlant.plantName;

  if (currentPlantName === 'ZZZ') {
    const enabledButtonKeyName = ['cmd_WC', 'cmd_OBS', 'cmd_ANA_WC', 'cmd_ANA_OBS', 'cmd_OBS_Conclusion', 'cmd_ANA_GFA', 'cmd_ANA_AFI', 'cmd_SOI_Analysis', 'cmd_GFA', 'cmd_PFA', 'cmd_AFI', 'cmd_PD', 'cmd_STR', 'cmd_BP', 'cmd_SOI_Entry', 'cmd_SharedFolder_1', 'cmd_SharedFolder_2', 'cmd_SharedFolder_3', 'cmd_SharedFolder_4', 'cmd_SharedFolder_5', 'cmd_TL_Comment', 'cmd_ADMenu', 'cmd_Translate', 'cmd_PastData_Search', 'cmd_Exit', 'cmd_ChangePasswd'];
    setButtonsEnabled(enabledButtonKeyName, true);
  } else if (dmDivision === 'F') {
    const disabledButtonKeyNames = ['cmd_GFA', 'cmd_PFA', 'cmd_AFI', 'cmd_PD', 'cmd_STR', 'cmd_BP', 'cmd_ANA_AFI', 'cmd_ANA_GFA', 'cmd_OBS_Conclusion'];
    const enabledButtonKeyNames = ['cmd_SOI_Entry', 'cmd_SOI_Analysis'];
    setButtonsEnabled(disabledButtonKeyNames, false);
    setButtonsEnabled(enabledButtonKeyNames, true);
  } else {
    const disabledButtonKeyNames = ['cmd_SOI_Entry', 'cmd_SOI_Analysis', 'cmd_ANA_AFI', 'cmd_ANA_GFA', 'cmd_OBS_Conclusion'];
    const enabledButtonKeyNames = ['cmd_GFA', 'cmd_PFA', 'cmd_AFI', 'cmd_STR'];
    if (dmDivision === 'M') {
      enabledButtonKeyNames.push('cmd_PD');
      enabledButtonKeyNames.push('cmd_BP');
    } else {
      disabledButtonKeyNames.push('cmd_PD');
      disabledButtonKeyNames.push('cmd_PDs');
    }
    setButtonsEnabled(disabledButtonKeyNames, false);
    setButtonsEnabled(enabledButtonKeyNames, true);
    if (language.code === LANGUAGE.ENGLISH_FIRST_LETTER) {
      isEnabledChangePR.value = false;
    } else {
      isEnabledChangePR.value = true;
    }
  }
};

// sharedFolderのTOPを5件取得するAPI
const getSharedFolderPath = async () => {
  const { payload } = await SharedFolderService.getSharedFolderMenu();
  const topPathFive = payload.slice(0, 5).sort((current, next) => current.menuPposi - next.menuPposi);
  const isEnglish = language.code === LANGUAGE.ENGLISH_FIRST_LETTER;
  for (let index = 0; index < topPathFive.length; index++) {
    if (isEnglish) {
      menuList.value[3].items[index].name = topPathFive[index].dispNameEn;
    } else {
      menuList.value[3].items[index].name = topPathFive[index].dispName;
    }
  }
};

// 4) ======= VUEJS LIFECYCLE ========
// onMounted
onMounted(() => {
  formLoad();
  getSharedFolderPath();
  connectionPlace.value = appStore.connectionPlace.connectionPlaceString;
  // hiding login dialog
  dialog.hide();
});
</script>

<style scoped>
.container {
  padding: 0 28px;
  max-width: 1684px;
  margin-right: auto;
  margin-left:auto;
}

.header {
  @apply tw-flex tw-items-center tw-relative;
}

.logo-img {
  max-width: 20vw;
}

.btn {

  height: 60px;
  font-size: 100%;
  @apply tw-w-full tw-text-center tw-px-4 tw-my-2;
}

.column {
  @apply tw-my-1 tw-ml-1;
}

.header-button {
  width: 10rem;
  margin: 0;
  padding: 0;
  font-size: 16px;
  height: 46px;
  @apply tw-mr-2 tw-flex tw-items-center tw-justify-center;
}
</style>
