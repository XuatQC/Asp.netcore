using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace FOFB.Server.Services
{
	public class MUserLoginService : BaseService, IMUserLoginService
	{
		public MUserLoginService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<long> AddAsync(MUserLogin entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<MUserLogin> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<MUserLogin>> GetAllAsync(MUserLogin entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		public Tuple<List<MUserLogin>, int> GetDataPagination(int currentpage, int pageSize, MUserLogin entity, string sortColumnName = null, string sortType = null)
		{
			throw new NotImplementedException();
		}

		#region ログイン
		/// <summary>
		/// ログイン
		/// </summary>
		/// <param name="muserLogin"></param>
		/// <param name="isAutoLogin"></param>
		/// <param name="currNetworkIp"></param>
		/// <returns></returns>
		public async Task<Tuple<MuserLoginSub, string, List<MkbnValue>>> Login(MUserLogin muserLogin, bool isAutoLogin, string currNetworkIp)
		{
			try
			{
				string errorMsg = string.Empty;
				bool isValidIp = false;

				if (muserLogin == null || string.IsNullOrEmpty(muserLogin.UserCd))
				{
					string[] arrreplacemsg = { "ユーザーコード" };
					errorMsg = Message.GetMessageInfo(Message.Common.MSG_IMPUT, arrreplacemsg);
					return new Tuple<MuserLoginSub, string, List<MkbnValue>>(null, errorMsg, null);
				}

				muserLogin.Password = Common.MD5Hash(muserLogin.Password);

				MuserLoginSub responeMuserLogin = this.UnitOfWork.MUserLogin.Login(muserLogin.UserCd, muserLogin.Password);

				if (responeMuserLogin == null)
				{
					return new Tuple<MuserLoginSub, string, List<MkbnValue>>(null, Message.Login.MSG_WRONG_INFO, null);
				}

				//responeMuserLogin.Password = null;

				isValidIp = Common.IsValidIp(currNetworkIp);

				// Check if User admin login from outsite amdin network
				if (!isValidIp)
				{
					if (responeMuserLogin.Department == ParamConst.Department.ADMIN)
					{
						errorMsg = Message.Login.MSG_IP_ACCESS_DENIE;
						return new Tuple<MuserLoginSub, string, List<MkbnValue>>(null, errorMsg, null);
					}
					else
					{
						// check if shop cannot receive money when expirce date but admin can
						bool isPaymentAble = this.IsPaymentAble(responeMuserLogin.BussinessCd);
						//bool isDeliveryAble = this.IsDeliveryAble(responeMuserLogin.BussinessCd);

						if (!isPaymentAble)
						{
							string[] arrreplacemsg = { "ログイン" };
							errorMsg = Message.GetMessageInfo(Message.Common.MSG_OUT_OF_PERIOD, arrreplacemsg);
							return new Tuple<MuserLoginSub, string, List<MkbnValue>>(responeMuserLogin, errorMsg, null);
						}
					}
				}

				responeMuserLogin.AccessToken = Common.GenerateJSONWebToken(responeMuserLogin, isAutoLogin);

				// Get ON/OFF password
				MkbnValue mkbnValue = new MkbnValue();
				mkbnValue.KbnCd = ParamConst.PassWord.KBN_CD;
				List<MkbnValue> lstOnOffPass = UnitOfWork.MkbnValue.GetAllAsync(mkbnValue, "KbnValue").Result;

				return new Tuple<MuserLoginSub, string, List<MkbnValue>>(responeMuserLogin, string.Empty, lstOnOffPass);
			}
			catch (Exception ex)
			{
				Log.Logger.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
				return null;
			}
		}
		#endregion

		#region 条件によるMuserLoginSubを取得する
		/// <summary>
		/// 条件によるMuserLoginSubを取得する
		/// </summary>
		/// <param name="muserLoginSub"></param>
		/// <returns></returns>
		public MuserLoginSub GetMuserLoginSub(MuserLoginSub muserLoginSub)
		{
			return this.UnitOfWork.MUserLogin.GetMuserLoginSub(muserLoginSub);
		}
		#endregion
		public Task<bool> UpdateAsync(List<MUserLogin> lstMuserLogin)
		{
			return this.UnitOfWork.MUserLogin.UpdateAsync(lstMuserLogin);
		}

		#region パスワード変更
		/// <summary>
		/// パスワード変更
		/// </summary>
		/// <param name="MUser"></param>
		/// <returns>Result</returns>
		public async Task<Tuple<bool, string>> ChangePassword(MUserLogin muser, string oldPassword)
		{
			string errorMsg = this.CheckValidPassword(muser, oldPassword);
			if (!string.IsNullOrEmpty(errorMsg))
			{
				// return false with error Message
				return new Tuple<bool, string>(false, errorMsg);
			}

			// Update Password
			muser.Password = Common.MD5Hash(muser.Password);
			List<MUserLogin> lstUpdateMuser = new List<MUserLogin>();
			lstUpdateMuser.Add(muser);

			var changePassResult = await this.UpdateAsync(lstUpdateMuser);
			if (!changePassResult)
			{
				// return false with error Message
				string[] arrreplacemsg = { "パスワード変更" };
				errorMsg = Message.GetMessageInfo(Message.Common.MSG_FAILED, arrreplacemsg);
				return new Tuple<bool, string>(false, errorMsg);
			}

			return new Tuple<bool, string>(true, string.Empty);
		}
		#endregion

		#region CheckValidPassword
		/// <summary>
		/// CheckValidPassword
		/// </summary>
		/// <param name="muser"></param>
		/// <returns></returns>
		private string CheckValidPassword(MUserLogin muser, string oldPassword)
		{
			MuserLoginSub muserLoginSubParam = new MuserLoginSub();
			muserLoginSubParam.UserCd = muser.UserCd;
			muserLoginSubParam.Password = Common.MD5Hash(oldPassword);

			// Check valid current password
			var mUserLoaded = this.UnitOfWork.MUserLogin.GetMuserLoginSub(muserLoginSubParam);
			if (mUserLoaded == null)
			{
				string[] arrReplaceMsg = { "現在のパスワード" };
				return Message.GetMessageInfo(Message.Common.MSG_NOT_WRONG_INFO, arrReplaceMsg);
			}
			return string.Empty;
		}
		#endregion

		#region パスワードリセット
		/// <summary>
		/// パスワードリセット
		/// </summary>
		/// <param name="password"></param>
		/// <param name="updateUserCd"></param>
		/// <returns></returns>
		public int ResetPassword(string password, string updateUserCd)
		{
			int resultResetPass = ParamConst.ResultType.SUCCESS;

			MUserLogin mUserLogin = new MUserLogin();
			mUserLogin.UserCd = password;
			List<MUserLogin> mUserLogins = UnitOfWork.MUserLogin.GetAllAsync(mUserLogin).Result;

			if (mUserLogins.Count > 0)
			{
				if (mUserLogins[0].Password != Common.MD5Hash(password))
				{
					mUserLogins[0].Password = Common.MD5Hash(password);
					mUserLogins[0].UpdateUserCd = updateUserCd;
					bool upPassShop = UnitOfWork.MUserLogin.UpdateAsync(mUserLogins).Result;
					if (!upPassShop)
					{
						return ParamConst.ResultType.ERROR;
					}
				}
			}
			return resultResetPass;
		}
		#endregion
	}
}
