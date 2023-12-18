using KPZ_React_Lab.Data;
using KPZ_React_Lab.ViewModels;
using KPZ_React_Lab.Services.Interfaces;

namespace KPZ_React_Lab.Services
{
     public class EmployeesService : IEmployeesService
     {
          private TeamManagementDataContext _dataContext;

          public EmployeesService(TeamManagementDataContext dataContext)
          {
               _dataContext = dataContext;
          }

          public EmployeeViewModel Create(EmployeeViewModel model)
          {
               var lastProduct = _dataContext.Projects.LastOrDefault();
               var newId = lastProduct is null ? 1 : lastProduct.Id + 1;

               model.Id = newId;
               _dataContext.Employees.Add(model);

               return model;
          }

          public void Delete(int id)
          {
               var modelToDelete = _dataContext.Employees.FirstOrDefault(p => p.Id == id);
               if (modelToDelete != null)
               {
                    _dataContext.Employees.Remove(modelToDelete);
               }
          }

          public EmployeeViewModel Get(int id)
          {
               return _dataContext.Employees.FirstOrDefault(p => p.Id == id);
          }

          public List<EmployeeViewModel> Get()
          {
               return _dataContext.Employees;
          }

          public EmployeeViewModel Update(EmployeeViewModel model)
          {
               var modelToUpdate = _dataContext.Employees.FirstOrDefault(p => p.Id == model.Id);
               if (modelToUpdate != null)
               {
                    modelToUpdate.Name = model.Name;
                    modelToUpdate.Surname = model.Surname;
                    modelToUpdate.Position = model.Position;
                    modelToUpdate.Salary = model.Salary;
               }

               return model;
          }
     }
}
