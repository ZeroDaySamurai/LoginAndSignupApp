using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // api/users
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        try{
            var users = await context.Users.ToListAsync();
            return Ok(users);
        }
        catch{
            return BadRequest("Error");
        }
    }

    [HttpGet("{id:int}")] // api/users/1
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        try{
            var user = await context.Users.FindAsync(id);

            if(user == null) return NotFound();
            
            return Ok(user);
        }
        catch{
            return BadRequest("Error");
        }
    }
}