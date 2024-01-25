public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public string ProfilePictureUrl { get; set; }
}
