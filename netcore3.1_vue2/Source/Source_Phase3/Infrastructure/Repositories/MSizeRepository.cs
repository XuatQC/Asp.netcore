using Dapper;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Infrastructure.Repositories
{

	public class MSizeRepository : BaseRepository, IMSizeRepository
	{
		public MSizeRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(MSize entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MSize> entity)
		{
			throw new NotImplementedException();
		}

		#region èåèÇ…ÇÊÇÈMSizeàÍóóÇéÊìæÇ∑ÇÈ
		/// <summary>
		/// èåèÇ…ÇÊÇÈMSizeàÍóóÇéÊìæÇ∑ÇÈ
		/// </summary>
		/// <param name="msize"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MSize>> GetAllAsync(MSize msize, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic msizeParam = new ExpandoObject();
				

				if (!string.IsNullOrEmpty(msize.SizeCd))
				{
					condition.Append(" AND SizeCd = @SizeCd");
					msizeParam.SizeCd = msize.SizeCd;
				}
				
				if (!string.IsNullOrEmpty(msize.SizeName))
				{
					condition.Append(" AND SizeName = @SizeName");
					msizeParam.SizeName = msize.SizeName;
				}

				condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF}");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT
										SizeCd
										,SizeName
										,SortIndex
										,TypeKbn
										,Category
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM msize
									WHERE 1=1
									{condition}");
				return (List<MSize>)await this.DbConn.QueryAsync<MSize>(sqlGetData.ToString(), (object)msizeParam);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return null;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion

		public Tuple<List<MSize>, int> GetDataPagination(int currentpage, int pageSize, MSize entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MSize> entity)
		{
			throw new NotImplementedException();
		}
	}
}