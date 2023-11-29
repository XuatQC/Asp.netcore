import BaseRepository from 'repositories/base/base.repository';

export default class ObsAttachRepository extends BaseRepository {
  constructor() {
    super('obs-attach');
  }

  // numberでobsAttach一覧を取得する
  list(num) {
    return this.client.get(`/list/${num}`);
  }

  // 写真オブジェクトを新規追加する
  add(formData) {
    return this.client.post('/', formData);
  }
}
