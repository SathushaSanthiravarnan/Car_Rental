using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Customer;
using CarRentalSystem.Application.DTOs.User;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Queries.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Customers
{
    public sealed class GetCustomerByIdQueryHandler
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
    {
        private readonly ICustomerRepository _customers;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customers, IMapper mapper)
            => (_customers, _mapper) = (customers, mapper);

        public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken ct)
        {
            return await _customers.Query()
                .Where(c => c.Id == request.CustomerId)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
