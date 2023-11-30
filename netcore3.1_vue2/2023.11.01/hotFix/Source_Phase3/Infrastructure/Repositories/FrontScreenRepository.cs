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
	public class FrontScreenRepository : BaseRepository, IFrontScreenRepository
	{
		public FrontScreenRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		#region 条件によるお客様サイトレイアウト一覧を取得する(画像が含まれない)
		/// <summary>
		/// 条件によるお客様サイトレイアウト一覧を取得する(画像が含まれない)
		/// </summary>
		/// <param name="frontscreenSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<FrontScreen>> GetAllAsync(FrontScreen frontscreen, string sortColumnName, string sortType)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic frontScreenParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(frontscreen.ScrType))
				{
					condition.Append(" AND ScrType = @ScrType");
					frontScreenParam.ScrType = frontscreen.ScrType;
				}
				if (!string.IsNullOrEmpty(frontscreen.BussinessCd))
				{
					condition.Append(" AND BussinessCd = @BussinessCd");
					frontScreenParam.BussinessCd = frontscreen.BussinessCd;
				}
				if (!string.IsNullOrEmpty(frontscreen.ProductCd))
				{
					condition.Append(" AND ProductCd = @ProductCd");
					frontScreenParam.ProductCd = frontscreen.ProductCd;
				}

				condition.Append($" AND DelFlg = { ParamConst.DelFlg.OFF } ");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					if (!string.IsNullOrEmpty(sortType))
					{
						sortType = " ASC";
					}
					else
					{
						sortType = " DESC";
					}
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT 
											ScrId
											,ScrType
											,TextTitle1
											,TextTitle2
											,TextTitle3
											,TextArea1
											,TextArea2
											,TextButton1
											,TextButton2
											,TextNortify1
											,TextNortify2
											,CheckBoxFlg1
											,CheckBoxFlg2
											,ButtonFlg
											,BussinessCd
											,ProductCd
											,CreateUserCd
											,CreateDtTm
											,UpdateUserCd
											,UpdateDtTm
											,DelFlg
									FROM frontscreen
									WHERE 1=1
									{condition}");
				return (List<FrontScreen>)await this.DbConn.QueryAsync<FrontScreen>(sqlGetData.ToString(), (object)frontScreenParam);
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

		#region 条件によるお客様サイトレイアウト一覧を取得する(画像が含まれる)
		/// <summary>
		/// 条件によるお客様サイトレイアウト一覧を取得する(画像が含まれる)
		/// </summary>
		/// <param name="frontscreenSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<FrontScreenSub>> GetAllSubAsync(FrontScreenSub frontscreenSub, string sortColumnName, string sortType)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic frontScreenSubpParam = new ExpandoObject();

				if (!string.IsNullOrEmpty(frontscreenSub.ScrType))
				{
					condition.Append(" AND FS.ScrType = @ScrType");
					frontScreenSubpParam.ScrType = frontscreenSub.ScrType;
				}
				if (!string.IsNullOrEmpty(frontscreenSub.BussinessCd))
				{
					condition.Append(" AND FS.BussinessCd = @BussinessCd");
					frontScreenSubpParam.BussinessCd = frontscreenSub.BussinessCd;
				}
				if (!string.IsNullOrEmpty(frontscreenSub.ProductCd))
				{
					condition.Append(" AND FS.ProductCd = @ProductCd");
					frontScreenSubpParam.ProductCd = frontscreenSub.ProductCd;
				}

				condition.Append($" AND FS.DelFlg = { ParamConst.DelFlg.OFF } ");

				if (!string.IsNullOrEmpty(sortColumnName))
				{
					if (!string.IsNullOrEmpty(sortType))
					{
						sortType = " ASC";
					}
					else
					{
						sortType = " DESC";
					}
					condition.Append(" ORDER BY " + sortColumnName + " " + sortType);
				}

				sqlGetData.Append(@$"SELECT FSI.ScrImgUrlId
											, FS.ScrId
											, FS.ScrType
											, FSI.ScrImgUrlId
											, FSI.ImagePosition
											, FSI.ImageUrl
											, FSI.DspIndex
											, FS.TextTitle1
											, FS.TextTitle2
											, FS.TextTitle3
											, FS.TextArea1
											, FS.TextArea2
											, FS.TextButton1
											, FS.TextButton2
											, FS.TextNortify1
											, FS.TextNortify2
											, FS.CheckBoxFlg1
											, FS.CheckBoxFlg2
											, FS.ButtonFlg
											, FS.ProductCd
											, FSI.CreateUserCd
											, FS.BussinessCd
									FROM frontscreen FS
									LEFT JOIN frontscreenimgurl FSI ON FS.ScrId = FSI.ScrId AND FSI.DelFlg = { ParamConst.DelFlg.OFF }
									WHERE 1=1
									{condition}");
				return (List<FrontScreenSub>)await this.DbConn.QueryAsync<FrontScreenSub>(sqlGetData.ToString(), (object)frontScreenSubpParam);
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

		public Tuple<List<FrontScreen>, int> GetDataPagination(int currentpage, int pageSize, FrontScreen data, string sortColumnName, string sortType)
		{
			throw new NotImplementedException();
		}

		#region FrontScreenを追加する
		/// <summary>
		/// FrontScreenを追加する
		/// </summary>
		/// <param name="frontscreen"></param>
		/// <returns></returns>
		public async Task<long> AddAsync(FrontScreen frontscreen)
		{
			try
			{
				return await this.DbConn.InsertAsync(frontscreen);
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

		#region FrontScreenを更新する
		/// <summary>
		/// FrontScreenを更新する
		/// </summary>
		/// <param name="frontScreens"></param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(List<FrontScreen> frontScreens)
		{
			try
			{
					return await this.DbConn.UpdateAsync(frontScreens);
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

		public Task<bool> DeleteAsync(List<FrontScreen> frontScreens)
		{
			throw new NotImplementedException();
		}

		#region FrontScreenを追加する
		/// <summary>
		/// FrontScreenを追加する
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public async Task<long> InsertFrontScreenByCreateBussiness(string sqlQuery)
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
	}
}
