using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Customers
{
    public sealed class SoftDeleteCustomerCommandHandler : IRequestHandler<SoftDeleteCustomerCommand , bool>
    {
        private readonly ICustomerRepository _customers;
        private readonly IApplicationDbContext _db;
        public SoftDeleteCustomerCommandHandler(ICustomerRepository customers, IApplicationDbContext db)
         => (_customers, _db) = (customers, db);
        public async Task<bool>Handle(SoftDeleteCustomerCommand request, CancellationToken ct)
        {
            var customer = await _customers.GetByIdAsync(request.CustomerId);
            if(customer is  null) return false; 
            customer.IsDeleted = true;
            await _db.SaveChangesAsync();
            return true;
        }
    }
    
    
}
