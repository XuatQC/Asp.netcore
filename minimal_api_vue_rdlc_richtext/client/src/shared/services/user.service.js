import BaseService from 'services/base.service';

class UserService extends BaseService {
  // plantNameとinitialでユーザ名を取得するAPI
  async getUserName(plantName, initial) {
    const condition = { plantName, initial };
    const { payload } = await this.dao.getUserName(condition);
    return payload;
  }
}
export default new UserService('user');
