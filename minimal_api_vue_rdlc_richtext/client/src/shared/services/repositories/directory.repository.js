import BaseRepository from 'repositories/base/base.repository';

export default class AuthRepository extends BaseRepository {
  constructor() {
    super('directory');
  }

  getDirectory() {
    return this.client.get('/');
  }
}
