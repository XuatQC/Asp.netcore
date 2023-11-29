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
    /// ���[�o��
    /// </summary>
    /// <param name="reportFileByteString">���|�[�g�t�@�C���̃o�C�g������</param>
    /// <param name="reportName">���|�[�g��</param>
    /// <param name="reportType">���|�[�g�̎��</param>
    public void ExportReport(byte[] reportFileByteString, string reportPath, string reportName, string reportType)
    {
        // ���|�[�g�o�̓p�X���擾����
        string absolutePath = ReportHelper.GetOutputFullFilePath(reportPath, reportName, reportType);
        // �t�@�C�����t�H���_�[�ɕۑ�����
        ReportHelper.SaveFile(absolutePath, reportFileByteString);
    }

    /// <summary>
    /// ���[�_�E�����[�h
    /// </summary>
    /// <param name="reportName">���|�[�g��</param>
    /// <param name="reportType">���|�[�g�̎��</param>
    /// <returns>File</returns>
    public IResult DownloadReport(string reportPath, string reportName, string reportType)
    {
        string absolutePath = ReportHelper.GetOutputFullFilePath(reportPath, reportName, reportType);
        string fileNameWithExtension = ReportHelper.GetReportName(reportName, reportType);

        // �t�@�C���̃R���e���c�^�C�v���擾����
        string contentType = ReportHelper.GetContentType(reportType);

        // �_�E�����[�h�t�@�C��
        return Results.File(absolutePath, contentType, fileNameWithExtension);
    }
}