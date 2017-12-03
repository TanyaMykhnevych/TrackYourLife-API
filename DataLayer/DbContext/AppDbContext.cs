using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Common.Constants;
using Common.Entities;
using Common.Entities.Identity;
using Common.Entities.Organ;
using Common.Entities.OrganDelivery;
using Common.Entities.OrganRequests;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.DbContext
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<OrganInfo> OrganInfos { get; set; }
        public DbSet<TransplantOrgan> TransplantOrgans { get; set; }
        
        public DbSet<OrganDataSnapshot> OrganDataSnapshots { get; set; }

        public DbSet<DonorRequest> DonorRequests { get; set; }
        public DbSet<PatientRequest> PatientRequests { get; set; }

        public DbSet<DonorMedicalExam> DonorMedicalExams { get; set; }

        public DbSet<RequestsRelation> RequestsRelations { get; set; }

        public DbSet<Clinic> Clinics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationConstants.DbConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            RemoveCascadeDeletingGlobally(modelBuilder);


            modelBuilder.Entity<AppUser>()
                .HasOne(p => p.UserInfo)
                .WithOne(i => i.AppUser)
                .HasForeignKey<UserInfo>(b => b.AppUserId);

            modelBuilder.Entity<DonorRequest>()
                .HasOne(ot => ot.DonorInfo)
                .WithMany()
                .HasForeignKey(ot => ot.DonorInfoId);

            modelBuilder.Entity<PatientRequest>()
                .HasOne(ot => ot.PatientInfo)
                .WithMany()
                .HasForeignKey(ot => ot.PatientInfoId);

            modelBuilder.Entity<RequestsRelation>()
                .HasIndex(p => new { p.DonorRequestId, p.PatientRequestId })
                .IsUnique();
        }

        private void RemoveCascadeDeletingGlobally(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
