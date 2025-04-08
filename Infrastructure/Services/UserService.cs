using System.Net;
using Domain.DTOs.BookingDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task<Response<List<GetUserDTO>>> GetAllAsync()
    {
        var users = await context.Users.ToListAsync();

        var data = users
            .Select(u => new GetUserDTO()
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Phone = u.Phone
            }).ToList();

        return new Response<List<GetUserDTO>>(data);
    }

    public async Task<Response<GetUserDTO>> GetByIdAsync(int ID)
    {
        var user = await context.Users.FindAsync(ID);
        if (user == null)
        {
            return new Response<GetUserDTO>(HttpStatusCode.BadRequest, "User not found");
        }

        var getUserDto = new GetUserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Phone = user.Phone
        };

        return new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        var user = await context.Users.FindAsync(ID);
        if (user == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "User not found");
        }

        context.Remove(user);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "User not deleted")
            : new Response<string>("User deleted successefully");
    }

    public async Task<Response<GetUserDTO>> CreateUser(CreateUserDTO createUser)
    {
        var user = new User()
        {
            Username = createUser.Username,
            Email = createUser.Email,
            Phone = createUser.Phone
        };

        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();

        var getUserDto = new GetUserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Phone = user.Phone
        };

        return result == 0
            ? new Response<GetUserDTO>(HttpStatusCode.BadRequest, "Student not created")
            : new Response<GetUserDTO>(getUserDto);
    }

    public async Task<Response<GetUserDTO>> UpdateAsync(int Id, GetUserDTO updateUserDTO)
    {
        var user = await context.Users.FindAsync(Id);
        if (user == null)
        {
            return new Response<GetUserDTO>(HttpStatusCode.NotFound, "User not found");
        }

        user.Username = updateUserDTO.Username;
        user.Email = updateUserDTO.Email;
        user.Phone = updateUserDTO.Phone;

        var result = await context.SaveChangesAsync();

        var getUserDto = new GetUserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Phone = user.Phone
        };

        return result == 0
            ? new Response<GetUserDTO>(HttpStatusCode.BadRequest, "User not updated")
            : new Response<GetUserDTO>(getUserDto);
    }
}