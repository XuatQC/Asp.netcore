using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.Entities.Reports;
using FNDSystem.Core.Dto.OBS;
using FNDSystem.Core.Helpers;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Reporting.NETCore;
using System.Text;

namespace FNDSystem.Core.Services;
public class ObsReportService : GeneticService, IObsReportService
{
    private readonly IReportService _reportService;
    public ObsReportService(IReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>
    /// WORD���[���_�E�����[�h����
    /// </summary>
    /// <param name="obs">Obs</param>
    /// <returns>�t�@�C��</returns>
    public IResult DownloadOutputWordReport(OutputWordDto obs)
    {
        string reportPath = string.IsNullOrEmpty(obs.Path) ? $"{obs.PlantName}\\DocInWord" : obs.Path;
        string reportName = obs.Number ?? string.Empty;
        string reportType = FndReport.Type.WORD;
        this.GenerateObsOutputWordReportFile(reportPath, reportName, reportType, obs);
        return _reportService.DownloadReport(reportPath, reportName, reportType);
    }

    /// <summary>
    /// OBS WORD���[�t�@�C�����쐬����
    /// </summary>
    /// <param name="reportName">���|�[�g��</param>
    /// <param name="reportType">���|�[�g�̎��</param>
    /// <param name="obs">Obs</param>
    private void GenerateObsOutputWordReportFile(string reportPath, string reportName, string reportType, OutputWordDto obs)
    {
        string rdlcFilePath = ReportHelper.GetTemplatePath("OutputWord");
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Encoding.GetEncoding("utf-8");
        Stream reportDefinition; // �t�@�C���܂��̓��\�[�X����� RDLC
        // ���|�[�g�̃f�[�^�\�[�X�B
        reportDefinition = new FileStream(rdlcFilePath, FileMode.Open);
        LocalReport report = new LocalReport();
        report.LoadReportDefinition(reportDefinition);
        var  imageFolder = ReportHelper.GetStaticFolder();
        List<ObsReport> obsList = new List<ObsReport> {
            new ObsReport
            {
                PlantName = obs.PlantName != null ? obs.PlantName : string.Empty,
                Number = obs.Number !=null ? obs.Number : string.Empty,
                Title = obs.Title,
                Scope = obs.Scope != null ? obs.Scope : string.Empty,
                Area = obs.Area != null ? obs.Area : string.Empty,
                IsEnglish = obs.IsEnglish != null ? obs.IsEnglish : false,
            }
        };
        obs?.Facts?.ForEach((fact) =>
            {
                if(fact.ImageUrl != "") fact.ImageUrl = new Uri($"{imageFolder}\\{fact.ImageUrl}").AbsoluteUri;
            }
        );
        obs?.Conclusions?.ForEach((conclusion) =>
            {
                if(conclusion.ImageUrl != "") conclusion.ImageUrl = new Uri($"{imageFolder}\\{conclusion.ImageUrl}").AbsoluteUri;
            }
        );

        report.EnableExternalImages = true;
        report.DataSources.Add(new ReportDataSource("OBS", obsList));
        report.DataSources.Add(new ReportDataSource("OBSFact", obs?.Facts));
        report.DataSources.Add(new ReportDataSource("OBSConclusion", obs?.Conclusions));

        string genderType = ReportHelper.GetRenderType(reportType);
        byte[] reportContent = report.Render(genderType);
        reportDefinition.Dispose();

        _reportService.ExportReport(reportContent, reportPath, reportName, reportType);
    }
}