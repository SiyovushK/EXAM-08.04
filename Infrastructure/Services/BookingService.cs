using System.Net;
using Domain.DTOs.UserDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookingService(DataContext context) : IBookingService
{
    public async Task<Response<List<GetBookingDTO>>> GetAllAsync()
    {
        var booking = await context.Bookings.ToListAsync();

        var data = booking
            .Select(b => new GetBookingDTO()
            {
                Id = b.Id,
                UserId = b.UserId,
                CarId = b.CarId,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                TotalPrice = b.TotalPrice
            }).ToList();

        return new Response<List<GetBookingDTO>>(data);
    }

    public async Task<Response<GetBookingDTO>> GetByIdAsync(int ID)
    {
        var booking = await context.Bookings.FindAsync(ID);
        if (booking == null)
        {
            return new Response<GetBookingDTO>(HttpStatusCode.BadRequest, "Car not found");
        }

        var getBookingDto = new GetBookingDTO()
        {
            Id = booking.Id,
            UserId = booking.UserId,
            CarId = booking.CarId,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            TotalPrice = booking.TotalPrice
        };

        return new Response<GetBookingDTO>(getBookingDto);
    }

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        var booking = await context.Bookings.FindAsync(ID);
        if (booking == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Booking not found");
        }

        context.Remove(booking);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Booking not deleted")
            : new Response<string>("Booking deleted successefully");
    }

    public async Task<Response<GetBookingDTO>> CreateUser(CreateBooking createBooking)
    {
        var booking = new Booking()
        {
            UserId = createBooking.UserId,
            CarId = createBooking.CarId,
            StartDate = createBooking.StartDate,
            EndDate = createBooking.EndDate,
            TotalPrice = createBooking.TotalPrice
        };

        await context.Bookings.AddAsync(booking);
        var result = await context.SaveChangesAsync();

        var getBookingDto = new GetBookingDTO()
        {
            Id = booking.Id,
            UserId = booking.UserId,
            CarId = booking.CarId,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            TotalPrice = booking.TotalPrice
        };

        return result == 0
            ? new Response<GetBookingDTO>(HttpStatusCode.BadRequest, "Booking not created")
            : new Response<GetBookingDTO>(getBookingDto);
    }
    
    public async Task<Response<GetBookingDTO>> UpdateAsync(int Id, GetBookingDTO updateBookingDTO)
    {
        var booking = await context.Bookings.FindAsync(Id);
        if (booking == null)
        {
            return new Response<GetBookingDTO>(HttpStatusCode.NotFound, "Booking not found");
        }

        booking.UserId = updateBookingDTO.UserId;
        booking.CarId = updateBookingDTO.CarId;
        booking.StartDate = updateBookingDTO.StartDate;
        booking.EndDate = updateBookingDTO.EndDate;
        booking.TotalPrice = updateBookingDTO.TotalPrice;

        var result = await context.SaveChangesAsync();

        var getBookingDto = new GetBookingDTO()
        {
            Id = booking.Id,
            UserId = booking.UserId,
            CarId = booking.CarId,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            TotalPrice = booking.TotalPrice
        };

        return result == 0
            ? new Response<GetBookingDTO>(HttpStatusCode.BadRequest, "Booking not updated")
            : new Response<GetBookingDTO>(getBookingDto);
    }
}