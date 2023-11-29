namespace FNDSystem.Core.Domain.Entities.Reports
{
    public partial class ObsReport : Obs
    {
        public ObsReport() { }

        public string PlantName { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public string Area { get; set; } = string.Empty;

        public string Facts { get; set; } = string.Empty;

        public bool? IsEnglish { get; set; } = false;
    }

    public partial class ObsFactReport
    {
        public int No { get; set; } = 0;

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string Facts { get; set; } = string.Empty;
    }
    public partial class ObsConclusionReport
    {
        public int No { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

    }

    public class ObsList
    {
        public string Title { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string OBSNo { get; set; } = string.Empty;
        public string PlantName { get; set; } = string.Empty;
        public string FactNo { get; set; } = string.Empty;
        public string FactTitle { get; set; } = string.Empty;
        public string FactContent { get; set; } = string.Empty;
        public string FactImageURL { get; set; } = string.Empty;
        public string ConclusionNo { get; set; } = string.Empty;
        public string ConclusionContent { get; set; } = string.Empty;
    }
}
