using AutoMapper;
using CarRentalSystem.Application.DTOs.CarCategory;
using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class CarCategoryProfile : Profile
    {
        public CarCategoryProfile()
        {
            // Entity -> DTO
            CreateMap<CarCategory, CarCategoryDto>();

            // DTO -> Entity (for creation)
            CreateMap<CreateCarCategoryDto, CarCategory>();

            // DTO -> Entity (for updates)
            CreateMap<UpdateCarCategoryDto, CarCategory>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
        }
    }
}
