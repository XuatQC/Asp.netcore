using FNDSystem.Core.Dto.OBS;
using Microsoft.AspNetCore.Http;

namespace FNDSystem.Core.ServicesContracts;

public interface IObsReportService
{
    IResult DownloadOutputWordReport(OutputWordDto obs);

}