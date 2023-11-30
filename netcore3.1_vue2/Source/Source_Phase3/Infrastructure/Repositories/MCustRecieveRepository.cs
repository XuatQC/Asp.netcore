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

	public class MCustRecieveRepository : BaseRepository, IMCustRecieveRepository
	{
		public MCustRecieveRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region 商品受取人の情報を追加する
		/// <summary>
		/// 商品受取人の情報を追加する
		/// </summary>
		/// <param name="mcustrecieve"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MCustRecieve mcustrecieve)
		{
			try
			{
				return await this.DbConn.InsertAsync(mcustrecieve);
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

		public Task<bool> DeleteAsync(List<MCustRecieve> mcustRecieve)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMCustRecieve一覧を取得する
		/// <summary>
		/// 条件によるMCustRecieve一覧を取得する
		/// </summary>
		/// <param name="mcustrecieve"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MCustRecieve>> GetAllAsync(MCustRecieve mcustRecieve, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mcustrecieveParam = new ExpandoObject();

				if (mcustRecieve.RecieveId > 0)
				{
					condition.Append(" AND RecieveId = @RecieveId");
					mcustrecieveParam.RecieveId = mcustRecieve.RecieveId;
				}

				if (!string.IsNullOrEmpty(mcustRecieve.OrderId))
				{
					condition.Append(" AND OrderId = @OrderId");
					mcustrecieveParam.OrderId = mcustRecieve.OrderId;
				}

				if (!string.IsNullOrEmpty(mcustRecieve.KanaFamilyName))
				{
					condition.Append(" AND KanaFamilyName = @KanaFamilyName");
					mcustrecieveParam.KanaFamilyName = mcustRecieve.KanaFamilyName;
				}

				if (!string.IsNullOrEmpty(mcustRecieve.KanaFirstName))
				{
					condition.Append(" AND KanaFirstName = @KanaFirstName");
					mcustrecieveParam.KanaFirstName = mcustRecieve.KanaFirstName;
				}

				if (!string.IsNullOrEmpty(mcustRecieve.KanjiFamilyName))
				{
					condition.Append(" AND KanjiFamilyName = @KanjiFamilyName");
					mcustrecieveParam.KanjiFamilyName = mcustRecieve.KanjiFamilyName;
				}

				if (!string.IsNullOrEmpty(mcustRecieve.KanjiFirstName))
				{
					condition.Append(" AND KanjiFirstName = @KanjiFirstName");
					mcustrecieveParam.KanjiFirstName = mcustRecieve.KanjiFirstName;
				}

				if (!string.IsNullOrEmpty(mcustRecieve.PhoneNumber))
				{
					condition.Append(" AND PhoneNumber = @PhoneNumber");
					mcustrecieveParam.PhoneNumber = mcustRecieve.PhoneNumber;
				}

				if (mcustRecieve.AddrId > 0)
				{
					condition.Append(" AND AddrId = @AddrId");
					mcustrecieveParam.AddrId = mcustRecieve.AddrId;
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
										RecieveId
										,OrderId
										,KanaFamilyName
										,KanaFirstName
										,KanjiFamilyName
										,KanjiFirstName
										,PhoneNumber
										,AddrId
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM mcustrecieve
									WHERE 1=1
									{condition}");
				return (List<MCustRecieve>)await this.DbConn.QueryAsync<MCustRecieve>(sqlGetData.ToString(), (object)mcustrecieveParam);
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

		public Tuple<List<MCustRecieve>, int> GetDataPagination(int currentpage, int pageSize, MCustRecieve mcustRecieve, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region 条件によるMCustrecieveSub一覧を取得する
		/// <summary>
		/// 条件によるMCustrecieveSub一覧を取得する
		/// </summary>
		/// <param name="mCustrecieveSub"></param>
		/// <returns></returns>
		public async Task<MCustrecieveSub> GetAllSubAsync(MCustrecieveSub mCustrecieveSub)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mCustRecieveSubParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(mCustrecieveSub.OrderId))
				{
					condition.Append(" AND mcr.OrderId = @OrderId");
					mCustRecieveSubParam.OrderId = mCustrecieveSub.OrderId;
				}
				condition.Append($" AND mcr.DelFlg = { ParamConst.DelFlg.OFF } ");

				sqlGetData.Append(@$"SELECT
										mcr.RecieveId
										, mcr.OrderId
										, mcr.KanaFamilyName
										, mcr.KanaFirstName
										, mcr.KanjiFamilyName
										, mcr.KanjiFirstName
										, mcr.PhoneNumber
										, mcr.AddrId
										, mcr.UpdateDtTm
										, mdr.AddrId
										, mdr.ZipCd
										, mdr.ZipCodeDsp
										, mdr.Province
										, mdr.AdrCd1
										, mdr.AdrCd2
										, mdr.AdrCd3
										, mdr.AdrType
										, mdr.UpdateDtTm AS UpdateDtTmAddress
									FROM mcustrecieve mcr
									INNER JOIN maddress mdr
										ON mcr.AddrId = mdr.AddrId
										AND mdr.DelFlg = { ParamConst.DelFlg.OFF }
									WHERE 1=1
										{condition}");
				return (MCustrecieveSub)await this.DbConn.QueryFirstOrDefaultAsync<MCustrecieveSub>(sqlGetData.ToString(), (object)mCustRecieveSubParam);
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

		#region MCustRecieveを更新する
		/// <summary>
		/// MCustRecieveを更新する
		/// </summary>
		/// <param name="lstMCustRecieve"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<MCustRecieve> lstMCustRecieve)
		{
			try
			{
				return await this.DbConn.UpdateAsync(lstMCustRecieve);
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
	}
}