using CarRentalSystem.Domain.Enums;



namespace CarRentalSystem.Domain.Entities
{
    public class Booking : AuditableEntity
    {
        public Guid CarId { get; set; }
        public Car Car { get; set; } = default!;

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public decimal TotalPrice { get; set; }
        public string Currency { get; set; } = "LKR";
        public string? Remarks { get; set; }


    }
}