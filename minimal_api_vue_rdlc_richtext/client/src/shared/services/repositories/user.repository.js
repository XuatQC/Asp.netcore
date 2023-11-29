import BaseRepository from 'repositories/base/base.repository';

export default class UserRepository extends BaseRepository {
  constructor() {
    super('user');
  }

  // plantNameとinitialによりユーザ名を取得するAPI
  getUserName(condition) {
    return this.client.get('/get-user-name', condition);
  }
}
