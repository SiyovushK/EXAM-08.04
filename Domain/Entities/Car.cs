using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Car
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Model { get; set; }
    [Required]
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;
    public List<Booking> Bookings { get; set; }
}