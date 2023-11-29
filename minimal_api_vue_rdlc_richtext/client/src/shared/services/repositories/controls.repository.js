import BaseRepository from 'repositories/base/base.repository';

export default class ControlsRepository extends BaseRepository {
  constructor() {
    super('controls');
  }

  // 最終操作日時を取得する
  getLastSyncDate() {
    return this.client.get('/last-sync-date');
  }
}
