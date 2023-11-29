import BaseService from 'services/base.service';

class ObsConclusionService extends BaseService {
  // numberでobsConclusion一覧を取得する
  async list(num) {
    const { payload } = await this.dao.list(num);
    return payload;
  }

  // numberでobsConclusionを取得する
  async getObsConclusion(num) {
    const { payload } = await this.dao.getObsConclusion(num);
    return payload;
  }

  // numberでobsConclusionを更新する
  async updateByNum(num, obsConclusion) {
    const { payload } = await this.dao.updateByNum(num, obsConclusion);
    return payload;
  }

  // idでobsConclusionを更新する
  async updateById(obsConclusion) {
    const { payload } = await this.dao.updateById(obsConclusion.id, obsConclusion);
    return payload;
  }

  // obsConclusionを新規追加する
  async addNew(obsConclusion) {
    const { payload } = await this.dao.addNew(obsConclusion);
    return payload;
  }
}
export default new ObsConclusionService('obs-conclusion');
