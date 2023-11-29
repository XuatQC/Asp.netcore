import BaseRepository from 'repositories/base/base.repository';

export default class WkWordListRepository extends BaseRepository {
  constructor() {
    super('wk-word-list');
  }

  // 印刷一覧取得API
  list(params) {
    return this.client.get('/set-data-to-print', params);
  }

  // データ並び替え一覧取得API
  sort() {
    return this.client.get('/sort-data');
  }
}
