import BaseRepository from 'repositories/base/base.repository';

export default class WktTranslateRepository extends BaseRepository {
  constructor() {
    super('wkt-trans-history');
  }

  // tmp translate historyの一覧取得API
  list(params) {
    return this.client.get('/list', params);
  }

  // 印刷履歴データ処理API
  processData(params) {
    return this.client.get('/process-data', params);
  }

  // translate historyテーブルの全て一時データを削除するAPI
  delAll() {
    return this.client.delete('');
  }
}
