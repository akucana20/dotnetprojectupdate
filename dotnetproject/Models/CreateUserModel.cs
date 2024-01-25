public class CreateUserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public string ProfilePictureUrl { get; set; }
}
