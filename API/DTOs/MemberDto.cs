using System;

namespace API.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public string? Username { get; set; } //User1234_Bomber
    public string? Fullname { get; set; } //Shakeel Hoosain
    public string? Email { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public List<PhotoDto>? Photos { get; set; }
}
