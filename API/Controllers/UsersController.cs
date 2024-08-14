using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        try{
            var users = await userRepository.GetUsersAsync();
            return Ok(users);
        }
        catch{
            return BadRequest("Error");
        }
    }

    [HttpGet("{username}")] // api/users/{username}
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        try{
            var user = await userRepository.GetUserByIdAsync(id);

            if(user == null) return NotFound();
            
            return Ok(user);
        }
        catch{
            return BadRequest("Error");
        }
    }
}