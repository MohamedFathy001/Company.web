using AutoMapper;
using Company.Data.Entities;
using Company.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Mapping
{
    public class DepartmentProfile :Profile
    {
        //Auto Mapper 3ashan el dto 
        public DepartmentProfile()
        {
            CreateMap<DepartmentModel, DepartmentDTO>().ReverseMap();
        }
    }
}
