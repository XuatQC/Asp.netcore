using FluentValidation;
using FluentValidation.Results;
using FNDSystem.API.Modules;
using FNDSystem.Core;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Dto.OBS;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Modules.Shared;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FNDSystem
{
    public class ObsModule : ControllerBase, IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            #region obs

            endpoints.MapGet("/obs/{num}", GetObsByNum)
                     .Produces<Obs>(200);

            endpoints.MapGet("/obs/list/revision", GetObsRevisionList)
                     .Produces<IEnumerable<NumberControl>>(200);

            endpoints.MapGet("/obs/list/multi-condition", GetObsListByMultiCondition)
                     .Produces<IEnumerable<Obs>>(200);

            endpoints.MapGet("/obs/list/req-tran2", GetObsListForReqTran2)
                     .Produces<IEnumerable<Obs>>(200);

            endpoints.MapGet("/obs/max-no", GetMaxNo)
                     .Produces<int>(200);

            endpoints.MapPost("/obs/add-new", AddNewObsAtScreenList)
                     .Produces<Obs>(200);

            endpoints.MapPost("/obs", CreateHandler)
                     .Produces<Obs>(200);

            endpoints.MapPut("/obs/{num}", Update)
                      .Produces<Obs>(200);

            endpoints.MapDelete("/obs", Delete)
                     .Produces<bool>(200);

            endpoints.MapDelete("/obs/wkt-tables", DeleteDataOfWktTables)
                     .Produces<bool>(200);

            endpoints.MapPost("/obs/save-process/", ObsSaveProcess)
                     .Produces<string>(200);

            endpoints.MapPost("/obs/work-table-conversion/{num}/{transSource}", ObsWorkTableConversion)
                     .Produces<bool>(200);

            endpoints.MapGet("obs/get-num/{plantName}/{fields}", ObsGetNum)
                     .Produces<NumObsDto>(200);

            #endregion

            #region obs_extend

            endpoints.MapGet("/obs/extend/list/only-obs-extend", GetOnlyObsExtendList)
                     .Produces<IEnumerable<DisObsReponseDto>>(200);

            endpoints.MapGet("/obs/extend/list", GetObsExtendList)
                     .Produces<IEnumerable<DisObsReponseDto>>(200);

            endpoints.MapGet("/obs/extend/clear-list", GetObsExtendListWhenClearClick)
                     .Produces<IEnumerable<DisObsReponseDto>>(200);

            endpoints.MapGet("/obs/extend/count", GetCountObsExtends)
                     .Produces<int>(200);

            endpoints.MapGet("/obs/extend/{num}", GetOnlyObsExtend)
                     .Produces<ObsExtend>(200);

            #endregion

            #region obs_extend_refer

            endpoints.MapGet("/obs/extend-refer/get-num", GetObsExtendReferNum)
                     .Produces<string>(200);

            endpoints.MapGet("/obs/extend-refer/list", GetObsExtendReferListByNum)
                     .Produces<IEnumerable<ObsExtendReferDto>>(200);

            #endregion

            #region obs_download_output_word

            endpoints.MapPost("/obs/reports/output-word", DownloadOutputWord)
                     .Produces<IResult>(200);

            #endregion

            return endpoints;
        }

        #region WORD帳票をダウンロードする
        /// <summary>
        /// WORD帳票をダウンロードする
        /// </summary>
        /// <param name="obsInfo">検索条件</param>
        /// <param name="oBSReportService">奉仕</param>
        /// <returns>ファイル</returns>
        private IResult DownloadOutputWord([FromBody] OutputWordDto obsInfo, IObsReportService oBSReportService, Serilog.ILogger logger)
        {
            logger.Information("========= Begin Report payload =========" + Environment.NewLine + JsonSerializer.Serialize(obsInfo) + Environment.NewLine + "========= End Report payload =========");
            return oBSReportService.DownloadOutputWordReport(obsInfo);
        }

        #endregion

        #region Obs

        #region 番号によりOBSを取得する
        /// <summary>
        /// 番号によりOBSを取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>obs</returns>
        private R<Obs> GetObsByNum(string num, [FromServices] IObsService obsService)
        {
            var response = obsService.GetObsByNum(num);

            return new R<Obs>()
            {
                Payload = response
            };
        }
        #endregion

        #region OBSリビジョン一覧を取得する
        /// <summary>
        /// OBSリビジョン一覧を取得する
        /// </summary>
        /// <param name="findObsDto">検索条件</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>OBS一覧</returns>
        private R<IEnumerable<NumberControl>?> GetObsRevisionList([AsParameters] FindObsRequestDto findObsDto, [FromServices] IObsService obsService)
        {
            var response = obsService.GetObsRevisionList(findObsDto);

            return new R<IEnumerable<NumberControl>?>()
            {
                Payload = response
            };
        }
        #endregion

        #region 複数条件でOBSを取得する
        /// <summary>
        /// 複数条件でOBSを取得する
        /// </summary>
        /// <param name="findObsDto">検索条件</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>OBS一覧</returns>
        private R<IEnumerable<Obs>> GetObsListByMultiCondition([AsParameters] FindObsRequestDto findObsDto, [FromServices] IObsService obsService)
        {
            var response = obsService.GetObsListByMultiCondition(findObsDto);

            return new R<IEnumerable<Obs>>()
            {
                Payload = response
            };
        }
        #endregion

        #region 翻訳依頼２へのOBS一覧を取得する
        /// <summary>
        /// 翻訳依頼２へのOBS一覧を取得する
        /// </summary>
        /// <param name="findObsDto">検索条件</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>OBS一覧</returns>
        private R<IEnumerable<Obs>> GetObsListForReqTran2([AsParameters] FindObsRequestDto findObsDto, [FromServices] IObsService obsService)
        {
            var response = obsService.GetObsListForReqTran2(findObsDto);

            return new R<IEnumerable<Obs>>()
            {
                Payload = response
            };
        }
        #endregion

        #region MAX数値を取得する
        /// <summary>
        /// MAX数値を取得する
        /// </summary>
        /// <param name="obsDto">検索条件</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>MAX数値</returns>
        private R<int> GetMaxNo([AsParameters] FindObsRequestDto obsDto, [FromServices] IObsService obsService)
        {
            var response = obsService.GetMaxNo(obsDto);

            return new R<int>()
            {
                Payload = response
            };
        }
        #endregion

        #region CreateHandler
        /// <summary>
        /// データ新規作成
        /// </summary>
        /// <param name="obsDto">更新するデータ</param>
        /// <param name="obsService">奉仕</param>
        /// <param name="validator">バリデーター</param>
        /// <returns>新しいob</returns>
        private IResult CreateHandler(ObsDto obsDto, [FromServices] IObsService obsService, IValidator<ObsDto> validator)
        {
            ValidationResult result = validator.Validate(obsDto);

            if (result.IsValid)
            {
                var response = obsService.Create(obsDto);

                return Results.Ok(new R<Obs>()
                {
                    Payload = response
                });
            }
            else
            {
                return Results.BadRequest(new R<Obs>()
                {
                    Code = 400,
                    Message = result.Errors[0].ErrorMessage,
                });
            }
        }
        #endregion

        #region OBSデータ削除
        /// <summary>
        /// OBSデータ削除
        /// </summary>
        /// <param name="deleteObsDto">データを削除する</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private R<bool> Delete([AsParameters] DeleteObsDto deleteObsDto, [FromServices] IObsService obsService)
        {
            var response = obsService.Delete(deleteObsDto);

            return new R<bool>()
            {
                Payload = response
            };
        }
        #endregion

        #region wktテーブルのデータを削除する
        /// <summary>
        /// wktテーブルのデータを削除する
        /// </summary>
        /// <param name="obsService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private R<bool> DeleteDataOfWktTables([FromServices] IObsService obsService)
        {
            var response = obsService.DeleteDataOfWktTables();

            return new R<bool>()
            {
                Payload = response
            };
        }
        #endregion

        #region 番号によりOBSを更新する
        /// <summary>
        /// 番号によりOBSを更新する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obs">Obs</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>新しいObs</returns>
        private R<Obs> Update(string num, ObsExtend obs, [FromServices] IObsService obsService)
        {
            var response = obsService.UpdateByNum(num, obs);
            return new R<Obs>()
            {
                Payload = response
            };
        }
        #endregion

        #region OBS新規作成
        /// <summary>
        /// OBS新規作成
        /// </summary>
        /// <param name="obsDto">Obs</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>新しいObs</returns>
        private R<Obs?> AddNewObsAtScreenList(FindObsRequestDto obsDto, [FromServices] IObsService obsService)
        {
            var response = obsService.AddNewObsAtScreenList(obsDto);

            return new R<Obs?>()
            {
                Payload = response
            };
        }
        #endregion

        #region obs 保存プロセス
        /// <summary>
        /// obs 保存プロセス
        /// </summary>
        /// <param name="requestHeaderDto"></param>
        /// <param name="request"></param>
        /// <param name="hostEnvironment"></param>
        /// <param name="obsService">奉仕</param>
        /// <returns>番号</returns>
        private R<string> ObsSaveProcess([FromHeader] RequestHeaderDto requestHeaderDto, HttpRequest request, IHostEnvironment hostEnvironment,
            [FromServices] IObsService obsService)
        {
            string path = Directory.GetCurrentDirectory();
            var photoDirectory = request.Form["photoPath"];
            string photoPath = string.Format("{0}\\wwwroot\\{1}\\", path, photoDirectory);
            string diskPath = photoPath.Replace(@"/",@"\");
            var directory = Path.GetDirectoryName(diskPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) Directory.CreateDirectory(directory);

            // データから値を準備します
            var facts = request.Form["facts[]"];
            var conclusions = request.Form["conclusions[]"];
            var obs = request.Form["obs"];
            var isNewFlagRequest = request.Form["isNewFlag"];
            
            // C# オブジェクトに変換し直します
            ObsSaveDto obsSave = new ObsSaveDto();
            ObsRequestDto? obsRequestDto = JsonSerializer.Deserialize<ObsRequestDto>(obs!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (obsRequestDto != null) obsSave.Obs = obsRequestDto;
            obsSave.IsNewFlg = JsonSerializer.Deserialize<bool>(isNewFlagRequest!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //obsFact と obs結論を変換します
            for (var index = 0; index < facts.Count; index++)
            {
                var factRequest = JsonSerializer.Deserialize<ObsFactRequestDto>(facts[index]!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (factRequest != null) obsSave.Facts.Add(factRequest);
            }
            for (var index = 0; index < conclusions.Count; index++)
            {
                var conclusionRequest = JsonSerializer.Deserialize<ObsConclusionRequestDto>(conclusions[index]!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (conclusionRequest != null) obsSave.Conclusions.Add(conclusionRequest);
            }
            // サービスを呼び出して API を保存する
            var response = obsService.ObsSaveProcess(requestHeaderDto.UserInitial, obsSave, diskPath);

            return new R<string>()
            {
                Payload = response
            };
        }

        #endregion

        #region テーブルObsWorkの変換
        /// <summary>
        /// テーブルObsWorkの変換
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="transSource">ソースを翻訳する</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private R<bool> ObsWorkTableConversion(string num, string transSource, [FromServices] IObsService obsService)
        {
            var response = obsService.ObsWorkTableConversion(num, transSource);
            return new R<bool>()
            {
                Payload = response
            };
        }

        #endregion

        #endregion

        #region OBSの番号とタイトルを取得する
        /// <summary>
        /// OBSの番号とタイトルを取得する
        /// </summary>
        /// <param name="plantName">検索条件</param>
        /// <param name="fields">検索条件</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>OBSの番号とタイトル</returns>
        private R<IEnumerable<NumObsDto>> ObsGetNum(string plantName, string fields, IObsService obsService)
        {

            var response = obsService.GetNum(plantName, fields);
            return new R<IEnumerable<NumObsDto>>()
            {
                Payload = response
            };
        }

        #endregion

        #region ObsExtend

        #region ObsExtend一覧取得
        /// <summary>
        /// ObsExtend一覧取得
        /// </summary>
        /// <param name="obsExtendDto">検索条件</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>OBS一覧</returns>
        private R<IEnumerable<DisObsReponseDto>> GetObsExtendList([AsParameters] FindObsRequestDto obsExtendDto, [FromServices] IObsService obsService)
        {
            var response = obsService.GetObsExtendList(obsExtendDto);
            return new R<IEnumerable<DisObsReponseDto>>()
            {
                Payload = response
            };
        }
        #endregion

        #region ObsExtendテーブルのデータを取得する
        /// <summary>
        /// ObsExtendテーブルのデータを取得する
        /// </summary>
        /// <param name="obsExtendDto">検索条件</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>obsextend一覧</returns>
        private R<IEnumerable<VwObsControl>> GetOnlyObsExtendList([AsParameters] FindObsRequestDto obsExtendDto, [FromServices] IObsService obsService)
        {
            var response = obsService.GetOnlyObsExtendList(obsExtendDto);
            return new R<IEnumerable<VwObsControl>>()
            {
                Payload = response
            };
        }
        #endregion

        #region クリアボタン押下時にObsExtend一覧を取得する
        /// <summary>
        /// クリアボタン押下時にObsExtend一覧を取得する
        /// </summary>
        /// <param name="obsExtendDto">検索条件</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>OBS一覧</returns>
        private R<IEnumerable<DisObsReponseDto>> GetObsExtendListWhenClearClick([AsParameters] FindObsRequestDto obsExtendDto, [FromServices] IObsService obsService)
        {
            var response = obsService.GetObsExtendListWhenClearClick(obsExtendDto);
            return new R<IEnumerable<DisObsReponseDto>>()
            {
                Payload = response
            };
        }
        #endregion

        #region ObsExtendの件数を取得する
        /// <summary>
        /// ObsExtendの件数を取得する
        /// </summary>
        /// <param name="obsExtendDto">検索条件</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>オブセクテンドの記録</returns>
        private R<int> GetCountObsExtends([AsParameters] FindObsRequestDto obsExtendDto, [FromServices] IObsService obsService)
        {
            var response = obsService.GetCountObsExtendList(obsExtendDto);
            return new R<int>()
            {
                Payload = response
            };
        }
        #endregion

        #region ObsExtendテーブルのデータを取得する
        /// <summary>
        /// ObsExtendテーブルのデータを取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsService">奉仕</param>
        /// <returns>ObsExtend</returns>
        private R<ObsExtend?> GetOnlyObsExtend(string num, [FromServices] IObsService obsService)
        {
            var response = obsService.GetOnlyObsExtend(num);
            return new R<ObsExtend?>()
            {
                Payload = response
            };
        }
        #endregion

        #endregion

        #region Obs_Extend_Refer

        #region ObsExtendReferの番号を取得する
        /// <summary>
        /// ObsExtendReferの番号を取得する
        /// </summary>
        /// <param name="findObsExtenReferDto">検索条件</param>
        /// <param name="obsExtenReferService">奉仕</param>
        /// <returns>番号</returns>
        private R<string> GetObsExtendReferNum([AsParameters] FindObsRequestDto findObsExtenReferDto, [FromServices] IObsService obsExtenReferService)
        {
            var response = obsExtenReferService.GetObsExtendReferNum(findObsExtenReferDto);
            return new R<string>()
            {
                Payload = response
            };
        }
        #endregion

        #region 番号によりObsExtendRefer一覧を取得する
        /// <summary>
        /// 番号によりObsExtendRefer一覧を取得する
        /// </summary>
        /// <param name="findObsExtenReferDto">検索条件</param>
        /// <param name="obsExtenReferService">奉仕</param>
        /// <returns>OBSExtend一覧</returns>
        private R<IEnumerable<ObsExtendReferDto>> GetObsExtendReferListByNum([AsParameters] FindObsExtendReferRequestDto findObsExtenReferDto, [FromServices] IObsService obsExtenReferService)
        {
            var response = obsExtenReferService.GetObsExtendReferListByNum(findObsExtenReferDto);
            return new R<IEnumerable<ObsExtendReferDto>>()
            {
                Payload = response
            };
        }
        #endregion

        #endregion
    }
}