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
    public class MSubBrandRepository : BaseRepository, IMSubBrandRepository
	{
		public MSubBrandRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}
		/// <summary>
		/// MSubBrandを追加する
		/// </summary>
		/// <param name="mbrand"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MSubBrand mbrand)
		{
			long result = -1;
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
		public Task<bool> DeleteAsync(List<MSubBrand> mbrand)
		{
			throw new NotImplementedException();
		}
		#region 条件によるMBrand一覧を取得する
		/// <summary>
		/// 条件によるMBrand一覧を取得する
		/// </summary>
		/// <param name="mSubBrand"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MSubBrand>> GetAllAsync(MSubBrand mSubBrand, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mSubBrandParam = new ExpandoObject();
				

				if (!string.IsNullOrEmpty(mSubBrand.BrandCd))
				{
					condition.Append(" AND BrandCd = @BrandCd");
					mSubBrandParam.BrandCd = mSubBrand.BrandCd;
				}

				if (!string.IsNullOrEmpty(mSubBrand.SubBrand))
				{
					condition.Append(" AND SubBrand = @SubBrand");
					mSubBrandParam.SubBrand = mSubBrand.SubBrand;
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
										 SubBrandId
										,BrandCd
										,SubBrand
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM msubbrand
									WHERE 1=1
									{condition}");
				return (List<MSubBrand>)await this.DbConn.QueryAsync<MSubBrand>(sqlGetData.ToString(), (object)mSubBrandParam);
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

		public Tuple<List<MSubBrand>, int> GetDataPagination(int currentpage, int pageSize, MSubBrand mbrand, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MSubBrand> mbrand)
		{
			throw new NotImplementedException();
		}

        public long AddAsyncSub(MSubBrand mBrand, string bussinessCd)
        {
            throw new NotImplementedException();
        }
    }
}