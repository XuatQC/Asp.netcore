using System.Reflection;
using System.Runtime.CompilerServices;

namespace FNDSystem.Core.Helpers
{
    internal static class ReportHelper
    {
        public static string GetRenderType(string reportType)
        {
            string? renderType;

            switch (reportType.ToUpper())
            {
                default:
                    renderType = FndReport.RenderType.PDF;
                    break;
                case FndReport.Type.PDF:
                    renderType = FndReport.RenderType.PDF;
                    break;
                case FndReport.Type.EXCEL:
                    renderType = FndReport.RenderType.EXCEL;
                    break;
                case FndReport.Type.WORD:
                    renderType = FndReport.RenderType.WORD;
                    break;
            }

            return renderType;
        }

        public static string GetReportName(string reportName, string reportType)
        {
            string? outputFileName;

            switch (reportType.ToUpper())
            {
                default:
                    outputFileName = string.Format("{0}{1}", reportName, FndReport.Extension.WORD);
                    break;
                case FndReport.Type.PDF:
                    outputFileName = string.Format("{0}{1}", reportName, FndReport.Extension.PDF);
                    break;
                case FndReport.Type.EXCEL:
                    outputFileName = string.Format("{0}{1}", reportName, FndReport.Extension.EXCEL);
                    break;
                case FndReport.Type.WORD:
                    outputFileName = string.Format("{0}{1}", reportName, FndReport.Extension.WORD);
                    break;
            }

            return outputFileName;
        }

        public static string GetContentType(string reportType)
        {
            string? outputContentType;

            switch (reportType.ToUpper())
            {
                default:
                    outputContentType = FndReport.ExportContentType.WORD;
                    break;
                case FndReport.Type.PDF:
                    outputContentType = FndReport.ExportContentType.PDF;
                    break;
                case FndReport.Type.EXCEL:
                    outputContentType = FndReport.ExportContentType.EXCEL;
                    break;
                case FndReport.Type.WORD:
                    outputContentType = FndReport.ExportContentType.WORD;
                    break;
            }

            return outputContentType;
        }

        public static string GetTemplatePath(string reportName)
        {
            string path = Assembly.GetExecutingAssembly().Location.Replace("FNDSystem.Core.dll", "");
            string rdlcFilePath = string.Format("{0}ReportTemplates\\{1}.rdlc", path, reportName);

            return rdlcFilePath;
        }

        public static string GetOutputFullFilePath(string reportPath, string reportName, string reportType)
        {
            string folderpath = GetOutputPath(reportPath);
            string fileNameWithExtension = GetReportName(reportName, reportType);
            string absolutePath = string.Format("{0}\\{1}", folderpath, fileNameWithExtension);

            return absolutePath;
        }

        public static string GetOutputPath(string reportPath)
        {
            string path = Assembly.GetExecutingAssembly().Location.Replace("FNDSystem.Core.dll", "");
            string absolutePath = string.Format("{0}\\Output\\{1}", path, reportPath);
            return absolutePath;
        }

        public static string GetStaticFolder()
        {
            string path = Path.GetFullPath("wwwroot");
            return path;
        }

        public static bool SaveFile(string filePath, byte[] byteArray)
        {
            try
            {
                var path = Path.GetDirectoryName(filePath);
                if(!string.IsNullOrEmpty(path) && !Directory.Exists(path)) Directory.CreateDirectory(path);
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
