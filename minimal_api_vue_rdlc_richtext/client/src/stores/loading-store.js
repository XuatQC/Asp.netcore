import { defineStore } from 'pinia';
import { Loading, QSpinnerPie } from 'quasar';

export const useLoadingStore = defineStore('loading', {
  state: () => (
    {
      // 複数APIを呼び出し中か確認
      isMultiLoading: false,
    }
  ),
  actions: {
    // ローディング表示
    show() {
      Loading.show({
        spinnerSize: 50,
        spinnerColor: 'primary',
        spinner: QSpinnerPie,
      });
    },

    // ローディング非表示
    hide() {
      Loading.hide();
    },

    // 複数APIでローディング表示
    showMulti() {
      this.isMultiLoading = true;
      this.show();
    },

    // 複数APIを呼出し後、ローディング非表示する
    hideMulti() {
      this.isMultiLoading = false;
      this.hide();
    },
  },
});
