using KPZ_React_Lab.ViewModels;

namespace KPZ_React_Lab.Data
{
     public class TeamManagementDataContext
     {
          public List<ProjectViewModel> Projects { get; set; }
          public List<EmployeeViewModel> Employees { get; set; }

          public TeamManagementDataContext()
          {
               Projects = new List<ProjectViewModel>();
               Employees = new List<EmployeeViewModel>();


               Employees.Add(
                    new EmployeeViewModel() { Name = "Olexiy", Surname = "Pryadko", Position = "Backend Developer", Salary = 600 }
               );
          }
     }
}
