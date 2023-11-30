using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Services
{
	public class FrontScreenService : BaseService, IFrontScreenService
	{
		public FrontScreenService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#region 条件によるお客様サイトレイアウト一覧を取得する(画像が含まれる)
		/// <summary>
		/// 条件によるお客様サイトレイアウト一覧を取得する(画像が含まれる)
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<FrontScreenSub>> GetAllSubAsync(FrontScreenSub frontScreenSub, string sortColumnName, string sortType)
		{
			return UnitOfWork.FrontScreen.GetAllSubAsync(frontScreenSub, sortColumnName, sortType);
		}
		#endregion

		#region 条件によるお客様サイトレイアウト一覧を取得する(画像が含まれない)
		/// <summary>
		/// 条件によるお客様サイトレイアウト一覧を取得する(画像が含まれない)
		/// </summary>
		/// <param name="frontScreen"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<FrontScreen>> GetAllAsync(FrontScreen frontScreen, string sortColumnName, string sortType)
		{
			return UnitOfWork.FrontScreen.GetAllAsync(frontScreen);
		}
		#endregion
		public Tuple<List<FrontScreen>, int> GetDataPagination(int currentpage, int pageSize, FrontScreen frontScreen, string sortColumnName, string sortType)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// MProductSubを更新する
		/// </summary>
		/// <param name="lstImage"></param>
		/// <param name="files"></param>
		/// <param name="mProductSub"></param>
		/// <param name="lstMProductImgUrlDelete"></param>
		/// <returns></returns>
		public bool UpdateAsyncSub(List<FrontScreenImgUrl> lstImage, IFormFileCollection files, FrontScreen frontScreen, List<FrontScreenImgUrl> lstImgDelete, bool isProductListScreen)
		{
			using (var transactionScope = new TransactionScope())
			{
				List<FrontScreen> lstFrontScreen = new List<FrontScreen>();
				lstFrontScreen.Add(frontScreen);
				// FrontScreenを更新する
				bool resultUpdateFrontScreen = UnitOfWork.FrontScreen.UpdateAsync(lstFrontScreen).Result;
				long resultAddFrontScreenImgUrl = 1;
				if (resultUpdateFrontScreen)
				{
					// FrontScreenImgを更新する
					if (lstImgDelete.Count != 0)
					{
						bool resultDeleteFrontScreenImgUrl = UnitOfWork.FrontScreenImgUrl.UpdateAsync(lstImgDelete).Result;
					}
					if (files.Count > 0)
					{
						List<FrontScreenImgUrl> lstFrontScreenImgUrlAdd = new List<FrontScreenImgUrl>();
						List<FrontScreenImgUrl> lstFrontScreenImgUrlUpd = new List<FrontScreenImgUrl>();

						for (int i = 0; i < lstImage.Count; i++)
						{
							InsertImagetoFolder(i, isProductListScreen, lstImage[i], files);
							if (lstImage[i].ScrImgUrlId == 0)
							{
								lstFrontScreenImgUrlAdd.Add(lstImage[i]);
							}
							else
							{
								lstFrontScreenImgUrlUpd.Add(lstImage[i]);
							}
						}
						// FrontScreenImgを更新する
						if (lstFrontScreenImgUrlUpd.Count != 0)
						{
							resultUpdateFrontScreen = this.UnitOfWork.FrontScreenImgUrl.UpdateAsync(lstFrontScreenImgUrlUpd).Result;
						}
						if (lstFrontScreenImgUrlAdd.Count != 0)
						{
							// FrontScreenImgを追加する
							resultAddFrontScreenImgUrl = UnitOfWork.FrontScreenImgUrl.AddListAsync(lstFrontScreenImgUrlAdd).Result;
						}
					}
				}
				if (resultUpdateFrontScreen && resultAddFrontScreenImgUrl != 0)
				{
					transactionScope.Complete();
				}
				return resultUpdateFrontScreen;
			}
		}
		/// <summary>
		/// Imageを追加する
		/// </summary>
		/// <param name="currentImg"></param>
		/// <param name="isProductListScreen"></param>
		/// <param name="frontScreenImg"></param>
		/// <param name="files"></param>
		private void InsertImagetoFolder(int currentImg, bool isProductListScreen, FrontScreenImgUrl frontScreenImg, IFormFileCollection files)
		{
			string imgFolder = Environment.CurrentDirectory;
			if (isProductListScreen)
			{
				imgFolder += ParamConst.PATH_IMAGE_PRODUCT_LIST + frontScreenImg.ImageUrl.Substring(0, frontScreenImg.ImageUrl.IndexOf("/"));
			}
			else
			{
				imgFolder += ParamConst.PATH_IMAGE_SCREEN + frontScreenImg.ImageUrl.Substring(0, frontScreenImg.ImageUrl.IndexOf("/"));
			}
			Directory.CreateDirectory(imgFolder);
			string path = Path.Combine(imgFolder, files[currentImg].FileName);
			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				files[currentImg].CopyTo(fileStream);
			}
		}
		public Task<bool> DeleteAsync(List<FrontScreen> frontScreens)
		{
			throw new NotImplementedException();
		}

		public Task<long> AddAsync(FrontScreen entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<FrontScreen> entity)
		{
			throw new NotImplementedException();
		}

		#region FrontScreenを追加する
		/// <summary>
		/// FrontScreenを追加する
		/// </summary>
		/// <param name="mBussiness"></param>
		/// <param name="bussinessCdDuplicate"></param>
		/// <param name="bussinessNameDuplicate"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		public long CreateFrontScreenByBussinessCd(MBussiness mBussiness, string bussinessCdDuplicate, string bussinessNameDuplicate, string updateUserCd)
		{
			long resultInsFrontScreen = 0;
			List<string> lstScrTypeGetImage = new List<string>();
			StringBuilder sqlInsertFrontScreen = new StringBuilder("");

			try
			{
				MkbnValue mkbnValue = new MkbnValue();
				mkbnValue.KbnCd = ParamConst.ScreenType.KBN_CD;
				List<MkbnValue> mkbnValues = UnitOfWork.MkbnValue.GetAllAsync(mkbnValue).Result;

				using (var transactionScope = new TransactionScope())
				{
					if (mkbnValues != null && mkbnValues.Count > 0)
					{
						for (int i = 0; i < mkbnValues.Count; i++)
						{
							CreateSqlFrontScreenQuery(mBussiness, sqlInsertFrontScreen, bussinessCdDuplicate, mkbnValues[i].KbnValue, updateUserCd);

							if (mkbnValues[i].KbnValue == ParamConst.ScreenType.TOP ||
								mkbnValues[i].KbnValue == ParamConst.ScreenType.PRODUCT_LIST)
							{
								CreateSqlFrontScreenImgQuery(mBussiness, sqlInsertFrontScreen, bussinessCdDuplicate, bussinessNameDuplicate, mkbnValues[i].KbnValue, updateUserCd);
								lstScrTypeGetImage.Add(mkbnValues[i].KbnValue);
							}
						}

						resultInsFrontScreen = UnitOfWork.FrontScreen.InsertFrontScreenByCreateBussiness(sqlInsertFrontScreen.ToString()).Result;

						if (resultInsFrontScreen > 0 && lstScrTypeGetImage.Count > 0)
						{
                            #region for add image top of product list
                            FrontScreenSub frontscreenSub = new FrontScreenSub();
							frontscreenSub.BussinessCd = mBussiness.BussinessCd;
							frontscreenSub.ScrType = ParamConst.ScreenType.PRODUCT_LIST;


							var lstFontScrSub = UnitOfWork.FrontScreen.GetAllSubAsync(frontscreenSub, "DspIndex", "ASC");

                            if (lstFontScrSub != null)
                            {
								List<FrontScreenSub> lstFontScrSubTmp = (List<FrontScreenSub>)lstFontScrSub.Result;
								var frontScreenSub = lstFontScrSubTmp.Where(item => item.ImagePosition == ParamConst.ImagePosition.TOP);

                                if (!frontScreenSub.ToList().Any())
                                {
									List<FrontScreenImgUrl> lstFrontScreenTopImgUrlAdd = new List<FrontScreenImgUrl>();
									FrontScreenImgUrl frontScreenImgUrl = new FrontScreenImgUrl();
									frontScreenImgUrl.ImagePosition = ParamConst.ImagePosition.TOP;
									frontScreenImgUrl.ScrId = lstFontScrSub.Result[0].ScrId;
									frontScreenImgUrl.ImageUrl = "default_top.jpg";
									frontScreenImgUrl.DspIndex = 1;
									frontScreenImgUrl.CreateUserCd = updateUserCd;
									lstFrontScreenTopImgUrlAdd.Add(frontScreenImgUrl);

									// Insert 
									long res = UnitOfWork.FrontScreenImgUrl.AddListAsync(lstFrontScreenTopImgUrlAdd).Result;
								}
							}
                            #endregion

                            for (int i = 0; i < lstScrTypeGetImage.Count; i++)
							{
								string pathFolder = GetPathFolder(lstScrTypeGetImage[i]);
								string pathDuplicate = Common.GetPathImageBarCd(bussinessNameDuplicate, pathFolder);
								string pathBussinessNew = Common.GetPathImageBarCd(mBussiness.BussinessName, pathFolder);
								string[] lstImage = Directory.GetFiles(pathDuplicate, "*.*");

								if (lstImage.Length > 0)
								{
									if (!Directory.Exists(pathBussinessNew))
									{
										Directory.CreateDirectory(pathBussinessNew);
									}

									foreach (string image in lstImage)
									{
										string imageName = image.Substring(pathDuplicate.Length + 1);
										File.Copy(Path.Combine(pathDuplicate, imageName), Path.Combine(pathBussinessNew, imageName), true);
									}
								}
							}
						}
						if (resultInsFrontScreen > 0)
						{
							transactionScope.Complete();
						}
					}
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
			}

			return resultInsFrontScreen;
		}
		#endregion

		#region Create sql insert for FrontScreen
		/// <summary>
		/// Create sql insert for FrontScreen
		/// </summary>
		/// <param name="mBussiness"></param>
		/// <param name="sqlInsertFrontScreen"></param>
		/// <param name="bussinessCdDuplicate"></param>
		/// <param name="scrType"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private StringBuilder CreateSqlFrontScreenQuery(MBussiness mBussiness,
														StringBuilder sqlInsertFrontScreen,
														string bussinessCdDuplicate,
														string scrType,
														string updateUserCd)
		{
			string conditionProductCd = string.Empty;
			if (scrType == ParamConst.ScreenType.PRODUCT_DETAIL)
			{
				conditionProductCd = @" AND ProductCd IS NULL ";
			}

			sqlInsertFrontScreen.Append($@"
										INSERT INTO frontscreen (
																	  ScrType
																	, TextTitle1
																	, TextTitle2
																	, TextTitle3
																	, TextArea1
																	, TextArea2
																	, TextButton1
																	, TextButton2
																	, TextNortify1
																	, TextNortify2
																	, CheckBoxFlg1
																	, CheckBoxFlg2
																	, ButtonFlg
																	, BussinessCd
																	, ProductCd
																	, CreateUserCd
																	, UpdateUserCd
																)
														SELECT
															  ScrType
															, TextTitle1
															, TextTitle2
															, TextTitle3
															, TextArea1
															, TextArea2
															, TextButton1
															, TextButton2
															, TextNortify1
															, TextNortify2
															, CheckBoxFlg1
															, CheckBoxFlg2
															, ButtonFlg
															, '{ mBussiness.BussinessCd }'
															, ProductCd
															, '{ updateUserCd }'
															, '{ updateUserCd }'
														FROM
															frontscreen
														WHERE
															ScrType = '{ scrType }' 
															AND BussinessCd = '{ bussinessCdDuplicate }'
															{ conditionProductCd };
										");
			return sqlInsertFrontScreen;
		}
		#endregion

		#region Create sql insert for FrontScreenImg
		/// <summary>
		/// Create sql insert for FrontScreenImg
		/// </summary>
		/// <param name="mBussiness"></param>
		/// <param name="sqlInsertFrontScreen"></param>
		/// <param name="bussinessCdDuplicate"></param>
		/// <param name="bussinessNameDuplicate"></param>
		/// <param name="scrType"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		private StringBuilder CreateSqlFrontScreenImgQuery(MBussiness mBussiness,
															StringBuilder sqlInsertFrontScreen,
															string bussinessCdDuplicate,
															string bussinessNameDuplicate,
															string scrType,
															string updateUserCd)
		{
			sqlInsertFrontScreen.Append($@"
										INSERT INTO frontscreenimgurl (
																		  ScrId
																		, ImageUrl
																		, ImagePosition
																		, DspIndex
																		, CreateUserCd
																		, UpdateUserCd
																	)
															SELECT
																	  (SELECT LAST_INSERT_ID())
																	, REPLACE(ImageUrl,
																				LOWER('{ bussinessNameDuplicate }'),
																				LOWER('{ mBussiness.BussinessName }'))
																	, ImagePosition
																	, DspIndex
																	, '{ updateUserCd }'
																	, '{ updateUserCd }'
															FROM
																frontscreenimgurl
															WHERE
																ScrId = (SELECT ScrId
																			FROM frontscreen
																			WHERE ScrType = '{ scrType }' 
																				AND BussinessCd = '{ bussinessCdDuplicate }')
																				AND DelFlg = {ParamConst.DelFlg.OFF};
										");
			return sqlInsertFrontScreen;
		}
		#endregion

		#region Get path Folder
		/// <summary>
		/// Get path Folder
		/// </summary>
		/// <param name="scrType"></param>
		/// <returns></returns>
		private string GetPathFolder(string scrType)
		{
			string pathFolder = string.Empty;

			if (scrType == ParamConst.ScreenType.TOP)
			{
				pathFolder = ParamConst.PATH_IMAGE_SCREEN;
			}
			else if (scrType == ParamConst.ScreenType.PRODUCT_LIST)
			{
				pathFolder = ParamConst.PATH_IMAGE_PRODUCT_LIST;
			}

			return pathFolder;
		}
		#endregion
	}
}
