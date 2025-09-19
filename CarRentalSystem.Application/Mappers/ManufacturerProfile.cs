using AutoMapper;
using CarRentalSystem.Application.DTOs.Manufacturer;
using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            // Maps the Manufacturer entity to the ManufacturerDto.
            CreateMap<Manufacturer, ManufacturerDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country))
                .ForMember(d => d.FoundedYear, o => o.MapFrom(s => s.FoundedYear))
                .ReverseMap();

            // Maps the DTOs used for creating and updating a manufacturer to the entity.
            CreateMap<CreateManufacturerDto, Manufacturer>();
            CreateMap<UpdateManufacturerDto, Manufacturer>();
        }
    }
}
