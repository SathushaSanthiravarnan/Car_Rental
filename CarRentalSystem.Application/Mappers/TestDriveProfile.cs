using AutoMapper;
using CarRentalSystem.Application.DTOs.TestDrive;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Mappers
{
    public sealed class TestDriveProfile : Profile
{
    public TestDriveProfile()
    {
        // For creating a new test drive request
        CreateMap<CreateTestDriveDto, TestDrive>()
            .ForMember(d => d.Status, o => o.MapFrom(s => TestDriveStatus.Pending));
            
        // For updating an existing test drive
        CreateMap<UpdateTestDriveDto, TestDrive>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.ApprovedBy, o => o.MapFrom(s => s.StaffId));
            
        // For displaying test drive details
        CreateMap<TestDrive, TestDriveDto>()
            .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.User.FirstName + " " + s.Customer.User.LastName))
            .ForMember(d => d.CarPlateNumber, o => o.MapFrom(s => s.Car.PlateNumber))
            .ForMember(d => d.ApprovedByStaffName, o => o.MapFrom(s => s.ApprovedByStaff != null ? s.ApprovedByStaff.User.FirstName + " " + s.ApprovedByStaff.User.LastName : null));
    }
}
}
