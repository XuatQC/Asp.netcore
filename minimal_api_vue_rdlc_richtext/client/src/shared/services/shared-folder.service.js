import BaseService from 'services/base.service';

class SharedFolderService extends BaseService {
  // dtoでsharedfolderの値を取得するAPI
  async getSharedFolderMenu() {
    return this.dao.getSharedFolderMenu();
  }

  // dtoでsharedfolder一覧を取得するAPI
  async getSharedFolderList() {
    return this.dao.getSharedFolderList();
  }
}

export default new SharedFolderService('shared-folder');
