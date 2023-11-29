import BaseRepository from 'repositories/base/base.repository';

export default class ObsScopeRepository extends BaseRepository {
  constructor() {
    super('obs-scope');
  }

  // numberでobsScope一覧を取得する
  list(num) {
    return this.client.get(`/list/${num}`);
  }

  // numberでobsScopeを取得する
  getObsScope(num) {
    return this.client.get(`/${num}`);
  }

  // numberでobsScopeを更新する
  updateByNum(num, obsScope) {
    return this.client.put(`/${num}`, obsScope);
  }

  // idでobsScopeを更新する
  updateById(obsScope) {
    return this.client.put(`/${obsScope.id}`, obsScope);
  }

  // obsScopeを新規追加する
  addNew(obsScope) {
    return this.client.post('/', obsScope);
  }
}
