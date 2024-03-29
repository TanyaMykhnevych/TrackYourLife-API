﻿// <auto-generated />
using Common.Enums;
using DataLayer.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DataLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20171203111231_OrganDeliveryInfo_RemoveUselessFields")]
    partial class OrganDeliveryInfo_RemoveUselessFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Common.Entities.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("Altitude");

                    b.Property<string>("City");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactPhone");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Longitude");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("Common.Entities.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Common.Entities.Organ.OrganInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<TimeSpan>("OutsideHumanPossibleTime");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("OrganInfos");
                });

            modelBuilder.Entity("Common.Entities.Organ.TransplantOrgan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalInfo");

                    b.Property<int>("ClinicId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<int?>("OrganDeliveryInfoId");

                    b.Property<int>("OrganInfoId");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.Property<int>("UserInfoId");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("OrganDeliveryInfoId")
                        .IsUnique()
                        .HasFilter("[OrganDeliveryInfoId] IS NOT NULL");

                    b.HasIndex("OrganInfoId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("TransplantOrgans");
                });

            modelBuilder.Entity("Common.Entities.OrganDelivery.OrganDataSnapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Altitude");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<double>("Longitude");

                    b.Property<int>("OrganDeliveryId");

                    b.Property<float>("Temperature");

                    b.Property<DateTime>("Time");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("OrganDeliveryId");

                    b.ToTable("OrganDataSnapshots");
                });

            modelBuilder.Entity("Common.Entities.OrganDelivery.OrganDeliveryInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<int>("TransplantOrganId");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("OrganDeliveryInfos");
                });

            modelBuilder.Entity("Common.Entities.OrganRequests.DonorMedicalExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClinicId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<int>("DonorRequestId");

                    b.Property<string>("Results");

                    b.Property<DateTime>("ScheduledAt");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("DonorRequestId");

                    b.ToTable("DonorMedicalExams");
                });

            modelBuilder.Entity("Common.Entities.OrganRequests.DonorRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<int>("DonorInfoId");

                    b.Property<string>("Message");

                    b.Property<int>("OrganInfoId");

                    b.Property<int>("Status");

                    b.Property<int?>("TransplantOrganId");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("DonorInfoId");

                    b.HasIndex("OrganInfoId");

                    b.HasIndex("TransplantOrganId")
                        .IsUnique()
                        .HasFilter("[TransplantOrganId] IS NOT NULL");

                    b.ToTable("DonorRequests");
                });

            modelBuilder.Entity("Common.Entities.OrganRequests.PatientRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Message");

                    b.Property<int>("OrganInfoId");

                    b.Property<int?>("PatientInfoId");

                    b.Property<int>("Priority");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("OrganInfoId");

                    b.HasIndex("PatientInfoId");

                    b.ToTable("PatientRequests");
                });

            modelBuilder.Entity("Common.Entities.OrganRequests.RequestsRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<int>("DonorRequestId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("PatientRequestId");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("DonorRequestId")
                        .IsUnique();

                    b.HasIndex("PatientRequestId")
                        .IsUnique();

                    b.HasIndex("DonorRequestId", "PatientRequestId")
                        .IsUnique();

                    b.ToTable("RequestsRelations");
                });

            modelBuilder.Entity("Common.Entities.UserInfo", b =>
                {
                    b.Property<int>("UserInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AppUserId");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Notes");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("SecondName");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("ZipCode");

                    b.HasKey("UserInfoId");

                    b.HasIndex("AppUserId")
                        .IsUnique()
                        .HasFilter("[AppUserId] IS NOT NULL");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Common.Entities.Organ.TransplantOrgan", b =>
                {
                    b.HasOne("Common.Entities.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Entities.OrganDelivery.OrganDeliveryInfo", "OrganDeliveryInfo")
                        .WithOne("TransplantOrgan")
                        .HasForeignKey("Common.Entities.Organ.TransplantOrgan", "OrganDeliveryInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Entities.Organ.OrganInfo", "OrganInfo")
                        .WithMany()
                        .HasForeignKey("OrganInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Entities.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Common.Entities.OrganDelivery.OrganDataSnapshot", b =>
                {
                    b.HasOne("Common.Entities.OrganDelivery.OrganDeliveryInfo", "OrganDelivery")
                        .WithMany("OrganDataSnapshots")
                        .HasForeignKey("OrganDeliveryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Common.Entities.OrganRequests.DonorMedicalExam", b =>
                {
                    b.HasOne("Common.Entities.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Entities.OrganRequests.DonorRequest", "DonorRequest")
                        .WithMany("DonorMedicalExams")
                        .HasForeignKey("DonorRequestId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Common.Entities.OrganRequests.DonorRequest", b =>
                {
                    b.HasOne("Common.Entities.UserInfo", "DonorInfo")
                        .WithMany()
                        .HasForeignKey("DonorInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Entities.Organ.OrganInfo", "OrganInfo")
                        .WithMany()
                        .HasForeignKey("OrganInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Entities.Organ.TransplantOrgan", "TransplantOrgan")
                        .WithOne("DonorRequest")
                        .HasForeignKey("Common.Entities.OrganRequests.DonorRequest", "TransplantOrganId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Common.Entities.OrganRequests.PatientRequest", b =>
                {
                    b.HasOne("Common.Entities.Organ.OrganInfo", "OrganInfo")
                        .WithMany()
                        .HasForeignKey("OrganInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Entities.UserInfo", "PatientInfo")
                        .WithMany()
                        .HasForeignKey("PatientInfoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Common.Entities.OrganRequests.RequestsRelation", b =>
                {
                    b.HasOne("Common.Entities.OrganRequests.DonorRequest", "DonorRequest")
                        .WithOne("RequestsRelation")
                        .HasForeignKey("Common.Entities.OrganRequests.RequestsRelation", "DonorRequestId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Entities.OrganRequests.PatientRequest", "PatientRequest")
                        .WithOne("RequestsRelation")
                        .HasForeignKey("Common.Entities.OrganRequests.RequestsRelation", "PatientRequestId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Common.Entities.UserInfo", b =>
                {
                    b.HasOne("Common.Entities.Identity.AppUser", "AppUser")
                        .WithOne("UserInfo")
                        .HasForeignKey("Common.Entities.UserInfo", "AppUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Common.Entities.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Common.Entities.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Entities.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Common.Entities.Identity.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
