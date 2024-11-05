using Personnel_Management.Data.EntityRepository;
using Personnel_Management.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel_Management.Business.NhanVienService
{
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRepository _nhanVienRepository;

        public NhanVienService(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        public async Task<IEnumerable<NhanVien>> GetAllAsync()
        {
            return await _nhanVienRepository.GetAllNhanViensAsync();
        }

        public async Task<NhanVien?> GetByIdAsync(int id)
        {
            return await _nhanVienRepository.GetNhanVienByIdAsync(id);
        }

        public async Task<NhanVien> AddAsync(NhanVien nhanVien)
        {
            await _nhanVienRepository.AddNhanVienAsync(nhanVien);
            return nhanVien;
        }

        //public async Task UpdateAsync(NhanVien nhanVien)
        //{
        //    await _nhanVienRepository.UpdateAsync(nhanVien);
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    await _nhanVienRepository.DeleteAsync(id);
        //}

        //public async Task<NhanVien?> GetByEmailAsync(string email)
        //{
        //    return await _nhanVienRepository.GetByEmailAsync(email);
        //}

        public NhanVien Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwtToken(NhanVien user)
        {
            throw new NotImplementedException();
        }
    }
}
