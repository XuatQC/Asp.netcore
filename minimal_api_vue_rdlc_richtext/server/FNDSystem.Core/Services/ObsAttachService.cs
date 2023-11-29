using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Dto.ObsFact;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ObsAttachService : GeneticService, IObsAttachService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ObsAttachService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    #region 写真番号によりOBS添付ファイル（写真）一覧を取得する
    /// <summary>
    /// 写真番号によりOBS添付ファイル（写真）一覧を取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>OBS添付ファイル（写真）リスト</returns>
    public IEnumerable<ObsAttach>? GetObsAttachListByImageCode(string num)
    {
        Expression<Func<ObsAttach, bool>> whereExpression = item => item.NumNoRevisions == num;
        var obsAttachResponseDtoList = unitOfWork.ObsAttachRepository.GetAll(whereExpression);

        return obsAttachResponseDtoList;
    }
    #endregion

    #region 写真番号によりOBS添付ファイル（写真）を取得する
    /// <summary>
    /// 写真番号によりOBS添付ファイル（写真）を取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>OBS添付ファイル（写真）</returns>
    public ObsAttach? GetObsAttachByImageCode(string num)
    {
        Expression<Func<ObsAttach, bool>> whereExp = item => item.NumNoRevisions == num;
        ObsAttach? obsAttach = unitOfWork.ObsAttachRepository.Get(whereExp);
        return obsAttach;
    }
    #endregion

    #region フォルダコピー
    /// <summary>
    /// フォルダコピー
    /// </summary>
    /// <param name="sourceFolder">ソースフォルダー</param>
    /// <param name="destFolder">移動先フォルダ</param>
    public void CopyFolder(string sourceFolder, string destFolder)
    {
        if (!Directory.Exists(destFolder))
        {
            Directory.CreateDirectory(destFolder);
        }

        string[] files = Directory.GetFiles(sourceFolder);
        foreach (string file in files)
        {
            string name = Path.GetFileName(file);
            string dest = Path.Combine(destFolder, name);
            File.Copy(file, dest, true);
        }

        string[] folders = Directory.GetDirectories(sourceFolder);
        foreach (string folder in folders)
        {
            string name = Path.GetFileName(folder);
            string dest = Path.Combine(destFolder, name);
            CopyFolder(folder, dest);
        }
    }
    #endregion

    #region obsAttachを挿入
    /// <summary>
    /// obsAttachを挿入
    /// </summary>
    /// <param name="obsAttachs">リクエスト obsAttach のリスト</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool? InsertObsAttachs(List<ObsAttachRequestDto> obsAttachs)
    {

        var obsAttachsHaveClear = new List<ObsFactNotDependOnRevisions>();
        foreach (var obsAttachRes in obsAttachs)
        {
            var obsAttach = mapper.Map<ObsAttach>(obsAttachRes);
            if (obsAttachsHaveClear.Count > 0 && obsAttachsHaveClear.Find((x) => x.FactNum == obsAttach.FactNum && x.NumNoRevisions == obsAttach.NumNoRevisions) == null)
            {
                unitOfWork.ObsAttachRepository.DeleteBy((x) => x.FactNum == obsAttach.FactNum && x.NumNoRevisions == obsAttach.NumNoRevisions);

                int maxNum = unitOfWork.ObsAttachRepository.MaxBy((x) => x.SerialNum, (x) => x.FactNum == obsAttachs[0].FactNum && x.NumNoRevisions == obsAttachs[0].NumNoRevisions) + 1;

                obsAttachsHaveClear.Add(new ObsFactNotDependOnRevisions
                {
                    FactNum = obsAttach.FactNum,
                    NumNoRevisions = obsAttach.NumNoRevisions is null ? string.Empty : obsAttach.NumNoRevisions,
                    MaxNum = maxNum,
                });
            }
            //そのserialNumがゼロの場合、それは新しいものです
            if (obsAttachRes.SerialNum == 0)
            {
                ObsFactNotDependOnRevisions? currentObsFact = null;
                if (obsAttachsHaveClear.Count > 0) currentObsFact = obsAttachsHaveClear.Find((x) => x.FactNum == obsAttach.FactNum && x.NumNoRevisions == obsAttach.NumNoRevisions);
                if (currentObsFact != null)
                {
                    currentObsFact.MaxNum++;
                    obsAttach.FileName = obsAttach.NumNoRevisions + @"-" + currentObsFact.MaxNum;
                    obsAttach.SerialNum = currentObsFact.MaxNum;
                }
            }
            unitOfWork.ObsAttachRepository.Add(obsAttach);
        }
        return false;
    }

    #endregion
}