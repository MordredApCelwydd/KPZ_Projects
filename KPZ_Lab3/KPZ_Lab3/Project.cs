using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KPZ_Lab3
{
     public class Project : INotifyPropertyChanged
     {
          public Project() { }
          public Project(string name, string description, int memberCount, string status, int projectDifficulty)
          {
               Name = name;
               Description = description;
               MemberCount = memberCount;
               Status = status;
               ProjectDifficulty = projectDifficulty;
          }

          public event PropertyChangedEventHandler? PropertyChanged;
          public void OnPropertyChanged([CallerMemberName] string prop = "")
          {
               if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
          }

          private int id;
          private string name;
          private string description;
          private int memberCount;
          private string status;
          private int projectDifficulty;
          public int Id
          {
               get { return id; }
               set
               {
                    id = value;
                    OnPropertyChanged("Id");
               }
          }
          public string Name
          {
               get { return name; }
               set
               {
                    name = value;
                    OnPropertyChanged("Name");
               }
          }
          public string Description
          {
               get { return description; }
               set
               {
                    description = value;
                    OnPropertyChanged("Description");
               }
          }
          public int MemberCount
          {
               get { return memberCount; }
               set
               {
                    memberCount = value;
                    OnPropertyChanged("MemberCount");
               }
          }
          public string Status
          {
               get { return status; }
               set
               {
                    status = value;
                    OnPropertyChanged("Status");
               }
          }
          public int ProjectDifficulty
        {
               get { return projectDifficulty; }
               set
               {
                    projectDifficulty = value;
                    OnPropertyChanged("ProjectDifficulty");
               }
          }
     }
}
