using ApprenticeshipApp.Domain.Entities;
using ApprenticeshipApp.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprenticeshipApp.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Inscription>()
                .HasKey(i => new { i.UserId, i.SessionId });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasOne(n => n.Apprentice)
                    .WithMany()
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(n => n.Session)
                    .WithMany()
                    .HasForeignKey(n => n.SessionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
