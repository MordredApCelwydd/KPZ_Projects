using KPZ_React_Lab.ViewModels;

namespace KPZ_React_Lab.Services.Interfaces
{
     public interface IProjectsService
     {

          public ProjectViewModel Create(ProjectViewModel model);

          public ProjectViewModel Update(ProjectViewModel model);
          public ProjectViewModel Get(int id);

          public List<ProjectViewModel> Get();

          public void Delete(int id);

     }
}
