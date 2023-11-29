import BaseService from 'services/base.service';

class WktTranslateHisService extends BaseService {
  // Translate history一時テーブルの一覧を取得する
  async list(param) {
    const { payload } = await this.dao.list(param);
    return payload;
  }

  // 印刷履歴データ処理API
  async processData(param) {
    const { payload } = await this.dao.processData(param);
    return payload;
  }

  // 全データ削除
  async delAll() {
    const { payload } = await this.dao.delAll();
    return payload;
  }
}
export default new WktTranslateHisService('wkt-trans-history');
