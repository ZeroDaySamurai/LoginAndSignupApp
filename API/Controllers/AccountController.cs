using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")] // api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.UserName))
        {
            return BadRequest("Username is taken");
        }

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDto.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")] // api/account/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        //Find the user
        var user = await context.Users.FirstOrDefaultAsync(
            x => x.UserName == loginDto.Username.ToLower()); //Tom is the same as tom
        
        //Validate the user
        if(user == null)
        {
            return Unauthorized("Invalid Username");
        }

        //Create a new hmac object with the password salt
        using var hmac = new HMACSHA512(user.PasswordSalt); //We using "using" to dispose of the hmac object after we are done with it

        //Compute the hash of the password
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        //Validate the password by comparing the computed hash with the password hash
        for(int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid Password");
            }
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower()); //Tom is the same as tom
    }
}
