import BaseRepository from 'repositories/base/base.repository';

export default class PoCRepository extends BaseRepository {
  constructor() {
    super('po-and-c');
  }

  // PO&C一覧取得
  getList() {
    return this.client.get('/list');
  }
}
