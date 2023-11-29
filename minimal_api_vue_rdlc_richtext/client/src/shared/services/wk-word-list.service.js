import BaseService from 'services/base.service';
import { useAppStore } from 'stores/app-store';
import { useAuthStore } from 'stores/auth-store';

class WkWordListRepository extends BaseService {
  // WKWord一覧API
  async list(from, sheet) {
    const { plantName } = useAppStore();
    const { lang } = useAuthStore();
    const condition = {
      plantName,
      language: lang,
      sheetName: sheet,
      callFrom: from,
    };
    const { payload } = await this.dao.list(condition);
    return payload;
  }

  // 一覧並び替えAPI
  async sort() {
    const { payload } = await this.dao.sort();
    return payload;
  }
}

export default new WkWordListRepository('wk-word-list');
