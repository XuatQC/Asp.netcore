import BaseService from 'services/base.service';
import { useAppStore } from 'stores/app-store';

class FieldService extends BaseService {
  // plantNameでプラントの値を取得する
  async list() {
    const { plantName } = useAppStore();
    const { payload } = await this.dao.list(plantName);
    return payload;
  }

  // 11件取得
  async get11() {
    const { plant } = useAppStore();
    const condition = {
      plantName: plant.plantName,
      dmDiv: plant.dmDivision,
    };
    const { payload } = await this.dao.get11(condition);
    return payload;
  }
}
export default new FieldService('field');
