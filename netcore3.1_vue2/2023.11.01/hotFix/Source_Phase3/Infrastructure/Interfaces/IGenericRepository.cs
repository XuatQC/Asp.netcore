using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<List<T>> GetAllAsync(T entity, string sortColumnName = null, string sortType = null);
		Tuple<List<T>, int> GetDataPagination(int currentpage, int pageSize, T entity, string sortColumnName = null, string sortType = null);
		Task<long> AddAsync(T entity);
		Task<bool> UpdateAsync(List<T> entity);
		Task<bool> DeleteAsync(List<T> entity);

	}
}
