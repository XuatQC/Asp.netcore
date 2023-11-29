import BaseRepository from 'repositories/base/base.repository';

export default class OBSRepository extends BaseRepository {
  constructor() {
    super('obs');
  }

  // plantNameで拡張一覧を取得するAPI
  getExtendList(params) {
    return this.client.get('/extend/list', params);
  }

  // OBS拡張一覧取得API
  getOnlyObsExtendList(params) {
    return this.client.get('/extend/only-obs-extend', params);
  }

  // クリアボタンでOBS拡張一覧を検索する
  getExtendClearList(params) {
    return this.client.get('/extend/clear-list', params);
  }

  // IDでOBSを1件取得する
  getByNum(obsNum) {
    return this.client.get(`/${obsNum}`);
  }

  // plantNameとfieldsでOBSの番号一覧を取得する
  getNumByPlantNameAndFields(plantName, fields) {
    return this.client.get(`/get-num/${plantName}/${fields}`);
  }

  // OBS修正API
  update(obs) {
    return this.client.put(`/${obs.num}`, obs);
  }

  // OBS追加API
  addNew(obs) {
    return this.client.post('/add-new', obs);
  }

  // wkt_tableのデータ削除API
  deleteDataWktTable() {
    return this.client.delete('/wkt-tables');
  }

  // OBSの存在チェック
  countByNum(params) {
    return this.client.get('/extend/count', params);
  }

  // OBSを番号で取得する
  getOBSByNumber(num) {
    return this.client.get(`/extend/${num}`);
  }

  // OBS削除
  deleteOBS(params) {
    return this.client.delete('', params);
  }

  // OBS参照一覧取得
  getRevisionList(params) {
    return this.client.get('/list/revision', params);
  }

  // OBSテーブルの最新フラグを更新する
  updateNewestFlag(condition) {
    return this.client.put('/newest-flag', condition);
  }

  // Wordファイルに出力する
  outputWord(obsInfo) {
    return this.client.post('/reports/output-word', obsInfo, {
      responseType: 'blob',
      rawValue: true,
    });
  }

  // OBS入力画面の保存処理
  saveAll(params) {
    return this.client.post('/save-process', params);
  }

  // obsExtendReferのnumberを取得するAPI
  getNumOfObsExtendRefer(params) {
    return this.client.get('/extend-refer/get-num', params);
  }

  // obsExtendRefer一覧取得API
  getObsExtendReferList(params) {
    return this.client.get('/extend-refer/list', params);
  }

  // WARN:delete this
  saveObsWithObsAttach(formData) {
    return this.client.put('/obs-attach', formData);
  }
}
