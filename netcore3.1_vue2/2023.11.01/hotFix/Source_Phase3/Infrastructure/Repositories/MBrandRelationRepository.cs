using Dapper;
using Dapper.Contrib.Extensions;
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

	public class MBrandRelationRepository : BaseRepository, IMBrandRelationRepository
	{
		public MBrandRelationRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}
		/// <summary>
		/// MBrandRelationÇí«â¡Ç∑ÇÈ
		/// </summary>
		/// <param name="mbrandrelation"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MBrandRelation mbrandrelation)
		{
			long result = ParamConst.ResultInsertBrand.INSERT_FAIL;
			try
			{
				using (IDbConnection con = this.DbConn)
				{
					result = await con.InsertAsync(mbrandrelation);
					return result;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return result;
			}
		}

		public Task<bool> DeleteAsync(List<MBrandRelation> mbrandrelation)
		{
			throw new NotImplementedException();
		}

		#region èåèÇ…ÇÊÇÈMBrandRelationàÍóóÇéÊìæÇ∑ÇÈ
		/// <summary>
		/// èåèÇ…ÇÊÇÈMBrandRelationàÍóóÇéÊìæÇ∑ÇÈ
		/// </summary>
		/// <param name="mbrandrelation"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MBrandRelation>> GetAllAsync(MBrandRelation mbrandrelation, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mbrandrelationParam = new ExpandoObject();


				if (mbrandrelation.BrandRltId > 0)
				{
					condition.Append(" AND BrandRltId = @BrandRltId");
					mbrandrelationParam.BrandRltId = mbrandrelation.BrandRltId;
				}

				if (!string.IsNullOrEmpty(mbrandrelation.BrandCd))
				{
					condition.Append(" AND BrandCd = @BrandCd");
					mbrandrelationParam.BrandCd = mbrandrelation.BrandCd;
				}

				if (!string.IsNullOrEmpty(mbrandrelation.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					mbrandrelationParam.BussinessCd = mbrandrelation.BussinessCd;
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
										BrandRltId
										,BrandCd
										,BussinessCd
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM Mbrandrelation
									WHERE 1=1
									{condition}");
				return (List<MBrandRelation>)await this.DbConn.QueryAsync<MBrandRelation>(sqlGetData.ToString(), (object)mbrandrelationParam);
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
		public Tuple<List<MBrandRelation>, int> GetDataPagination(int currentpage, int pageSize, MBrandRelation mbrandrelation, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MBrandRelation> mbrandrelation)
		{
			throw new NotImplementedException();
		}
	}
}