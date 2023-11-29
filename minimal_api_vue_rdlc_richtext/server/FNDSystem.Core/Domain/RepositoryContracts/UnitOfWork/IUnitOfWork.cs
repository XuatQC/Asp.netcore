using FNDSystem.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace FNDSystem.Core.Domain.RepositoryContracts;

public interface IUnitOfWork
{
    ITenantRepository TenantRepository { get; }

    IAccountRepository AccountRepository { get; }

    IObsRepository ObsRepository { get; }

    IObsFactRepository ObsFactRepository { get; }

    IObsConclusionRepository ObsConclusionRepository { get; }

    IObsAttachRepository ObsAttachRepository { get; }

    IPeerReviewRepository PeerReviewRepository { get; }

    IReviewMemberRepository ReviewMemberRepository { get; }

    IWktObsFactRepository WktObsFactRepository { get; }

    IWktObsConclusionRepository WktObsConclusionRepository { get; }

    IObsFactPastRepository ObsFactPastRepository { get; }

    IObsConclusionPastRepository ObsConclusionPastRepository { get; }

    IItemMasterRepository ItemMasterRepository { get; }

    ITranslateRepository TranslateRepository { get; }

    IUserRepository UserRepository { get; }

    ISharedFolderRepository SharedFolderRepository { get; }

    IWkWordListRepository WkWordListRepository { get; }

    IWktAfiExampleRepository WktAfiExampleRepository { get; }

    IWktAnalysisObsRepository WktAnalysisObsRepository { get; }

    IWktAnalysisObsConclusionRepository WktAnalysisObsConclusionRepository { get; }

    IWktAnalysisWcRepository WktAnalysisWcRepository { get; }

    IWktGfaExamplesRepository WktGfaExamplesRepository { get; }

    IWktPfaExamplesRepository WktPfaExamplesRepository { get; }

    IWktSoiExampleRepository WktSoiExampleRepository { get; }

    IWktStrExamplesRepository WktStrExamplesRepository { get; }

    IWktTransHistoryRepository WktTransHistoryRepository { get; }

    IWktErrorDataRepository WktErrorDataRepository { get; }

    IWktWcListRepository WktWcListRepository { get; }

    IWktAnalysisAfiExampleRepository WktAnalysisAfiExampleRepository { get; }

    IWktAnalysisGfaExampleRepository WktAnalysisGfaExampleRepository { get; }

    IFieldRepository FieldRepository { get; }

    IControlsRepository ControlsRepository { get; }

    IPrintRepository PrintRepository { get; }

    IVwObsControlRepository VwObsControlRepository {get;}

    IWktTranslateRepository WktTranslateRepository { get; }

    IActivityRepository ActivityRepository { get; }

    IPoAndCRepository PoAndCRepository { get; }

    ISafetyCultureRepository SafetyCultureRepository { get; }

    INumberControlRepository NumberControlRepository{ get; }

    IVwTranslateControlRepository VwTranslateControlRepository { get; }

    void SaveChanges();

    IDbContextTransaction BeginTransaction();
}
