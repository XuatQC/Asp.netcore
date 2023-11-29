using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;
using FNDSystem;

namespace FNDSystem
{
    public class PrintModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/print/output-word/pre", PreOutputWord)
                     .Produces<IEnumerable<VwTranslateControl>>(200);

            endpoints.MapPost("/print/output-word/processing", ProcessingOutputWord)
                     .Produces<IEnumerable<VwTranslateControl>>(200);


            return endpoints;
        }

        #region Word出力処理の準備
        /// <summary>
        /// Word出力処理の準備
        /// </summary>
        /// <param name="findTranslateRequestDto">検索条件</param>
        /// <param name="printService">奉仕</param>
        /// <returns>WORDファイル格納情報ファイル一覧</returns>
        private R<IEnumerable<VwTranslateControl>> PreOutputWord([AsParameters] FindTranslateRequestDto findTranslateRequestDto, [FromServices] IPrintService printService)
        {
            var response = printService.PreOutputWord(findTranslateRequestDto);
            return new R<IEnumerable<VwTranslateControl>>()
            {
                Payload = response
            };
        }
        #endregion

        #region Word出力処理
        /// <summary>
        /// Word出力処理
        /// </summary>
        /// <param name="printTranRequesDto">検索条件</param>
        /// <param name="printService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private R<bool?> ProcessingOutputWord(PrintTranRequesDto printTranRequesDto, [FromServices] IPrintService printService)
        {
            var response = printService.ProcessingOutputWord(printTranRequesDto.Language, printTranRequesDto.preTranslateList, printTranRequesDto.obsPathsDto);
            return new R<bool?>()
            {
                Payload = response
            };
        }
        #endregion
    }
}