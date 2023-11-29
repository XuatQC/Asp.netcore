import BaseService from 'services/base.service';

class SafetyCultureService extends BaseService {
  // 安全文化一覧取得
  async getList() {
    const { payload } = await this.dao.getList();
    return payload;
  }
}

export default new SafetyCultureService('safety-culture');
