using BackEnd.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealState.DAL.Context;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BackEnd.DAL.Context
{
    public class BakEndContext : IdentityDbContext<ApplicationUser>, IBackEndContext
    {
       
        public BakEndContext(DbContextOptions<BakEndContext> options) : base(options)
        {

        }
        public DbSet<ApplicationRole> ApplicationRoles{ get; set; }
     //   public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole{ get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
    
        public DbSet<Category> Categories{ get; set; } 
        public DbSet<SubCategory> SubCategories{ get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // set application user relations
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
               

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            // set application role relations
            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                      });
          



        }
    }
}
