using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JuniorOnly.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CandidateProfile> CandidateProfiles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Many-to-Many: Offer <-> Tag
            modelBuilder.Entity<Offer>()
                .HasMany(o => o.Tags)
                .WithMany(t => t.Offers);

            // Configure Company -> JobOffers relationship
            modelBuilder.Entity<Company>()
                .HasMany(c => c.JobOffers)
                .WithOne(o => o.Company)
                .HasForeignKey(o => o.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure One-to-One: ApplicationUser <-> CandidateProfile
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.CandidateProfile)
                .WithOne(p => p.User)
                .HasForeignKey<CandidateProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ApplicationUser -> Company relationship
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Company)
                .WithOne(c => c.CreatedByUser)
                .HasForeignKey<Company>(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Application relationships
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Offer)
                .WithMany(o => o.Applications)
                .HasForeignKey(a => a.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Applicant)
                .WithMany()
                .HasForeignKey(a => a.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Favorite relationships
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.JobOffer)
                .WithMany()
                .HasForeignKey(f => f.JobOfferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.CandidateProfile)
                .WithMany(cp => cp.Favorites)
                .HasForeignKey(f => f.CandidateProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure DesiredJobTitles as JSON column
            var stringArrayComparer = new ValueComparer<string[]>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray()
            );

            modelBuilder.Entity<CandidateProfile>()
                .Property(cp => cp.DesiredJobTitles)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                )
                .Metadata.SetValueComparer(stringArrayComparer);

            modelBuilder.Entity<Offer>()
                .ToTable(t => t.HasCheckConstraint("CK_Offer_SalaryRange", "[SalaryMax] > [SalaryMin]"));
        }

    }
}
