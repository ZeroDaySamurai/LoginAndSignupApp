using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API;

public class RegisterDto
{
    //Add [FromQuery] if you want to use query parameters in swagger
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }
}
