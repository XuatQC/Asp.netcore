using FNDSystem.Core.Domain.Entities;
using Modules.Shared;

namespace FNDSystem.API.Modules.CurrentDirectory
{
    public class Directory : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/directory/", GetCurrentDirectory)
                .Produces<IEnumerable<PeerReview>>(200);
            return endpoints;
        }

        #region 現在のディレクトリを取得します
        /// <summary>
        /// 現在のディレクトリを取得します
        /// </summary>
        /// <param name="hostEnvironment">検索条件</param>
        /// <returns>現在のディレクトリ</returns>
        public IResult GetCurrentDirectory(IHostEnvironment hostEnvironment)
        {
            string diskPath = hostEnvironment.ContentRootPath.Replace(hostEnvironment.ApplicationName, "").Replace(@"\\",@"\");
            return Results.Ok(new R<string>()
            {
                Code = 200,
                Payload = diskPath
            });
        }

        #endregion
    }
}
