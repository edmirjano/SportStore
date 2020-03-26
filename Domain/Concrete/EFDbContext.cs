using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products
        {
            get; set;
        }
    }
}