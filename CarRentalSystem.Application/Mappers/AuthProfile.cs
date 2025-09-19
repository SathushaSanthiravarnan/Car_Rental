using AutoMapper;
using CarRentalSystem.Application.DTOs.Auth;
using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class AuthProfile : Profile
    {
        public AuthProfile()
        {
            // User -> AuthUserDto (record with ctor)
            CreateMap<User, AuthUserDto>()
                .ConstructUsing(u => new AuthUserDto(
                    u.Id,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.Role.ToString()
                ));

            // RegisterGuestDto -> User (for creation)
            CreateMap<RegisterGuestDto, User>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.IsActive, o => o.MapFrom(_ => true))
                .ForMember(d => d.EmailConfirmed, o => o.MapFrom(_ => false))
                // explicitly ignore fields we set elsewhere or don’t want AutoMapper touching
                .ForMember(d => d.PasswordHash, o => o.Ignore())
                .ForMember(d => d.PasswordSalt, o => o.Ignore())
                .ForMember(d => d.Role, o => o.Ignore())
                .ForMember(d => d.GoogleSubjectId, o => o.Ignore())
                .ForMember(d => d.GoogleEmail, o => o.Ignore())
                .ForMember(d => d.StaffProfile, o => o.Ignore())
                .ForMember(d => d.CustomerProfile, o => o.Ignore())
                .ForMember(d => d.Address, o => o.Ignore())
                .ForMember(d => d.Phone, o => o.Ignore())
                .ForMember(d => d.Username, o => o.Ignore())
                .ForMember(d => d.LastLoginUtc, o => o.Ignore());

            // (Optional) We usually DON'T need LoginDto -> User; remove to avoid confusion.
            // If you still want it:
            // CreateMap<LoginDto, User>()
            //     .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            //     .ForAllMembers(o => o.Ignore()); // but generally unnecessary
        }
    }
}