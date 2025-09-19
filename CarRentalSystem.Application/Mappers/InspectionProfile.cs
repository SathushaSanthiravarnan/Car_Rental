using AutoMapper;
using CarRentalSystem.Application.DTOs.Inspection;
using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class InspectionProfile : Profile
    {
        public InspectionProfile()
        {
            CreateMap<Inspection, InspectionDto>();

            CreateMap<CreateInspectionDto, Inspection>()
                .ForMember(d => d.Id, o => o.Ignore());

            CreateMap<UpdateInspectionDto, Inspection>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CarId, o => o.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
