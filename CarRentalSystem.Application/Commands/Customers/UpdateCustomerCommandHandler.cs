using AutoMapper;
using CarRentalSystem.Application.DTOs.Customer;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Customers
{
    public sealed class UpdateCustomerCommandHandler
    : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customers;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customers, IApplicationDbContext db, IMapper mapper)
            => (_customers, _db, _mapper) = (customers, db, mapper);

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken ct)
        {
            var customer = await _customers.Query()
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == request.CustomerId, ct)
                ?? throw new KeyNotFoundException("Customer not found.");


            _mapper.Map(request.Dto, customer);

            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CustomerDto>(customer);
        }
    }
    }
