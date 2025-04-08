using Domain.DTOs.UserDTO;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IBookingService
{
    Task<Response<List<GetBookingDTO>>> GetAllAsync();
    Task<Response<GetBookingDTO>> GetByIdAsync(int ID);
    Task<Response<string>> DeleteAsync(int ID);    
    Task<Response<GetBookingDTO>> CreateUser(CreateBooking createBooking);
    Task<Response<GetBookingDTO>> UpdateAsync(int Id, GetBookingDTO updateBookingDTO);
}