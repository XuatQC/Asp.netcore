import BaseService from 'services/base.service';

class TranslateService extends BaseService {
  // OfferNumでTranslate一覧を取得する
  async list(param) {
    const { payload } = await this.dao.list(param);
    return payload;
  }

  // OfferNumでTranslateを取得する
  async getTranslate(param) {
    const { payload } = await this.dao.getTranslate(param);
    return payload;
  }

  // numでoffer numberを取得する
  async getOfferNumber(plantName, lNum) {
    const condition = { plantName, lNum };
    const { payload } = await this.dao.getOfferNumber(condition);
    return payload;
  }

  // 翻訳完了確認
  async checkTransCompConfirm(num) {
    const { payload } = await this.dao.checkTransCompConfirm(num);
    return payload;
  }

  // 翻訳データの作成
  async createTranslateData(tableName, plantName, translateData) {
    const result = this.dao.createTranslateData(tableName, plantName, translateData);
    return result;
  }

  // WKTTranslateテーブルに翻訳データが保存できるか確認
  async loadTranslateDataToWkt(plantName) {
    const result = this.dao.loadTranslateDataToWkt(plantName);
    return result;
  }
}
export default new TranslateService('translate');
