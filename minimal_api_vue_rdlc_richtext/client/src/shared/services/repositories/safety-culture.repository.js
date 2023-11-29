import BaseRepository from 'repositories/base/base.repository';

export default class SafetyCultureRepository extends BaseRepository {
  constructor() {
    super('safety-culture');
  }

  // 安全文化一覧取得
  getList() {
    return this.client.get('/list');
  }
}
