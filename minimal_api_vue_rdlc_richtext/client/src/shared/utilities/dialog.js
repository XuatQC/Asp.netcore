import { useDialogStore } from 'stores/dialog-store';
import { shallowRef } from 'vue';
import { CONFIRM_DIALOG, MODAL } from 'helpers/constant';

class DialogManger {
  // ポップアップ表示
  show = (titleVal, contentVal, configVal) => new Promise((resolve) => {
    // 画面がスクロール中ならボディーにpaddingを追加する
    const isScrollVisible = document.body.scrollHeight > window.innerHeight;
    if (isScrollVisible) {
      document.body.classList.add('!tw-pr-[17px]');
    }
    // ダイアログのタイトル、内容、設定を行う
    const item = {
      title: titleVal,
      content: contentVal,
      config: configVal,
      show: true,
      close: resolve,
    };
    const { show } = useDialogStore();
    show(item);
  });

  // ポップアップをコンポーネントとして表示する
  showContent = (title, content, config) => {
    const component = shallowRef(content);
    config = {
      showFooter: false,
      isComponent: true,
      ...config,
    };
    return this.show(title, component, config);
  };

  // ポップアップを通知として表示する
  showMessage = (title, content, config) => {
    config = {
      showFooter: true,
      buttons: [MODAL.OK],
      isComponent: false,
      isUseI18n: false,
      ...config,
    };
    return this.show(title, content, config);
  };

  // ポップアップを確認として表示する
  showConfirm = (title, content, config) => {
    config = {
      showFooter: true,
      buttons: [CONFIRM_DIALOG.YES, CONFIRM_DIALOG.NO],
      isComponent: false,
      isUseI18n: true,
      ...config,
    };
    return this.show(title, content, config);
  };

  // ポップアップを閉じて結果を返す
  hide = (value) => {
    const { last, hide } = useDialogStore();
    // ポップアップが存在しない場合、処理を中止する
    if (!last) return;
    last.show = false;
    // ポップアップが閉じた後、ボディーのpaddingを削除する
    document.body.classList.remove('!tw-pr-[17px]');
    // promiseを終了し、値を返してポップアップを非表示する
    setTimeout(() => {
      last.close(value);
      hide();
    }, 200);
  };
}

export default new DialogManger();
