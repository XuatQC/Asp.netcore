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

	public class MBussinessRepository : BaseRepository, IMBussinessRepository
	{
		public MBussinessRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(MBussiness entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MBussiness> mbussiness)
		{
			throw new NotImplementedException();
		}

		#region èåèÇ…ÇÊÇÈMBussinessàÍóóÇéÊìæÇ∑ÇÈ
		/// <summary>
		/// èåèÇ…ÇÊÇÈMBussinessàÍóóÇéÊìæÇ∑ÇÈ
		/// </summary>
		/// <param name="mbussiness"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MBussiness>> GetAllAsync(MBussiness mbussiness, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mbussinessParam = new ExpandoObject();
				

				if (!string.IsNullOrEmpty(mbussiness.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					mbussinessParam.BussinessCd = mbussiness.BussinessCd;
				}
				
				if (!string.IsNullOrEmpty(mbussiness.BussinessName))
				{
					condition.Append(" AND BussinessName = @BussinessName");
					mbussinessParam.BussinessName = mbussiness.BussinessName;
				}
				
				if (!string.IsNullOrEmpty(mbussiness.CreateUserCd))
				{
					condition.Append(" AND CreateUserCd = @CreateUserCd");
					mbussinessParam.CreateUserCd = mbussiness.CreateUserCd;
				}

				condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF}");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					if (!string.IsNullOrEmpty(sortType))
					{
						sortType = "ASC";
					}
					else
					{
						sortType = "DESC";
					}
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT
										BussinessCd
										,BussinessName
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM mbussiness
									WHERE 1=1
									{condition}");
				return (List<MBussiness>)await this.DbConn.QueryAsync<MBussiness>(sqlGetData.ToString(), (object)mbussinessParam);
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

		public Tuple<List<MBussiness>, int> GetDataPagination(int currentpage, int pageSize, MBussiness mbussiness, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MBussiness> mbussiness)
		{
			throw new NotImplementedException();
		}
	}
}