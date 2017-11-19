using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Common.Constants;
using Common.Entities;
using Common.Entities.Identity;
using Common.Entities.Organ;
using Common.Entities.OrganDelivery;
using Common.Entities.OrganQueries;
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

        public DbSet<OrganDeliveryInfo> OrganDeliveryInfos { get; set; }
        public DbSet<OrganDataSnapshot> OrganDataSnapshots { get; set; }

        public DbSet<DonorOrganQuery> DonorOrganQueries { get; set; }
        public DbSet<PatientOrganQuery> PatientOrganQueries { get; set; }

        public DbSet<DonorMedicalExam> DonorMedicalExams { get; set; }

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

            modelBuilder.Entity<OrganDeliveryInfo>()
                .HasOne(ot => ot.Donor)
                .WithMany()
                .HasForeignKey(ot => ot.DonorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrganDeliveryInfo>()
                .HasOne(ot => ot.Patient)
                .WithMany()
                .HasForeignKey(ot => ot.PatientId);
            modelBuilder.Entity<OrganDeliveryInfo>()
                .HasOne(ot => ot.ToClinic)
                .WithMany()
                .HasForeignKey(ot => ot.ToClinicId);
            modelBuilder.Entity<OrganDeliveryInfo>()
                .HasOne(ot => ot.FromClinic)
                .WithMany()
                .HasForeignKey(ot => ot.FromClinicId);
            modelBuilder.Entity<OrganDeliveryInfo>()
                .HasOne(ot => ot.TransplantOrgan)
                .WithOne(x => x.OrganDeliveryInfo)
                .HasForeignKey<TransplantOrgan>(ot => ot.OrganDeliveryInfoId)
                .IsRequired(false);

            modelBuilder.Entity<DonorOrganQuery>()
                .HasOne(ot => ot.DonorInfo)
                .WithMany()
                .HasForeignKey(ot => ot.DonorInfoId);
            
            modelBuilder.Entity<DonorOrganQuery>()
                .HasOne(ot => ot.PatientOrganQuery)
                .WithOne()
                .IsRequired(false);

            modelBuilder.Entity<PatientOrganQuery>()
                .HasOne(ot => ot.PatientInfo)
                .WithMany()
                .HasForeignKey(ot => ot.PatientInfoId);
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
