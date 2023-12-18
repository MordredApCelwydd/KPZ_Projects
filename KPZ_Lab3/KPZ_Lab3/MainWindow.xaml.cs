using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;

namespace KPZ_Lab3
{
   
     public partial class MainWindow : Window
     {
          private ProjectListContext db = new ProjectListContext();
          

          public MainWindow()
          {
               InitializeComponent();
               db.Projects.Load();
               ProjectDataGrid.ItemsSource = db.Projects.Local.ToObservableCollection();
          }

          private void AddButton_Click(object sender, RoutedEventArgs e)
          {
               string name = TextBox_Name.Text;
               string description = TextBox_Description.Text;
               int memberCount = int.Parse(TextBox_MemberCount.Text);
               string status = TextBox_Status.Text;
               int projectDiffulty = int.Parse(TextBox_ProjectDifficulty.Text);
               Project newProject = new Project(name, description, memberCount, status, projectDiffulty);

               db.Add(newProject);
               db.SaveChanges();
          }

          private void DeleteButton_Click(object sender, RoutedEventArgs e)
          {
               Project project = ProjectDataGrid.SelectedItem as Project;

               if(project != null)
               {
                    db.Remove(project);
                    db.SaveChanges();
               }
          }

          private void UpdateButton_Click(object sender, RoutedEventArgs e)
          {
               Project project = ProjectDataGrid.SelectedItem as Project;

               if (project != null)
               {
                    if (!TextBox_Name.Text.IsNullOrEmpty())
                        project.Name = TextBox_Name.Text;
                    if (!TextBox_Description.Text.IsNullOrEmpty())
                        project.Description = TextBox_Description.Text;
                    if (!TextBox_MemberCount.Text.IsNullOrEmpty())
                        project.MemberCount = int.Parse(TextBox_MemberCount.Text);
                    if (!TextBox_Status.Text.IsNullOrEmpty())
                        project.Status = TextBox_Status.Text;
                    if (!TextBox_ProjectDifficulty.Text.IsNullOrEmpty())
                        project.ProjectDifficulty = int.Parse(TextBox_ProjectDifficulty.Text);
                    db.Update(project);
                    db.SaveChanges();
               }
          }
     }
}
