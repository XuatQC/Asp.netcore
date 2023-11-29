import BaseRepository from 'repositories/base/base.repository';

export default class FieldRepository extends BaseRepository {
  constructor() {
    super('field');
  }

  // field一覧の取得
  list(plantName) {
    return this.client.get('/list', { plantName });
  }

  // 11件取得
  get11(params) {
    return this.client.get('/11-record', params);
  }
}
