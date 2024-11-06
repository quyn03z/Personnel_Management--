using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;


namespace Personnel_Management.Data.BaseRepository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected readonly QuanLyNhanSuContext _context;

		public BaseRepository(QuanLyNhanSuContext context)
		{
			_context = context;
		}

		public IEnumerable<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public T? GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);  
		}

		public async Task Add(T entity)
		{
			_context.Add<T>(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Update(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var entity = _context.Set<T>().Find(id);
			if(entity != null)
			{
				_context.Set<T>().Remove(entity);
				await _context.SaveChangesAsync();
			}

		}

		public async Task Delete(T entity)
		{
			_context.Set<T>().Remove(entity);  
			await _context.SaveChangesAsync();
		}

		public IQueryable<T> GetQuery()
		{
			return _context.Set<T>().AsQueryable();
		}

		public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate)
		{
			return _context.Set<T>().Where(predicate);
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
