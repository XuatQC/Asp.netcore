import BaseRepository from 'repositories/base/base.repository';

export default class ItemMasterpository extends BaseRepository {
  constructor() {
    super('item-master');
  }

  // 項目マスタ一覧を取得する
  list(params) {
    return this.client.get('/list', params);
  }
}
