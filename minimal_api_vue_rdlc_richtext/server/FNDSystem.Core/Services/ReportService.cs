using FNDSystem.Core.Helpers;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Http;

namespace FNDSystem.Core.Services;
public class ReportService : GeneticService, IReportService
{
    public ReportService()
    {
    }

    /// <summary>
    /// 帳票出力
    /// </summary>
    /// <param name="reportFileByteString">レポートファイルのバイト文字列</param>
    /// <param name="reportName">レポート名</param>
    /// <param name="reportType">レポートの種類</param>
    public void ExportReport(byte[] reportFileByteString, string reportPath, string reportName, string reportType)
    {
        // レポート出力パスを取得する
        string absolutePath = ReportHelper.GetOutputFullFilePath(reportPath, reportName, reportType);
        // ファイルをフォルダーに保存する
        ReportHelper.SaveFile(absolutePath, reportFileByteString);
    }

    /// <summary>
    /// 帳票ダウンロード
    /// </summary>
    /// <param name="reportName">レポート名</param>
    /// <param name="reportType">レポートの種類</param>
    /// <returns>File</returns>
    public IResult DownloadReport(string reportPath, string reportName, string reportType)
    {
        string absolutePath = ReportHelper.GetOutputFullFilePath(reportPath, reportName, reportType);
        string fileNameWithExtension = ReportHelper.GetReportName(reportName, reportType);

        // ファイルのコンテンツタイプを取得する
        string contentType = ReportHelper.GetContentType(reportType);

        // ダウンロードファイル
        return Results.File(absolutePath, contentType, fileNameWithExtension);
    }
}