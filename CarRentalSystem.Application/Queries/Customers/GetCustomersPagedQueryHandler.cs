using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.Customer;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Customers
{
    public sealed class GetCustomersPagedQueryHandler
          : IRequestHandler<GetCustomersPagedQuery, PagedResultDto<CustomerDto>>
    {
        private readonly ICustomerRepository _customers;
        private readonly IMapper _mapper;

        public GetCustomersPagedQueryHandler(ICustomerRepository customers, IMapper mapper)
            => (_customers, _mapper) = (customers, mapper);

        public async Task<PagedResultDto<CustomerDto>> Handle(GetCustomersPagedQuery request, CancellationToken ct)
        {
            var query = _customers.Query()
                .Include(c => c.User); // Include the related User entity

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(c => c.User.FirstName) // Order by the user's first name
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<CustomerDto>(items, total, request.Page, request.PageSize);
        }
    }
}
