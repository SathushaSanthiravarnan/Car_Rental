using AutoMapper;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
