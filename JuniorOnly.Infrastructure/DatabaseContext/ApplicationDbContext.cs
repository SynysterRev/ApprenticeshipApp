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
        public DbSet<JobSector> JobSectors { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure One-to-Many: Offer <-> JobSector
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.JobSector)
                .WithMany(j => j.Offers)
                .HasForeignKey(o => o.JobSectorId)
                .OnDelete(DeleteBehavior.Restrict);

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
                .HasKey(f => new { f.CandidateProfileId, f.JobOfferId });

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
                (c1, c2) => (c1 ?? new string[0]).SequenceEqual(c2 ?? new string[0]),
                c => (c ?? new string[0]).Aggregate(0, (a, v) => HashCode.Combine(a, v == null ? 0 : v.GetHashCode())),
                c => c == null ? new string[0] : c.ToArray()
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

            modelBuilder.Entity<JobSector>()
                .Property(js => js.Name)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<JobSector>()
                .HasIndex(js => js.Name)
                .IsUnique();
        }

    }
}
