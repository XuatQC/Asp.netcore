import BaseService from 'services/base.service';

class PeerReviewService extends BaseService {
  // 全peerviewの一覧取得
  async getAll() {
    return this.dao.getAll();
  }

  async getByPlantNameAndHold() {
    return this.dao.getByPlantNameAndHold();
  }

  // peerReviewMaster取得API
  async getPeerReviewMaster(peerReview) {
    return this.dao.getPeerReviewMaster(peerReview);
  }

  // plantNameでpeerReviewを取得する
  async getByPlantName(plantName) {
    const { payload } = await this.dao.getByPlantName(plantName);
    return payload;
  }
}

export default new PeerReviewService('peer-review');
