using FOFB.Server.Services.Interfaces;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace FOFB.Server.Services
{
	public class RolesService : BaseService, IRolesService
	{
		public RolesService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public IRolesRepository Roles { get; set; }

		public Task<long> AddAsync(Roles entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<Roles> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<Roles>> GetAllAsync(Roles entity, string propetyOrder, string typeOrder)
		{
			return UnitOfWork.Roles.GetAllAsync(entity);
		}

		#region 条件によるRoles一覧を取得する
		/// <summary>
		/// 条件によるRoles一覧を取得する
		/// </summary>
		/// <param name="rolesSub"></param>
		/// <param name="sortColumnName"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<RolesSub>> GetAllSubAsync(RolesSub rolesSub, string sortColumnName = null, string sortType = null)
		{
			return UnitOfWork.Roles.GetAllSubAsync(rolesSub);
		}
		#endregion

		public Tuple<List<Roles>, int> GetDataPagination(int currentpage, int pageSize, Roles data, string propetyOrder, string typeOrder)
		{
			throw new NotImplementedException();
		}

		#region 権限を登録する
		/// <summary>
		/// 権限を登録する
		/// </summary>
		/// <param name="rolesSub"></param>
		/// <returns></returns>
		public bool InsertRoles(RolesSub rolesSub)
		{
			bool result = false;

			try
			{
				using (var transactionScope = new TransactionScope())
				{
					Roles roles = new Roles();
					roles = (Roles)rolesSub;
					long roleId = UnitOfWork.Roles.AddAsync(roles).Result;

					if (roleId == 0)
					{
						return result;
					}

					List<RolesDetail> rolesDetails = new List<RolesDetail>();
					RolesDetail rolesDetail = null;

					if (rolesSub.ActionTypeArr.Count == 0)
					{
						return result;
					}

					for (int i = 0; i < rolesSub.ActionTypeArr.Count; i++)
					{
						rolesDetail = ConvertObjectRolesDetail(rolesSub, rolesSub.ActionTypeArr[i]);
						rolesDetails.Add(rolesDetail);
					}

					long resultInsert = UnitOfWork.RolesDetail.AddListAsync(rolesDetails).Result;
					if (resultInsert == 0)
					{
						return result;
					}

					List<MenuRoleRelation> menuRoleRelations = new List<MenuRoleRelation>();
					MenuRoleRelation menuRoleRelation = null;

					int[] menuDefault = { ParamConst.MenuId.MANAGE_ORDER, ParamConst.MenuId.MANAGE_STOCK, ParamConst.MenuId.MANAGE_MAIL, ParamConst.MenuId.MANAGE_SCREEN };

					for (int i = 0; i < menuDefault.Count(); i++)
					{
						menuRoleRelation = new MenuRoleRelation();
						menuRoleRelation.RoleId = (int)roleId;
						menuRoleRelation.MenuId = menuDefault[i];
						menuRoleRelation.CreateUserCd = rolesSub.UpdateUserCd;
						menuRoleRelation.UpdateUserCd = rolesSub.UpdateUserCd;

						menuRoleRelations.Add(menuRoleRelation);
					}

					long resultInsertMenu = UnitOfWork.MenuRoleRelation.AddListAsync(menuRoleRelations).Result;
					if (resultInsertMenu > 0)
					{
						result = true;
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

			return result;
		}
		#endregion

		public Task<bool> UpdateAsync(List<Roles> entity)
		{
			throw new NotImplementedException();
		}

		#region 権限を更新する
		/// <summary>
		/// 権限を更新する
		/// </summary>
		/// <param name="rolesSub"></param>
		/// <returns></returns>
		public int UpdateRoles(RolesSub rolesSub)
		{
			int result = ParamConst.ResultType.ERROR;
			long resultInsert = 0;
			bool resultUpdate = false;

			Roles roles = new Roles();
			roles.RoleId = rolesSub.RoleId;
			List<Roles> rolesss = UnitOfWork.Roles.GetAllAsync(roles).Result;

			RolesDetail rolesDetail = new RolesDetail();
			rolesDetail.RoleId = rolesSub.RoleId;
			rolesDetail.DelFlg = true;
			List<RolesDetail> rolesDetails = UnitOfWork.RolesDetail.GetAllAsync(rolesDetail).Result;

			List<RolesDetail> rolesDetailsAdd = new List<RolesDetail>();
			List<RolesDetail> rolesDetailsUpd = new List<RolesDetail>();
			RolesDetail rolesDetailChk = null;

			if (rolesss != null && rolesss.Count > 0 && rolesDetails != null && rolesDetails.Count > 0)
			{
				if (rolesss[0].UpdateDtTm != rolesSub.UpdateDtTm)
				{
					return ParamConst.ResultType.UPDATED;
				}

				try
				{
					using (var transactionScope = new TransactionScope())
					{
						// 追加
						List<string> actionTypeAdd = rolesSub.ActionTypeArr.Except(rolesSub.OldActionTypeArr).ToList();
						if (actionTypeAdd.Count > 0)
						{
							for (int i = 0; i < actionTypeAdd.Count; i++)
							{
								rolesDetailChk = rolesDetails.FirstOrDefault(x => x.ActionType == actionTypeAdd[i]);
								if (rolesDetailChk != null)
								{
									rolesDetailChk.UpdateUserCd = rolesSub.UpdateUserCd;
									rolesDetailChk.DelFlg = false;                      // DelFlg = 0
									rolesDetailsUpd.Add(rolesDetailChk);
								}
								else
								{
									rolesDetailChk = ConvertObjectRolesDetail(rolesSub, actionTypeAdd[i]);
									rolesDetailsAdd.Add(rolesDetailChk);
								}
							}
						}

						if (rolesDetailsAdd.Count > 0)
						{
							resultInsert = UnitOfWork.RolesDetail.AddListAsync(rolesDetailsAdd).Result;
							if (resultInsert == 0)
							{
								return result;
							}
						}

						// 削除
						List<string> actionTypeDel = rolesSub.OldActionTypeArr.Except(rolesSub.ActionTypeArr).ToList();
						if (actionTypeDel.Count > 0)
						{
							for (int i = 0; i < actionTypeDel.Count; i++)
							{
								rolesDetailChk = rolesDetails.FirstOrDefault(x => x.ActionType == actionTypeDel[i]);
								if (rolesDetailChk != null)
								{
									rolesDetailChk.UpdateUserCd = rolesSub.UpdateUserCd;
									rolesDetailChk.DelFlg = true;                       // DelFlg = 1
									rolesDetailsUpd.Add(rolesDetailChk);
								}
							}
						}

						if (rolesDetailsUpd.Count > 0)
						{
							resultUpdate = UnitOfWork.RolesDetail.UpdateAsync(rolesDetailsUpd).Result;
							if (!resultUpdate)
							{
								return result;
							}
						}

						List<Roles> rolesUpd = new List<Roles>();
						rolesUpd.Add((Roles)rolesSub);
						resultUpdate = UnitOfWork.Roles.UpdateAsync(rolesUpd).Result;

						if (resultUpdate)
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

		#region 権限詳細をマッピングする
		/// <summary>
		/// 権限詳細をマッピングする
		/// </summary>
		/// <param name="rolesSub"></param>
		/// <returns></returns>
		private RolesDetail ConvertObjectRolesDetail(RolesSub rolesSub, string actionType)
		{
			RolesDetail rolesDetail = new RolesDetail();
			rolesDetail.RoleId = rolesSub.RoleId;
			rolesDetail.ActionType = actionType;
			rolesDetail.CreateUserCd = rolesSub.UpdateUserCd;
			rolesDetail.UpdateUserCd = rolesSub.UpdateUserCd;

			return rolesDetail;
		}
		#endregion
	}
}