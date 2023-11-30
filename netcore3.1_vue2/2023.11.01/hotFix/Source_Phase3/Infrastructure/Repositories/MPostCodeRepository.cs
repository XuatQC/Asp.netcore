using Dapper;
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
	public class MPostCodeRepository : BaseRepository, IMPostCodeRepository
	{
		public MPostCodeRepository(SettingsConfig config, IDbConnection conn) : base(config, conn)
		{
		}

		public Task<long> AddAsync(MPostCode mPostCode)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MPostCode> mPostCode)
		{
			throw new NotImplementedException();
		}

		#region 条件による郵便番号一覧を取得する
		/// <summary>
		/// 条件による郵便番号一覧を取得する
		/// </summary>
		/// <param name="mpostcode"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public async Task<List<MPostCode>> GetAllAsync(MPostCode mpostcode, string sortColumnName = null, string sortType = null)
		{
			try
			{
				StringBuilder condition = new StringBuilder("");
				StringBuilder sqlGetData = new StringBuilder("");
				dynamic mpostcodeParam = new ExpandoObject();

				#region パラメータ
				if (mpostcode.MPostId > 0)
				{
					condition.Append(" AND MPostId = @MPostId");
					mpostcodeParam.MPostId = mpostcode.MPostId;
				}
				
				if (!string.IsNullOrEmpty(mpostcode.ZipCd))
				{
					condition.Append(" AND ZipCd = @ZipCd");
					mpostcodeParam.ZipCd = mpostcode.ZipCd;
				}
				
				if (mpostcode.AddressCd > 0)
				{
					condition.Append(" AND AddressCd = @AddressCd");
					mpostcodeParam.AddressCd = mpostcode.AddressCd;
				}
				
				if (mpostcode.RegionCd > 0)
				{
					condition.Append(" AND RegionCd = @RegionCd");
					mpostcodeParam.RegionCd = mpostcode.RegionCd;
				}
				
				if (mpostcode.CityCd > 0)
				{
					condition.Append(" AND CityCd = @CityCd");
					mpostcodeParam.CityCd = mpostcode.CityCd;
				}
				
				if (mpostcode.StreetCd > 0)
				{
					condition.Append(" AND StreetCd = @StreetCd");
					mpostcodeParam.StreetCd = mpostcode.StreetCd;
				}
				
				if (!string.IsNullOrEmpty(mpostcode.ZipCodeDsp))
				{
					condition.Append(" AND ZipCodeDsp = @ZipCodeDsp");
					mpostcodeParam.ZipCodeDsp = mpostcode.ZipCodeDsp;
				}

				if (!string.IsNullOrEmpty(mpostcode.Province))
				{
					condition.Append(" AND Province = @Province");
					mpostcodeParam.Province = mpostcode.Province;
				}
				
				if (!string.IsNullOrEmpty(mpostcode.City))
				{
					condition.Append(" AND City = @City");
					mpostcodeParam.City = mpostcode.City;
				}
				
				if (!string.IsNullOrEmpty(mpostcode.Town))
				{
					condition.Append(" AND Town = @Town");
					mpostcodeParam.Town = mpostcode.Town;
				}
				
				if (!string.IsNullOrEmpty(mpostcode.Nom))
				{
					condition.Append(" AND Nom = @Nom");
					mpostcodeParam.Nom = mpostcode.Nom;
				}
				
				if (!string.IsNullOrEmpty(mpostcode.Addition))
				{
					condition.Append(" AND Addition = @Addition");
					mpostcodeParam.Addition = mpostcode.Addition;
				}
				
				if (!string.IsNullOrEmpty(mpostcode.OfficePlaceCd))
				{
					condition.Append(" AND OfficePlaceCd = @OfficePlaceCd");
					mpostcodeParam.OfficePlaceCd = mpostcode.OfficePlaceCd;
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
					condition.Append(" order by " + sortColumnName + " " + sortType);
				}
				#endregion

				sqlGetData.Append(@$"SELECT	 
										MPostId
										,ZipCd
										,AddressCd
										,RegionCd
										,CityCd
										,StreetCd
										,ZipCodeDsp
										,CompanyFlg
										,StopFlg
										,Province
										,ProvinceKana
										,City
										,CityKana
										,Town
										,TownKana
										,TownAlter
										,StreetName
										,Nom
										,NomKana
										,Addition
										,OfficeName
										,OfficeNameKana
										,OfficePlace
										,OfficePlaceCd
										,IsPriority
										,CreateUserCd
										,CreateDtTm
										,UpdateUserCd
										,UpdateDtTm
										,DelFlg
									FROM mpostcode
									WHERE 1=1
									{condition}");
				return (List<MPostCode>)await this.DbConn.QueryAsync<MPostCode>(sqlGetData.ToString(), (object)mpostcodeParam);
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

		public Tuple<List<MPostCode>, int> GetDataPagination(int currentpage, int pageSize, MPostCode mPostCode, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MPostCode> mPostCode)
		{
			throw new NotImplementedException();
		}
	}
}