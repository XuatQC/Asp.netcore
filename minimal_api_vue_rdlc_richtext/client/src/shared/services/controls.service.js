import BaseService from 'services/base.service';

class ControlsService extends BaseService {
  async getLastSyncDate() {
    const { payload } = await this.dao.getLastSyncDate();
    return payload;
  }
}
export default new ControlsService('controls');
