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

	public class MAddressRepository : BaseRepository, IMAddressRepository
	{
		public MAddressRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region 条件によるMAddress一覧を取得する
		/// <summary>
		/// 条件によるMAddress一覧を取得する
		/// </summary>
		/// <param name="maddress"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MAddress>> GetAllAsync(MAddress maddress, string sortColumnName, string sortType)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic maddressParam = new ExpandoObject();
				

				if (maddress.AddrId > 0)
				{
					condition.Append(" AND AddrId = @AddrId");
					maddressParam.AddrId = maddress.AddrId;
				}
				
				if (!string.IsNullOrEmpty(maddress.ZipCd))
				{
					condition.Append(" AND ZipCd = @ZipCd");
					maddressParam.ZipCd = maddress.ZipCd;
				}
				
				if (!string.IsNullOrEmpty(maddress.Province))
				{
					condition.Append(" AND Province = @Province");
					maddressParam.Province = maddress.Province;
				}
				
				if (!string.IsNullOrEmpty(maddress.AdrCd1))
				{
					condition.Append(" AND AdrCd1 = @AdrCd1");
					maddressParam.AdrCd1 = maddress.AdrCd1;
				}
				
				if (!string.IsNullOrEmpty(maddress.AdrCd2))
				{
					condition.Append(" AND AdrCd2 = @AdrCd2");
					maddressParam.AdrCd2 = maddress.AdrCd2;
				}
				
				if (!string.IsNullOrEmpty(maddress.AdrCd3))
				{
					condition.Append(" AND AdrCd3 = @AdrCd3");
					maddressParam.AdrCd3 = maddress.AdrCd3;
				}
				
				if (!string.IsNullOrEmpty(maddress.AdrType))
				{
					condition.Append(" AND AdrType = @AdrType");
					maddressParam.AdrType = maddress.AdrType;
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
										AddrId
										,ZipCd
										,Province
										,AdrCd1
										,AdrCd2
										,AdrCd3
										,AdrType
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM maddress
									WHERE 1=1
									{condition}");
					return (List<MAddress>)await this.DbConn.QueryAsync<MAddress>(sqlGetData.ToString(), (object)maddressParam);
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

		public Tuple<List<MAddress>, int> GetDataPagination(int currentpage, int pageSize, MAddress maddress, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region MAddressを更新する
		/// <summary>
		/// MAddressを更新する
		/// </summary>
		/// <param name="mAddresses"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<MAddress> mAddresses)
		{
			try
			{
				return await this.DbConn.UpdateAsync(mAddresses);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return false;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion

		#region MAddressを追加する
		/// <summary>
		/// MAddressを追加する
		/// </summary>
		/// <param name="maddress"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MAddress maddress)
		{
			try
			{
				return await this.DbConn.InsertAsync(maddress);
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return 0;
			}
			finally
			{
				this.DbConn.Close();
			}
		}
		#endregion

		public Task<bool> DeleteAsync(List<MAddress> mAddresses)
		{
			throw new NotImplementedException();
		}
	}
}