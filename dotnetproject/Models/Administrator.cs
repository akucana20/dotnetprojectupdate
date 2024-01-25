using dotnetproject.Models;

namespace dotnetproject.Models { 
public class Administrator
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AdminLevel { get; set; }

    public User User { get; set; }
}
 }