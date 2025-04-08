namespace Domain.DTOs.UserDTO;

public class CreateBooking
{
    public int UserId { get; set; }
    public int CarId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal TotalPrice { get; set; }
}