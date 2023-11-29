import BaseRepository from 'repositories/base/base.repository';

export default class ActivityRepository extends BaseRepository {
  constructor() {
    super('activity');
  }

  // plantNameで一覧を取得する
  list(plantName) {
    return this.client.get(`/list/${plantName}`);
  }
}
