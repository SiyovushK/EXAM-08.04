using System.Net;
using Domain.DTOs.CarDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CarService(DataContext context) : ICarService
{
    public async Task<Response<List<GetCarDTO>>> GetAllAsync()
    {
        var cars = await context.Cars.ToListAsync();

        var data = cars
            .Select(c => new GetCarDTO()
            {
                Id = c.Id,
                Model = c.Model,
                PricePerDay = c.PricePerDay,
                IsAvailable = c.IsAvailable
            }).ToList();

        return new Response<List<GetCarDTO>>(data);
    }

    public async Task<Response<GetCarDTO>> GetByIdAsync(int ID)
    {
        var car = await context.Cars.FindAsync(ID);
        if (car == null)
        {
            return new Response<GetCarDTO>(HttpStatusCode.BadRequest, "Car not found");
        }

        var getCarDto = new GetCarDTO()
        {
            Id = car.Id,
            Model = car.Model,
            PricePerDay = car.PricePerDay,
            IsAvailable = car.IsAvailable
        };

        return new Response<GetCarDTO>(getCarDto);
    }

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        var car = await context.Cars.FindAsync(ID);
        if (car == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Car not found");
        }

        context.Remove(car);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Car not deleted")
            : new Response<string>("Car deleted successefully");
    }

    public async Task<Response<GetCarDTO>> CreateUser(CreateCarDTO createCar)
    {
        var car = new Car()
        {
            Model = createCar.Model,
            PricePerDay = createCar.PricePerDay,
            IsAvailable = createCar.IsAvailable
        };

        await context.Cars.AddAsync(car);
        var result = await context.SaveChangesAsync();

        var getCarDto = new GetCarDTO()
        {
            Id = car.Id,
            Model = car.Model,
            PricePerDay = car.PricePerDay,
            IsAvailable = car.IsAvailable
        };

        return result == 0
            ? new Response<GetCarDTO>(HttpStatusCode.BadRequest, "Car not created")
            : new Response<GetCarDTO>(getCarDto);
    }
    
    public async Task<Response<GetCarDTO>> UpdateAsync(int Id, GetCarDTO updateCarDTO)
    {
        var car = await context.Cars.FindAsync(Id);
        if (car == null)
        {
            return new Response<GetCarDTO>(HttpStatusCode.NotFound, "Car not found");
        }

        car.Model = updateCarDTO.Model;
        car.PricePerDay = updateCarDTO.PricePerDay;
        car.IsAvailable = updateCarDTO.IsAvailable;

        var result = await context.SaveChangesAsync();

        var getCarDto = new GetCarDTO()
        {
            Id = car.Id,
            Model = car.Model,
            PricePerDay = car.PricePerDay,
            IsAvailable = car.IsAvailable
        };

        return result == 0
            ? new Response<GetCarDTO>(HttpStatusCode.BadRequest, "Car not updated")
            : new Response<GetCarDTO>(getCarDto);
    }

    // public async Task<Response<AvailableCarDto>> AvailableCars()
    // {
        
    // }
}