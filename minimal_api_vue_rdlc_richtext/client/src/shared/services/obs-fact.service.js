import BaseService from 'services/base.service';

class ObsFactService extends BaseService {
  // numberでobsFact一覧を取得する
  async list(num) {
    const { payload } = await this.dao.list(num);
    return payload;
  }

  // numberでobsFactを取得する
  async getObsFact(num) {
    const { payload } = await this.dao.getObsFact(num);
    return payload;
  }

  // obsFactを新規追加する
  async addNew(obsFact) {
    const { payload } = await this.dao.addNew(obsFact);
    return payload;
  }

  // numberでobsFactを更新する
  async updateByNum(num, obsFact) {
    const { payload } = await this.dao.updateByNum(num, obsFact);
    return payload;
  }

  // idでobsFactを更新する
  async updateById(obsFact) {
    const { payload } = await this.dao.updateById(obsFact.id, obsFact);
    return payload;
  }
}
export default new ObsFactService('obs-fact');
