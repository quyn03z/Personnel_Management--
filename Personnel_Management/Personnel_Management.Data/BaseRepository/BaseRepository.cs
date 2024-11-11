using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Personnel_Management.Data.EntityRepository;


namespace Personnel_Management.Data.BaseRepository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected readonly QuanLyNhanSuContext _context;
		protected readonly DbSet<T> _dbSet;
        protected readonly INhanVienRepository _nhanVienRepository;
        public BaseRepository(QuanLyNhanSuContext context)
		{
			_context = context;
			var typeOfDbSet = typeof(DbSet<T>);
			foreach (var prop in context.GetType().GetProperties())
			{
				if (typeOfDbSet == prop.PropertyType)
				{
					_dbSet = prop.GetValue(context, null) as DbSet<T>;
					break;
				}
			}

			if (_dbSet == null)
			{
				_dbSet = context.Set<T>();
			}
		}

		public IEnumerable<T> GetAll()
		{
			return _dbSet.ToList();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public T? GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "NhanVienId") == id);
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public void Update(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			var entity = _dbSet.Find(id);
			if (entity != null)
			{
				_dbSet.Remove(entity);
			}
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}

		public IQueryable<T> GetQuery()
		{
			return _dbSet.AsQueryable();
		}

		public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate)
		{
			return _dbSet.Where(predicate);
		}

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
		{
			IQueryable<T> query = _dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return orderBy(query);
			}

			return query;
		}

		public void Detach(T entity)
		{
			_context.Entry(entity).State = EntityState.Detached;
		}


		public string HashPassword(string password)
		{
			using (var sha256 = SHA256.Create())
			{
				var bytes = Encoding.UTF8.GetBytes(password);
				var hash = sha256.ComputeHash(bytes);
				return Convert.ToBase64String(hash);
			}
		}


	}
}
