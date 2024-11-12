﻿using Microsoft.AspNetCore.Mvc;
using Personnel_Management.Business.LuongService;
using Personnel_Management.Business.NhanVienService;
using Personnel_Management.Models.Models;

namespace Personnel_Management.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuongController : ControllerBase
    {
        private readonly ILuongService _luongService;
        private readonly INhanVienService _nhanVienService;


        private readonly QuanLyNhanSuContext _context;
        public LuongController(ILuongService luongService, INhanVienService nhanVienService , QuanLyNhanSuContext context)
        {
            _nhanVienService = nhanVienService;
            _luongService = luongService;
            _context = context;
        }
        [HttpGet("{nhanVienId}")]
        public async Task<IActionResult> GetLuongByNhanVienId(int nhanVienId)
        {
            var nhanVien = await _nhanVienService.GetByIdAsync(nhanVienId);
            if (nhanVien == null)
            {
                return NotFound($"Nhân viên với ID {nhanVienId} không tồn tại.");
            }

            try
            {
               var luong = await _luongService.GetByIdAsync(nhanVienId);
                return Ok(luong);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi thêm lương: {ex.Message}");
            }
        }

        [HttpPost("{nhanVienId}")]
        public async Task<IActionResult> AddLuongForNhanVien(int nhanVienId, [FromBody] Luong luong)
        {
            // Kiểm tra xem nhân viên tồn tại không
            var nhanVien = await _nhanVienService.GetByIdAsync(nhanVienId);
            if (nhanVien == null)
            {
                return NotFound($"Nhân viên với ID {nhanVienId} không tồn tại.");
            }

            // Kiểm tra xem đã tồn tại lương cho nhân viên này chưa.  Bạn có thể thêm logic phức tạp hơn ở đây nếu cần.
            if (nhanVien.Luongs.Any())
            {
                return BadRequest($"Nhân viên với ID {nhanVienId} đã có lương.");
            }

            // Gán nhân viên cho lương
            luong.NhanVienId = nhanVienId;

            try
            {
                await _luongService.AddAsync(luong);
                return Ok(luong);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi thêm lương: {ex.Message}");
            }
        }
        [HttpPut("{nhanVienId}")]
        public async Task<IActionResult> Update(int nhanVienId, Luong luong)
        {
            var nhanVien = await _nhanVienService.GetByIdAsync(nhanVienId);
            if (nhanVien == null)
            {
                return NotFound($"Nhân viên với ID {nhanVienId} không tồn tại.");
            }

            if (nhanVienId != luong.NhanVienId)
            {
                return BadRequest();
            }

            try
            {
                var existingLuong = await _luongService.GetByIdAsync(luong.NhanVienId);

                if (existingLuong != null)
                {
                    await _luongService.UpdateAsync(luong);
                    return NoContent();

                }
                else
                {
                    luong.NhanVienId = nhanVienId;
                    await _luongService.AddAsync(luong);
                    return Ok(luong);
                }

               
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Error updating employee: {ex.Message}");
            }
        }
    }

}

