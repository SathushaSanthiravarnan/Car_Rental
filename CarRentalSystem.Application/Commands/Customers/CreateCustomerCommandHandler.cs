using AutoMapper;
using CarRentalSystem.Application.DTOs.Customer;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Customers
{
    public sealed class CreateCustomerCommandHandler :IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customers;
        private readonly IApplicationDbContext _db;
        private readonly IUserRepository _users;

        public CreateCustomerCommandHandler(ICustomerRepository customer ,IUserRepository users,IApplicationDbContext db ,IMapper mapper)
        {
            _customers = customer;
            _users = users;
            _db = db;
            _mapper = mapper;
        }
        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken ct)
        {
            var user = await _users.GetByIdAsync(request.Dto.UserId, ct);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            var customer = _mapper.Map<Customer>(request.Dto);
            await _customers.AddAsync(customer, ct);
            await _db.SaveChangesAsync();
            var customerDto = _mapper.Map<CustomerDto>(customer);
            customerDto = customerDto with
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone
            };
            return customerDto;
        }
    } 
}
