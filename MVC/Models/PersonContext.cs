using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class PersonContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }

        public PersonContext(DbContextOptions<PersonContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
