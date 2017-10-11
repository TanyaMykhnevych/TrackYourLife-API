﻿// <auto-generated />
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
    [Migration("20171011211809_PatientOrganQuery_HasDonorOrganQuery")]
    partial class PatientOrganQuery_HasDonorOrganQuery
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Entities.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("Altitude");

                    b.Property<string>("City");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactPhone");

                    b.Property<string>("Country");

                    b.Property<string>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("DataLayer.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DataLayer.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("PasswordHash");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataLayer.Entities.Identity.UserRole", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersToRoles");
                });

            modelBuilder.Entity("DataLayer.Entities.Organ.OrganInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<TimeSpan>("OutsideHumanPossibleTime");

                    b.HasKey("Id");

                    b.ToTable("OrganInfos");
                });

            modelBuilder.Entity("DataLayer.Entities.Organ.TransplantOrgan", b =>
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

            modelBuilder.Entity("DataLayer.Entities.OrganDelivery.OrganDataSnapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Altitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("OrganDeliveryId");

                    b.Property<float>("Temperature");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("OrganDeliveryId");

                    b.ToTable("OrganDataSnapshots");
                });

            modelBuilder.Entity("DataLayer.Entities.OrganDelivery.OrganDeliveryInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<int>("DonorId");

                    b.Property<int>("FromClinicId");

                    b.Property<int>("PatientId");

                    b.Property<DateTime>("StartTransportTime");

                    b.Property<int>("Status");

                    b.Property<int>("ToClinicId");

                    b.Property<int>("TransplantOrganId");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("DonorId");

                    b.HasIndex("FromClinicId");

                    b.HasIndex("PatientId");

                    b.HasIndex("ToClinicId");

                    b.ToTable("OrganDeliveryInfos");
                });

            modelBuilder.Entity("DataLayer.Entities.OrganQueries.DonorMedicalExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClinicId");

                    b.Property<int>("DonorOrganQueryId");

                    b.Property<string>("Results");

                    b.Property<DateTime>("ScheduledAt");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("DonorOrganQueryId");

                    b.ToTable("DonorMedicalExams");
                });

            modelBuilder.Entity("DataLayer.Entities.OrganQueries.DonorOrganQuery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<int>("DonorInfoId");

                    b.Property<string>("Message");

                    b.Property<int>("OrganInfoId");

                    b.Property<int?>("PatientOrganQueryId");

                    b.Property<int>("Status");

                    b.Property<int?>("TransplantOrganId");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("DonorInfoId");

                    b.HasIndex("OrganInfoId");

                    b.HasIndex("PatientOrganQueryId")
                        .IsUnique()
                        .HasFilter("[PatientOrganQueryId] IS NOT NULL");

                    b.HasIndex("TransplantOrganId")
                        .IsUnique()
                        .HasFilter("[TransplantOrganId] IS NOT NULL");

                    b.ToTable("DonorOrganQueries");
                });

            modelBuilder.Entity("DataLayer.Entities.OrganQueries.PatientOrganQuery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DonorOrganQueryId");

                    b.Property<string>("Message");

                    b.Property<int>("OrganInfoId");

                    b.Property<int?>("PatientInfoId");

                    b.Property<int>("Priority");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("DonorOrganQueryId");

                    b.HasIndex("OrganInfoId");

                    b.HasIndex("PatientInfoId");

                    b.ToTable("PatientOrganQueries");
                });

            modelBuilder.Entity("DataLayer.Entities.UserInfo", b =>
                {
                    b.Property<int>("UserInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("SecondName");

                    b.Property<DateTime?>("Updated");

                    b.Property<string>("UpdatedBy");

                    b.Property<int>("UserId");

                    b.Property<string>("ZipCode");

                    b.HasKey("UserInfoId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("DataLayer.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("DataLayer.Entities.Identity.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.Identity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataLayer.Entities.Organ.TransplantOrgan", b =>
                {
                    b.HasOne("DataLayer.Entities.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.OrganDelivery.OrganDeliveryInfo", "OrganDeliveryInfo")
                        .WithOne("TransplantOrgan")
                        .HasForeignKey("DataLayer.Entities.Organ.TransplantOrgan", "OrganDeliveryInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.Organ.OrganInfo", "OrganInfo")
                        .WithMany()
                        .HasForeignKey("OrganInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataLayer.Entities.OrganDelivery.OrganDataSnapshot", b =>
                {
                    b.HasOne("DataLayer.Entities.OrganDelivery.OrganDeliveryInfo", "OrganDelivery")
                        .WithMany("OrganDataSnapshots")
                        .HasForeignKey("OrganDeliveryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataLayer.Entities.OrganDelivery.OrganDeliveryInfo", b =>
                {
                    b.HasOne("DataLayer.Entities.UserInfo", "Donor")
                        .WithMany()
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.Clinic", "FromClinic")
                        .WithMany()
                        .HasForeignKey("FromClinicId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.UserInfo", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.Clinic", "ToClinic")
                        .WithMany()
                        .HasForeignKey("ToClinicId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataLayer.Entities.OrganQueries.DonorMedicalExam", b =>
                {
                    b.HasOne("DataLayer.Entities.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.OrganQueries.DonorOrganQuery", "DonorOrganQuery")
                        .WithMany("DonorMedicalExams")
                        .HasForeignKey("DonorOrganQueryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataLayer.Entities.OrganQueries.DonorOrganQuery", b =>
                {
                    b.HasOne("DataLayer.Entities.UserInfo", "DonorInfo")
                        .WithMany()
                        .HasForeignKey("DonorInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.Organ.OrganInfo", "OrganInfo")
                        .WithMany()
                        .HasForeignKey("OrganInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.OrganQueries.PatientOrganQuery", "PatientOrganQuery")
                        .WithOne()
                        .HasForeignKey("DataLayer.Entities.OrganQueries.DonorOrganQuery", "PatientOrganQueryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.Organ.TransplantOrgan", "TransplantOrgan")
                        .WithOne("DonorOrganQuery")
                        .HasForeignKey("DataLayer.Entities.OrganQueries.DonorOrganQuery", "TransplantOrganId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataLayer.Entities.OrganQueries.PatientOrganQuery", b =>
                {
                    b.HasOne("DataLayer.Entities.OrganQueries.DonorOrganQuery", "DonorOrganQuery")
                        .WithMany()
                        .HasForeignKey("DonorOrganQueryId");

                    b.HasOne("DataLayer.Entities.Organ.OrganInfo", "OrganInfo")
                        .WithMany()
                        .HasForeignKey("OrganInfoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataLayer.Entities.UserInfo", "PatientInfo")
                        .WithMany()
                        .HasForeignKey("PatientInfoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataLayer.Entities.UserInfo", b =>
                {
                    b.HasOne("DataLayer.Entities.Identity.User", "User")
                        .WithOne("UserInfo")
                        .HasForeignKey("DataLayer.Entities.UserInfo", "UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
