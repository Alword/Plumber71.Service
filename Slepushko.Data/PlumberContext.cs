using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slepushko.Data
{
    public class PlumberContext : DbContext
    {
        public DbSet<ServiceTitle> ServiceTitles { get; set; }

        public PlumberContext() 
        {
            Database.EnsureCreated();
        }

        public PlumberContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("test1");
            }
        }
    }
}
