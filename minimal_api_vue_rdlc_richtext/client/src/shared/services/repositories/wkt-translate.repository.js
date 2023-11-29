import BaseRepository from 'repositories/base/base.repository';

export default class WktTranslateRepository extends BaseRepository {
  constructor() {
    super('wkt-translate');
  }

  // OfferNumで翻訳一覧を取得するAPI
  list(params) {
    return this.client.get('/list', params);
  }

  // OfferNumで翻訳を取得するAPI
  getWktTranslate(params) {
    return this.client.get('/', params);
  }
}
