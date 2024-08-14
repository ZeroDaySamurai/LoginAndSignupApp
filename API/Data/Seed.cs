using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(DataContext context)
    {
        if(await context.Users.AnyAsync()) return;

        var userDate =  await File.ReadAllTextAsync("Data/UserSeedData.json");

        //Properties of the JsonSerializerOptions class can be used to control the behavior of the JsonSerializer class
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var users = JsonSerializer.Deserialize<List<AppUser>>(userDate, options);

        if (users == null)
        {
            return;
        }

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();

            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);
        }
        
        await context.SaveChangesAsync();
    }
}
