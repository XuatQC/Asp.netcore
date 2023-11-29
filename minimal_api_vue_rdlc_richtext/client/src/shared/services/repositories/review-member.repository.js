import BaseRepository from 'repositories/base/base.repository';

export default class ReviewMemberRepository extends BaseRepository {
  constructor() {
    super('review-member');
  }

  // plantNameでReviewMemberを取得するAPI
  list(plantName) {
    return this.client.get('/list', { plantName });
  }

  // plantNameとinitialでReviewMemberを取得するAPI
  get(condition) {
    return this.client.get('/', condition);
  }

  // plantNameでreviewMemberEditorの値を取得する
  getReviewMemberEditorByPlantName(plantName) {
    return this.client.get(`/${plantName}`);
  }
}
