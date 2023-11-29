import BaseRepository from 'repositories/base/base.repository';

export default class SharedFolder extends BaseRepository {
  constructor() {
    super('shared-folder');
  }

  // Sharedfolderの値をdtoで取得するAPI
  getSharedFolderMenu() {
    return this.client.get('/menu-list');
  }

  // sharedfolder一覧をdtoで取得するAPI
  getSharedFolderList() {
    return this.client.get('/list');
  }
}
