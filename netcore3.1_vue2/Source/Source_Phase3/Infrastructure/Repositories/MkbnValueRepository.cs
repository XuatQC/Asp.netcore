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

	public class MkbnValueRepository : BaseRepository, IMkbnValueRepository
	{
		public MkbnValueRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(MkbnValue mkbnvalue)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MkbnValue> mkbnvalue)
		{
			throw new NotImplementedException();
		}

		#region 条件による区分値定義一覧を取得する
		/// <summary>
		/// 条件による区分値定義一覧を取得する
		/// </summary>
		/// <param name="mkbnvalue"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MkbnValue>> GetAllAsync(MkbnValue mkbnvalue, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mkbnvalueParam = new ExpandoObject();


				if (!string.IsNullOrEmpty(mkbnvalue.KbnCd))
				{
					condition.Append(" AND KbnCd = @KbnCd");
					mkbnvalueParam.KbnCd = mkbnvalue.KbnCd;
				}
				condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF } ");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	 
											KbnCd
											,KbnName
											,KbnValue
											,KbnValueName
											,NumberValue
											,StringValue
											,CreateUserCd
											,CreateDtTm
											,UpdateUserCd
											,UpdateDtTm
											,DelFlg
									FROM Mkbnvalue
									WHERE 1=1
										{condition}");
					return (List<MkbnValue>)await this.DbConn.QueryAsync<MkbnValue>(sqlGetData.ToString(), (object)mkbnvalueParam);
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

		public Tuple<List<MkbnValue>, int> GetDataPagination(int currentpage, int pageSize, MkbnValue mkbnvalue, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MkbnValue> mkbnvalue)
		{
			throw new NotImplementedException();
		}
		
	}
}