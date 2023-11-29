import { Notify } from 'quasar';

class ToastUtil {
  success(message) {
    Notify.create({
      type: 'positive',
      message,
      position: 'bottom-right',
    });
  }

  warning(message) {
    Notify.create({
      type: 'warning',
      message,
      position: 'bottom-right',
    });
  }

  error(message) {
    Notify.create({
      type: 'negative',
      message,
      color: 'red',
      position: 'bottom-right',
    });
  }

  exception() {
    this.error('サーバとの通信時にエラーが発生しました');
  }
}

export default new ToastUtil();
