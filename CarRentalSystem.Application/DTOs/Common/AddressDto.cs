using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Common
{
    public sealed record AddressDto(
        string Street,
        string City,
        string State,
        string PostalCode,
        string Country
    );
}
