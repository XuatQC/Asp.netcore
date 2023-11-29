import { useAuthStore } from 'stores/auth-store';

class EditorService {
  // 編集者一覧の値を一旦固定する
  async list() {
    const auth = useAuthStore();
    return [auth.user];
  }
}
export default new EditorService();
