using Dapper;
using Dapper.Contrib.Extensions;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Infrastructure.Repositories
{
	public class MShopRepository : BaseRepository, IMShopRepository
	{
		public MShopRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region MShopを追加する
		/// <summary>
		/// MShopを追加する
		/// </summary>
		/// <param name="mShop"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MShop mShop)
		{
			try
			{
				return await this.DbConn.InsertAsync(mShop);
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

		#region MShopを追加する
		/// <summary>
		/// MShopを追加する
		/// </summary>
		/// <param name="mShops"></param>
		/// <returns></returns>
		public async Task<long> AddListAsync(List<MShop> mShops)
		{
			try
			{
				return await this.DbConn.InsertAsync(mShops);
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

		public Task<bool> DeleteAsync(List<MShop> entity)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMShop一覧を取得する
		/// <summary>
		/// 条件によるMShop一覧を取得する
		/// </summary>
		/// <param name="mShop"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MShop>> GetAllAsync(MShop mShop, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mShopParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(mShop.ShopCd))
				{
					condition.Append(" AND ShopCd = @ShopCd");
					mShopParam.ShopCd = mShop.ShopCd;
				}
				if (!string.IsNullOrEmpty(mShop.ListShopCd))
				{
					condition.Append($" AND ShopCd IN ({ mShop.ListShopCd })");
				}
				if (mShop.AreaCd != 0)
				{
					condition.Append(" AND AreaCd = @AreaCd");
					mShopParam.AreaCd = mShop.AreaCd;
				}
				if (mShop.Area2Cd != 0)
				{
					condition.Append(" AND Area2Cd = @Area2Cd");
					mShopParam.Area2Cd = mShop.Area2Cd;
				}
				if (!string.IsNullOrEmpty(mShop.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					mShopParam.BussinessCd = mShop.BussinessCd;
				}
				if (mShop.Status != null)
				{
					if (!(bool)mShop.Status)
					{
						condition.Append($" AND Status = { ParamConst.ShopStatus.OPEN } ");
					}
					else
					{
						condition.Append($" AND Status = { ParamConst.ShopStatus.CLOSE } ");
					}
				}
				if (mShop.DisplayFlg != null)
				{
					if (!(bool)mShop.DisplayFlg)
					{
						condition.Append($" AND DisplayFlg = { ParamConst.ShowHiddenItem.SHOW } ");
					}
					else
					{
						condition.Append($" AND DisplayFlg = { ParamConst.ShowHiddenItem.HIDDEN } ");
					}
				}
				condition.Append($" AND DelFlg = { ParamConst.DelFlg.OFF } ");
				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT 
										ShopCd
										, ShopName
										, AreaCd
										, Area2Cd
										, AddrId
										, MailAddress
										, PhoneNumber
										, FaxNumber
										, Status
										, StartDate
										, EndDate
										, AffiliateDepartmentCd
										, AffiliateDepartmentName
										, OpenTime
										, Square
										, BussinessCd
										, SalePersonCd
										, SalePersonName
										, UsedDutyFree
										, ContractType
										, DisplayFlg
										, UpdateDtTm
									FROM
										mshop
									WHERE 1=1
									{condition}");
				return (List<MShop>)await this.DbConn.QueryAsync<MShop>(sqlGetData.ToString(), (object)mShopParam);
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

		public Tuple<List<MShop>, int> GetDataPagination(int currentpage, int pageSize, MShop entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region CSVから店舗を追加する
		/// <summary>
		/// CSVから店舗を追加する
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public async Task<long> InsertOrUpdateDataCSV(string sqlQuery)
		{
			try
			{
				return (long)this.DbConn.ExecuteAsync(sqlQuery).Result;
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

		#region 検索条件による店舗一覧
		/// <summary>
		/// 検索条件による店舗一覧
		/// </summary>
		/// <param name="currentpage"></param>
		/// <param name="pageSize"></param>
		/// <param name="mShopSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Tuple<List<MShopSub>, int> GetDataMShopSubPagination(int currentpage, int pageSize, MShopSub mShopSub, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder select = new StringBuilder("");
				StringBuilder table = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mShopSubParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(mShopSub.ShopCd))
				{
					condition.Append($" AND ms.ShopCd LIKE '%{ mShopSub.ShopCd }%'");
				}
				if (!string.IsNullOrEmpty(mShopSub.ShopName))
				{
					condition.Append($" AND ms.ShopName LIKE '%{ mShopSub.ShopName }%'");
				}
				if (!string.IsNullOrEmpty(mShopSub.Address))
				{
					int index = mShopSub.Address.IndexOf(ParamConst.CHARACTER_ADD);
					string address = string.Empty;
					if (index != -1)
					{
						address = mShopSub.Address.Replace(ParamConst.CHARACTER_ADD, "");
					}
					else
					{
						address = mShopSub.Address;
					}
					condition.Append(@$" AND CONCAT(adr.ZipCodeDsp, kbnProvince.KbnValueName, adr.AdrCd1, adr.AdrCd2 ) Like '%{ address }%'");
				}
				if (mShopSub.Area2Cd != 0)
				{
					condition.Append($" AND ms.Area2Cd = @Area2Cd");
					mShopSubParam.Area2Cd = mShopSub.Area2Cd;
				}
				if (!string.IsNullOrEmpty(mShopSub.MailAddress))
				{
					condition.Append($" AND ms.MailAddress LIKE '%{ mShopSub.MailAddress }%'");
				}
				if (!string.IsNullOrEmpty(mShopSub.BussinessCd))
				{
					condition.Append(" AND ms.BussinessCd = @BussinessCd");
					mShopSubParam.BussinessCd = mShopSub.BussinessCd;
				}
				if (!string.IsNullOrEmpty(mShopSub.DisplayFlg.ToString()))
				{
					condition.Append(" AND ms.DisplayFlg = @DisplayFlg");
					mShopSubParam.DisplayFlg = mShopSub.DisplayFlg;
				}

				condition.Append($" AND ms.DelFlg = { ParamConst.DelFlg.OFF } ");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				select.Append(@$"SELECT
									ms.ShopCd
									, ms.ShopName
									, ms.AreaCd
									, ms.Area2Cd
									, kbnArea2.KbnValueName AS Area2Name
									, ms.AddrId
									, ms.MailAddress
									, ms.PhoneNumber
									, ms.FaxNumber
									, ms.Status
									, ms.StartDate
									, ms.EndDate
									, ms.AffiliateDepartmentCd
									, ms.AffiliateDepartmentName
									, ms.OpenTime
									, ms.Square
									, ms.BussinessCd
									, ms.SalePersonCd
									, ms.SalePersonName
									, ms.UsedDutyFree
									, ms.ContractType
									, ms.DisplayFlg
									, ms.CreateUserCd
									, ms.UpdateUserCd
									, ms.UpdateDtTm
									, adr.ZipCd
									, adr.ZipCodeDsp
									, adr.Province
									, kbnProvince.KbnValueName AS ProvinceName
									, adr.AdrCd1
									, adr.AdrCd2
									, adr.UpdateDtTm AS UpdateDtTmAddress
								");

				table.Append($@" FROM mshop ms
								INNER JOIN maddress adr
									ON adr.AddrId = ms.AddrId
									AND adr.DelFlg = { ParamConst.DelFlg.OFF }
								INNER JOIN mkbnvalue kbnProvince
									ON kbnProvince.KbnCd = '{ ParamConst.Province.KBN_CD }'
									AND kbnProvince.KbnValue = adr.Province
									AND kbnProvince.DelFlg = { ParamConst.DelFlg.OFF }
								INNER JOIN mkbnvalue kbnArea2
									ON kbnArea2.KbnCd = '{ ParamConst.Area2.KBN_CD }'
									AND kbnArea2.KbnValue = ms.Area2Cd
									AND kbnArea2.DelFlg = { ParamConst.DelFlg.OFF }
								WHERE 1=1
									{ condition }");

				sqlGetData.Append($@"{ select } { table }
									 LIMIT { pageSize } OFFSET { pageSize * (currentpage - 1) }");

				List<MShopSub> lstMShopSub = this.DbConn.Query<MShopSub>(sqlGetData.ToString(), (object)mShopSubParam).ToList();
				return new Tuple<List<MShopSub>, int>(lstMShopSub, GetCount(table.ToString(), (object)mShopSubParam));
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

		#region MShopを更新する
		/// <summary>
		/// MShopを更新する
		/// </summary>
		/// <param name="mShops"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<MShop> mShops)
		{
			try
			{
				return await this.DbConn.UpdateAsync(mShops);
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

		#region Get Count MShop
		/// <summary>
		/// Get Count OrdersSub
		/// </summary>
		/// <param name="table"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		private int GetCount(string table, object param)
		{
			try
			{
				string sqlGetCountData = string.Empty;
				sqlGetCountData = $@"SELECT
										COUNT(ms.ShopCd)
									{ table }";

				return this.DbConn.Query<int>(sqlGetCountData, param).First();
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
	}
}
