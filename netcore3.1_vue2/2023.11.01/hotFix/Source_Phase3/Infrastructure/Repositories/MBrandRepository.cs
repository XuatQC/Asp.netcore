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
    public class MBrandRepository : BaseRepository, IMBrandRepository
	{
		public MBrandRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}
		/// <summary>
		/// MBrandÇí«â¡Ç∑ÇÈ
		/// </summary>
		/// <param name="mbrand"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MBrand mbrand)
		{
			long result = ParamConst.ResultInsertBrand.INSERT_FAIL;
			try
			{
				using (IDbConnection con = this.DbConn)
				{
					result = await con.InsertAsync(mbrand);
					return result;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return result;
			}
		}

		public Task<bool> DeleteAsync(List<MBrand> mbrand)
		{
			throw new NotImplementedException();
		}

		#region èåèÇ…ÇÊÇÈMBrandàÍóóÇéÊìæÇ∑ÇÈ
		/// <summary>
		/// èåèÇ…ÇÊÇÈMBrandàÍóóÇéÊìæÇ∑ÇÈ
		/// </summary>
		/// <param name="mbrand"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MBrand>> GetAllAsync(MBrand mbrand, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mbrandParam = new ExpandoObject();
				

				if (!string.IsNullOrEmpty(mbrand.BrandCd))
				{
					condition.Append(" AND BrandCd = @BrandCd");
					mbrandParam.BrandCd = mbrand.BrandCd;
				}

				if (!string.IsNullOrEmpty(mbrand.BrandName))
				{
					condition.Append(" AND BrandName = @BrandName");
					mbrandParam.BrandName = mbrand.BrandName;
				}

				if (!string.IsNullOrEmpty(mbrand.SubBrand))
				{
					condition.Append(" AND SubBrand = @SubBrand");
					mbrandParam.SubBrand = mbrand.SubBrand;
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
										BrandCd
										,BrandName
										,SubBrand
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM mbrand
									WHERE 1=1
									{condition}");
				return (List<MBrand>)await this.DbConn.QueryAsync<MBrand>(sqlGetData.ToString(), (object)mbrandParam);
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

		#region èåèÇ…ÇÊÇÈMBrandSubàÍóóÇéÊìæÇ∑ÇÈ
		/// <summary>
		/// èåèÇ…ÇÊÇÈMBrandSubàÍóóÇéÊìæÇ∑ÇÈ
		/// </summary>
		/// <param name="mBrandSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MBrandSub>> GetAllSubAsync(MBrandSub mBrandSub, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mbrandSubParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(mBrandSub.BussinessCd))
				{
					condition.Append(" AND mbr.BussinessCd = @BussinessCd");
					mbrandSubParam.BussinessCd = mBrandSub.BussinessCd;
				}

				condition.Append($" AND mb.DelFlg = {ParamConst.DelFlg.OFF}");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT
										mb.BrandCd
										, mb.BrandName
										, mb.SubBrand
										, mbr.BussinessCd
										, mbr.BrandRltId
									FROM mbrand mb
									INNER JOIN mbrandrelation mbr
										ON mb.BrandCd = mbr.BrandCd
										AND mbr.DelFlg = { ParamConst.DelFlg.OFF }
									WHERE 1=1
										{condition}");
				return (List<MBrandSub>)await this.DbConn.QueryAsync<MBrandSub>(sqlGetData.ToString(), (object)mbrandSubParam);
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

		public Tuple<List<MBrand>, int> GetDataPagination(int currentpage, int pageSize, MBrand mbrand, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MBrand> mbrand)
		{
			throw new NotImplementedException();
		}

        public long AddAsyncSub(MBrand mBrand, string bussinessCd)
        {
            throw new NotImplementedException();
        }
    }
}