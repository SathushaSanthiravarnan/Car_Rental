using AutoMapper;
using CarRentalSystem.Application.DTOs.Staff;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.Enums;
using System;

namespace CarRentalSystem.Application.Mappers
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<Staff, StaffDto>()
                .ForMember(d => d.FirstName, m => m.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName, m => m.MapFrom(s => s.User.LastName))
                .ForMember(d => d.Phone, m => m.MapFrom(s => s.User.Phone));

            CreateMap<StaffInvite, StaffInviteDto>();

            CreateMap<CreateStaffDto, Staff>()
                .ForMember(d => d.Type,
                    m => m.MapFrom(s => ParseStaffTypeOrDefault(s.StaffType, StaffType.FrontDesk)));

            CreateMap<UpdateStaffDto, Staff>()
                .ForMember(d => d.Type, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.StaffType));
                    opt.MapFrom(src => ParseStaffTypeOrDefault(src.StaffType, StaffType.FrontDesk));
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }

        private static StaffType ParseStaffTypeOrDefault(string? value, StaffType @default)
        {
            return Enum.TryParse<StaffType>(value, true, out var parsed) ? parsed : @default;
        }
    }
}