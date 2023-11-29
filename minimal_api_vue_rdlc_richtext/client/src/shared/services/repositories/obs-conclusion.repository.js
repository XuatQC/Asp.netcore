import BaseRepository from 'repositories/base/base.repository';

export default class ObsConclusionRepository extends BaseRepository {
  constructor() {
    super('obs-conclusion');
  }

  // numberでobsConclusion一覧を取得する
  list(num) {
    return this.client.get(`/list/${num}`);
  }

  // numberでobsConclusionを取得する
  getObsConclusion(num) {
    return this.client.get(`/${num}`);
  }

  // numberでobsConclusionを更新する
  updateByNum(num, obsConclusion) {
    return this.client.put(`/${num}`, obsConclusion);
  }

  // idでobsConclusionを更新する
  updateById(obsConclusion) {
    return this.client.put(`/${obsConclusion.id}`, obsConclusion);
  }

  // obsConclusionを新規追加する
  addNew(obsConclusion) {
    return this.client.post('/', obsConclusion);
  }
}
