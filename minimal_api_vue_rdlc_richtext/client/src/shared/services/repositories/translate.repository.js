import BaseRepository from 'repositories/base/base.repository';

export default class TranslateRepository extends BaseRepository {
  constructor() {
    super('translate');
  }

  // OfferNumで翻訳一覧を取得するAPI
  list(params) {
    return this.client.get('/list', params);
  }

  // OfferNumで翻訳を取得するAPI
  getTranslate(params) {
    return this.client.get('/', params);
  }

  // offer numberの取得
  getOfferNumber(params) {
    return this.client.get('/offer-num', params);
  }

  // 翻訳が完了済みか確認
  checkTransCompConfirm(num) {
    return this.client.get(`/is-trans-comp-confirm/${num}`);
  }

  // 翻訳データの作成
  createTranslateData(tableName, plantName, translateData) {
    return this.client.post(`/${tableName}/${plantName}`, translateData);
  }

  // translateの存在チェックし、あればwkt Translateに入れる
  loadTranslateDataToWkt(plantName) {
    return this.client.get(`/load/${plantName}`);
  }
}
