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
	public class MBankRepository : BaseRepository, IMBankRepository
	{
		public MBankRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(MBank entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MBank> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMbank一覧を取得する
		/// <summary>
		/// 条件によるMbank一覧を取得する
		/// </summary>
		/// <param name="mbank"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MBank>> GetAllAsync(MBank mbank, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mbankParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(mbank.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					mbankParam.BussinessCd = mbank.BussinessCd;
				}

				condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF}");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT
										BussinessCd
										, BankName
										, AccountNumber
										, AccountName
										, Branch
										, CreateUserCd
										, CreateDtTm
										, UpdateUserCd
										, UpdateDtTm
										, DelFlg
									FROM mbank
									WHERE 1=1
										{condition}");
				return (List<MBank>)await this.DbConn.QueryAsync<MBank>(sqlGetData.ToString(), (object)mbankParam);
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

		public Tuple<List<MBank>, int> GetDataPagination(int currentpage, int pageSize, MBank entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MBank> entity)
		{
			throw new NotImplementedException();
		}
	}
}
