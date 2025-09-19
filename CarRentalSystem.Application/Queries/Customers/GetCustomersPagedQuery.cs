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
    //public sealed record GetCustomersPagedQuery(PagedRequestDto Page)
    //: IRequest<PagedResultDto<CustomerDto>>;

    //public sealed class GetCustomersPagedQueryHandler
    //    : IRequestHandler<GetCustomersPagedQuery, PagedResultDto<CustomerDto>>
    //{
    //    private readonly ICustomerRepository _repo;
    //    private readonly IMapper _mapper;

    //    public GetCustomersPagedQueryHandler(ICustomerRepository repo, IMapper mapper)
    //    {
    //        _repo = repo; _mapper = mapper;
    //    }

    //    public async Task<PagedResultDto<CustomerDto>> Handle(GetCustomersPagedQuery request, CancellationToken ct)
    //    {
    //        var q = _repo.Query();

    //        if (!string.IsNullOrWhiteSpace(request.Page.Search))
    //        {
    //            var s = request.Page.Search.Trim();
    //            q = q.Where(c =>
    //                (c.User.FirstName + " " + c.User.LastName).Contains(s) ||
    //                (c.EmailForContact ?? "").Contains(s));
    //        }

    //        var total = await q.CountAsync(ct);

    //        var items = await q
    //            .Skip((request.Page.Page - 1) * request.Page.PageSize)
    //            .Take(request.Page.PageSize)
    //            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
    //            .ToListAsync(ct);

    //        return new PagedResultDto<CustomerDto>(items, request.Page.Page, request.Page.PageSize, total);
    //    }
    //}
    public sealed record GetCustomersPagedQuery(int Page = 1, int PageSize = 20)
     : IRequest<PagedResultDto<CustomerDto>>;
}
