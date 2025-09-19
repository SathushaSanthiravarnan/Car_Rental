using AutoMapper;
using CarRentalSystem.Application.DTOs.Payment;
using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            // Map: Payment Entity -> PaymentDto
            // This is used for reading/retrieving payment data.
            CreateMap<Payment, PaymentDto>();

            // Map: CreatePaymentDto -> Payment Entity
            // This is used for creating a new payment.
            // We ignore properties that are set by the business logic (e.g., PaymentDate, Status).
            CreateMap<CreatePaymentDto, Payment>()
                .ForMember(d => d.PaymentDate, o => o.Ignore())
                .ForMember(d => d.Status, o => o.Ignore());

            // Map: UpdatePaymentDto -> Payment Entity
            // This is used for updating an existing payment.
            // .ForAllMembers(opts => opts.Condition(...)) ensures that only non-null values
            // from the DTO are used to update the entity, leaving other properties unchanged.
            CreateMap<UpdatePaymentDto, Payment>()
                .ForMember(d => d.BookingId, o => o.Ignore()) // Do not allow changing the booking ID
                .ForMember(d => d.PaymentDate, o => o.Ignore()) // Payment date is not updated via DTO
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
