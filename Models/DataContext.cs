using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS_WEBSITE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LMS_WEBSITE.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<AdminUser> adminUsers { get; set; }
    }
}