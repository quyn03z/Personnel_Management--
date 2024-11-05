using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.BaseRepository
{
	public interface IBaseRepository<T> where T : class
	{
		Task<T> GetByIdAsync(Guid id);
		Task<T> GetByEmailAsync(string email);

		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(Guid id);

		IEnumerable<T> GetAll();
	}
}
