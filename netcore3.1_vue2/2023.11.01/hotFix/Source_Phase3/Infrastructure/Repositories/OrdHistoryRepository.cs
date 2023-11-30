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

	public class OrdHistoryRepository : BaseRepository, IOrdHistoryRepository
	{
		public OrdHistoryRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region 条件によるOrdHistory一覧を取得する
		/// <summary>
		/// 条件によるOrdHistory一覧を取得する
		/// </summary>
		/// <param name="ordhistory"></param>
		/// <param name="propetyOrder"></param>
		/// <param name="typeOrder"></param>
		/// <returns></returns>
		public async Task<List<OrdHistory>> GetAllAsync(OrdHistory ordhistory, string propetyOrder, string typeOrder)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic ordhistoryParam = new ExpandoObject();
				
				
				if (!string.IsNullOrEmpty(ordhistory.OrderId))
				{
					condition.Append(" AND OrderId = @OrderId");
					ordhistoryParam.OrderId = ordhistory.OrderId;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.HistType))
				{
					condition.Append(" AND HistType = @HistType");
					ordhistoryParam.HistType = ordhistory.HistType;
				}

				if (ordhistory.MailId != 0)
				{
					condition.Append(" AND MailId = @MailId");
					ordhistoryParam.MailId = ordhistory.MailId;
				}

				if (!string.IsNullOrEmpty(ordhistory.LastStatus))
				{
					condition.Append(" AND LastStatus = @LastStatus");
					ordhistoryParam.LastStatus = ordhistory.LastStatus;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.UpdatedStatus))
				{
					condition.Append(" AND UpdatedStatus = @UpdatedStatus");
					ordhistoryParam.UpdatedStatus = ordhistory.UpdatedStatus;
				}
				
				
				if (!string.IsNullOrEmpty(ordhistory.MailType))
				{
					condition.Append(" AND MailType = @MailType");
					ordhistoryParam.MailType = ordhistory.MailType;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.MailStatus))
				{
					condition.Append(" AND MailStatus = @MailStatus");
					ordhistoryParam.MailStatus = ordhistory.MailStatus;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.ErrorCd))
				{
					condition.Append(" AND ErrorCd = @ErrorCd");
					ordhistoryParam.ErrorCd = ordhistory.ErrorCd;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.ErrorDescription))
				{
					condition.Append(" AND ErrorDescription = @ErrorDescription");
					ordhistoryParam.ErrorDescription = ordhistory.ErrorDescription;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.ProductCd))
				{
					condition.Append(" AND ProductCd = @ProductCd");
					ordhistoryParam.ProductCd = ordhistory.ProductCd;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.Size))
				{
					condition.Append(" AND Size = @Size");
					ordhistoryParam.Size = ordhistory.Size;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.Color))
				{
					condition.Append(" AND Color = @Color");
					ordhistoryParam.Color = ordhistory.Color;
				}
				
				
				if (!string.IsNullOrEmpty(ordhistory.LastSize))
				{
					condition.Append(" AND LastSize = @LastSize");
					ordhistoryParam.LastSize = ordhistory.LastSize;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.UpdatedSize))
				{
					condition.Append(" AND UpdatedSize = @UpdatedSize");
					ordhistoryParam.UpdatedSize = ordhistory.UpdatedSize;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.LastColor))
				{
					condition.Append(" AND LastColor = @LastColor");
					ordhistoryParam.LastColor = ordhistory.LastColor;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.UpdatedColor))
				{
					condition.Append(" AND UpdatedColor = @UpdatedColor");
					ordhistoryParam.UpdatedColor = ordhistory.UpdatedColor;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.LastShopCd))
				{
					condition.Append(" AND LastShopCd = @LastShopCd");
					ordhistoryParam.LastShopCd = ordhistory.LastShopCd;
				}
				
				if (!string.IsNullOrEmpty(ordhistory.UpdatedShopCd))
				{
					condition.Append(" AND UpdatedShopCd = @UpdatedShopCd");
					ordhistoryParam.UpdatedShopCd = ordhistory.UpdatedShopCd;
				}
				condition.Append($" AND DelFlg = {ParamConst.DelFlg.OFF}");

				if (!string.IsNullOrEmpty(propetyOrder))
				{
					condition.Append(" ORDER BY " + propetyOrder + " " + typeOrder);
				}

				sqlGetData.Append(@$"SELECT	 
										OrdHistId
										,OrderId
										,HistType
										,LastStatus
										,UpdatedStatus
										,MailId
										,MailType
										,MailStatus
										,ErrorCd
										,ErrorDescription
										,ProductCd
										,Size
										,Color
										,LastQuantity
										,UpdatedlQuantity
										,LastSize
										,UpdatedSize
										,LastColor
										,UpdatedColor
										,LastShopCd
										,UpdatedShopCd
										,Department
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM OrdHistory
									WHERE 1=1
									{condition}");
				return (List<OrdHistory>)await this.DbConn.QueryAsync<OrdHistory>(sqlGetData.ToString(), (object)ordhistoryParam);
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

		#region 条件によるOrdHistory一覧を取得する
		/// <summary>
		/// 条件によるOrdHistory一覧を取得する
		/// </summary>
		/// <param name="ordhistory"></param>
		/// <param name="propetyOrder"></param>
		/// <param name="typeOrder"></param>
		/// <returns></returns>
		public async Task<List<OrdHistorySub>> GetAllSubAsync(OrdHistory ordhistory, string sortColumnName, string sortType)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic ordhistoryParam = new ExpandoObject();


				if (!string.IsNullOrEmpty(ordhistory.OrderId))
				{
					condition.Append(" AND ordHist.OrderId = @OrderId");
					ordhistoryParam.OrderId = ordhistory.OrderId;
				}

				if (!string.IsNullOrEmpty(ordhistory.HistType))
				{
					condition.Append(" AND ordHist.HistType = @HistType");
					ordhistoryParam.HistType = ordhistory.HistType;
				}

				if (!string.IsNullOrEmpty(ordhistory.LastStatus))
				{
					condition.Append(" AND ordHist.LastStatus = @LastStatus");
					ordhistoryParam.LastStatus = ordhistory.LastStatus;
				}

				if (!string.IsNullOrEmpty(ordhistory.UpdatedStatus))
				{
					condition.Append(" AND ordHist.UpdatedStatus = @UpdatedStatus");
					ordhistoryParam.UpdatedStatus = ordhistory.UpdatedStatus;
				}


				if (!string.IsNullOrEmpty(ordhistory.MailType))
				{
					condition.Append(" AND ordHist.MailType = @MailType");
					ordhistoryParam.MailType = ordhistory.MailType;
				}

				if (!string.IsNullOrEmpty(ordhistory.MailStatus))
				{
					condition.Append(" AND ordHist.MailStatus = @MailStatus");
					ordhistoryParam.MailStatus = ordhistory.MailStatus;
				}

				if (!string.IsNullOrEmpty(ordhistory.ErrorCd))
				{
					condition.Append(" AND ordHist.ErrorCd = @ErrorCd");
					ordhistoryParam.ErrorCd = ordhistory.ErrorCd;
				}

				if (!string.IsNullOrEmpty(ordhistory.ProductCd))
				{
					condition.Append(" AND ordHist.ProductCd = @ProductCd");
					ordhistoryParam.ProductCd = ordhistory.ProductCd;
				}

				if (!string.IsNullOrEmpty(ordhistory.Size))
				{
					condition.Append(" AND ordHist.Size = @Size");
					ordhistoryParam.Size = ordhistory.Size;
				}

				if (!string.IsNullOrEmpty(ordhistory.Color))
				{
					condition.Append(" AND ordHist.Color = @Color");
					ordhistoryParam.Color = ordhistory.Color;
				}


				if (!string.IsNullOrEmpty(ordhistory.LastSize))
				{
					condition.Append(" AND ordHist.LastSize = @LastSize");
					ordhistoryParam.LastSize = ordhistory.LastSize;
				}

				if (!string.IsNullOrEmpty(ordhistory.UpdatedSize))
				{
					condition.Append(" AND ordHist.UpdatedSize = @UpdatedSize");
					ordhistoryParam.UpdatedSize = ordhistory.UpdatedSize;
				}

				if (!string.IsNullOrEmpty(ordhistory.LastColor))
				{
					condition.Append(" AND ordHist.LastColor = @LastColor");
					ordhistoryParam.LastColor = ordhistory.LastColor;
				}

				if (!string.IsNullOrEmpty(ordhistory.UpdatedColor))
				{
					condition.Append(" AND ordHist.UpdatedColor = @UpdatedColor");
					ordhistoryParam.UpdatedColor = ordhistory.UpdatedColor;
				}

				if (!string.IsNullOrEmpty(ordhistory.LastShopCd))
				{
					condition.Append(" AND ordHist.LastShopCd = @LastShopCd");
					ordhistoryParam.LastShopCd = ordhistory.LastShopCd;
				}

				if (!string.IsNullOrEmpty(ordhistory.UpdatedShopCd))
				{
					condition.Append(" AND ordHist.UpdatedShopCd = @UpdatedShopCd");
					ordhistoryParam.UpdatedShopCd = ordhistory.UpdatedShopCd;
				}

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					sortType = !string.IsNullOrEmpty(sortType) ? sortType : " DESC";
					condition.Append(" order by " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT	 
										ordHist.OrdHistId
										,ordHist.OrderId
										,ordHist.HistType
										,ordHist.LastStatus
										,ordHist.UpdatedStatus
										,ordHist.MailId
										,ordHist.MailType
										,ordHist.MailStatus
										,ordHist.ErrorCd
										,ordHist.ErrorDescription
										,ordHist.ProductCd
										,(select SizeName from msize where SizeCd = ordHist.Size) Size
										,(select ColorName from mcolor where ColorCd = ordHist.Color) Color 
										,ordHist.LastQuantity
										,ordHist.UpdatedlQuantity
										,(select SizeName from msize where SizeCd = ordHist.LastSize) LastSize
										,(select SizeName from msize where SizeCd = ordHist.UpdatedSize) UpdatedSize
										,(select ColorName from mcolor where ColorCd = ordHist.LastColor) LastColor
										,(select ColorName from mcolor where ColorCd = ordHist.UpdatedColor) UpdatedColor
										,ordHist.LastShopCd
										,ordHist.UpdatedShopCd
										,ordHist.Department
										,ordHist.CreateUserCd
										,ordHist.CreateDtTm
										,ordHist.UpdateUserCd
										,ordHist.UpdateDtTm
										,ordHist.DelFlg
										,(select ShopName from mshop where ShopCd = ordHist.LastShopCd) LastShopName
										,(select ShopName from mshop where ShopCd = ordHist.UpdatedShopCd) UpdatedShopName
										,product.ProductName
										,ord.PaymentWay
										,ord.ReceiveWay
									FROM OrdHistory ordHist
									INNER JOIN orders ord
										ON ordHist.OrderId = ord.OrderId
									LEFT JOIN mproduct product
										ON product.ProductCd = ordHist.ProductCd
									LEFT JOIN sendmail sm
										ON sm.MailId = ordHist.MailId
									AND (sm.SendTo = '{ParamConst.SendTo.SENDTO_CUSTOMER}' OR sm.SendTo is null)
									WHERE 1=1
									{condition}");
				return (List<OrdHistorySub>)await this.DbConn.QueryAsync<OrdHistorySub>(sqlGetData.ToString(), (object)ordhistoryParam);
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
		public Tuple<List<OrdHistory>, int> GetDataPagination(int currentpage, int pageSize, OrdHistory ordhistory, string propetyOrder, string typeOrder)
		{
			throw new NotImplementedException();
		}

		#region OrdHistoryを更新する
		/// <summary>
		/// OrdHistoryを更新する
		/// </summary>
		/// <param name="lstOrdHistory"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<OrdHistory> lstOrdHistory)
		{
			try
			{
				return await this.DbConn.UpdateAsync(lstOrdHistory);
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

		#region OrdHistoryを追加する
		/// <summary>
		/// OrdHistoryを追加する
		/// </summary>
		/// <param name="ordhistory"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(OrdHistory ordhistory)
		{
			try
			{
				return await this.DbConn.InsertAsync(ordhistory);
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
		public Task<bool> DeleteAsync(List<OrdHistory> ordhistory)
		{
			throw new NotImplementedException();
		}

		#region OrdHistory一覧を追加する
		/// <summary>
		/// OrdHistory一覧を追加する
		/// </summary>
		/// <param name="ordhistory"></param>
		/// <returns></returns>
		public async Task<long> AddListAsync(List<OrdHistory> ordhistory)
		{
			try
			{
				return await this.DbConn.InsertAsync(ordhistory);
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