using CarRentalSystem.Application.DTOs.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Customers
{
    public sealed record CreateCustomerCommand(CreateCustomerDto Dto) : IRequest<CustomerDto>;
}
