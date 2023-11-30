using FOFB.Server.Services.Interfaces;
using FOFB.Shared;
using FOFB.Shared.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace FOFB.Server.Services
{
	public class MenuService : BaseService, IMenuService
	{
		public MenuService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public Task<List<Menu>> GetMenusByRoleAsync(int roleId)
		{
			return this.UnitOfWork.Menu.GetMenuByRoleAsync(roleId);
		}

		#region GetMenusByUserAsync
		/// <summary>
		/// GetMenusByUserAsync
		/// </summary>
		/// <param name="muserLogin"></param>
		/// <param name="menu"></param>
		/// <param name="sortByColumn"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public Task<List<Menu>> GetMenusByUserAsync(MuserLoginSub muserLogin, Menu menu, string sortByColumn, string sortType)
		{
			//return this.UnitOfWork.Menu.GetMenusByUserAsync(muserLogin, menu, sortByColumn, sortType);
			Task<List<Menu>> lstMenu = this.UnitOfWork.Menu.GetMenusByUserAsync(muserLogin, menu, sortByColumn, sortType);
			if (!string.IsNullOrEmpty(muserLogin.Permissions))
			{
				var actPerArray = muserLogin.Permissions.Split(",");
				if (actPerArray.Contains(ParamConst.RoleDetail.SHOP_REPRESENT))
				{
					// get shop menu
					Menu menuParam = new Menu();
					menuParam.Department = ParamConst.Department.SHOP;
					menuParam.Href = "/shop/home";

					Menu menuShopHome = this.UnitOfWork.Menu.GetAllAsync(menuParam, null, null).Result.FirstOrDefault();
					lstMenu.Result.Add(menuShopHome);
				}
			}
			return lstMenu;
		}
		#endregion

		#region IsUserCanAccessMenu
		/// <summary>
		/// IsUserCanAccessMenu
		/// </summary>
		/// <param name="userCd"></param>
		/// <param name="href"></param>
		/// <returns></returns>
		public bool IsUserCanAccessMenu(string userCd, string href)
		{
			MUserLogin muserLogin = new MUserLogin();
			Menu menu = new Menu();
			muserLogin.UserCd = userCd;
			menu.Href = href;

			Task<List<Menu>> lstMenu = this.UnitOfWork.Menu.GetMenusByUserAsync(muserLogin, menu, null, null);

			return (lstMenu.Result.Count > 0);
		}
		#endregion

		public Task<long> AddAsync(Menu entity)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(List<Menu> entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<Menu>> GetAllAsync(Menu menu, string sortOrder, string sortType)
		{
			return this.UnitOfWork.Menu.GetAllAsync(menu);
		}

		public Tuple<List<Menu>, int> GetDataPagination(int currentpage, int pageSize, Menu data, string propetyOrder, string typeOrder)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(List<Menu> entity)
		{
			throw new NotImplementedException();
		}
	}
}