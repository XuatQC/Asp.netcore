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
	public class InventorySymbolRepository : BaseRepository, IInventorySymbolRepository
	{
		public InventorySymbolRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(InventorySymbol inventorySymbol)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<InventorySymbol> inventorySymbols)
		{
			throw new NotImplementedException();
		}

		#region 条件による在庫数一覧を取得する
		/// <summary>
		/// 条件による在庫数一覧を取得する
		/// </summary>
		/// <param name="inventorysymbol"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<InventorySymbol>> GetAllAsync(InventorySymbol inventorysymbol, string sortColumnName, string sortType)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic inventorysymbolParam = new ExpandoObject();


				if (!string.IsNullOrEmpty(inventorysymbol.InventoryNumCheck))
				{
					condition.Append(" AND InventoryNumCheck = @InventoryNumCheck");
					inventorysymbolParam.InventoryNumCheck = inventorysymbol.InventoryNumCheck;
				}

				if (!string.IsNullOrEmpty(inventorysymbol.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					inventorysymbolParam.BussinessCd = inventorysymbol.BussinessCd;
				}
				condition.Append(" AND DelFlg = " + ParamConst.DelFlg.OFF);

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
										InventoryNumCheck
										, BussinessCd
										, CreateUserCd
										, CreateDtTm
										, UpdateUserCd
										, UpdateDtTm
										, DelFlg
									FROM InventorySymbol
									WHERE 1=1
									{condition}");
				using (IDbConnection con = this.DbConn)
				{
					return (List<InventorySymbol>)await this.DbConn.QueryAsync<InventorySymbol>(sqlGetData.ToString(), (object)inventorysymbolParam);
				}
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

		public Tuple<List<InventorySymbol>, int> GetDataPagination(int currentpage, int pageSize, InventorySymbol inventorySymbol, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateAsync(List<InventorySymbol> inventorySymbols)
		{
			try
			{
				using (IDbConnection con = this.DbConn)
				{
					return await con.UpdateAsync(inventorySymbols);
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return false;
			}
		}
	}
}
