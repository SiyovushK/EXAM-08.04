namespace Domain.DTOs.CarDTO;

public class GetCarDTO
{
    public int Id { get; set; }
    public string Model { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; }
}