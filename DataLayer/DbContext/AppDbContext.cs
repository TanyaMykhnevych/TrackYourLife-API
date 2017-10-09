using DataLayer.Entities;
using DataLayer.Entities.Identity;
using DataLayer.Entities.Organ;
using DataLayer.Entities.OrganDelivery;
using DataLayer.Entities.OrganQueries;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace DataLayer.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UsersToRoles { get; set; }

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
            //TODO: replace to 'appsettings.json' and retrieve
            optionsBuilder.UseSqlServer(@"Server=.;Database=TrackYourLife;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            RemoveCascadeDeletingGlobally(modelBuilder);


            modelBuilder.Entity<User>()
                .HasOne(p => p.UserInfo)
                .WithOne(i => i.User)
                .HasForeignKey<UserInfo>(b => b.UserId);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.RoleId, ur.UserId });

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
                .HasForeignKey<TransplantOrgan>(ot => ot.OrganDeliveryInfoId);

            modelBuilder.Entity<DonorOrganQuery>()
                .HasOne(ot => ot.DonorInfo)
                .WithMany()
                .HasForeignKey(ot => ot.DonorInfoId);

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
