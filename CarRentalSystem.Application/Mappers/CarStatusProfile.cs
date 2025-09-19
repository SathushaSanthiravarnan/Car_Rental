using AutoMapper;
using CarRentalSystem.Application.DTOs.CarStatus;
using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class CarStatusProfile : Profile
    {
        public CarStatusProfile()
        {
            CreateMap<CarStatus, CarStatusDto>().ReverseMap();
            CreateMap<CreateCarStatusDto, CarStatus>();
            CreateMap<UpdateCarStatusDto, CarStatus>();
        }
    }
}
