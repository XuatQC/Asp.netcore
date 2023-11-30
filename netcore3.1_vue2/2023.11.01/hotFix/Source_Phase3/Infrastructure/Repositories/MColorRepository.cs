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
	public class MColorRepository : BaseRepository, IMColorRepository
	{
		public MColorRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(MColor mcolor)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MColor> mcolor)
		{
			throw new NotImplementedException();
		}

		#region èåèÇ…ÇÊÇÈMColoràÍóóÇéÊìæÇ∑ÇÈ
		/// <summary>
		/// èåèÇ…ÇÊÇÈMColoràÍóóÇéÊìæÇ∑ÇÈ
		/// </summary>
		/// <param name="mcolor"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MColor>> GetAllAsync(MColor mcolor, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mcolorParam = new ExpandoObject();


				if (!string.IsNullOrEmpty(mcolor.ColorCd))
				{
					condition.Append(" AND ColorCd = @ColorCd");
					mcolorParam.ColorCd = mcolor.ColorCd;
				}

				if (!string.IsNullOrEmpty(mcolor.ColorName))
				{
					condition.Append(" AND ColorName = @ColorName");
					mcolorParam.ColorName = mcolor.ColorName;
				}

				if (!string.IsNullOrEmpty(mcolor.ColorGroup))
				{
					condition.Append(" AND ColorGroup = @ColorGroup");
					mcolorParam.ColorGroup = mcolor.ColorGroup;
				}

				condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF}");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	 
											ColorCd
											,ColorName
											,SortIndex
											,ColorGroup
											,CreateUserCd
											,CreateDtTm
											,UpdateUserCd
											,UpdateDtTm
											,DelFlg
									FROM mcolor
									WHERE 1=1
									{condition}");
				return (List<MColor>)await this.DbConn.QueryAsync<MColor>(sqlGetData.ToString(), (object)mcolorParam);
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

		public Tuple<List<MColor>, int> GetDataPagination(int currentpage, int pageSize, MColor mcolor, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MColor> mcolor)
		{
			throw new NotImplementedException();
		}
	}
}