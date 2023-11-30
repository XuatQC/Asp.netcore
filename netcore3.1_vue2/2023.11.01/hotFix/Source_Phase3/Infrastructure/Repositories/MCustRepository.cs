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
	public class MCustRepository : BaseRepository, IMCustRepository
	{
		public MCustRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region MCustを追加する
		/// <summary>
		/// MCustを追加する
		/// </summary>
		/// <param name="mcust"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(MCust mcust)
		{
			try
			{
				return await this.DbConn.InsertAsync(mcust);
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

		public Task<bool> DeleteAsync(List<MCust> mcust)
		{
			throw new NotImplementedException();
		}

		#region  条件によるMCust一覧を取得する
		/// <summary>
		/// 条件によるMCust一覧を取得する
		/// </summary>
		/// <param name="mcust"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MCust>> GetAllAsync(MCust mcust, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mcustParam = new ExpandoObject();


				if (mcust.CustId > 0)
				{
					condition.Append(" AND CustId = @CustId");
					mcustParam.CustId = mcust.CustId;
				}

				if (!string.IsNullOrEmpty(mcust.KanaFamilyName))
				{
					condition.Append(" AND KanaFamilyName = @KanaFamilyName");
					mcustParam.KanaFamilyName = mcust.KanaFamilyName;
				}

				if (!string.IsNullOrEmpty(mcust.KanaFirstName))
				{
					condition.Append(" AND KanaFirstName = @KanaFirstName");
					mcustParam.KanaFirstName = mcust.KanaFirstName;
				}

				if (!string.IsNullOrEmpty(mcust.KanjiFamilyName))
				{
					condition.Append(" AND KanjiFamilyName = @KanjiFamilyName");
					mcustParam.KanjiFamilyName = mcust.KanjiFamilyName;
				}

				if (!string.IsNullOrEmpty(mcust.KanjiFirstName))
				{
					condition.Append(" AND KanjiFirstName = @KanjiFirstName");
					mcustParam.KanjiFirstName = mcust.KanjiFirstName;
				}

				if (!string.IsNullOrEmpty(mcust.PhoneNumber))
				{
					condition.Append(" AND PhoneNumber = @PhoneNumber");
					mcustParam.PhoneNumber = mcust.PhoneNumber;
				}

				if (mcust.AddrId > 0)
				{
					condition.Append(" AND AddrId = @AddrId");
					mcustParam.AddrId = mcust.AddrId;
				}

				if (!string.IsNullOrEmpty(mcust.MailAddress))
				{
					condition.Append(" AND MailAddress = @MailAddress");
					mcustParam.MailAddress = mcust.MailAddress;
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
										CustId
										,KanaFamilyName
										,KanaFirstName
										,KanjiFamilyName
										,KanjiFirstName
										,PhoneNumber
										,AddrId
										,MailAddress
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM mcust
									WHERE 1=1
									{condition}");
				return (List<MCust>)await this.DbConn.QueryAsync<MCust>(sqlGetData.ToString(), (object)mcustParam);
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

		public Tuple<List<MCust>, int> GetDataPagination(int currentpage, int pageSize, MCust mcust, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region お客様詳細情報
		/// <summary>
		/// お客様詳細情報
		/// </summary>
		/// <param name="mCustSub"></param>
		/// <returns></returns>
		public async Task<MCustSub> GetAllSubAsync(MCustSub mCustSub)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mCustSubParam = new ExpandoObject();

				if (mCustSub.CustId != 0)
				{
					condition.Append(" AND CustId = @CustId");
					mCustSubParam.CustId = mCustSub.CustId;
				}
				condition.Append($" AND mc.DelFlg = { ParamConst.DelFlg.OFF } ");

				sqlGetData.Append(@$"SELECT
										mc.CustId
										, mc.KanaFamilyName
										, mc.KanaFirstName
										, mc.KanjiFamilyName
										, mc.KanjiFirstName
										, mc.PhoneNumber
										, mc.AddrId
										, mc.MailAddress
										, mc.CreateUserCd
										, mc.CreateDtTm
										, mc.UpdateDtTm
										, adr.AddrId
										, adr.ZipCd
										, adr.ZipCodeDsp
										, adr.Province
										, adr.AdrCd1
										, adr.AdrCd2
										, adr.AdrCd3
										, adr.AdrType
									FROM mcust mc
									INNER JOIN maddress adr
										ON mc.AddrId = adr.AddrId
											AND adr.DelFlg = { ParamConst.DelFlg.OFF }
									WHERE 1=1
										{condition}");
				return (MCustSub)await this.DbConn.QueryFirstOrDefaultAsync<MCustSub>(sqlGetData.ToString(), (object)mCustSubParam);
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

		#region MCustを更新する
		/// <summary>
		/// MCustを更新する
		/// </summary>
		/// <param name="lstMCust"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<MCust> lstMCust)
		{
			try
			{
				return await this.DbConn.UpdateAsync(lstMCust);
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
