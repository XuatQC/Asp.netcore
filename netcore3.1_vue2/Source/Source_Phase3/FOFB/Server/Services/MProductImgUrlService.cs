using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Utility;

namespace FOFB.Server.Services
{
	public class MProductImgUrlService : BaseService, IMProductImgUrlService
	{
		public MProductImgUrlService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MProductImgUrl mProductImgUrl)
		{
			throw new NotImplementedException();
		}


        public Task<bool> DeleteAsync(List<MProductImgUrl> mProductImgUrls)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 条件によるMProductImgUrl一覧を取得する
		/// </summary>
		/// <param name="mProductImgUrl"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MProductImgUrl>> GetAllAsync(MProductImgUrl mProductImgUrl, string sortColumnName, string sortType)
		{
			return UnitOfWork.MProductImgUrl.GetAllAsync(mProductImgUrl, sortColumnName, sortType);
		}

		public Tuple<List<MProductImgUrl>, int> GetDataPagination(int currentpage, int pageSize, MProductImgUrl mProductImgUrl, string sortColumnName, string sortType)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MProductImgUrl> mProductImgUrls)
		{
			throw new NotImplementedException();
		}
	}
}
