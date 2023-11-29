import BaseService from 'services/base.service';
import { useAppStore } from 'stores/app-store';

class ReviewMemberService extends BaseService {
  // plantNameでReviewMemberを取得するAPI
  async list() {
    const { plantName } = useAppStore();
    const { payload } = await this.dao.list(plantName);
    return payload;
  }

  // plantNameとinitialでReviewMemberを取得するAPI
  async get(plantName, initial) {
    const condition = { plantName, initial };
    const { payload } = await this.dao.get(condition);
    return payload;
  }

  // plantNameでreviewMemberEditorの値を取得する
  async getReviewMemberEditorByPlantName(plantName) {
    const { payload } = await this.dao.getReviewMemberEditorByPlantName(plantName);
    return payload;
  }
}
export default new ReviewMemberService('review-member');
