using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPZ_Lab3
{
     public class ProjectListContext : DbContext
     {
          public DbSet<Project> Projects { get; set; }
          public ProjectListContext() 
          { 
               //Database.EnsureCreated();
          }
          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
               optionsBuilder.UseSqlServer(@"Server=Home-PC\MSSQLSERV;Database=KPZ_Lab3_DB;Trusted_Connection=True;TrustServerCertificate=True");
          }
     }
}
