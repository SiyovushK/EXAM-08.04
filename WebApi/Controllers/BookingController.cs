using Domain.DTOs.UserDTO;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetBookingDTO>>> GetAllAsync()
    {
        return await bookingService.GetAllAsync();
    }

    [HttpGet("id")]
    public async Task<Response<GetBookingDTO>> GetByIdAsync(int ID)
    {
        return await bookingService.GetByIdAsync(ID);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await bookingService.DeleteAsync(ID);
    }   

    [HttpPost]
    public async Task<Response<GetBookingDTO>> CreateUser(CreateBooking createBooking)
    {
        return await bookingService.CreateUser(createBooking);
    }

    [HttpPut] 
    public async Task<Response<GetBookingDTO>> UpdateAsync(int Id, GetBookingDTO updateBookingDTO)
    {
        return await bookingService.UpdateAsync(Id, updateBookingDTO);
    }
}