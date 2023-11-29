import BaseService from 'services/base.service';
import { useAppStore } from 'stores/app-store';
import { useAuthStore } from 'stores/auth-store';

class AuthService extends BaseService {
  // ログイン
  async login(data) {
    try {
      const loginResponse = await this.dao.login(data);
      // エラーになっているフィールドが有無か確認
      if (loginResponse.payload.fieldError === null) {
        const { signIn } = useAuthStore();
        const user = loginResponse.payload;
        signIn(user);
      }
      return loginResponse;
    } catch (ex) {
      return ex;
    }
  }

  // ログアウト
  async logout() {
    const app = useAppStore();
    const auth = useAuthStore();
    app.clearAll();
    auth.signOut();
  }

  // トークンが有効か確認
  async check() {
    try {
      const { signIn } = useAuthStore();
      const { payload } = await this.dao.check();
      if (payload) {
        signIn(payload);
      }
      return true;
    } catch {
      return false;
    }
  }
}
export default new AuthService('auth');
