using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class TranslateModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/translate/", GetTranslate)
                     .Produces<Translate>(200);

            endpoints.MapGet("/translate/list", FinManyHandler)
                     .Produces<IEnumerable<Translate>>(200);

            endpoints.MapGet("/translate/offer-num", GetOfferNumList)
                     .Produces<IEnumerable<string>>(200);

            endpoints.MapGet("/translate/is-trans-comp-confirm/{num}", IsTransCompConfirm)
                     .Produces<IEnumerable<string>>(200);

            endpoints.MapPost("/translate/{pTableName}/{gPlantName}", CreateTransData)
                     .Produces<bool>(200);

            endpoints.MapGet("/translate/load/{gPlantName}", LoadTranslateData)
                     .Produces<bool>(200);
            return endpoints;

        }

        #region 翻訳情報取得
        /// <summary>
        /// 翻訳情報取得
        /// </summary>
        /// <param name="translateService">奉仕</param>
        /// <returns>WORDファイル格納情報ファイル</returns>
        private R<VwTranslateControl> GetTranslate([AsParameters] FindTranslateRequestDto findTranslateDto, [FromServices] ITranslateService translateService)
        {
            var response = translateService.GetTranslate(findTranslateDto);
            return new R<VwTranslateControl>()
            {
                Payload = response
            };
        }
        #endregion

        #region 条件で一覧を取得する
        /// <summary>
        /// 条件で一覧を取得する
        /// </summary>
        /// <param name="findTranslateDto">検索条件</param>
        /// <param name="translateService">奉仕</param>
        /// <returns>WORDファイル格納情報ファイル一覧</returns>
        private R<IEnumerable<VwTranslateControl>> FinManyHandler([AsParameters] FindTranslateRequestDto findTranslateDto, [FromServices] ITranslateService translateService)
        {
            var response = translateService.GetMany(findTranslateDto);
            return new R<IEnumerable<VwTranslateControl>>()
            {
                Payload = response
            };
        }
        #endregion

        #region OfferNum一覧を取得する
        /// <summary>
        /// OfferNum一覧を取得する
        /// </summary>
        /// <param name="findTranslateDto">検索条件</param>
        /// <param name="translateService">奉仕</param>
        /// <returns>OfferNum一覧</returns>
        private R<IEnumerable<OfferNumResponseDto>> GetOfferNumList([AsParameters] FindTranslateRequestDto findTranslateDto, [FromServices] ITranslateService translateService)
        {
            var response = translateService.GetOfferNumList(findTranslateDto);
            return new R<IEnumerable<OfferNumResponseDto>>()
            {
                Payload = response
            };
        }
        #endregion

        #region 翻訳データ作成
        /// <summary>
        /// 翻訳データ作成
        /// </summary>
        /// <param name="findTranslateDto">WORDファイル格納情報ファイル</param>
        /// <param name="pTableName">テーブル名</param>
        /// <param name="gPlantName">プラント名</param>
        /// <param name="translateService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private R<bool> CreateTransData(FindTranslateRequestDto findTranslateDto, string pTableName, string gPlantName, [FromServices] ITranslateService translateService)
        {
            var response = translateService.CreateTransData(findTranslateDto, pTableName, gPlantName);
            return new R<bool>()
            {
                Payload = response
            };
        }
        #endregion

        #region 翻訳が完了したか確認
        /// <summary>
        /// 翻訳が完了したか確認
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="translateService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private R<bool> IsTransCompConfirm(string num, [FromServices] ITranslateService translateService)
        {
            var response = translateService.IsTransCompConfirm(num);
            return new R<bool>()
            {
                Payload = response
            };
        }
        #endregion

        #region 翻訳データ取得
        /// <summary>
        /// 翻訳データ取得
        /// </summary>
        /// <param name="gPlantName">プラント名</param>
        /// <param name="translateService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private R<bool> LoadTranslateData(string gPlantName, [FromServices] ITranslateService translateService)
        {
            var result = translateService.LoadTranslateData(gPlantName);
            return new R<bool>()
            {
                Payload = result,
            };
        }
        #endregion
    }
}