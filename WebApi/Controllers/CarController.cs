using Domain.DTOs.CarDTO;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController(ICarService carService) : ControllerBase
{

    [HttpGet]
    public async Task<Response<List<GetCarDTO>>> GetAllAsync()
    {
        return await carService.GetAllAsync();
    }

    [HttpGet("id")]
    public async Task<Response<GetCarDTO>> GetByIdAsync(int ID)
    {
        return await carService.GetByIdAsync(ID);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await carService.DeleteAsync(ID);
    }

    [HttpPost]
    public async Task<Response<GetCarDTO>> CreateUser(CreateCarDTO createCar)
    {
        return await carService.CreateUser(createCar);
    }
    
    [HttpPut]
    public async Task<Response<GetCarDTO>> UpdateAsync(int Id, GetCarDTO updateCarDTO)
    {
        return await carService.UpdateAsync(Id, updateCarDTO);
    }

}