using Domain.DTOs.BookingDTO;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<List<GetUserDTO>>> GetAllAsync();
    Task<Response<GetUserDTO>> GetByIdAsync(int ID);
    Task<Response<string>> DeleteAsync(int ID);
    Task<Response<GetUserDTO>> CreateUser(CreateUserDTO createUser);
    Task<Response<GetUserDTO>> UpdateAsync(int Id, GetUserDTO updateUserDTO);
}