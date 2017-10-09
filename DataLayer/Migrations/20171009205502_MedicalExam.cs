using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class MedicalExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientQueue");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "TransplantOrgans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DonorMedicalExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClinicId = table.Column<int>(type: "int", nullable: false),
                    DonorOrganQueryId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    When = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorMedicalExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorMedicalExams_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorMedicalExams_DonorOrganQueries_DonorOrganQueryId",
                        column: x => x.DonorOrganQueryId,
                        principalTable: "DonorOrganQueries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonorMedicalExams_ClinicId",
                table: "DonorMedicalExams",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorMedicalExams_DonorOrganQueryId",
                table: "DonorMedicalExams",
                column: "DonorOrganQueryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonorMedicalExams");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "TransplantOrgans");

            migrationBuilder.CreateTable(
                name: "PatientQueue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrganInfoId = table.Column<int>(nullable: false),
                    PatientOrganQueryId = table.Column<int>(nullable: false),
                    PatientUserInfoId = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientQueue_OrganInfos_OrganInfoId",
                        column: x => x.OrganInfoId,
                        principalTable: "OrganInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientQueue_PatientOrganQueries_PatientOrganQueryId",
                        column: x => x.PatientOrganQueryId,
                        principalTable: "PatientOrganQueries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientQueue_UserInfos_PatientUserInfoId",
                        column: x => x.PatientUserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientQueue_OrganInfoId",
                table: "PatientQueue",
                column: "OrganInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientQueue_PatientOrganQueryId",
                table: "PatientQueue",
                column: "PatientOrganQueryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientQueue_PatientUserInfoId",
                table: "PatientQueue",
                column: "PatientUserInfoId");
        }
    }
}
