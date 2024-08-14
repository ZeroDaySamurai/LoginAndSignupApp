namespace API.Models;

public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; } //User1234_Bomber
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public string? Fullname { get; set; } //Shakeel Hoosain
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public List<Photo> Photos { get; set; } = [];
}
