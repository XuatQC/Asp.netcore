using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace FNDSystem.Infrastructure.DataAccess.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DBMasterContext masterDBContext;
    private readonly DBSlaveContext slaveDBContext;

    private ITenantRepository? tenantRepository;
    private IAccountRepository? accountRepository;
    private IObsRepository? obsRepository;
    private IPeerReviewRepository? peerReviewRepository;
    private IObsFactRepository? obsFactRepository;
    private IObsAttachRepository? obsAttachRepository;
    private IObsConclusionRepository? obsConclusionRepository;
    private IReviewMemberRepository? reviewMemberRepository;
    private IWktObsFactRepository? wktObsFactRepository;
    private IWktObsConclusionRepository? wktObsConclusionRepository;
    private IObsFactPastRepository? obsFactPastRepository;
    private IObsConclusionPastRepository? obsConclusionPastRepository;
    private IItemMasterRepository? itemMasterRepository;
    private ITranslateRepository? translateRepoitory;
    private IUserRepository? userRepository;
    private ISharedFolderRepository? sharedFolderRepository;
    private IWkWordListRepository? wkWordListRepository;
    private IWktAfiExampleRepository? wktAfiExampleRepository;
    private IWktAnalysisObsRepository? wktAnalysisObsRepository;
    private IWktAnalysisObsConclusionRepository? wktAnalysisObsConclusionRepository;
    private IWktAnalysisWcRepository? wktAnalysisWcRepository;
    private IWktGfaExamplesRepository? wktGfaExamplesRepository;
    private IWktPfaExamplesRepository? wktPfaExamplesRepository;
    private IWktSoiExampleRepository? wktSoiExampleRepository;
    private IWktStrExamplesRepository? wktStrExamplesRepository;
    private IWktTransHistoryRepository? wktTransHistoryRepository;
    private IWktErrorDataRepository? wktErrorDataRepository;
    private IWktWcListRepository? wktWcListRepository;
    private IWktAnalysisAfiExampleRepository? wktAnalysisAfiExampleRepository;
    private IWktAnalysisGfaExampleRepository? wktAnalysisGfaExampleRepository;
    private IFieldRepository? fieldRepository;
    private IControlsRepository? controlsRepository;
    private IPrintRepository? printRepository;
    private IWktTranslateRepository? wktTranslateRepoitory;
    private IActivityRepository? activityRepository;
    private IPoAndCRepository? poAndCRepository;
    private ISafetyCultureRepository? safetyCultureRepository;
    private INumberControlRepository? numberControlRepository;
    private IVwTranslateControlRepository? vwTranslateControlRepository;
    private IVwObsControlRepository? vwObsControlRepository;
    public UnitOfWork(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext)
    {
        this.masterDBContext = masterDBContext;
        this.slaveDBContext = slaveDBContext;
    }

    #region TenantRepository
    public ITenantRepository TenantRepository
    {
        get { return tenantRepository = tenantRepository ?? new TenantRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region AccountRepository
    public IAccountRepository AccountRepository
    {
        get { return accountRepository = accountRepository ?? new AccountRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ObsRepository
    public IObsRepository ObsRepository
    {
        get { return obsRepository = obsRepository ?? new ObsRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region PeerReviewRepository
    public IPeerReviewRepository PeerReviewRepository
    {
        get { return peerReviewRepository = peerReviewRepository ?? new PeerReviewRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ObsFactRepository
    public IObsFactRepository ObsFactRepository
    {
        get { return obsFactRepository = obsFactRepository ?? new ObsFactRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ObsConclusionRepository
    public IObsConclusionRepository ObsConclusionRepository
    {
        get { return obsConclusionRepository = obsConclusionRepository ?? new ObsConclusionRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ObsAttachRepository
    public IObsAttachRepository ObsAttachRepository
    {
        get { return obsAttachRepository = obsAttachRepository ?? new ObsAttachRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ReviewMemberRepository
    public IReviewMemberRepository ReviewMemberRepository
    {
        get { return reviewMemberRepository = reviewMemberRepository ?? new ReviewMemberRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktObsFactRepository
    public IWktObsFactRepository WktObsFactRepository
    {
        get { return wktObsFactRepository = wktObsFactRepository ?? new WktObsFactRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktObsConclusionRepository
    public IWktObsConclusionRepository WktObsConclusionRepository
    {
        get { return wktObsConclusionRepository = wktObsConclusionRepository ?? new WktObsConclusionRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ObsFactPastRepository
    public IObsFactPastRepository ObsFactPastRepository
    {
        get { return obsFactPastRepository = obsFactPastRepository ?? new ObsFactPastRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ObsConclusionPastRepository
    public IObsConclusionPastRepository ObsConclusionPastRepository
    {
        get { return obsConclusionPastRepository = obsConclusionPastRepository ?? new ObsConclusionPastRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ItemMasterRepository
    public IItemMasterRepository ItemMasterRepository
    {
        get { return itemMasterRepository = itemMasterRepository ?? new ItemMasterRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region TranslateRepoitory
    public ITranslateRepository TranslateRepository
    {
        get { return translateRepoitory = translateRepoitory ?? new TranslateRepoitory(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region UserRepository
    public IUserRepository UserRepository
    {
        get { return userRepository = userRepository ?? new UserRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region SharedFolderRepository
    public ISharedFolderRepository SharedFolderRepository
    {
        get { return sharedFolderRepository = sharedFolderRepository ?? new SharedFolderRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WkWordListRepository
    public IWkWordListRepository WkWordListRepository
    {
        get { return wkWordListRepository = wkWordListRepository ?? new WkWordListRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktAfiExampleRepository
    public IWktAfiExampleRepository WktAfiExampleRepository
    {
        get { return wktAfiExampleRepository = wktAfiExampleRepository ?? new WktAfiExampleRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktAnalysisObsRepository
    public IWktAnalysisObsRepository WktAnalysisObsRepository
    {
        get { return wktAnalysisObsRepository = wktAnalysisObsRepository ?? new WktAnalysisObsRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktAnalysisObsConclusionRepository
    public IWktAnalysisObsConclusionRepository WktAnalysisObsConclusionRepository
    {
        get { return wktAnalysisObsConclusionRepository = wktAnalysisObsConclusionRepository ?? new WktAnalysisObsConclusionRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktAnalysisWcRepository
    public IWktAnalysisWcRepository WktAnalysisWcRepository
    {
        get { return wktAnalysisWcRepository = wktAnalysisWcRepository ?? new WktAnalysisWcRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktGfaExamplesRepository
    public IWktGfaExamplesRepository WktGfaExamplesRepository
    {
        get { return wktGfaExamplesRepository = wktGfaExamplesRepository ?? new WktGfaExamplesRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktPfaExamplesRepository
    public IWktPfaExamplesRepository WktPfaExamplesRepository
    {
        get { return wktPfaExamplesRepository = wktPfaExamplesRepository ?? new WktPfaExamplesRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktSoiExampleRepository
    public IWktSoiExampleRepository WktSoiExampleRepository
    {
        get { return wktSoiExampleRepository = wktSoiExampleRepository ?? new WktSoiExampleRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktStrExamplesRepository
    public IWktStrExamplesRepository WktStrExamplesRepository
    {
        get { return wktStrExamplesRepository = wktStrExamplesRepository ?? new WktStrExamplesRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktTransHistoryRepository
    public IWktTransHistoryRepository WktTransHistoryRepository
    {
        get { return wktTransHistoryRepository = wktTransHistoryRepository ?? new WktTransHistoryRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktErrorDataRepository
    public IWktErrorDataRepository WktErrorDataRepository
    {
        get { return wktErrorDataRepository = wktErrorDataRepository ?? new WktErrorDataRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktWcListRepository
    public IWktWcListRepository WktWcListRepository
    {
        get { return wktWcListRepository = wktWcListRepository ?? new WktWcListRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktAnalysisAfiExampleRepository
    public IWktAnalysisAfiExampleRepository WktAnalysisAfiExampleRepository
    {
        get { return wktAnalysisAfiExampleRepository = wktAnalysisAfiExampleRepository ?? new WktAnalysisAfiExampleRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktAnalysisGfaExampleRepository
    public IWktAnalysisGfaExampleRepository WktAnalysisGfaExampleRepository
    {
        get { return wktAnalysisGfaExampleRepository = wktAnalysisGfaExampleRepository ?? new WktAnalysisGfaExampleRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region FieldRepository
    public IFieldRepository FieldRepository
    {
        get { return fieldRepository = fieldRepository ?? new FieldRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ControlsRepository
    public IControlsRepository ControlsRepository
    {
        get { return controlsRepository = controlsRepository ?? new ControlsRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region PrintRepository
    public IPrintRepository PrintRepository
    {
        get { return printRepository = printRepository ?? new PrintRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region ActivityRepository
    public IActivityRepository ActivityRepository
    {
        get { return activityRepository = activityRepository ?? new ActivityRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region WktTranslateRepoitory
    public IWktTranslateRepository WktTranslateRepository
    {
        get { return wktTranslateRepoitory = wktTranslateRepoitory ?? new WktTranslateRepoitory(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region PoAndCRepository
    public IPoAndCRepository PoAndCRepository
    {
        get { return poAndCRepository = poAndCRepository ?? new PoAndCRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region SafetyCultureRepository
    public ISafetyCultureRepository SafetyCultureRepository
    {
        get { return safetyCultureRepository = safetyCultureRepository ?? new SafetyCultureRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region NumberControlRepository
    public INumberControlRepository NumberControlRepository
    {
        get { return numberControlRepository = numberControlRepository ?? new NumberControlRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region VwTranslateControlRepoitory
    public IVwTranslateControlRepository VwTranslateControlRepository
    {
        get { return vwTranslateControlRepository = vwTranslateControlRepository ?? new VwTranslateControlRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    #region VwObsControlRepository
    public IVwObsControlRepository VwObsControlRepository
    {
        get { return vwObsControlRepository = vwObsControlRepository ?? new VwObsControlRepository(masterDBContext, slaveDBContext); }
    }
    #endregion

    /// <summary>
    /// コミット
    /// </summary>
    public void SaveChanges()
        => masterDBContext.SaveChanges();

    public IDbContextTransaction BeginTransaction()
        => masterDBContext.Database.BeginTransaction();
}
