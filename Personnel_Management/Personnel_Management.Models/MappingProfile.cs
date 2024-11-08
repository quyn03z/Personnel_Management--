using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Personnel_Management.Models.DTO;
using Personnel_Management.Models.Models;

namespace Personnel_Management.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NhanVien, NhanVienDto>()
                .ForMember(dest => dest.PhongBan, opt => opt.MapFrom(src => src.PhongBan)); // Map PhongBan

            CreateMap<PhongBan, DepartmentDto>(); // Map PhongBan sang DepartmentDto
        }
    }
}
