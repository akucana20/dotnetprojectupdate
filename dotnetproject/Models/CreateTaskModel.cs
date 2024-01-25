public class CreateTaskModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int AssignedToEmployeeId { get; set; }
    public int ProjectId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
