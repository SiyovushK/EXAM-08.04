using Domain.DTOs.BookingDTO;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetUserDTO>>> GetAllAsync()
    {
        return await userService.GetAllAsync();
    }
    
    [HttpGet("id")]
    public async Task<Response<GetUserDTO>> GetByIdAsync(int ID)
    {
        return await userService.GetByIdAsync(ID);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await userService.DeleteAsync(ID);
    }
    
    [HttpPost]
    public async Task<Response<GetUserDTO>> CreateUser(CreateUserDTO createUser)
    {
        return await userService.CreateUser(createUser);
    }
    
    [HttpPut]
    public async Task<Response<GetUserDTO>> UpdateAsync(int Id, GetUserDTO updateUserDTO)
    {
        return await userService.UpdateAsync(Id, updateUserDTO);
    }
}