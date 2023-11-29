import BaseRepository from 'repositories/base/base.repository';

export default class PeerReviewRepository extends BaseRepository {
  constructor() {
    super('peer-review');
  }

  // 全peerview一覧取得
  getAll() {
    return this.client.get('/get-by-hold/');
  }

  // plantNameとholdでpeerview一覧を取得する
  getByPlantNameAndHold() {
    return this.client.get('/get-by-plant-name-hold/');
  }

  // peerReviewMaster取得API
  getPeerReviewMaster(peerReview) {
    return this.client.get('/master-check/', peerReview);
  }

  // plantNameでpeerReviewを取得する
  getByPlantName(plantName) {
    return this.client.get(`/${plantName}`);
  }
}
