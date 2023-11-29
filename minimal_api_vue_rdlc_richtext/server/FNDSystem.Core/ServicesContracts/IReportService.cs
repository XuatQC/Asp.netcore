using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using Microsoft.AspNetCore.Http;

namespace FNDSystem.Core.ServicesContracts;

public interface IReportService 
{
    IResult DownloadReport(string reportPath, string reportName, string reportType);
    void ExportReport(byte[] reportFileByteString, string reportPath, string reportName, string reportType);
}