using AutoMapper;
using CarRentalSystem.Application.DTOs.Booking;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.ValueObjects;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class BookingProfile : Profile
    {
        public BookingProfile()
        {
            // Entity -> DTO
            CreateMap<Booking, BookingDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.PickUpDate, o => o.MapFrom(s => s.PickUpDate))
                .ForMember(d => d.ReturnDate, o => o.MapFrom(s => s.ReturnDate))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
                .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.TotalPrice))
                .ForMember(d => d.Car, o => o.MapFrom(s => s.Car))
                .ForMember(d => d.Customer, o => o.MapFrom(s => s.Customer));

            // DTO -> Entity (Create)
            CreateMap<CreateBookingDto, Booking>();

            // DTO -> Entity (Update)
            CreateMap<UpdateBookingDto, Booking>();
               
            // Address <-> AddressDto
          //  CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
