import BaseService from 'services/base.service';
import { useAppStore } from 'stores/app-store';
import { useAuthStore } from 'stores/auth-store';

class OBSService extends BaseService {
  // numberでOBSを取得する
  async getOBSByNumber(num) {
    const { payload } = await this.dao.getOBSByNumber(num);
    return payload;
  }

  // OBS削除
  async deleteOBS(obs) {
    const { plantName } = useAppStore();
    const { initial } = useAuthStore();
    const condition = {
      plantName,
      lastUser: initial,
      kinds: obs.kinds,
      fields: obs.fields,
      partId: obs.partId,
      serialNum: obs.serialNum,
      language: obs.language,
      num: obs.num,
    };
    const { payload } = await this.dao.deleteOBS(condition);
    return payload;
  }

  // OBS参照一覧を取得する
  async getRevisionList(obs) {
    const condition = {
      numNoRevisions: obs.numNoRevisions,
      revisions: obs.revisions,
    };
    const { payload } = await this.dao.getRevisionList(condition);
    return payload;
  }

  // plantNameで拡張一覧を取得するAPI
  async getExtendList(condition = {}) {
    const { plantName } = useAppStore();
    condition.plantName = plantName;
    const { payload } = await this.dao.getExtendList(condition);
    return payload;
  }

  // OBS拡張一覧を取得するAPI
  async getOnlyObsExtendList(condition) {
    const { payload } = await this.dao.getOnlyObsExtendList(condition);
    return payload;
  }

  // クリアボタンでOBS拡張一覧を検索する
  async getExtendClearList() {
    const { plantName } = useAppStore();
    const { lang } = useAuthStore();
    const condition = { plantName, language: lang };
    const { payload } = await this.dao.getExtendClearList(condition);
    return payload;
  }

  // obsNumでOBSを1件取得する
  async getByNum(obsNum) {
    const obsResult = await this.dao.getByNum(obsNum);
    return obsResult.payload;
  }

  // plantNameとfieldsでOBSの番号一覧を取得する
  async getNumByPlantNameAndFields(plantName, fields) {
    const numList = await this.dao.getNumByPlantNameAndFields(plantName, fields);
    const { payload } = numList;
    if (payload !== null) {
      return payload;
    }
    return [];
  }

  // OBS追加API
  async addNew(obs) {
    const { payload } = await this.dao.addNew(obs);
    return payload;
  }

  // OBS修正API
  async update(obs) {
    const { payload } = await this.dao.update(obs);
    return payload;
  }

  // wkt_tableのデータ削除API
  async deleteDataWktTable() {
    const { payload } = await this.dao.deleteDataWktTable();
    return payload;
  }

  // OBSの存在チェック
  async countByNum(plantName, lNum) {
    try {
      const condition = { plantName, lNum };
      const { payload } = await this.dao.countByNum(condition);
      return payload;
    } catch {
      return 0;
    }
  }

  // OBSテーブルの最新フラグを更新する
  async updateNewestFlag(obs) {
    const condition = {
      plantName: obs.plantName,
      kinds: obs.kinds,
      fields: obs.fields,
      partId: obs.partId,
      serialNum: obs.serialNum,
      revisions: obs.revisions,
      language: obs.language,
    };
    const { payload } = await this.dao.updateNewestFlag(condition);
    return payload;
  }

  async outputWord(obsInfo) {
    const fileResponse = await this.dao.outputWord(obsInfo);
    return fileResponse;
  }

  // OBS入力画面の保存処理
  async saveAll(saveObs) {
    const { payload } = await this.dao.saveAll(saveObs);
    return payload;
  }

  // obsExtendReferのnumberを取得するAPI
  async getNumOfObsExtendRefer(condition) {
    const { payload } = await this.dao.getNumOfObsExtendRefer(condition);
    return payload;
  }

  // obsExtendRefer一覧取得API
  async getObsExtendReferList(condition) {
    const { payload } = await this.dao.getObsExtendReferList(condition);
    return payload;
  }

  async saveObsWithObsAttach(formData) {
    const { payload } = await this.dao.saveObsWithObsAttach(formData);
    return payload;
  }
}
export default new OBSService('obs');
