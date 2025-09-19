using AutoMapper;
using CarRentalSystem.Application.DTOs.Car;
using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(d => d.CarModelName, o => o.MapFrom(s => s.CarModel.Name))
                .ForMember(d => d.CarStatusName, o => o.MapFrom(s => s.CarStatus.Name))
                .ReverseMap();

            CreateMap<CreateCarDto, Car>();
            CreateMap<UpdateCarDto, Car>();
        }
    }
}
