using KPZ_React_Lab.ViewModels;
using KPZ_React_Lab.Services.Interfaces;
using KPZ_React_Lab.Data;

namespace KPZ_React_Lab.Services
{
     public class ProjectsService : IProjectsService
     {
          private TeamManagementDataContext _dataContext;

          public ProjectsService(TeamManagementDataContext dataContext)
          {
               _dataContext = dataContext;
          }

          public ProjectViewModel Create(ProjectViewModel model)
          {
               var lastProduct = _dataContext.Projects.LastOrDefault();
               var newId = lastProduct is null ? 1 : lastProduct.Id + 1;

               model.Id = newId;
               _dataContext.Projects.Add(model);

               return model;
          }

          public void Delete(int id)
          {
               var modelToDelete = _dataContext.Projects.FirstOrDefault(p => p.Id == id);
               if (modelToDelete != null)
               {
                    _dataContext.Projects.Remove(modelToDelete);
               }
          }

          public ProjectViewModel Get(int id)
          {
               return _dataContext.Projects.FirstOrDefault(p => p.Id == id);
          }

          public List<ProjectViewModel> Get()
          {
               return _dataContext.Projects;
          }

          public ProjectViewModel Update(ProjectViewModel model)
          {
               var modelToUpdate = _dataContext.Projects.FirstOrDefault(p => p.Id == model.Id);
               if (modelToUpdate != null)
               {
                    modelToUpdate.Name = model.Name;
                    modelToUpdate.Description = model.Description;
                    modelToUpdate.Status = model.Status;
                    modelToUpdate.MemberCount = model.MemberCount;
               }

               return model;
          }
     }
}
