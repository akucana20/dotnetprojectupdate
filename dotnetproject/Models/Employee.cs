public class Employee
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }

    public User User { get; set; }
}
