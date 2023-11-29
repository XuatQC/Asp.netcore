using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure
{
    public partial class DBMasterContext : DbContext
    {
        #region DB Core
        public DbSet<Tenant>? Tenants { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Obs>? Obss { get; set; }
        public DbSet<ObsFact>? ObsFacts { get; set; }
        public DbSet<ObsConclusion>? ObsConclusions { get; set; }
        public DbSet<ObsAttach>? ObsAttachs { get; set; }
        public DbSet<PeerReview>? PeerReviews { get; set; }
        public DbSet<ReviewMember>? ReviewMembers { get; set; }
        public DbSet<WktObsFact>? WktObsFacts { get; set; }
        public DbSet<WktObsConclusion>? WktObsWktObsConclusions { get; set; }
        public DbSet<ObsFactPast>? ObsFactPasts { get; set; }
        public DbSet<ObsConclusionPast>? ObsConclusionPasts { get; set; }
        public DbSet<ItemMaster>? ItemMasters { get; set; }
        public DbSet<Translate>? Translates { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<SharedFolder>? SharedFolders { get; set; }
        public DbSet<WkWordList>? WkWordLists { get; set; }
        public DbSet<WktAfiExample>? WktAfiExamples { get; set; }
        public DbSet<WktAnalysisObs>? WktAnalysisObss { get; set; }
        public DbSet<WktAnalysisObsConclusion>? WktAnalysisObsConclusions { get; set; }
        public DbSet<WktAnalysisWc>? WktAnalysisWcs { get; set; }
        public DbSet<WktGfaExamples>? WktGfaExamples { get; set; }
        public DbSet<WktPfaExamples>? WktPfaExamples { get; set; }
        public DbSet<WktSoiExample>? WktSoiExamples { get; set; }
        public DbSet<WktStrExamples>? WktStrExamples { get; set; }
        public DbSet<WktTransHistory>? WktTransHistorys { get; set; }
        public DbSet<WktErrorData>? WktErrorDatas { get; set; }
        public DbSet<WktWcList>? WktWcLists { get; set; }
        public DbSet<WktAnalysisAfiExample>? WktAnalysisAfiExamples { get; set; }
        public DbSet<WktAnalysisGfaExamples>? WktAnalysisGfaExamples { get; set; }
        public DbSet<Field>? Fields { get; set; }
        public DbSet<Controls>? Controlss { get; set; }
        public DbSet<ObsExtend>? ObsExtendResponses { get; set; }
        public DbSet<WktTranslate>? WktTranslates { get; set; }
        public DbSet<Activity>? Activities { get; set; }
        public DbSet<PoAndC>? PoAndCs { get; set; }
        public DbSet<SafetyCulture>? SafetyCultures { get; set; }
        public DbSet<NumberControl>? NumberControls { get; set; }
        #endregion

        public DBMasterContext(DbContextOptions<DBMasterContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasMany(a => a.Accounts).WithOne(t => t.Tenant).HasPrincipalKey(q => q.Code).HasForeignKey(s => s.TenantCode);
            modelBuilder.Entity<PeerReview>().HasOne(e => e.ReviewMember).WithOne(e => e.PeerReview).HasPrincipalKey<PeerReview>(pr => pr.PlantName).HasForeignKey<ReviewMember>(rm => rm.PlantName);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // to do
        }
    }

    public partial class DBSlaveContext : DbContext
    {
        #region DB Core
        public DbSet<Tenant>? Tenants { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Obs>? Obss { get; set; }
        public DbSet<ObsFact>? ObsFacts { get; set; }
        public DbSet<ObsConclusion>? ObsConclusions { get; set; }
        public DbSet<ObsAttach>? ObsAttachs { get; set; }
        public DbSet<PeerReview>? PeerReviews { get; set; }
        public DbSet<ReviewMember>? ReviewMembers { get; set; }
        public DbSet<WktObsFact>? WktObsFacts { get; set; }
        public DbSet<WktObsConclusion>? WktObsWktObsConclusions { get; set; }
        public DbSet<ObsFactPast>? ObsFactPasts { get; set; }
        public DbSet<ObsConclusionPast>? ObsConclusionPasts { get; set; }
        public DbSet<ItemMaster>? ItemMasters { get; set; }
        public DbSet<OfferNumResponseDto>? OfferNumResponseDtos { get; set; }
        public DbSet<Translate>? Translates { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<SharedFolder>? SharedFolders { get; set; }
        public DbSet<WkWordList>? WkWordLists { get; set; }
        public DbSet<WktAfiExample>? WktAfiExamples { get; set; }
        public DbSet<WktAnalysisObs>? WktAnalysisObss { get; set; }
        public DbSet<WktAnalysisObsConclusion>? WktAnalysisObsConclusions { get; set; }
        public DbSet<WktAnalysisWc>? WktAnalysisWcs { get; set; }
        public DbSet<WktGfaExamples>? WktGfaExamples { get; set; }
        public DbSet<WktPfaExamples>? WktPfaExamples { get; set; }
        public DbSet<WktSoiExample>? WktSoiExamples { get; set; }
        public DbSet<WktStrExamples>? WktStrExamples { get; set; }
        public DbSet<WktTransHistory>? WktTransHistorys { get; set; }
        public DbSet<WktErrorData>? WktErrorDatas { get; set; }
        public DbSet<WktWcList>? WktWcLists { get; set; }
        public DbSet<WktAnalysisAfiExample>? WktAnalysisAfiExamples { get; set; }
        public DbSet<WktAnalysisGfaExamples>? WktAnalysisGfaExamples { get; set; }
        public DbSet<Field>? Fields { get; set; }
        public DbSet<ControlsResponseDto>? ControlsResponses { get; set; }
        public DbSet<Controls>? Controlss { get; set; }
        public DbSet<ObsExtend>? ObsExtends { get; set; }
        public DbSet<WktTranslate>? WktTranslates { get; set; }
        public DbSet<Activity>? Activities { get; set; }
        public DbSet<PoAndC>? PoAndCs { get; set; }
        public DbSet<SafetyCulture>? SafetyCultures { get; set; }
        public DbSet<NumberControl>? NumberControls { get; set; }
        #endregion

        #region DB Custom Result
        public DbSet<DisObsReponseDto>? DisObs { get; set; }
        public DbSet<PeerReviewResponseDto>? PeerReviewesponses { get; set; }
        public DbSet<ObsExtendReferDto>? ObsExtendRefers { get; set; }
        public DbSet<FieldResponseDto>? FieldResponses { get; set; }
        public DbSet<ReviewMemberResponseDto>? ReviewMemberResponses { get; set; }
        public DbSet<ReviewMemberEditorResponseDto>? ReviewMemberEditorResponseDto { get; set; }
        public DbSet<PrintDataResponeseDto>? PrintResponses { get; set; }
        public DbSet<UserResponseDto>? UserResponseDtos { get; set; }
        public DbSet<TranslateReponseDto>? TranslateReponseDtos { get; set; }
        public DbSet<ObsExtendedReferObsFactDto>? ObsExtendedReferObsFactDtos { get; set; }
        public DbSet<ObsExtendedReferObsConclusionDto>? ObsExtendedReferObsConclusionDtos { get; set; }
        public DbSet<WktTransHistoryExtendDto>? WktTransHistoryExtendDtos { get; set; }
        public DbSet<VwTranslateControl>? VwTranslateControls { get; set; }
        public DbSet<VwObsControl>? VwObsControls { get; set; }
        public DbSet<VwObsFactAttach> VwObsFactAttach { get; set; }

        #endregion

        public DBSlaveContext(DbContextOptions<DBSlaveContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // to do
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasMany(a => a.Accounts).WithOne(t => t.Tenant).HasPrincipalKey(q => q.Code).HasForeignKey(s => s.TenantCode);
            modelBuilder.Entity<PeerReview>().HasOne(e => e.ReviewMember).WithOne(e => e.PeerReview).HasPrincipalKey<PeerReview>(pr => pr.PlantName).HasForeignKey<ReviewMember>(rm => rm.PlantName);
        }
    }
}