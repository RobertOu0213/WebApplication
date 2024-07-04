namespace WebApplication1.Models.DTO
{
    public class EmployeeDto
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ReportsTo { get; set; }
    }
}
