import BaseService from 'services/base.service';

class DirectoryService extends BaseService {
  // サーバーのローカルパスを取得する
  async getDirectory() {
    const result = await this.dao.getDirectory();
    return result;
  }
}
export default new DirectoryService('directory');
