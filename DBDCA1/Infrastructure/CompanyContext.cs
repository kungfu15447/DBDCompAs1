using Microsoft.EntityFrameworkCore;
﻿using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class CompanyContext : DbContext
    {
        public CompanyContext()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=<INSERT_SERVER_NAME>;Initial Catalog=Company;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasKey(d => d.DNumber);

            modelBuilder.Entity<Department>()
                .Property(d => d.EmpCount)
                .HasComputedColumnSql();
        }

        public DbSet<Department> Department { get; set; }
    }
}
