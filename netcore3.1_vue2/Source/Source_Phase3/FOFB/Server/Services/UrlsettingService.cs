using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FOFB.Server.Services
{
	public class UrlSettingService : BaseService, IUrlSettingService
	{
		public UrlSettingService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(UrlSetting urlsetting)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<UrlSetting> urlsettings)
		{
			throw new NotImplementedException();
		}

		#region 条件によるURL一覧を取得する
		/// <summary>
		/// 条件によるURL一覧を取得する
		/// </summary>
		/// <param name="urlsetting"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<UrlSetting>> GetAllAsync(UrlSetting urlsetting, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.UrlSetting.GetAllAsync(urlsetting, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<UrlSetting>, int> GetDataPagination(int currentpage, int pageSize, UrlSetting urlsetting, string sortColumnName, string sortType)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<UrlSetting> urlsettings)
		{
			throw new NotImplementedException();
		}
		#region URLを保存、変更する
		/// <summary>
		/// URLを保存、変更する
		/// </summary>
		/// <param name="urlsettings"></param>
		/// <returns></returns>
		public bool AddOrUpdate(UrlSetting urlsettings)
		{
			return UnitOfWork.UrlSetting.AddOrUpdate(urlsettings);
		}
		#endregion

		#region 存在をチェックする
		/// <summary>
		/// 存在をチェックする
		/// </summary>
		/// <param name="urlsetting"></param>
		/// <returns></returns>
		public bool IsDupplicateUrl(UrlSetting urlsetting)
		{
			return UnitOfWork.UrlSetting.IsDuplicateUrl(urlsetting);
		}
		#endregion
	}
}
