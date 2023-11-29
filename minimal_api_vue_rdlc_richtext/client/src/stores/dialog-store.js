import { defineStore } from 'pinia';

export const useDialogStore = defineStore('dialog', {
  state: () => (
    {
      // 表示中のポップアップ一覧
      dialog: [],
    }
  ),
  getters: {
    // 先頭に表示されているポップアップ取得
    last: (state) => (state.dialog.length > 0 ? state.dialog[state.dialog.length - 1] : null),
  },
  actions: {
    // ポップアップを追加表示する
    show(data) {
      this.dialog.push(data);
    },

    // 先頭に表示されているポップアップを非表示する
    hide() {
      this.dialog.pop();
    },
  },
});
