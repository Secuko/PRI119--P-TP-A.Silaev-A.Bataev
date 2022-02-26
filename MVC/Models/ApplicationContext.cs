using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<SearchRequest> SearchRequests { get; set; }
        public DbSet<VolRequest> VolRequests { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}