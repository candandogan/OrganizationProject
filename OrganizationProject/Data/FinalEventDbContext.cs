using Microsoft.EntityFrameworkCore;
using OrganizationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Data
{
    public class FinalEventDbContext :DbContext
    {
        public FinalEventDbContext(DbContextOptions<FinalEventDbContext> options) : base(options)
        {

        }
        //public DbSet<Book> Books { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ConfirmThing> ConfirmThings { get; set; }
        public DbSet<ConfirmThingUser> ConfirmThingUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ConfirmThingUser>()
                .HasKey(cf => new { cf.UserId, cf.ConfirmThingId });

            base.OnModelCreating(modelBuilder);

        }
    }
}
