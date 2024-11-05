using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Data.BaseRepository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		public Task AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByEmailAsync(string email)
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}
	}
}
