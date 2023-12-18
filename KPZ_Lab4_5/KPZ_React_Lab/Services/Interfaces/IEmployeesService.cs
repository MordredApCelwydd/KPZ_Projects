using KPZ_React_Lab.ViewModels;

namespace KPZ_React_Lab.Services.Interfaces
{
     public interface IEmployeesService
     {
          public EmployeeViewModel Create(EmployeeViewModel model);

          public EmployeeViewModel Update(EmployeeViewModel model);
          public EmployeeViewModel Get(int id);

          public List<EmployeeViewModel> Get();

          public void Delete(int id);
     }
}
