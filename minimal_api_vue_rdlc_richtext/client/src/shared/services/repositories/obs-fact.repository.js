import BaseRepository from 'repositories/base/base.repository';

export default class ObsFactRepository extends BaseRepository {
  constructor() {
    super('obs-fact');
  }

  // numberでobsFact一覧を取得する
  list(num) {
    return this.client.get(`/list/${num}`);
  }

  // numberでobsFactを取得する
  getObsFact(num) {
    return this.client.get(`/${num}`);
  }

  // numberでobsFactを更新する
  updateByNum(num, obsScope) {
    return this.client.put(`/${num}`, obsScope);
  }

  // idでobsFactを更新する
  updateById(obsScope) {
    return this.client.put(`/${obsScope.id}`, obsScope);
  }

  // obsFactを新規追加する
  addNew(obsScope) {
    return this.client.post('/', obsScope);
  }
}
