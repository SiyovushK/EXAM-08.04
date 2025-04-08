using Domain.DTOs.CarDTO;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICarService
{
    Task<Response<List<GetCarDTO>>> GetAllAsync();
    Task<Response<GetCarDTO>> GetByIdAsync(int ID);
    Task<Response<string>> DeleteAsync(int ID);
    Task<Response<GetCarDTO>> CreateUser(CreateCarDTO createCar);
    Task<Response<GetCarDTO>> UpdateAsync(int Id, GetCarDTO updateCarDTO);
}