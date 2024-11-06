using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.BaseRepository
{
	public interface IBaseRepository<T> where T : class
	{

		IEnumerable<T> GetAll();
		Task<IEnumerable<T>> GetAllAsync();
		T? GetById(int id);
		Task<T?> GetByIdAsync(int id);
		Task Add(T entity);
		Task Update(T entity);
		Task Delete(int id);
		Task Delete(T entity);
		void Detach(T entity);

		IQueryable<T> GetQuery();
		IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate);
	
	}
}
