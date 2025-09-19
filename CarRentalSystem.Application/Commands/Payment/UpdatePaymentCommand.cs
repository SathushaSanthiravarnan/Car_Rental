using CarRentalSystem.Application.DTOs.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Payment
{
   
        public sealed record UpdatePaymentCommand(Guid PaymentId, UpdatePaymentDto Dto) : IRequest<PaymentDto>;
       
    
}
