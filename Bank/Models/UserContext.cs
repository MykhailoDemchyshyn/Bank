using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bank.Models
{
    public class UserContext:DbContext
    {
       public UserContext():base("DefaultConnection")
       { }

        public DbSet<User> Users { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Role> Roles { get; set; }


    }
}