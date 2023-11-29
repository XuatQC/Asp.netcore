import BaseService from 'services/base.service';

class PoCService extends BaseService {
  // PO&C一覧取得
  async getList() {
    const { payload } = await this.dao.getList();
    return payload;
  }
}

export default new PoCService('po-and-c');
