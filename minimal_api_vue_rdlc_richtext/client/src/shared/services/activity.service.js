import BaseService from 'services/base.service';
import { useAppStore } from 'stores/app-store';

class ActivityService extends BaseService {
  // plantNameで一覧を取得する
  async list() {
    const { plantName } = useAppStore();
    const { payload } = await this.dao.list(plantName);
    return payload;
  }
}
export default new ActivityService('activity');
