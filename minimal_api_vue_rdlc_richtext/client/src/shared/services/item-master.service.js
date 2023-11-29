import BaseService from 'services/base.service';

class ItemMasterService extends BaseService {
  // plantNameでプラントの値を取得する
  async list() {
    const condition = {
      item: '',
      code: null,
    };
    const { payload } = await this.dao.list(condition);
    return payload;
  }
}
export default new ItemMasterService('item-master');
