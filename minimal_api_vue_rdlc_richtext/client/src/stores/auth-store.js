import { defineStore } from 'pinia';
import { useSessionStorage } from '@vueuse/core';
import { LANGUAGE } from 'helpers/constant';

export const useAuthStore = defineStore('auth', {
  state: () => (
    // ログインユーザ情報
    { user: useSessionStorage('user_initial', {}) }

  ),
  getters: {
    // ユーザがログイン済みか確認
    isLoggedIn: (state) => state.user?.id,
    // ユーザのトークン取得
    token: (state) => state.user?.token,
    // ユーザの言語取得
    lang: (state) => (state.user?.language === LANGUAGE.ENGLISH_FIRST_LETTER ? LANGUAGE.ENGLISH_ID : LANGUAGE.ORIGINAL_ID),
    // 初期ユーザ情報の取得
    initial: (state) => state.user?.initial,
  },
  actions: {
    // ログインユーザ情報の設定
    signIn(userData) {
      this.user = userData;
    },
    // ログインユーザ情報の取得
    signOut() {
      this.user = {};
    },
    // 編集可否判定
    canEdit(partId, editor) {
      return (editor === this.initial) || (!editor && partId === this.initial);
    },
  },
});
