import BaseService from 'services/base.service';

class PrintService extends BaseService {
  // 翻訳かどうか承認対象の翻訳一覧のデータを取得する
  async getOutputWord(transData) {
    const result = await this.dao.getOutPutWord(transData);
    return result;
  }

  // 翻訳と承認されている一覧のデータを処理する
  async getOutputWordProcessing(transData) {
    const result = await this.dao.getOutputWordProcessing(transData);
    return result.payload;
  }
}

export default new PrintService('print');
