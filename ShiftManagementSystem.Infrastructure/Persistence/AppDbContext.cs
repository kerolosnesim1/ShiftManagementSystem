using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ShiftManagementSystem.Domain.Entities;

namespace ShiftManagementSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftAssignment> ShiftAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
             //configure user entity
            modelBuilder,Entity<User>(entity =>{
                entity.HashKey(e => e.Id);
                entity.property(e => e.Name).ISRequired(),HasMaxLength(100);
                entity.property(e => e.Email).ISRequired(),HasMaxLength(100);
                entity.property(e => e.PasswordHash).ISRequired();
            })

            //configure shift entity
            modelBuilder.Entity<Shift>(entity =>{
                entity.HashKey(e => e.Id);
                entity.property(e => e.Date).ISRequired():
                entity.property(e => e.StartTime).ISRequired();
                entity.property(e => e.EndTime).IsRequired();
            })

          
             // Configure ShiftAssignment entity
            modelBuilder.Entity<ShiftAssignment>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // Configure relationship with User
                entity.HasOne(e => e.User)
                      .WithMany(u => u.ShiftAssignments)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                // Configure relationship with Shift
                entity.HasOne(e => e.Shift)
                      .WithMany(s => s.ShiftAssignments)
                      .HasForeignKey(e => e.ShiftId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

