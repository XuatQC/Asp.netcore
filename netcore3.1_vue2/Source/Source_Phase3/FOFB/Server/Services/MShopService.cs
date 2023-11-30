using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Services
{
	public class MShopService : BaseService, IMShopService
	{
		public MShopService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#region 条件によるMShop一覧を取得する
		/// <summary>
		/// 条件によるMShop一覧を取得する
		/// </summary>
		/// <param name="mShop"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MShop>> GetAllAsync(MShop mShop, string sortColumnName, string sortType)
		{
			return UnitOfWork.MShop.GetAllAsync(mShop, sortColumnName, sortType);
		}
		#endregion

		public Tuple<List<MShop>, int> GetDataPagination(int currentpage, int pageSize, MShop mShop, string sortColumnName, string sortType)
		{
			throw new NotImplementedException();
		}

		public Task<long> AddAsync(MShop mShop)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MShop> mShops)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MShop> mShops)
		{
			throw new NotImplementedException();
		}

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
			return UnitOfWork.MShop.GetDataMShopSubPagination(currentpage, pageSize, mShopSub, sortColumnName, sortType);
		}
		#endregion

		#region 店舗情報を登録する
		/// <summary>
		/// 店舗情報を登録する
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <returns></returns>
		public object InsertShopInfo(MShopSub mShopSub)
		{
			bool isInsertSuccess = false;

			MShop mShopByShopCd = new MShop();
			mShopByShopCd.ShopCd = mShopSub.ShopCd;
			List<MShop> mShops = UnitOfWork.MShop.GetAllAsync(mShopByShopCd).Result;

			if (mShops != null && mShops.Count > 0)
			{
				return new { error = Message.Common.MSG_ALREADY_EXIST.Replace("*", ParamConst.TextItem.SHOP_CD) };
			}

			try
			{
				using (var transactionScope = new TransactionScope())
				{
					long addrId = InsertMAddress(mShopSub);

					if (addrId == 0)
					{
						return new { isInsertSuccess = isInsertSuccess };
					}

					mShopSub.AddrId = (int)addrId;
					mShopSub.CreateUserCd = mShopSub.UpdateUserCd;

					long shopCd = UnitOfWork.MShop.AddAsync((MShop)mShopSub).Result;

					if (shopCd == 0)
					{
						return new { isInsertSuccess = isInsertSuccess };
					}

					MUserLogin mUserLogin = ConvertObjectMUserLogin(mShopSub);
					long userCd = UnitOfWork.MUserLogin.AddAsync(mUserLogin).Result;

					if (userCd > 0)
					{
						isInsertSuccess = true;
					}
					else
					{
						return new { isInsertSuccess = isInsertSuccess };
					}

					transactionScope.Complete();
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
			}

			return new { isInsertSuccess = isInsertSuccess };
		}
		#endregion

		#region 店舗情報を更新する
		/// <summary>
		/// 店舗情報を更新する
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <returns></returns>
		public int UpdateShopInfo(MShopSub mShopSub)
		{
			int result = ParamConst.ResultType.ERROR;

			MShop mShopByShopCd = new MShop();
			mShopByShopCd.ShopCd = mShopSub.ShopCd;
			List<MShop> mShops = UnitOfWork.MShop.GetAllAsync(mShopByShopCd).Result;

			MAddress mAddress = new MAddress();
			mAddress.AddrId = mShopSub.AddrId;
			List<MAddress> mAddressByAddrId = UnitOfWork.MAddress.GetAllAsync(mAddress).Result;

			if (mShops != null && mShops.Count > 0 && mAddressByAddrId != null && mAddressByAddrId.Count > 0)
			{
				if (mShops[0].UpdateDtTm != mShopSub.UpdateDtTm || mAddressByAddrId[0].UpdateDtTm != mShopSub.UpdateDtTmAddress)
				{
					return ParamConst.ResultType.UPDATED;
				}

				try
				{
					using (var transactionScope = new TransactionScope())
					{
						bool resultUpdAdrShop = UpdateMAddress(mShopSub);

						if (!resultUpdAdrShop)
						{
							return result;
						}

						List<MShop> mShopsAdd = new List<MShop>();
						mShopsAdd.Add((MShop)mShopSub);
						bool resultUpdShop = UnitOfWork.MShop.UpdateAsync(mShopsAdd).Result;

						if (resultUpdShop)
						{
							result = ParamConst.ResultType.SUCCESS;
						}
						else
						{
							return result;
						}

						transactionScope.Complete();
					}
				}
				catch (Exception e)
				{
					Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				}
			}

			return result;
		}
		#endregion

		#region csvファイルデータを読み込む
		/// <summary>
		/// csvファイルデータを読み込む
		/// </summary>
		/// <param name="files"></param>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		public object CheckDataCsv(IFormFileCollection files, string bussinessCd)
		{
			string dLine = "\n";
			List<List<string>> list = Common.ReadCsv(files[0], "utf-8");
			List<MShopSub> lstMShopSub = new List<MShopSub>();
			List<MPostCode> mPostCodes = new List<MPostCode>();

			try
			{
				MShop mShop = new MShop();
				List<MShop> mShops = UnitOfWork.MShop.GetAllAsync(mShop).Result;

				MBussiness mBussiness = new MBussiness();
				mBussiness.BussinessCd = bussinessCd;
				List<MBussiness> mBussinesses = UnitOfWork.MBussiness.GetAllAsync(mBussiness).Result;

				MkbnValue mkbnValue = new MkbnValue();
				List<MkbnValue> mkbnValues = UnitOfWork.MkbnValue.GetAllAsync(mkbnValue).Result;

				if (list.Count > 0)
				{
					string[] timeArr = null;
					int timeOpen = 0;
					int timeClose = 0;
					bool isStartDateFormat = false;
					bool isEndDateFormat = false;
					MShopSub mShopSub = null;
					MShop mShopByShopCd = null;
					MPostCode mPostCodeByZipCd = null;
					MkbnValue province = null;
					MkbnValue area2 = null;
					MkbnValue area = null;
					MkbnValue contractType = null;
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].Count != ParamConst.IdxDataCsvShop.TOTAL_COLUMN)
						{
							return new { error = Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.FILE_CSV) };
						}

						if (list[i][ParamConst.IdxDataCsvShop.BUSSINESS_CD].ToLower() != mBussinesses[0].BussinessName.ToLower())
						{
							continue;
						}
						mShopSub = new MShopSub();

						#region バリデーション

						#region 店舗コード
						mShopSub.IsAdd = true;
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.SHOP_CD]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.SHOP_CD) + dLine;
						}
						else if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.SHOP_CD], ParamConst.Regex.REGEX_NUMBER).Success)
						{
							mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.SHOP_CD) + dLine;
						}
						else if (list[i][ParamConst.IdxDataCsvShop.SHOP_CD].Length != 6)
						{
							mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.SHOP_CD).Replace("$length", "6") + dLine;
						}
						else
						{
							mShopByShopCd = mShops.FirstOrDefault(x => x.ShopCd == list[i][ParamConst.IdxDataCsvShop.SHOP_CD]);
							if (mShopByShopCd != null)
							{
								mShopSub.IsAdd = false;
							}
						}
						#endregion

						mShopSub.ShopCd = list[i][ParamConst.IdxDataCsvShop.SHOP_CD];

						#region 店舗名
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.SHOP_NAME]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.SHOP_NAME) + dLine;
						}
						else if (list[i][ParamConst.IdxDataCsvShop.SHOP_NAME].Length > 60)
						{
							mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.SHOP_NAME).Replace("$length", "60") + dLine;
						}
						#endregion

						mShopSub.ShopName = list[i][ParamConst.IdxDataCsvShop.SHOP_NAME];

						#region 郵便番号
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.ZIP_CD]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.ZIP_CD) + dLine;
						}
						else if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.ZIP_CD], ParamConst.Regex.REGEX_NUMBER).Success)
						{
							mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.ZIP_CD) + dLine;
						}
						else if (list[i][ParamConst.IdxDataCsvShop.ZIP_CD].Length != 7)
						{
							mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.ZIP_CD).Replace("$length", "7") + dLine;
						}
						else
						{
							if (mPostCodes.Count > 0)
							{
								mPostCodeByZipCd = mPostCodes.FirstOrDefault(x => x.ZipCd == list[i][ParamConst.IdxDataCsvShop.ZIP_CD]);
								if (mPostCodeByZipCd == null)
								{
									mPostCodeByZipCd = GetMPostCodeByZipCd(list[i][ParamConst.IdxDataCsvShop.ZIP_CD]);
								}
							}
							else
							{
								mPostCodeByZipCd = GetMPostCodeByZipCd(list[i][ParamConst.IdxDataCsvShop.ZIP_CD]);
							}

							if (mPostCodeByZipCd == null)
							{
								mShopSub.TxtError += Message.Common.MSG_NOT_EXIST.Replace("*", ParamConst.TextItem.ZIP_CD) + dLine;
							}
							else
							{
								mPostCodes.Add(mPostCodeByZipCd);
							}
						}
						#endregion

						mShopSub.ZipCd = list[i][ParamConst.IdxDataCsvShop.ZIP_CD];

						#region 都道府県
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.PROVINCE]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.PROVINCE) + dLine;
						}
						else
						{
							province = mkbnValues.FirstOrDefault(x => x.KbnCd == ParamConst.Province.KBN_CD &&
																		x.KbnValueName == list[i][ParamConst.IdxDataCsvShop.PROVINCE]);
							if (province == null)
							{
								mShopSub.TxtError += Message.Common.MSG_NOT_EXIST.Replace("*", ParamConst.TextItem.PROVINCE) + dLine;
							}
							else
							{
								mShopSub.Province = province.KbnValue;
							}
						}
						#endregion

						mShopSub.ProvinceName = list[i][ParamConst.IdxDataCsvShop.PROVINCE];

						#region 所在地１
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.ADR_CD_1]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.ADR_CD1_SHOP) + dLine;
						}
						else if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.ADR_CD_1], ParamConst.Regex.REGEX_SPECIAL_ADDRESS).Success)
						{
							mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.ADR_CD1_SHOP) + dLine;
						}
						else if (list[i][ParamConst.IdxDataCsvShop.ADR_CD_1].Length > 200)
						{
							mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.ADR_CD1_SHOP).Replace("$length", "200") + dLine;
						}
						#endregion

						mShopSub.AdrCd1 = list[i][ParamConst.IdxDataCsvShop.ADR_CD_1];

						#region 所在地２
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.ADR_CD_2]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.ADR_CD2_SHOP) + dLine;
						}
						else if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.ADR_CD_2], ParamConst.Regex.REGEX_SPECIAL_ADDRESS).Success)
						{
							mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.ADR_CD2_SHOP) + dLine;
						}
						else if (list[i][ParamConst.IdxDataCsvShop.ADR_CD_2].Length > 200)
						{
							mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.ADR_CD2_SHOP).Replace("$length", "200") + dLine;
						}
						#endregion

						mShopSub.AdrCd2 = list[i][ParamConst.IdxDataCsvShop.ADR_CD_2];

						#region エリア２
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.AREA2_CD]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.AREA2) + dLine;
						}
						else
						{
							area2 = mkbnValues.FirstOrDefault(x => x.KbnCd == ParamConst.Area2.KBN_CD &&
																	x.KbnValueName == list[i][ParamConst.IdxDataCsvShop.AREA2_CD]);
							if (area2 == null)
							{
								mShopSub.TxtError += Message.Common.MSG_PLEASE_ENTER.Replace("$item", ParamConst.TextItem.AREA2)
																					.Replace("$value", Message.MShop.MSG_AREA2_VALUE)
																					.Replace("$text", Message.Common.MSG_PLEASE_ENTER_ONE) + dLine;
							}
							else
							{
								mShopSub.Area2Cd = int.Parse(area2.KbnValue);
							}
						}
						#endregion

						mShopSub.TxtArea2Cd = list[i][ParamConst.IdxDataCsvShop.AREA2_CD];

						#region エリア
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.AREA_CD]))
						{
							area = mkbnValues.FirstOrDefault(x => x.KbnCd == ParamConst.Area.KBN_CD &&
																	x.KbnValueName == list[i][ParamConst.IdxDataCsvShop.AREA_CD]);
							if (area == null)
							{
								mShopSub.TxtError += Message.Common.MSG_PLEASE_ENTER.Replace("$item", ParamConst.TextItem.AREA)
																					.Replace("$value", Message.MShop.MSG_AREA_VALUE)
																					.Replace("$text", Message.Common.MSG_PLEASE_ENTER_ONE) + dLine;
							}
							else
							{
								mShopSub.AreaCd = int.Parse(area.KbnValue);
							}
						}
						#endregion

						mShopSub.TxtAreaCd = list[i][ParamConst.IdxDataCsvShop.AREA_CD];

						#region メールアドレス
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.MAIL_ADDRESS]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.MAIL_ADDRESS) + dLine;
						}
						else if (list[i][ParamConst.IdxDataCsvShop.MAIL_ADDRESS].Length > 50)
						{
							mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.MAIL_ADDRESS).Replace("$length", "50") + dLine;
						}
						else if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.MAIL_ADDRESS], ParamConst.Regex.REGEX_MAIL).Success)
						{
							mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.MAIL_ADDRESS) + dLine;
						}
						#endregion

						mShopSub.MailAddress = list[i][ParamConst.IdxDataCsvShop.MAIL_ADDRESS];

						#region TEL
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.PHONE_NUMBER]))
						{
							if (list[i][ParamConst.IdxDataCsvShop.PHONE_NUMBER].Length < 9)
							{
								mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.TEL) + dLine;
							}
							else if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.PHONE_NUMBER], ParamConst.Regex.REGEX_TEL_FAX).Success)
							{
								mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.TEL) + dLine;
							}
							else if (list[i][ParamConst.IdxDataCsvShop.PHONE_NUMBER].Length > 15)
							{
								mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.TEL).Replace("$length", "15") + dLine;
							}
						}
						#endregion

						mShopSub.PhoneNumber = list[i][ParamConst.IdxDataCsvShop.PHONE_NUMBER];

						#region FAX
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.FAX]))
						{
							if (list[i][ParamConst.IdxDataCsvShop.PHONE_NUMBER].Length < 9)
							{
								mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.FAX) + dLine;
							}
							else if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.FAX], ParamConst.Regex.REGEX_TEL_FAX).Success)
							{
								mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.FAX) + dLine;
							}
							else if (list[i][ParamConst.IdxDataCsvShop.FAX].Length > 15)
							{
								mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.FAX).Replace("$length", "15") + dLine;
							}
						}
						#endregion

						mShopSub.FaxNumber = list[i][ParamConst.IdxDataCsvShop.FAX];

						#region 出退店区分
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.STATUS]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.STORE_STATUS) + dLine;
						}
						else if (
								list[i][ParamConst.IdxDataCsvShop.STATUS] != ParamConst.ShopStatus.OPEN_TEXT &&
								list[i][ParamConst.IdxDataCsvShop.STATUS] != ParamConst.ShopStatus.CLOSE_TEXT
								)
						{
							mShopSub.TxtError += Message.Common.MSG_PLEASE_IMPUT_ONE.Replace("$item", ParamConst.TextItem.STORE_STATUS)
																					.Replace("$value", Message.MShop.MSG_STATUS_VALUE) + dLine;
						}
						else
						{
							mShopSub.Status = list[i][ParamConst.IdxDataCsvShop.STATUS] == ParamConst.ShopStatus.OPEN_TEXT ? false : true;
						}
						#endregion

						mShopSub.TextStatus = list[i][ParamConst.IdxDataCsvShop.STATUS];

						#region 開店日
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.START_DATE]))
						{
							if (list[i][ParamConst.IdxDataCsvShop.STATUS] == ParamConst.ShopStatus.OPEN_TEXT)
							{
								mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.START_DATE) + dLine;
							}
						}
						else if (list[i][ParamConst.IdxDataCsvShop.START_DATE].Length != 8)
						{
							mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.START_DATE).Replace("$length", "8") + dLine;
						}
						else
						{
							isStartDateFormat = false;
							DateTime startDate;
							if (!DateTime.TryParseExact(
								list[i][ParamConst.IdxDataCsvShop.START_DATE],
								"yyyyMMdd",
								CultureInfo.InvariantCulture,
								DateTimeStyles.AssumeUniversal,
								out startDate))
							{
								mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.START_DATE) + dLine;
							}
							else
							{
								isStartDateFormat = true;
							}
						}
						#endregion

						mShopSub.StartDate = list[i][ParamConst.IdxDataCsvShop.START_DATE];

						#region 閉店日
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.END_DATE]))
						{
							if (list[i][ParamConst.IdxDataCsvShop.STATUS] == ParamConst.ShopStatus.CLOSE_TEXT)
							{
								mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.END_DATE) + dLine;
							}
						}
						else if (list[i][ParamConst.IdxDataCsvShop.END_DATE].Length != 8)
						{
							mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.END_DATE).Replace("$length", "8") + dLine;
						}
						else
						{
							isEndDateFormat = false;
							DateTime endDate;
							if (!DateTime.TryParseExact(
								list[i][ParamConst.IdxDataCsvShop.END_DATE],
								"yyyyMMdd",
								CultureInfo.InvariantCulture,
								DateTimeStyles.AssumeUniversal,
								out endDate))
							{
								mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.END_DATE) + dLine;
							}
							else
							{
								isEndDateFormat = true;
							}
						}
						#endregion

						mShopSub.EndDate = list[i][ParamConst.IdxDataCsvShop.END_DATE];

						if (isStartDateFormat && isEndDateFormat)
						{
							if (Int32.Parse(mShopSub.StartDate) > Int32.Parse(mShopSub.EndDate))
							{
								mShopSub.TxtError += Message.MShop.MSG_DATE_OPEN_THAN_DATE_CLOSE + dLine;
							}
						}

						#region 所属部門
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.AFFILIATE_DEPARTMENT_CD]))
						{
							if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.AFFILIATE_DEPARTMENT_CD], ParamConst.Regex.REGEX_NUMBER).Success)
							{
								mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.AFFILIATE_DEPARTMENT_CD) + dLine;
							}
							else if (list[i][ParamConst.IdxDataCsvShop.AFFILIATE_DEPARTMENT_CD].Length > 4)
							{
								mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.AFFILIATE_DEPARTMENT_CD).Replace("$length", "4") + dLine;
							}
						}
						#endregion

						mShopSub.AffiliateDepartmentCd = list[i][ParamConst.IdxDataCsvShop.AFFILIATE_DEPARTMENT_CD];

						#region 所属部門名
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.AFFILIATE_DEPARTMENT_NAME]))
						{
							if (list[i][ParamConst.IdxDataCsvShop.AFFILIATE_DEPARTMENT_NAME].Length > 30)
							{
								mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.AFFILIATE_DEPARTMENT_NAME).Replace("$length", "30") + dLine;
							}
						}
						#endregion

						mShopSub.AffiliateDepartmentName = list[i][ParamConst.IdxDataCsvShop.AFFILIATE_DEPARTMENT_NAME];

						#region 営業時間
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.OPEN_TIME]))
						{
							timeArr = list[i][ParamConst.IdxDataCsvShop.OPEN_TIME].Split('-');
							if (
								timeArr.Count() != 2 ||
								!Regex.Match(timeArr[0], ParamConst.Regex.REGEX_TIME).Success ||
								!Regex.Match(timeArr[1], ParamConst.Regex.REGEX_TIME).Success
								)
							{
								mShopSub.TxtError += Message.MShop.MSG_OPEN_TIME_INCORRECT.Replace("*", ParamConst.TextItem.OPEN_TIME) + dLine;
							}
							else
							{
								timeOpen = int.Parse(timeArr[0].Replace(":", ""));
								timeClose = int.Parse(timeArr[1].Replace(":", ""));
								if (timeOpen > timeClose)
								{
									mShopSub.TxtError += Message.MShop.MSG_TIME_OPEN_THAN_TIME_CLOSE + dLine;
								}
							}
						}
						#endregion

						mShopSub.OpenTime = list[i][ParamConst.IdxDataCsvShop.OPEN_TIME];

						#region 坪数
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.SQUARE]))
						{
							if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.SQUARE], ParamConst.Regex.REGEX_FLOAT).Success)
							{
								mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.SQUARE) + dLine;
							}
							else
							{
								mShopSub.Square = float.Parse(list[i][ParamConst.IdxDataCsvShop.SQUARE]);
							}
						}
						#endregion

						mShopSub.TextSquare = list[i][ParamConst.IdxDataCsvShop.SQUARE];

						#region 営業担当者
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.SALE_PERSON_CD]))
						{
							if (!Regex.Match(list[i][ParamConst.IdxDataCsvShop.SALE_PERSON_CD], ParamConst.Regex.REGEX_NUMBER).Success)
							{
								mShopSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.SALE_PERSON_CD) + dLine;
							}
							else if (list[i][ParamConst.IdxDataCsvShop.SALE_PERSON_CD].Length > 3)
							{
								mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.SALE_PERSON_CD).Replace("$length", "3") + dLine;
							}
						}
						#endregion

						mShopSub.SalePersonCd = list[i][ParamConst.IdxDataCsvShop.SALE_PERSON_CD];

						#region 営業担当者名
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.SALE_PERSON_NAME]))
						{
							if (list[i][ParamConst.IdxDataCsvShop.SALE_PERSON_NAME].Length > 30)
							{
								mShopSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.SALE_PERSON_NAME).Replace("$length", "30") + dLine;
							}
						}
						#endregion

						mShopSub.SalePersonName = list[i][ParamConst.IdxDataCsvShop.SALE_PERSON_NAME];

						#region 免税使用
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.USED_DUTY_FREE]))
						{
							if (
								list[i][ParamConst.IdxDataCsvShop.USED_DUTY_FREE] != ParamConst.UsedDutyFree.FREE_TEXT &&
								list[i][ParamConst.IdxDataCsvShop.USED_DUTY_FREE] != ParamConst.UsedDutyFree.NOT_FREE_TEXT
								)
							{
								mShopSub.TxtError += Message.Common.MSG_PLEASE_IMPUT_ONE.Replace("$item", ParamConst.TextItem.USED_DUTY_FREE)
																						.Replace("$value", Message.MShop.MSG_USED_DUTY_FREE_VALUE) + dLine;
							}
							else
							{
								mShopSub.UsedDutyFree = list[i][ParamConst.IdxDataCsvShop.USED_DUTY_FREE] == ParamConst.UsedDutyFree.FREE_TEXT ? false : true;
							}
						}
						#endregion

						mShopSub.TextUsedDutyFree = list[i][ParamConst.IdxDataCsvShop.USED_DUTY_FREE];

						#region 契約形態
						if (!string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.CONTRACT_TYPE]))
						{
							contractType = mkbnValues.FirstOrDefault(x => x.KbnCd == ParamConst.ContractType.KBN_CD &&
																		x.KbnValueName == list[i][ParamConst.IdxDataCsvShop.CONTRACT_TYPE]);
							if (contractType == null)
							{
								mShopSub.TxtError += Message.Common.MSG_PLEASE_ENTER.Replace("$item", ParamConst.TextItem.CONTRACT_TYPE)
																					.Replace("$value", Message.MShop.MSG_CONTRACT_TYPE_VALUE)
																					.Replace("$text", Message.Common.MSG_PLEASE_ENTER_ONE) + dLine;
							}
							else
							{
								mShopSub.ContractType = int.Parse(contractType.KbnValue);
							}
						}
						#endregion

						mShopSub.TextContractType = list[i][ParamConst.IdxDataCsvShop.CONTRACT_TYPE];

						#region 表示/非表示
						if (string.IsNullOrEmpty(list[i][ParamConst.IdxDataCsvShop.DISPLAY_FLG]))
						{
							mShopSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.DISPLAY_FLG) + dLine;
						}
						else if (
								list[i][ParamConst.IdxDataCsvShop.DISPLAY_FLG] != ParamConst.ShowHiddenItem.SHOW_TEXT &&
								list[i][ParamConst.IdxDataCsvShop.DISPLAY_FLG] != ParamConst.ShowHiddenItem.HIDDEN_TEXT
								)
						{
							mShopSub.TxtError += Message.Common.MSG_PLEASE_IMPUT_ONE.Replace("$item", ParamConst.TextItem.DISPLAY_FLG)
																					.Replace("$value", Message.MShop.MSG_DISPLAY_FLG_VALUE) + dLine;
						}
						else
						{
							mShopSub.DisplayFlg = list[i][ParamConst.IdxDataCsvShop.DISPLAY_FLG] == ParamConst.ShowHiddenItem.SHOW_TEXT ? false : true;
						}
						#endregion

						mShopSub.TextDisplayFlg = list[i][ParamConst.IdxDataCsvShop.DISPLAY_FLG];

						mShopSub.BussinessCd = bussinessCd;

						#endregion

						lstMShopSub.Add(mShopSub);

						isStartDateFormat = false;
						isEndDateFormat = false;
					}
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return new { error = Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.FILE_CSV) };
			}

			return new { listShopCsv = lstMShopSub };
		}
		#endregion

		#region CSVから店舗を追加する
		/// <summary>
		/// CSVから店舗を追加する
		/// </summary>
		/// <param name="mShopSubs"></param>
		/// <param name="lstShopCdUpd"></param>
		/// <returns></returns>
		public object InsertDataCsv(List<MShopSub> mShopSubs, string lstShopCdUpd)
		{
			long resultInsShop = 0;
			bool isInsertSuccess = false;
			List<MShop> mShops = null;
			StringBuilder sqlImportCsv = new StringBuilder("");

			try
			{
				if (!string.IsNullOrEmpty(lstShopCdUpd))
				{
					MShop mShop = new MShop();
					mShop.ListShopCd = lstShopCdUpd;
					mShops = UnitOfWork.MShop.GetAllAsync(mShop).Result;
				}

				using (var transactionScope = new TransactionScope())
				{
					MShop mShopUpd = null;
					for (int i = 0; i < mShopSubs.Count; i++)
					{
						if (mShopSubs[i].IsAdd)
						{
							CreateSqlShopCSVQuery(mShopSubs[i], sqlImportCsv);
						}
						else
						{
							mShopUpd = mShops.FirstOrDefault(x => x.ShopCd == mShopSubs[i].ShopCd);
							CreateSqlShopCSVQuery(mShopSubs[i], sqlImportCsv, mShopUpd != null ? mShopUpd.AddrId : 0);
						}
					}
					resultInsShop = UnitOfWork.MShop.InsertOrUpdateDataCSV(sqlImportCsv.ToString()).Result;

					isInsertSuccess = (resultInsShop > 0);
					if (isInsertSuccess)
					{
						transactionScope.Complete();
					}
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
			}

			return new { isInsertSuccess = isInsertSuccess };
		}
		#endregion

		#region CreateInsertShopCSVQuery
		/// <summary>
		/// Create query insert shop, address, muserLogin
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <param name="sqlImportCsv"></param>
		/// <param name="addrId"></param>
		/// <returns></returns>
		private StringBuilder CreateSqlShopCSVQuery(MShopSub mShopSub, StringBuilder sqlImportCsv, int addrId = 0)
		{
			string zipCodeDsp = !string.IsNullOrEmpty(mShopSub.ZipCodeDsp) ? mShopSub.ZipCodeDsp
													: mShopSub.ZipCd.Substring(0, 3) + "-" + mShopSub.ZipCd.Substring(3);

			if (addrId == 0)
			{
				sqlImportCsv.Append($@"
								INSERT INTO maddress (
														ZipCd
														, ZipCodeDsp
														, Province
														, AdrCd1
														, AdrCd2
														, AdrType
														, CreateUserCd
														, UpdateUserCd
													)
											VALUES (
													  '{mShopSub.ZipCd}'
													, '{zipCodeDsp}'
													, '{mShopSub.Province}'
													, '{mShopSub.AdrCd1}'
													, '{mShopSub.AdrCd2}'
													, '{ParamConst.AdrType.ADR_SHOP}'
													, '{mShopSub.UpdateUserCd}'
													, '{mShopSub.UpdateUserCd}'
												);

								INSERT INTO mshop (
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
													, CreateUserCd
													, UpdateUserCd
												)
										VALUES (
												  '{mShopSub.ShopCd}'
												, '{mShopSub.ShopName}'
												, '{mShopSub.AreaCd}'
												, '{mShopSub.Area2Cd}'
												,  (SELECT LAST_INSERT_ID())
												, '{mShopSub.MailAddress}'
												, '{mShopSub.PhoneNumber}'
												, '{mShopSub.FaxNumber}'
												,  {mShopSub.Status}
												, '{mShopSub.StartDate}'
												, '{mShopSub.EndDate}'
												, '{mShopSub.AffiliateDepartmentCd}'
												, '{mShopSub.AffiliateDepartmentName}'
												, '{mShopSub.OpenTime}'
												,  {mShopSub.Square}
												, '{mShopSub.BussinessCd}'
												, '{mShopSub.SalePersonCd}'
												, '{mShopSub.SalePersonName}'
												,  {mShopSub.UsedDutyFree}
												,  {mShopSub.ContractType}
												,  {mShopSub.DisplayFlg}
												, '{mShopSub.UpdateUserCd}'
												, '{mShopSub.UpdateUserCd}'
												);

								INSERT INTO muserlogin (
														  UserCd
														, Password
														, Department
														, RefreshToken
														, RefreshTokenExpiryTime
														, CreateUserCd
														, UpdateUserCd
														)
												VALUES (
														  '{mShopSub.ShopCd}'
														, md5('{mShopSub.ShopCd}')
														, {ParamConst.Department.SHOP}
														, md5('{mShopSub.ShopCd}')
														, now()
														, '{mShopSub.UpdateUserCd}'
														, '{mShopSub.UpdateUserCd}'
														);
							");
			}
			else
			{
				sqlImportCsv.Append($@"
										UPDATE maddress
										SET
											  ZipCd = '{mShopSub.ZipCd}'
											, ZipCodeDsp = '{zipCodeDsp}'
											, Province = '{mShopSub.Province}'
											, AdrCd1 = '{mShopSub.AdrCd1}'
											, AdrCd2 = '{mShopSub.AdrCd2}'
											, UpdateUserCd = '{mShopSub.UpdateUserCd}'
										WHERE AddrId = { addrId };

										UPDATE mshop
										SET
											  ShopName = '{ mShopSub.ShopName }'
											, AreaCd = '{ mShopSub.AreaCd }'
											, Area2Cd = '{ mShopSub.Area2Cd }'
											, MailAddress = '{ mShopSub.MailAddress }'
											, PhoneNumber = '{ mShopSub.PhoneNumber }'
											, FaxNumber = '{ mShopSub.FaxNumber }'
											, Status = { mShopSub.Status }
											, StartDate = '{mShopSub.StartDate}'
											, EndDate = '{mShopSub.EndDate}'
											, AffiliateDepartmentCd = '{mShopSub.AffiliateDepartmentCd}'
											, AffiliateDepartmentName = '{mShopSub.AffiliateDepartmentName}'
											, OpenTime = '{mShopSub.OpenTime}'
											, Square = { mShopSub.Square }
											, BussinessCd = { mShopSub.BussinessCd }
											, SalePersonCd = '{mShopSub.SalePersonCd}'
											, SalePersonName = '{mShopSub.SalePersonName}'
											, UsedDutyFree = { mShopSub.UsedDutyFree }
											, ContractType = { mShopSub.ContractType }
											, DisplayFlg = { mShopSub.DisplayFlg }
											, UpdateUserCd = '{ mShopSub.UpdateUserCd }'
										WHERE ShopCd = '{ mShopSub.ShopCd }';
									");
			}

			return sqlImportCsv;
		}
		#endregion

		#region 店舗の住所を追加する
		/// <summary>
		/// 店舗の住所を追加する
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <returns></returns>
		private long InsertMAddress(MShopSub mShopSub)
		{
			long addrId = 0;

			MAddress mAddress = new MAddress();
			mAddress.ZipCd = mShopSub.ZipCd;
			mAddress.ZipCodeDsp = !string.IsNullOrEmpty(mShopSub.ZipCodeDsp) ? mShopSub.ZipCodeDsp
																				: mShopSub.ZipCd.Substring(0, 3) + "-" + mShopSub.ZipCd.Substring(3);
			mAddress.Province = mShopSub.Province;
			mAddress.AdrCd1 = mShopSub.AdrCd1;
			mAddress.AdrCd2 = mShopSub.AdrCd2;
			mAddress.AdrType = ParamConst.AdrType.ADR_SHOP;
			mAddress.CreateUserCd = mShopSub.UpdateUserCd;
			mAddress.UpdateUserCd = mShopSub.UpdateUserCd;

			addrId = UnitOfWork.MAddress.AddAsync(mAddress).Result;
			return addrId;
		}
		#endregion

		#region 店舗の住所を更新する
		/// <summary>
		/// 店舗の住所を更新する
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <returns></returns>
		private bool UpdateMAddress(MShopSub mShopSub)
		{
			bool resultUpdAdrShop = false;
			MAddress mAddress = new MAddress();
			mAddress.AddrId = mShopSub.AddrId;
			mAddress.ZipCd = mShopSub.ZipCd;
			mAddress.ZipCodeDsp = mShopSub.ZipCodeDsp;
			mAddress.Province = mShopSub.Province;
			mAddress.AdrCd1 = mShopSub.AdrCd1;
			mAddress.AdrCd2 = mShopSub.AdrCd2;
			mAddress.AdrType = ParamConst.AdrType.ADR_SHOP;
			mAddress.CreateUserCd = mShopSub.CreateUserCd;
			mAddress.UpdateUserCd = mShopSub.UpdateUserCd;

			List<MAddress> mAddresses = new List<MAddress>();
			mAddresses.Add(mAddress);

			resultUpdAdrShop = UnitOfWork.MAddress.UpdateAsync(mAddresses).Result;
			return resultUpdAdrShop;
		}
		#endregion

		#region ログインユーザー情報をマッピングする
		/// <summary>
		/// ログインユーザー情報をマッピングする
		/// </summary>
		/// <param name="mShopSub"></param>
		/// <returns></returns>
		private MUserLogin ConvertObjectMUserLogin(MShopSub mShopSub)
		{
			MUserLogin mUserLogin = new MUserLogin();
			mUserLogin.UserCd = mShopSub.ShopCd;
			mUserLogin.Password = Common.MD5Hash(mShopSub.ShopCd);
			mUserLogin.Department = ParamConst.Department.SHOP;
			mUserLogin.RefreshToken = Common.MD5Hash(mShopSub.ShopCd);
			mUserLogin.RefreshTokenExpiryTime = DateTime.Now;
			mUserLogin.CreateUserCd = mShopSub.UpdateUserCd;
			mUserLogin.UpdateUserCd = mShopSub.UpdateUserCd;

			return mUserLogin;
		}
		#endregion

		#region 郵便番号による郵便一覧
		/// <summary>
		/// 郵便番号による郵便一覧
		/// </summary>
		/// <param name="zipCd"></param>
		/// <returns></returns>
		private MPostCode GetMPostCodeByZipCd(string zipCd)
		{
			MPostCode mPostCode = new MPostCode();
			mPostCode.ZipCd = zipCd;
			List<MPostCode> mPostCodes = UnitOfWork.MPostCode.GetAllAsync(mPostCode).Result;

			return mPostCodes.Count > 0 ? mPostCodes[0] : null;
		}
		#endregion
	}
}
