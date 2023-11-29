import AuthRepository from 'repositories/auth.repository';
import PeerReviewRepository from 'repositories/peer-review.repository';
import OBSRepository from 'repositories/obs.repository';
import FieldRepository from 'repositories/field.repository';
import ReviewMemberRepository from 'repositories/review-member.repository';
import SharedFolderRepository from 'repositories/shared-folder.repository';
import DirectoryRepository from 'repositories/directory.repository';
import ControlsRepository from 'repositories/controls.repository';
import WkWordListRepository from 'repositories/wk-word-list.repository';
import WktTranslateRepository from 'repositories/wkt-translate.repository';
import TranslateRepository from 'repositories/translate.repository';
import ObsScopeRepository from 'repositories/obs-scope.repository';
import ObsFactRepository from 'repositories/obs-fact.repository';
import ObsConclusionRepository from 'repositories/obs-conclusion.repository';
import ObsAttachRepository from 'repositories/obs-attach.repository';
import ActivityRepository from 'repositories/activity.repository';
import UserRepository from 'repositories/user.repository';
import PoCRepository from 'repositories/poc.repository';
import PrintRepository from 'repositories/print.repository';
import ItemMasterRepository from 'repositories/item-master.repository';
import WktTranslateHisRepository from 'repositories/wkt-trans-history.repository';
import SafetyCultureRepository from 'repositories/safety-culture.repository';

class RepositoryFactory {
  getRepository(serviceName) {
    switch (serviceName) {
      case 'auth':
        return new AuthRepository();
      case 'peer-review':
        return new PeerReviewRepository();
      case 'obs':
        return new OBSRepository();
      case 'field':
        return new FieldRepository();
      case 'review-member':
        return new ReviewMemberRepository();
      case 'shared-folder':
        return new SharedFolderRepository();
      case 'directory':
        return new DirectoryRepository();
      case 'controls':
        return new ControlsRepository();
      case 'wk-word-list':
        return new WkWordListRepository();
      case 'translate':
        return new TranslateRepository();
      case 'wkt-translate':
        return new WktTranslateRepository();
      case 'obs-scope':
        return new ObsScopeRepository();
      case 'obs-fact':
        return new ObsFactRepository();
      case 'obs-conclusion':
        return new ObsConclusionRepository();
      case 'obs-attach':
        return new ObsAttachRepository();
      case 'activity':
        return new ActivityRepository();
      case 'user':
        return new UserRepository();
      case 'po-and-c':
        return new PoCRepository();
      case 'print':
        return new PrintRepository();
      case 'item-master':
        return new ItemMasterRepository();
      case 'wkt-trans-history':
        return new WktTranslateHisRepository();
      case 'safety-culture':
        return new SafetyCultureRepository();
      default:
        throw Error('Invalid param');
    }
  }
}

export default new RepositoryFactory();
