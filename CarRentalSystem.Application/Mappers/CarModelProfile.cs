using AutoMapper;
using CarRentalSystem.Application.DTOs.CarModelDto;
using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class CarModelProfile : Profile
    {
        public CarModelProfile()
        {
            // Maps the CarModel entity to the CarModelDto.
            // It's configured to map related properties like Manufacturer and Category names, if they exist in the DTO.
            CreateMap<CarModel, CarModelDto>()
                .ForMember(d => d.ManufacturerName, o => o.MapFrom(s => s.Manufacturer.Name))
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.CarCategory.Name))
                .ReverseMap();

            // Maps the DTOs used for creating and updating a CarModel to the entity.
            CreateMap<CreateCarModelDto, CarModel>();
            CreateMap<UpdateCarModelDto, CarModel>();
        }
    }
}
