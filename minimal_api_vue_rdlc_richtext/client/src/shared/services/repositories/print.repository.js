import BaseRepository from 'repositories/base/base.repository';

export default class TranslateRepository extends BaseRepository {
  constructor() {
    super('print');
  }

  // 処理続行かどうか承認対象の翻訳一覧のデータ取得する
  getOutPutWord(transData) {
    return this.client.get('/output-word/pre', transData);
  }

  // 処理続行と承認されている一覧のデータを処理する
  getOutputWordProcessing(transData) {
    return this.client.post('/output-word/processing', transData);
  }
}
