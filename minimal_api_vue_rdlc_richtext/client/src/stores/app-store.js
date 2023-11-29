import { defineStore } from 'pinia';
import { useSessionStorage } from '@vueuse/core';
import { DIVISION } from 'helpers/enum';
import { LANGUAGE } from 'helpers/constant';

export const useAppStore = defineStore('app', {
  state: () => (
    {
      plant: useSessionStorage('current_plant', {}),
      connectionPlace: useSessionStorage('connection_place', {}),
      folderStore: useSessionStorage('folder_store', {}),
      peerReviewMaster: useSessionStorage('peer_review_master', {}),
      language: useSessionStorage('language', {}),
      theme: useSessionStorage('theme', {}),
    }
  ),
  getters: {
    isDisplayEnglish: (state) => state.language.value === LANGUAGE.ENGLISH_CODE,
    pocMode: (state) => state.plant?.offerDivision === DIVISION.POC,
    plantName: (state) => state.plant?.plantName,
  },
  actions: {
    // 現在プラントの値をセットする
    setPlant(data) {
      this.plant = data;
    },

    // 現在connection_placeの値をセットする
    setConnectionPlace(data) {
      this.connectionPlace = data;
    },

    // 現在PeerReviewMasterの値をセットする
    setPeerReviewMaster(data) {
      this.peerReviewMaster = data;
    },

    // 現在folderStoreの値をセットする
    setFolderStore(data) {
      this.folderStore = data;
    },

    // 言語の値をセットする
    setLanguage(lang) {
      this.language = lang;
    },

    setTheme(theme) {
      this.theme.className = theme;
    },

    // storeとstorageの値を削除する
    clearAll() {
      this.plant = {};
      this.connectionPlace = {};
      this.folderStore = {};
      this.peerReviewMaster = {};
      this.language = {};
    },
  },
});
