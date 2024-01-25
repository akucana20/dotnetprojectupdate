public class UpdateTaskModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public int AssignedToEmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
