namespace _4Create.Application.Features.Employees.Models
{
    public class EmployeeRequestModel
    {
        public EmployeeRequestModel()
        {
                
        }
        public EmployeeRequestModel(int? id, string? email, string? title)
        {
            EmployeeId = id;
            EmployeeEmail = email;
            EmployeeTitle = title;
        }
        public int? EmployeeId { get; set; }
        public string? EmployeeTitle { get; set; }
        public string? EmployeeEmail { get; set; }
    }
}
