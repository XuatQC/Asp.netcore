import BaseService from 'services/base.service';

class WktTranslateService extends BaseService {
  // OfferNumでWktTranslate一覧を取得する
  async list(param) {
    const { payload } = await this.dao.list(param);
    return payload;
  }

  // OfferNumでWkTranslateを取得する
  async getWktTranslate(param) {
    const { payload } = await this.dao.getWktTranslate(param);
    return payload;
  }
}
export default new WktTranslateService('wkt-translate');
