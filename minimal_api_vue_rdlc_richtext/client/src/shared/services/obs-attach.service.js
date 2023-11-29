import BaseService from 'services/base.service';

class ObsAttachService extends BaseService {
  // numberでobsAttach一覧を取得する
  async list(num) {
    const { payload } = await this.dao.list(num);
    return payload;
  }

  // 写真オブジェクトを新規追加する
  async add(formData) {
    const { payload } = await this.dao.add(formData);
    return payload;
  }
}
export default new ObsAttachService('obs-attach');
