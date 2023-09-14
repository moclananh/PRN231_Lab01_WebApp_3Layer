using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        //dbset
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        //mock data
        protected override void OnModelCreating(ModelBuilder optionBuilder)
        {
            optionBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Cat01" },
                 new Category { CategoryId = 2, CategoryName = "Cat02" },
                  new Category { CategoryId = 3, CategoryName = "Cat03" },
                   new Category { CategoryId = 4, CategoryName = "Cat04" },
                    new Category { CategoryId = 5, CategoryName = "Cat05" },
                     new Category { CategoryId = 6, CategoryName = "Cat06" }
                     );
        }
    }
}
