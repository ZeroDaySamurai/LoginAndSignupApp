using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        //Generate the token key from the appsettings.json file
        var tokenKey = config["TokenKey"] ?? throw new ArgumentNullException("TokenKey is missing from appsettings.json");

        //Check if the token key is at least 64 characters long
        if(tokenKey.Length < 64)
        {
            throw new ArgumentException("TokenKey must be at least 64 characters long");
        }

        //Create a new instance of the SymmetricSecurityKey class
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        //Create a new list of claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName), //Add the user's username to the claims
        };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //Sign the token with the key

        //Create a new SecurityTokenDescriptor object with the claims, expiry date, and signing credentials
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), //Add the claims to the token
            Expires = DateTime.UtcNow.AddDays(7), //Token expires in 7 days
            SigningCredentials = credentials //Add the signing credentials to the token
        };

        var tokenHandler = new JwtSecurityTokenHandler(); //Create a new instance of the JwtSecurityTokenHandler class  
        var token = tokenHandler.CreateToken(tokenDescriptor); //Create a new token using the token descriptor

        return tokenHandler.WriteToken(token); //Return the token as a string
    }
}
