using MHA.Data.Domain;
using MHA.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.DataContext
{
    public class NewsContext:DbContext
    {
        public NewsContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsContext, Configuration>());
        }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
