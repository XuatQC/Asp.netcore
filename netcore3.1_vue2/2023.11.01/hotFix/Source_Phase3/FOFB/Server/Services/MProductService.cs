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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Services
{
	public class MProductService : BaseService, IMProductService
	{
		public MProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>
		/// 条件による商品一覧を取得する
		/// </summary>
		/// <param name="mProductSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MProductSub>> GetAllSubAsync(MProductSub mProductSub, string sortColumnName, string sortType)
		{
			return UnitOfWork.MProduct.GetAllSubAsync(mProductSub, sortColumnName, sortType);
		}

		#region 条件による商品一覧を取得する
		/// <summary>
		/// 条件による商品一覧を取得する
		/// </summary>
		/// <param name="mProduct"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<MProduct>> GetAllAsync(MProduct mProduct, string sortColumnName, string sortType)
		{
			return UnitOfWork.MProduct.GetAllAsync(mProduct, sortColumnName, sortType);
		}
		#endregion

		/// <summary>
		/// CheckDataCsv
		/// </summary>
		/// <param name="files"></param>
		/// <returns></returns>
		public object CheckDataCsv(IFormFileCollection files, string bussinessCd)
		{
			string dLine = "\n";
			List<List<string>> listProductCsv = Common.ReadCsv(files[0], "utf-8");
			List<MProductSub> lstMProductSub = new List<MProductSub>();

			try
			{
				MProduct mProductCheck = new MProduct();
				List<MProduct> lstMProductCheck = UnitOfWork.MProduct.GetAllAsync(mProductCheck, null, null).Result;

				MProductSub mProductSubCheck = new MProductSub();
				mProductSubCheck.BussinessCd = bussinessCd;
				List<MProductSub> lstMProductSubCheck = UnitOfWork.MProduct.GetListProductMaintain(mProductSubCheck, null, null).Result;

				MSize mSize = new MSize();
				List<MSize> lstMSize = UnitOfWork.MSize.GetAllAsync(mSize).Result;

				MColor mColor = new MColor();
				List<MColor> lstMColor = UnitOfWork.MColor.GetAllAsync(mColor).Result;

				MBrandRelation mBrandRelation = new MBrandRelation();
				mBrandRelation.BussinessCd = bussinessCd;
				List<MBrandRelation> lstBrandRelation = UnitOfWork.MBrandRelation.GetAllAsync(mBrandRelation).Result;

				MSubBrand mSubBrand = new MSubBrand();
				List<MSubBrand> lstSubBrand = UnitOfWork.MSubBrand.GetAllAsync(mSubBrand).Result;

				MBrand mBrand = new MBrand();
				List<MBrand> lstBrand = UnitOfWork.MBrand.GetAllAsync(mBrand).Result;

				decimal priceParse = 0;
				int countWrongInput = 0;
				int MAX_COUT_WRONG_INPUT = 11;

				if (listProductCsv.Count > 0)
				{
					MProductSub mProductSub = null;
					for (int i = 0; i < listProductCsv.Count; i++)
					{
						if (listProductCsv[i].Count != 11)
						{
							return new { error = Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.FILE_CSV) };
						}
						mProductSub = new MProductSub();

						//品番を確認する
						mProductSub.ProductCd = listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_CD];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_CD]))
						{
							mProductSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.PRODUCT_CD) + dLine;
							countWrongInput++;
						}
						else if (listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_CD].Length > 7)
						{
							mProductSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.PRODUCT_CD).Replace("$length", "7") + dLine;
						}
						else if (listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_CD].Length != 7)
						{
							mProductSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.PRODUCT_CD) + dLine;
						}
						else if (lstMProductCheck.Where(x => x.ProductCd == listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_CD]).ToList().Count != 0
							&& lstMProductSubCheck.Where(x => x.ProductCd == listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_CD]).ToList().Count == 0)
						{
							mProductSub.TxtError += Message.Common.MSG_ERROR_PRODUCT_CD + dLine;
						}

						//サイズを確認する
						mProductSub.SizeName = listProductCsv[i][ParamConst.IdxDataCsv.SIZE_NAME];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.SIZE_NAME]))
						{
							mProductSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.SIZE_NAME) + dLine;
							countWrongInput++;
						}
						else if (lstMSize.Where(x => x.SizeName == listProductCsv[i][ParamConst.IdxDataCsv.SIZE_NAME]).ToList().Count == 0)
						{
							mProductSub.TxtError += Message.Common.MSG_NOT_EXIST.Replace("*", ParamConst.TextItem.SIZE_NAME).Replace("$length", "1") + dLine;
						}
						else
						{
							mProductSub.SizeCd = lstMSize.FirstOrDefault(x => x.SizeName == listProductCsv[i][ParamConst.IdxDataCsv.SIZE_NAME]).SizeCd;
						}

						//カラーを確認する
						mProductSub.ColorName = listProductCsv[i][ParamConst.IdxDataCsv.COLOR_NAME];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.COLOR_NAME]))
						{
							mProductSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.COLOR_NAME) + dLine;
							countWrongInput++;
						}
						else if (lstMColor.Where(x => x.ColorName == listProductCsv[i][ParamConst.IdxDataCsv.COLOR_NAME]).ToList().Count == 0)
						{
							mProductSub.TxtError += Message.Common.MSG_NOT_EXIST.Replace("*", ParamConst.TextItem.COLOR_NAME).Replace("$length", "1") + dLine;
						}
						else
						{
							mProductSub.ColorCd = lstMColor.FirstOrDefault(x => x.ColorName == listProductCsv[i][ParamConst.IdxDataCsv.COLOR_NAME]).ColorCd;
						}

						//SKUを確認する
						mProductSub.Sku = mProductSub.ProductCd + mProductSub.ColorCd + mProductSub.SizeCd;

						//年を確認する
						mProductSub.TxtYear = listProductCsv[i][ParamConst.IdxDataCsv.YEAR];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.YEAR]))
						{
							countWrongInput++;
							mProductSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.YEAR) + dLine;
						}
						else if (!Regex.Match(listProductCsv[i][ParamConst.IdxDataCsv.YEAR], ParamConst.Regex.REGEX_NUMBER).Success || mProductSub.TxtYear.Length != 4)
						{
							mProductSub.TxtError += Message.Common.MSG_ERROR_YEAR + dLine;
						}

						//商品名を確認する
						mProductSub.ProductName = listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_NAME];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_NAME]))
						{
							countWrongInput++;
							mProductSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.PRODUCT_NAME) + dLine;
						}
						else if (listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_NAME].Length > 50)
						{
							mProductSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.PRODUCT_NAME).Replace("$length", "50") + dLine;
						}

						//商品名1を確認する
						mProductSub.ProductName1 = listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_NAME1];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_NAME1]))
						{
							countWrongInput++;
							mProductSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.PRODUCT_NAME1) + dLine;
						}
						else if (listProductCsv[i][ParamConst.IdxDataCsv.PRODUCT_NAME1].Length > 50)
						{
							mProductSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.PRODUCT_NAME1).Replace("$length", "50") + dLine;
						}

						//ブランド名を確認する
						mProductSub.BrandName = listProductCsv[i][ParamConst.IdxDataCsv.BRAND_NAME];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.BRAND_NAME]))
						{
							countWrongInput++;
							mProductSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.BRAND_NAME) + dLine;
						}
						else if (lstBrand.Where(x => x.BrandName == listProductCsv[i][ParamConst.IdxDataCsv.BRAND_NAME]).ToList().Count == 0)
						{
							mProductSub.TxtError += Message.Common.MSG_ERROR_BRAND_NAME + dLine;
						}
						else
						{
							mProductSub.BrandCd = lstBrand.FirstOrDefault(x => x.BrandName == listProductCsv[i][ParamConst.IdxDataCsv.BRAND_NAME]).BrandCd;
							mProductSub.BrandRltId = lstBrandRelation.FirstOrDefault(x => x.BrandCd == mProductSub.BrandCd).BrandRltId;
						}

						//サブブランド名を確認する
						mProductSub.SubBrand = listProductCsv[i][ParamConst.IdxDataCsv.BRAND_SUB];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.BRAND_SUB]))
						{
							countWrongInput++;
							if (lstSubBrand.Where(x => x.SubBrand == listProductCsv[i][ParamConst.IdxDataCsv.BRAND_SUB]
												 && x.BrandCd == mProductSub.BrandCd).ToList().Count == 0)
							{
								mProductSub.TxtError += Message.Common.MSG_ERROR_SUB_BRAND + dLine;
							}
						}
						else if (listProductCsv[i][ParamConst.IdxDataCsv.BRAND_SUB].Length > 30)
						{
							mProductSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.BRAND_SUB).Replace("$length", "30") + dLine;
						}
						else if (lstSubBrand.Where(x => x.SubBrand == listProductCsv[i][ParamConst.IdxDataCsv.BRAND_SUB]
								&& x.BrandCd == mProductSub.BrandCd).ToList().Count == 0)
						{
							mProductSub.TxtError += Message.Common.MSG_NOT_EXIST.Replace("*", ParamConst.TextItem.BRAND_SUB).Replace("$length", "1") + dLine;
						}
						else if (mProductSub.BrandCd != null)
						{
							mProductSub.SubBrandId = lstSubBrand.FirstOrDefault(x => x.BrandCd == mProductSub.BrandCd &&
														x.SubBrand == listProductCsv[i][ParamConst.IdxDataCsv.BRAND_SUB]).SubBrandId;
						}
						else
						{
							mProductSub.SubBrandId = lstSubBrand.FirstOrDefault(x => x.SubBrand == listProductCsv[i][ParamConst.IdxDataCsv.BRAND_SUB]).SubBrandId;
						}

						//在庫を確認する
						mProductSub.InventoryNumber = listProductCsv[i][ParamConst.IdxDataCsv.INVENTORY_NUMBER];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.INVENTORY_NUMBER]))
						{
							countWrongInput++;
							mProductSub.TxtError += Message.Common.MSG_ERROR_INVENTORY_NUMBER + dLine;
						}
						else if (!Regex.Match(listProductCsv[i][ParamConst.IdxDataCsv.INVENTORY_NUMBER], ParamConst.Regex.REGEX_NUMBER).Success || mProductSub.TxtYear.Length > 11)
						{
							mProductSub.TxtError += Message.Common.MSG_ERROR_INVENTORY_NUMBER + dLine;
						}
						else if (int.Parse(listProductCsv[i][ParamConst.IdxDataCsv.INVENTORY_NUMBER]) == 0)
						{
							mProductSub.TxtError += Message.Common.MSG_ERROR_INVENTORY_NUMBER + dLine;
						}

						//価格を確認する
						mProductSub.TxtPrice = listProductCsv[i][ParamConst.IdxDataCsv.PRICE];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.PRICE]))
						{
							countWrongInput++;
							mProductSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.PRICE) + dLine;
						}
						else if (!decimal.TryParse(mProductSub.TxtPrice, out priceParse)
								|| mProductSub.TxtPrice.Length > 18
								|| decimal.Parse(mProductSub.TxtPrice) <= 0)
						{
							mProductSub.TxtError += Message.Common.MSG_ERROR_WRONG_PRICE + dLine;
						}

						//表示を確認する
						mProductSub.TxtDisplay = listProductCsv[i][ParamConst.IdxDataCsv.DISPLAY];

						if (string.IsNullOrEmpty(listProductCsv[i][ParamConst.IdxDataCsv.DISPLAY]))
						{
							countWrongInput++;
							mProductSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.DISPLAY) + dLine;
						}
						else if (mProductSub.TxtDisplay != "表示" && mProductSub.TxtDisplay != "非表示")
						{
							mProductSub.TxtError += Message.Common.MSG_ERROR_DISPLAY + dLine;
						}

						if (countWrongInput != MAX_COUT_WRONG_INPUT)
						{
							lstMProductSub.Add(mProductSub);
						}
						countWrongInput = 0;
					}
				}
				return lstMProductSub;
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return new { error = Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.FILE_CSV) };
			}

		}
		public Tuple<List<MProduct>, int> GetDataPagination(int currentpage, int pageSize, MProduct mProduct, string sortColumnName, string sortType)
		{
			throw new NotImplementedException();
		}

		public Task<long> AddAsync(MProduct mProduct)
		{
			return UnitOfWork.MProduct.AddAsync(mProduct);
		}

		public Task<bool> UpdateAsync(List<MProduct> mProducts)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MProduct> mProducts)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 詳細画面の商品情報を取得する
		/// </summary>
		/// <param name="productCd"></param>
		/// <returns></returns>
		public List<MProductSub> GetlistProductForScreenDetail(string productCd)
		{
			return UnitOfWork.MProduct.GetlistProductForScreenDetail(productCd);
		}

		public Task<List<MProductSub>> GetListProductMaintain(MProductSub mProductSub, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.MProduct.GetListProductMaintain(mProductSub, sortColumnName, sortType);
		}
		/// <summary>
		/// MProductSubを追加する
		/// </summary>
		/// <param name="lstImage"></param>
		/// <param name="files"></param>
		/// <param name="mProductSub"></param>
		/// <returns></returns>
		public long AddAsyncSub(List<MProductImgUrl> lstImage, IFormFileCollection files, MProductSub mProductSub, string bussinessCd)
		{
			try
			{
				using (var transactionScope = new TransactionScope())
				{
					// Productを追加する
					long resultInsProduct = UnitOfWork.MProduct.AddAsync(mProductSub.DataProduct).Result;
					// FrontScreenを追加する
					List<FrontScreen> lstFrontScreen = new List<FrontScreen>();
					List<FrontScreen> lstFrontScreenAdd = new List<FrontScreen>();
					FrontScreen frontScreen = new FrontScreen();
					frontScreen.ScrType = ParamConst.SCREEN_PRODUCT_DETAIL;
					lstFrontScreen = UnitOfWork.FrontScreen.GetAllAsync(frontScreen).Result;
					frontScreen.TextTitle2 = lstFrontScreen[0].TextTitle2;
					frontScreen.TextArea1 = lstFrontScreen[0].TextArea1;
					frontScreen.TextArea2 = lstFrontScreen[0].TextArea2;
					frontScreen.TextButton1 = lstFrontScreen[0].TextButton1;
					frontScreen.TextButton2 = lstFrontScreen[0].TextButton2;
					frontScreen.TextButton2 = lstFrontScreen[0].TextButton2;
					frontScreen.TextNortify1 = lstFrontScreen[0].TextNortify1;
					frontScreen.TextNortify2 = lstFrontScreen[0].TextNortify2;
					frontScreen.CreateUserCd = mProductSub.DataProduct.CreateUserCd;
					frontScreen.BussinessCd = bussinessCd;
					frontScreen.ProductCd = mProductSub.DataProduct.ProductCd;
					lstFrontScreenAdd.Add(frontScreen);
					long resultInsFrontScreen = UnitOfWork.FrontScreen.AddAsync(lstFrontScreenAdd[0]).Result;
					long resultInsImage = 0;
					if (resultInsProduct != ParamConst.ResultInsertProduct.INSERT_FAIL && resultInsFrontScreen != 0)
					{
						// ProductDetailを追加する
						for (int i = 0; i < mProductSub.LstMProductDetail.Count; i++)
						{
							mProductSub.LstMProductDetail[i].CreateUserCd = mProductSub.DataProduct.CreateUserCd;
						}
						resultInsProduct = UnitOfWork.MProductDetail.AddListAsync(mProductSub.LstMProductDetail).Result;
						if (resultInsProduct != 0)
						{
							if (files.Count > 0)
							{
								// ProductImgを追加する
								resultInsImage = this.UnitOfWork.MProductImgUrl.AddListAsync(lstImage).Result;
								if (resultInsImage != 0)
								{
									// Imageを追加する
									string imgFolder = Environment.CurrentDirectory;
									string path = string.Empty;
									for (int i = 0; i < files.Count; i++)
									{
										imgFolder = Environment.CurrentDirectory;
										if (lstImage[i].IsMainInListPage == true)
										{
											imgFolder += ParamConst.PATH_IMAGE_PRODUCT_LIST;
										}
										else
										{
											imgFolder += ParamConst.PATH_IMAGE_PRODUCT_DETAIL;
										}
										imgFolder += lstImage[i].ImageUrl.Substring(0, lstImage[i].ImageUrl.IndexOf("/")) + "/" + lstImage[i].ProductCd;
										Directory.CreateDirectory(imgFolder);
										path = Path.Combine(imgFolder, files[i].FileName);
										using (var fileStream = new FileStream(path, FileMode.Create))
										{
											files[i].CopyTo(fileStream);
										}
									}
								}

							}
						}
					}
					transactionScope.Complete();
					return resultInsImage;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return 0;
			}

		}

		/// <summary>
		/// MProductSubを更新する
		/// </summary>
		/// <param name="lstImage"></param>
		/// <param name="files"></param>
		/// <param name="mProductSub"></param>
		/// <param name="lstMProductImgUrlDelete"></param>
		/// <returns></returns>
		public object UpdateAsyncSub(List<MProductImgUrl> lstImage, IFormFileCollection files, MProductSub mProductSub, List<MProductImgUrl> lstMProductImgUrlDelete)
		{
			try
			{
				using (var transactionScope = new TransactionScope())
				{
					List<MProduct> lstMProduct = new List<MProduct>();
					lstMProduct.Add(mProductSub.DataProduct);
					// Prodcutを更新する
					bool resultUpdateProduct = UnitOfWork.MProduct.UpdateAsync(lstMProduct).Result;
					if (resultUpdateProduct)
					{
						//商品詳細を削除する
						bool resultDeleteProductDetail = UnitOfWork.MProductDetail.DeleteAsync(mProductSub.lstProductDelete).Result;
						if (!resultDeleteProductDetail && mProductSub.lstProductDelete.Count != 0)
						{
							return new { error = Message.Common.MSG_ERROR_SIZE_WAS_ORDER };
						}
						MProductDetail mProductDetail = new MProductDetail();
						List<MProductDetail> lstProductDetail = UnitOfWork.MProductDetail.GetAllAsync(mProductDetail).Result;
						List<MProductDetail> lstProductDetailAdd = new List<MProductDetail>();
						List<MProductDetail> lstProductDetailUpd = new List<MProductDetail>();
						for (int i = 0; i < mProductSub.LstMProductDetail.Count; i++)
						{
							mProductDetail = lstProductDetail.FirstOrDefault(x => x.Sku == mProductSub.LstMProductDetail[i].Sku);
							if (mProductDetail == null)
							{
								mProductSub.LstMProductDetail[i].CreateUserCd = mProductSub.DataProduct.UpdateUserCd;
								lstProductDetailAdd.Add(mProductSub.LstMProductDetail[i]);
							}
							else
							{
								mProductSub.LstMProductDetail[i].UpdateUserCd = mProductSub.DataProduct.UpdateUserCd;
								lstProductDetailUpd.Add(mProductSub.LstMProductDetail[i]);
							}
						}
						// ProdcutDetailを更新する
						bool resultUpdateProductDetail = UnitOfWork.MProductDetail.UpdateAsync(lstProductDetailUpd).Result;
						if (resultUpdateProductDetail || lstProductDetailUpd.Count == 0)
						{
							if (lstProductDetailAdd.Count > 0)
							{
								// ProductDetailを追加する
								long resultAddProductDetail = UnitOfWork.MProductDetail.AddListAsync(lstProductDetailAdd).Result;
							}
							for (int i = 0; i < lstMProductImgUrlDelete.Count; i++)
							{
								lstMProductImgUrlDelete[i].UpdateUserCd = mProductSub.DataProduct.UpdateUserCd;
							}
							// ProdcutImgを更新する
							bool resultDeletelstProductImgUrl = UnitOfWork.MProductImgUrl.UpdateAsync(lstMProductImgUrlDelete).Result;
							if (files.Count > 0)
							{

								if (resultDeletelstProductImgUrl || lstMProductImgUrlDelete.Count == 0)
								{
									List<MProductImgUrl> lstProductImgUrlAdd = new List<MProductImgUrl>();
									List<MProductImgUrl> lstProductImgUrlUpd = new List<MProductImgUrl>();
									for (int i = 0; i < lstImage.Count; i++)
									{
										if (lstImage[i].ProductImgUrlId == 0)
										{
											lstImage[i].CreateUserCd = mProductSub.DataProduct.UpdateUserCd;
											lstProductImgUrlAdd.Add(lstImage[i]);
										}
										else
										{
											lstImage[i].UpdateUserCd = mProductSub.DataProduct.UpdateUserCd;
											lstProductImgUrlUpd.Add(lstImage[i]);
										}
									}
									// ProdcutImgを更新する
									bool resultUpdateImage = this.UnitOfWork.MProductImgUrl.UpdateAsync(lstProductImgUrlUpd).Result;
									if (resultUpdateImage || lstProductImgUrlUpd.Count == 0)
									{
										if (lstProductImgUrlAdd.Count > 0)
										{
											// ProductImgを追加する
											long resultAddProductImgUrl = UnitOfWork.MProductImgUrl.AddListAsync(lstProductImgUrlAdd).Result;
										}
										// Imageを追加する
										string imgFolder = Environment.CurrentDirectory;
										string path = string.Empty;
										int currentImg = 0;
										for (int i = 0; i < lstImage.Count; i++)
										{
											if (!lstImage[i].IsHaveFile)
											{
												continue;
											}
											imgFolder = Environment.CurrentDirectory;
											if (lstImage[i].IsMainInListPage)
											{
												imgFolder += ParamConst.PATH_IMAGE_PRODUCT_LIST;
											}
											else
											{
												imgFolder += ParamConst.PATH_IMAGE_PRODUCT_DETAIL;
											}
											imgFolder += lstImage[i].ImageUrl.Substring(0, lstImage[i].ImageUrl.IndexOf("/")) + "/" + lstImage[i].ProductCd;
											Directory.CreateDirectory(imgFolder);
											path = Path.Combine(imgFolder, files[currentImg].FileName);
											using (var fileStream = new FileStream(path, FileMode.Create))
											{
												files[currentImg].CopyTo(fileStream);
											}
											currentImg++;
										}
									}
								}
							}
						}
					}
					transactionScope.Complete();
					return resultUpdateProduct;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return null;
			}

		}

		#region CSVファイルから商品を追加する
		/// <summary>
		/// CSVファイルから商品を追加する
		/// </summary>
		/// <param name="lstProductSub"></param>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		public long InsertDataCsv(List<MProductSub> lstProductSub, string bussinessCd)
		{
			StringBuilder sqlImportCsv = new StringBuilder("");
			StringBuilder sqlQueryProduct = new StringBuilder("");
			StringBuilder sqlQueryFrontScreen = new StringBuilder("");
			StringBuilder sqlQueryProductDetai = new StringBuilder("");
			List<string> lstProductByCd = new List<string>();
			List<string> lstFrontScreenByCd = new List<string>();
			long resultInsProduct = 0;

			try
			{
				using (var transactionScope = new TransactionScope())
				{
					MProduct mProduct = new MProduct();
					List<MProduct> lstProduct = UnitOfWork.MProduct.GetAllAsync(mProduct).Result;

					FrontScreen frontScreen = new FrontScreen();
					frontScreen.ScrType = ParamConst.SCREEN_PRODUCT_DETAIL;
					List<FrontScreen> lstFrontScreen = UnitOfWork.FrontScreen.GetAllAsync(frontScreen).Result;

					MProductDetail mProductDetail = new MProductDetail();
					List<MProductDetail> lstMProductDetail = UnitOfWork.MProductDetail.GetAllAsync(mProductDetail).Result;

					FrontScreen frontScreenByProductCd = null;

					for (int i = 0; i < lstProductSub.Count; i++)
					{
						lstProductSub[i].Year = int.Parse(lstProductSub[i].TxtYear);
						lstProductSub[i].Price = decimal.Parse(lstProductSub[i].TxtPrice);
						lstProductSub[i].DisplayFlg = lstProductSub[i].TxtDisplay == ParamConst.ShowHiddenItem.HIDDEN_TEXT;

						if (lstProductByCd.Where(x => x == lstProductSub[i].ProductCd).ToList().Count == 0)
						{
							if (lstProduct.Where(x => x.ProductCd == lstProductSub[i].ProductCd).ToList().Count == 0)
							{
								CreateSqlProductCSVQuery(lstProductSub[i], sqlQueryProduct);
							}
							else
							{
								CreateSqlProductCSVQuery(lstProductSub[i], sqlQueryProduct, false);
							}
							lstProductByCd.Add(lstProductSub[i].ProductCd);
						}

						frontScreenByProductCd = lstFrontScreen.FirstOrDefault(x => x.ProductCd == lstProductSub[i].ProductCd);
						if (frontScreenByProductCd == null)
						{
							if (lstFrontScreenByCd.Where(x => x == lstProductSub[i].ProductCd).ToList().Count == 0)
							{
								CreateSqlFrontScreenCSVQuery(lstProductSub[i], sqlQueryFrontScreen, lstFrontScreen[0], bussinessCd);
								lstFrontScreenByCd.Add(lstProductSub[i].ProductCd);
							}
						}

						if (lstMProductDetail.Where(x => x.Sku == lstProductSub[i].Sku).ToList().Count == 0)
						{
							CreateSqlProductDetaiCSVQuery(lstProductSub[i], sqlQueryProductDetai);
						}
						else
						{
							CreateSqlProductDetaiCSVQuery(lstProductSub[i], sqlQueryProductDetai, false);
						}
					}

					sqlImportCsv.Append($"{ sqlQueryProduct } { sqlQueryFrontScreen } { sqlQueryProductDetai }");

					resultInsProduct = UnitOfWork.MProduct.InsertOrUpdateDataCSV(sqlImportCsv.ToString()).Result;

					if (resultInsProduct > 0)
					{
						transactionScope.Complete();
					}
					return resultInsProduct;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return 0;
			}
		}
		#endregion

		#region Create query insert/update mproduct
		/// <summary>
		/// Create query insert/update mproduct
		/// </summary>
		/// <param name="mProductSub"></param>
		/// <param name="sqlQueryProduct"></param>
		/// <param name="isAdd"></param>
		/// <returns></returns>
		private StringBuilder CreateSqlProductCSVQuery(MProductSub mProductSub, StringBuilder sqlQueryProduct, bool isAdd = true)
		{
			int MAX_SIZE_CAN_ORDER_DEFAULT = 3;
			if (isAdd)
			{
				sqlQueryProduct.Append($@"
										INSERT INTO mproduct (
																  ProductCd
																, ProductName
																, ProductName1
																, Year
																, BrandRltId
																, Price
																, DisplayFlg
																, MaxSizeCanOrder
																, MaxAmountCanOrder
																, SubBrandId
																, CreateUserCd
																, UpdateUserCd
															)
													VALUES (
															  '{ mProductSub.ProductCd }'
															, '{ mProductSub.ProductName }'
															, '{ mProductSub.ProductName1 }'
															,  { mProductSub.Year }
															,  { mProductSub.BrandRltId }
															,  { mProductSub.Price }
															,  { mProductSub.DisplayFlg }
															,  { MAX_SIZE_CAN_ORDER_DEFAULT }
															,  { mProductSub.MaxAmountCanOrder }
															,  { int.Parse(mProductSub.SubBrandId) }
															, '{ mProductSub.CreateUserCd }'
															, '{ mProductSub.CreateUserCd }'
															);
										");
			}
			else
			{
				sqlQueryProduct.Append($@"
										UPDATE mproduct
										SET
											  ProductName = '{ mProductSub.ProductName }'
											, ProductName1 = '{ mProductSub.ProductName1 }'
											, Year = { mProductSub.Year }
											, BrandRltId = { mProductSub.BrandRltId }
											, Price = { mProductSub.Price }
											, DisplayFlg = { mProductSub.DisplayFlg }
											, SubBrandId = { int.Parse(mProductSub.SubBrandId) }
											, UpdateUserCd = '{ mProductSub.CreateUserCd }'
										WHERE ProductCd = '{ mProductSub.ProductCd }';
										");
			}

			return sqlQueryProduct;
		}
		#endregion

		#region Create query insert frontscreen
		/// <summary>
		/// Create query insert frontscreen
		/// </summary>
		/// <param name="mProductSub"></param>
		/// <param name="sqlQueryFrontScreen"></param>
		/// <param name="frontScreen"></param>
		/// <param name="bussinessCd"></param>
		/// <returns></returns>
		private StringBuilder CreateSqlFrontScreenCSVQuery(MProductSub mProductSub, StringBuilder sqlQueryFrontScreen, FrontScreen frontScreen, string bussinessCd)
		{
			sqlQueryFrontScreen.Append($@"
											INSERT INTO frontscreen (
																	  ScrType
																	, TextTitle2
																	, TextArea1
																	, TextArea2
																	, TextButton1
																	, TextButton2
																	, TextNortify1
																	, TextNortify2
																	, BussinessCd
																	, ProductCd
																	, CreateUserCd
																	, UpdateUserCd
																)
														VALUES (
																  '{ ParamConst.SCREEN_PRODUCT_DETAIL }'
																, '{ frontScreen.TextTitle2 }'
																, '{ frontScreen.TextArea1 }'
																, '{ frontScreen.TextArea2 }'
																, '{ frontScreen.TextButton1 }'
																, '{ frontScreen.TextButton2 }'
																, '{ frontScreen.TextNortify1 }'
																, '{ frontScreen.TextNortify2 }'
																, '{ bussinessCd }'
																, '{ mProductSub.ProductCd }'
																, '{ mProductSub.CreateUserCd }'
																, '{ mProductSub.CreateUserCd }'
																);
										");

			return sqlQueryFrontScreen;
		}
		#endregion

		#region Create query insert/update mproductdetail
		/// <summary>
		/// Create query insert/update mproductdetail
		/// </summary>
		/// <param name="mProductSub"></param>
		/// <param name="sqlQueryProductDetai"></param>
		/// <param name="isAdd"></param>
		/// <returns></returns>
		private StringBuilder CreateSqlProductDetaiCSVQuery(MProductSub mProductSub, StringBuilder sqlQueryProductDetai, bool isAdd = true)
		{
			if (isAdd)
			{
				sqlQueryProductDetai.Append($@"
										INSERT INTO mproductdetail (
																	  Sku
																	, ProductCd
																	, ColorCd
																	, InventoryNumber
																	, SizeCd
																	, CreateUserCd
																	, UpdateUserCd
																	)
															VALUES (
																	  '{ mProductSub.Sku }'
																	, '{ mProductSub.ProductCd }'
																	, '{ mProductSub.ColorCd }'
																	,  { int.Parse(mProductSub.InventoryNumber) }
																	, '{ mProductSub.SizeCd }'
																	, '{ mProductSub.CreateUserCd }'
																	, '{ mProductSub.CreateUserCd }'
																	);
										");
			}
			else
			{
				sqlQueryProductDetai.Append($@"
										UPDATE mproductdetail
										SET
											  InventoryNumber = { int.Parse(mProductSub.InventoryNumber) }
											, UpdateUserCd = '{ mProductSub.CreateUserCd }'
										WHERE Sku = '{ mProductSub.Sku }';
										");
			}

			return sqlQueryProductDetai;
		}
		#endregion
	}
}
