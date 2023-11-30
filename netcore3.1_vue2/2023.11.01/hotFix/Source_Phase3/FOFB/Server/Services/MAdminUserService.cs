using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Services
{
	public class MAdminUserService : BaseService, IMAdminUserService
	{
		public MAdminUserService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MAdminUser entity)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// ユーザー追加
		/// </summary>
		/// <param name="bussinessCd"></param>
		/// <param name="adminUser"></param>
		/// <returns></returns>
		public long AddAsyncSub(string bussinessCd, MAdminUserSub adminUser)
		{
			using (var transactionScope = new TransactionScope())
			{
				if (adminUser.RoleId != ParamConst.Role.SUPER_ADMIN)
				{
					adminUser.BussinessCd = bussinessCd;
				}
				long resultInsRoleDtl = 0;
				long resultInsUser = this.UnitOfWork.MAdminUser.AddAsync(adminUser).Result;
				if (resultInsUser != -1)
				{
					MUserLogin mUserLogin = new MUserLogin();
					mUserLogin.UserCd = adminUser.UserCd;
					mUserLogin.Password = Common.MD5Hash(adminUser.Password);
					mUserLogin.Department = ParamConst.Department.ADMIN;
					mUserLogin.RefreshToken = Common.MD5Hash(adminUser.Password);
					mUserLogin.RefreshTokenExpiryTime = DateTime.Now;
					mUserLogin.CreateUserCd = adminUser.CreateUserCd;
					long resultInsUserLogin = this.UnitOfWork.MUserLogin.AddAsync(mUserLogin).Result;
					if (resultInsUserLogin != -1)
					{
						UserRoleRelation userRoleRelation = new UserRoleRelation();
						userRoleRelation.RoleId = adminUser.RoleId;
						userRoleRelation.UserCd = adminUser.UserCd;
						userRoleRelation.CreateUserCd = adminUser.CreateUserCd;
						resultInsRoleDtl = this.UnitOfWork.UserRoleRelation.AddAsync(userRoleRelation).Result;
						transactionScope.Complete();
					}
				}
				return resultInsRoleDtl;
			}
		}

		public Task<bool> DeleteAsync(List<MAdminUser> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<MAdminUser>> GetAllAsync(MAdminUser entity, string sortColumnName = null, string sortType = null)
		{
			MAdminUser mAdminPara = new MAdminUser();
			return this.UnitOfWork.MAdminUser.GetAllAsync(mAdminPara);
		}
		public Task<List<MAdminUserSub>> GetAllSubAsync(MAdminUser entity, string sortColumnName = null, string sortType = null)
		{
			return this.UnitOfWork.MAdminUser.GetAllSubAsync(entity);
		}

		public Tuple<List<MAdminUser>, int> GetDataPagination(int currentpage, int pageSize, MAdminUser entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<MAdminUser> entity)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// ユーザー編集
		/// </summary>
		/// <param name="adminUser"></param>
		/// <returns></returns>

		public bool UpdateAsyncSub(MAdminUserSub adminUser)
		{
			using (var transactionScope = new TransactionScope())
			{
				bool resultupdRoleDtl = false;
				List<MAdminUser> mAdminUsers = new List<MAdminUser>();
				mAdminUsers.Add(adminUser);
				if (adminUser.RoleId == ParamConst.Role.SUPER_ADMIN)
				{
					adminUser.BussinessCd = null;
				}
				bool resultupdUser = UnitOfWork.MAdminUser.UpdateAsync(mAdminUsers).Result;
				if (resultupdUser)
				{
					UserRoleRelation userRoleRelation = new UserRoleRelation();
					userRoleRelation.UserCd = adminUser.UserCd;
					List<UserRoleRelation> userRoleRelations = UnitOfWork.UserRoleRelation.GetAllAsync(userRoleRelation).Result;
					if (userRoleRelations.Count > 0)
					{
						userRoleRelations[0].RoleId = adminUser.RoleId;
						userRoleRelations[0].UpdateUserCd = adminUser.UpdateUserCd;
						resultupdRoleDtl = UnitOfWork.UserRoleRelation.UpdateAsync(userRoleRelations).Result;
					}
				}

				if (resultupdRoleDtl)
				{
					transactionScope.Complete();
				}

				return resultupdRoleDtl;
			}
		}
		/// <summary>
		/// ユーザー削除
		/// </summary>
		/// <param name="lstAdminUser"></param>
		/// <returns></returns>
		public bool DeleteAsyncSub(List<MAdminUserSub> lstAdminUser)
		{
			using (var transactionScope = new TransactionScope())
			{
				bool resultDelRoleDtl = false;
				MUserLogin mUserLogin = new MUserLogin();
				UserRoleRelation userRoleRelation = new UserRoleRelation();
				List<MUserLogin> mUserLogins = new List<MUserLogin>();
				List<MUserLogin> lstUserLoginDel = new List<MUserLogin>();
				List<UserRoleRelation> userRoleRelations = new List<UserRoleRelation>();
				List<UserRoleRelation> lstUserRoleRelationDel = new List<UserRoleRelation>();
				List<MAdminUser> lstMAdminUserDel = new List<MAdminUser>();

				for (int i = 0; i < lstAdminUser.Count; i++)
				{
					lstAdminUser[i].DelFlg = true;
					lstMAdminUserDel.Add(lstAdminUser[i]);
					mUserLogin.UserCd = lstAdminUser[i].UserCd;
					mUserLogins = this.UnitOfWork.MUserLogin.GetAllAsync(mUserLogin).Result;
					mUserLogins[0].DelFlg = true;
					lstUserLoginDel.Add(mUserLogins[0]);
					userRoleRelation.UserCd = lstAdminUser[i].UserCd;
					userRoleRelations = this.UnitOfWork.UserRoleRelation.GetAllAsync(userRoleRelation).Result;
					userRoleRelations[0].DelFlg = true;
					lstUserRoleRelationDel.Add(userRoleRelations[0]);
				}
				bool resultDelUser = this.UnitOfWork.MAdminUser.UpdateAsync(lstMAdminUserDel).Result;
				if (resultDelUser)
				{
					bool resultDelUserLogin = this.UnitOfWork.MUserLogin.UpdateAsync(lstUserLoginDel).Result;
					if (resultDelUserLogin)
					{
						resultDelRoleDtl = this.UnitOfWork.UserRoleRelation.UpdateAsync(lstUserRoleRelationDel).Result;
						if (resultDelRoleDtl)
						{
							transactionScope.Complete();
						}
					}
				}
				return resultDelRoleDtl;
			}
		}
		/// <summary>
		/// csvファイルデータを読み込む
		/// </summary>
		/// <param name="files"></param>
		/// <returns></returns>
		public object CheckDataCsv(IFormFileCollection files, string bussinessCd)
		{
			string dLine = "\n";
			List<List<string>> listUserCsv = Common.ReadCsv(files[0], "utf-8");
			List<MAdminUserSub> lstMAdminUserSub = new List<MAdminUserSub>();
			try
			{
				MAdminUserSub mAdminUser = new MAdminUserSub();
				List<MAdminUserSub> lstMAdminUser = UnitOfWork.MAdminUser.GetAllSubAsync(mAdminUser, null, null).Result;

				MBussiness mBussiness = new MBussiness();
				List<MBussiness> lstMBussiness = UnitOfWork.MBussiness.GetAllAsync(mBussiness, null, null).Result;

				Roles roles = new Roles();
				List<Roles> lstRole = UnitOfWork.Roles.GetAllAsync(roles, null, null).Result;
				int countWrongInput = 0;
				int MAX_COUT_WRONG_INPUT = 5;
				if (listUserCsv.Count > 0)
				{
					MAdminUserSub mAdminUserSub = null;
					for (int i = 0; i < listUserCsv.Count; i++)
					{
						if (listUserCsv[i].Count != 7)
						{
							return new { error = Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.FILE_CSV) };
						}
						mAdminUserSub = new MAdminUserSub();

						mAdminUserSub.IsAdd = true;

						//管理者コードを確認する
						mAdminUserSub.UserCd = listUserCsv[i][ParamConst.IdxDataCsvUser.USER_CD];

						if (string.IsNullOrEmpty(listUserCsv[i][ParamConst.IdxDataCsvUser.USER_CD]))
						{
							mAdminUserSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.USER_CD) + dLine;
							countWrongInput++;
						}
						else if (listUserCsv[i][ParamConst.IdxDataCsvUser.USER_CD].Length != 8 ||
								!Regex.Match(listUserCsv[i][ParamConst.IdxDataCsvUser.USER_CD], ParamConst.Regex.REGEX_USER_PASSWORD).Success)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_ERROR_USER_CD + dLine;
						}
						else
						{
							mAdminUser = lstMAdminUser.FirstOrDefault(x => x.UserCd == listUserCsv[i][ParamConst.IdxDataCsvUser.USER_CD]);
							if (mAdminUser != null)
							{
								if (mAdminUser.DelFlg)
								{
									mAdminUserSub.TxtError += Message.Common.MSG_USER_WAS_DELETED + dLine;
								}
								else
								{
									mAdminUserSub.IsAdd = false;
								}
							}
						}

						//名前（漢字）を確認する
						mAdminUserSub.UserNameKanji = listUserCsv[i][ParamConst.IdxDataCsvUser.USER_NAME_KANJI];

						if (string.IsNullOrEmpty(listUserCsv[i][ParamConst.IdxDataCsvUser.USER_NAME_KANJI]))
						{
							mAdminUserSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.USER_NAME_KANJI) + dLine;
							countWrongInput++;
						}
						else if (listUserCsv[i][ParamConst.IdxDataCsvUser.USER_NAME_KANJI].Length > 40)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.USER_NAME_KANJI).Replace("$length", "40") + dLine;
						}

						//名前（かな）を確認する
						mAdminUserSub.UserNameKana = listUserCsv[i][ParamConst.IdxDataCsvUser.USER_NAME_KANA];

						if (string.IsNullOrEmpty(listUserCsv[i][ParamConst.IdxDataCsvUser.USER_NAME_KANA]))
						{
							mAdminUserSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.USER_NAME_KANA) + dLine;
							countWrongInput++;
						}
						else if (listUserCsv[i][ParamConst.IdxDataCsvUser.USER_NAME_KANA].Length > 40)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.USER_NAME_KANA).Replace("$length", "40") + dLine;
						}
						else if (!Regex.Match(listUserCsv[i][ParamConst.IdxDataCsvUser.USER_NAME_KANA], ParamConst.Regex.REGEX_KANA).Success)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_ERROR_USER_NAME_KANA + dLine;
						}

						//メールアドレスを確認する
						mAdminUserSub.EmailAddress = listUserCsv[i][ParamConst.IdxDataCsvUser.EMAIL_ADDRESS];

						if (string.IsNullOrEmpty(listUserCsv[i][ParamConst.IdxDataCsvUser.EMAIL_ADDRESS]))
						{
							mAdminUserSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.MAIL_ADDRESS) + dLine;
							countWrongInput++;
						}
						else if (listUserCsv[i][ParamConst.IdxDataCsvUser.EMAIL_ADDRESS].Length > 50)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.MAIL_ADDRESS).Replace("$length", "50") + dLine;
						}
						else if (!Regex.Match(listUserCsv[i][ParamConst.IdxDataCsvUser.EMAIL_ADDRESS], ParamConst.Regex.REGEX_MAIL).Success)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.MAIL_ADDRESS) + dLine;
						}

						//権限IDを確認する
						mAdminUserSub.RoleName = listUserCsv[i][ParamConst.IdxDataCsvUser.ROLE_NAME];

						if (string.IsNullOrEmpty(listUserCsv[i][ParamConst.IdxDataCsvUser.ROLE_NAME]))
						{
							mAdminUserSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.ROLE_NAME) + dLine;
							countWrongInput++;
						}
						else if (lstRole.Where(x => x.RoleName == listUserCsv[i][ParamConst.IdxDataCsvUser.ROLE_NAME]).ToList().Count == 0)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_NOT_EXIST.Replace("*", ParamConst.TextItem.ROLE_NAME) + dLine;
						}
						else
						{
							mAdminUserSub.RoleId = lstRole.FirstOrDefault(x => x.RoleName == listUserCsv[i][ParamConst.IdxDataCsvUser.ROLE_NAME]).RoleId;
						}

						//パスワードを確認する
						mAdminUserSub.Password = listUserCsv[i][ParamConst.IdxDataCsvUser.PASSWORD];

						if (string.IsNullOrEmpty(listUserCsv[i][ParamConst.IdxDataCsvUser.PASSWORD]))
						{
							mAdminUserSub.TxtError += Message.Common.MSG_IMPUT.Replace("*", ParamConst.TextItem.PASSWORD) + dLine;
						}
						else if (listUserCsv[i][ParamConst.IdxDataCsvUser.PASSWORD].Length > 32)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_LENGTH_TOO_LONG.Replace("*", ParamConst.TextItem.PASSWORD).Replace("$length", "32") + dLine;
						}
						else if (listUserCsv[i][ParamConst.IdxDataCsvUser.PASSWORD].Length < 8 ||
								!Regex.Match(listUserCsv[i][ParamConst.IdxDataCsvUser.PASSWORD], ParamConst.Regex.REGEX_USER_PASSWORD).Success)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_ERROR_PASSWORD + dLine;
						}

						if (string.IsNullOrEmpty(listUserCsv[i][ParamConst.IdxDataCsvUser.BUSSINESS_NAME]))
						{
							mAdminUserSub.BussinessCd = null;

							if (listUserCsv[i][ParamConst.IdxDataCsvUser.ROLE_NAME] != ParamConst.TextItem.SUPER_ADMIN_ROLE_NAME)
							{
								mAdminUserSub.TxtError += Message.Common.MSG_ERROR_BUSSINESS + dLine;
							}
						}
						else if (lstMBussiness.Where(x => x.BussinessName == listUserCsv[i][ParamConst.IdxDataCsvUser.BUSSINESS_NAME]).ToList().Count != 0
								&& mAdminUserSub.RoleId == 1)
						{
							mAdminUserSub.TxtError += Message.Common.MSG_ERROR_ROLE_NAME + dLine;
							mAdminUserSub.BussinessCd = lstMBussiness.FirstOrDefault(x => x.BussinessName == listUserCsv[i][ParamConst.IdxDataCsvUser.BUSSINESS_NAME]).BussinessCd;
						}
						else if (lstMBussiness.Where(x => x.BussinessName == listUserCsv[i][ParamConst.IdxDataCsvUser.BUSSINESS_NAME]).ToList().Count != 0)
						{
							mAdminUserSub.BussinessCd = lstMBussiness.FirstOrDefault(x => x.BussinessName == listUserCsv[i][ParamConst.IdxDataCsvUser.BUSSINESS_NAME]).BussinessCd;
						}

						if (countWrongInput != MAX_COUT_WRONG_INPUT)
						{
							lstMAdminUserSub.Add(mAdminUserSub);
						}
						countWrongInput = 0;
					}
				}
				return lstMAdminUserSub;

			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return new { error = Message.Common.MSG_INPUT_FORMAT.Replace("*", ParamConst.TextItem.FILE_CSV) };
			}

		}

		#region CSVファイルからユーザーを追加する
		/// <summary>
		/// CSVファイルからユーザーを追加する
		/// </summary>
		/// <param name="lstMAdminUserSub"></param>
		/// <param name="lstShopCdUpd"></param>
		/// <returns></returns>
		public long InsertDataCsv(List<MAdminUserSub> lstMAdminUserSub, string lstShopCdUpd)
		{
			long resultInsUser = 0;
			StringBuilder sqlImportCsv = new StringBuilder("");

			try
			{
				using (var transactionScope = new TransactionScope())
				{
					for (int i = 0; i < lstMAdminUserSub.Count; i++)
					{
						CreateSqlUserCSVQuery(lstMAdminUserSub[i], sqlImportCsv);
					}

					resultInsUser = UnitOfWork.MAdminUser.InsertOrUpdateDataCSV(sqlImportCsv.ToString()).Result;

					if (resultInsUser > 0)
					{
						transactionScope.Complete();
					}
					return resultInsUser;
				}
			}
			catch (Exception e)
			{
				Log.Logger.Error(e, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return resultInsUser;
			}
		}
		#endregion

		#region Create query insert/update madminuser, muserLogin, userrolerelation
		/// <summary>
		/// Create query insert/update madminuser, muserLogin, userrolerelation
		/// </summary>
		/// <param name="mAdminUserSub"></param>
		/// <param name="sqlImportCsv"></param>
		/// <returns></returns>
		private StringBuilder CreateSqlUserCSVQuery(MAdminUserSub mAdminUserSub, StringBuilder sqlImportCsv)
		{
			StringBuilder bussinessCd = new StringBuilder("");
			if (string.IsNullOrEmpty(mAdminUserSub.BussinessCd))
			{
				bussinessCd.Append("null");
			}
			else
			{
				bussinessCd.Append($"'{ mAdminUserSub.BussinessCd }'");
			}

			if (mAdminUserSub.IsAdd)
			{
				sqlImportCsv.Append($@"
										INSERT INTO madminuser (
																  UserCd
																, UserNameKanji
																, UserNameKana
																, EmailAddress
																, BussinessCd
																, CreateUserCd
																, UpdateUserCd
																)
														VALUES (
																  '{ mAdminUserSub.UserCd }'
																, '{ mAdminUserSub.UserNameKanji }'
																, '{ mAdminUserSub.UserNameKana }'
																, '{ mAdminUserSub.EmailAddress }'
																,  { bussinessCd }
																, '{ mAdminUserSub.CreateUserCd}'
																, '{ mAdminUserSub.CreateUserCd}'
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
																  '{ mAdminUserSub.UserCd }'
																, md5('{ mAdminUserSub.Password }')
																, { ParamConst.Department.ADMIN }
																, md5('{ mAdminUserSub.Password }')
																, now()
																, '{ mAdminUserSub.CreateUserCd}'
																, '{ mAdminUserSub.CreateUserCd}'
																);

										INSERT INTO userrolerelation (
																		  RoleId
																		, UserCd
																		, CreateUserCd
																		, UpdateUserCd
																		)
																VALUES (
																		   { mAdminUserSub.RoleId }
																		, '{ mAdminUserSub.UserCd }'
																		, '{ mAdminUserSub.CreateUserCd}'
																		, '{ mAdminUserSub.CreateUserCd}'
																		);
									");
			}
			else
			{
				sqlImportCsv.Append($@"
										UPDATE madminuser
										SET
											  UserNameKanji = '{ mAdminUserSub.UserNameKanji }'
											, UserNameKana = '{ mAdminUserSub.UserNameKana }'
											, EmailAddress = '{ mAdminUserSub.EmailAddress }'
											, BussinessCd = { bussinessCd }
											, UpdateUserCd = '{ mAdminUserSub.CreateUserCd }'
										WHERE UserCd = '{ mAdminUserSub.UserCd }';

										UPDATE muserlogin
										SET
											  Password = md5('{ mAdminUserSub.Password }')
										WHERE UserCd = '{ mAdminUserSub.UserCd }';

										UPDATE userrolerelation
										SET
											  RoleId = { mAdminUserSub.RoleId }
										WHERE UserCd = '{ mAdminUserSub.UserCd }';
									");
			}

			return sqlImportCsv;
		}
		#endregion
	}
}