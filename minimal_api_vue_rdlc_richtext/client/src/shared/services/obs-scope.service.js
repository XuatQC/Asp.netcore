import BaseService from 'services/base.service';

class ObsScopeService extends BaseService {
  // numberでobsScope一覧を取得する
  async list(num) {
    const { payload } = await this.dao.list(num);
    return payload;
  }

  // numberでobsScopeを取得する
  async getObsScope(num) {
    const { payload } = await this.dao.getObsScope(num);
    return payload;
  }

  // obsScopeを新規追加する
  async addNew(obsScope) {
    const { payload } = await this.dao.addNew(obsScope);
    return payload;
  }

  // numberでobsScopeを更新する
  async updateByNum(num, obsScope) {
    const { payload } = await this.dao.updateByNum(num, obsScope);
    return payload;
  }

  // idでobsScopeを更新する
  async updateById(obsScope) {
    const { payload } = await this.dao.updateById(obsScope.id, obsScope);
    return payload;
  }
}
export default new ObsScopeService('obs-scope');
