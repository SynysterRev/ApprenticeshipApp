using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Enums;
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

            // avoid to display "delete" offers
            modelBuilder.Entity<Offer>()
                .HasQueryFilter(o => !o.IsDeleted);

            modelBuilder.Entity<Application>()
                .HasQueryFilter(a => !a.Offer.IsDeleted);

            modelBuilder.Entity<Favorite>()
                .HasQueryFilter(f => !f.JobOffer.IsDeleted);

            modelBuilder.Entity<JobSector>()
                .Property(js => js.Name)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<JobSector>()
                .HasIndex(js => js.Name)
                .IsUnique();

            SeedJobSectors(modelBuilder);
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                SeedFakeOffers(modelBuilder);
            }
        }

        private void SeedJobSectors(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;

            modelBuilder.Entity<JobSector>().HasData(
                new JobSector
                {
                    Id = Guid.Parse("2338D5AA-1B27-4BFE-85F7-4903E6D3434A"),
                    Name = "Informatique",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 07, 24, 14, 47, 27, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 07, 24, 14, 47, 27, DateTimeKind.Utc),
                },
                new JobSector
                {
                    Id = Guid.Parse("0353CC9B-8A46-4CB4-BD53-E4204C8C7F0D"),
                    Name = "Santé",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 07, 24, 14, 47, 27, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2025, 07, 24, 14, 47, 27, DateTimeKind.Utc),
                }
            );
        }

        private void SeedFakeOffers(ModelBuilder modelBuilder)
        {
            var companyId = Guid.Parse("badc08ad-c99b-406d-8ea4-7f0585be6a5f");
            var jobSectorId1 = Guid.Parse("2338d5aa-1b27-4bfe-85f7-4903e6d3434a");
            var jobSectorId2 = Guid.Parse("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d");

            var offers = new List<Offer>();
            var baseDate = new DateTime(2025, 8, 4);

            for (int i = 0; i < 100; i++)
            {
                var offer = new Offer
                {
                    Id = Guid.Parse($"00000000-0000-0000-0000-{i+1:D12}"),
                    Title = $"Title {i}",
                    Description = $"Description {i}",
                    Location = $"Location {i}",
                    ContractType = (ContractType)(i % 5),
                    ExperienceRequired = i % 3,
                    SalaryMin = 20000 + i * 1000,
                    SalaryMax = 50000 + i * 1000,
                    SalaryPeriod = (SalaryPeriod)(i % 3),
                    RemoteType = (RemoteType)(i % 3),
                    PublishedAt = baseDate.AddDays(i),
                    UpdatedAt = baseDate.AddDays(i),
                    CompanyId = companyId,
                    JobSectorId = i % 2 == 0 ? jobSectorId1 : jobSectorId2,
                    IsDeleted = false,
                    DeletedAt = null
                };

                offers.Add(offer);
            }

            modelBuilder.Entity<Offer>().HasData(offers);
        }

    }
}
