using System.Linq;
using System.Security.Cryptography;
using System.Text;
using dotnetproject.Models; 
using dotnetproject.Data;
using dotnetproject.Services;


namespace dotnetproject.Services
{
    public interface IUserService
    {
        User CreateUser(CreateUserModel model);
        User UpdateUser(int userId, UpdateUserModel model);
        bool DeleteUser(int userId);
        
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User CreateUser(CreateUserModel model)
        {
            var passwordHash = HashPassword(model.Password);
            var user = new User
            {
                Username = model.Username,
                PasswordHash = passwordHash,
                Email = model.Email,
                Role = model.Role,
                ProfilePictureUrl = model.ProfilePictureUrl
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User UpdateUser(int userId, UpdateUserModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }

            user.Username = model.Username;
            user.Email = model.Email;
            user.Role = model.Role;
            user.ProfilePictureUrl = model.ProfilePictureUrl;

            _context.SaveChanges();

            return user;
        }

        public bool DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }

        private byte[] HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
