using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class PatientOrganQuery_HasDonorOrganQuery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientOrganQueries_TransplantOrgans_TransplantOrganId",
                table: "PatientOrganQueries");

            migrationBuilder.DropIndex(
                name: "IX_PatientOrganQueries_TransplantOrganId",
                table: "PatientOrganQueries");

            migrationBuilder.DropIndex(
                name: "IX_DonorOrganQueries_TransplantOrganId",
                table: "DonorOrganQueries");

            migrationBuilder.DropColumn(
                name: "TransplantOrganId",
                table: "PatientOrganQueries");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "DonorMedicalExams");

            migrationBuilder.DropColumn(
                name: "When",
                table: "DonorMedicalExams");

            migrationBuilder.AddColumn<int>(
                name: "DonorOrganQueryId",
                table: "PatientOrganQueries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientOrganQueryId",
                table: "DonorOrganQueries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Results",
                table: "DonorMedicalExams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledAt",
                table: "DonorMedicalExams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PatientOrganQueries_DonorOrganQueryId",
                table: "PatientOrganQueries",
                column: "DonorOrganQueryId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorOrganQueries_PatientOrganQueryId",
                table: "DonorOrganQueries",
                column: "PatientOrganQueryId",
                unique: true,
                filter: "[PatientOrganQueryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DonorOrganQueries_TransplantOrganId",
                table: "DonorOrganQueries",
                column: "TransplantOrganId",
                unique: true,
                filter: "[TransplantOrganId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorOrganQueries_PatientOrganQueries_PatientOrganQueryId",
                table: "DonorOrganQueries",
                column: "PatientOrganQueryId",
                principalTable: "PatientOrganQueries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientOrganQueries_DonorOrganQueries_DonorOrganQueryId",
                table: "PatientOrganQueries",
                column: "DonorOrganQueryId",
                principalTable: "DonorOrganQueries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorOrganQueries_PatientOrganQueries_PatientOrganQueryId",
                table: "DonorOrganQueries");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientOrganQueries_DonorOrganQueries_DonorOrganQueryId",
                table: "PatientOrganQueries");

            migrationBuilder.DropIndex(
                name: "IX_PatientOrganQueries_DonorOrganQueryId",
                table: "PatientOrganQueries");

            migrationBuilder.DropIndex(
                name: "IX_DonorOrganQueries_PatientOrganQueryId",
                table: "DonorOrganQueries");

            migrationBuilder.DropIndex(
                name: "IX_DonorOrganQueries_TransplantOrganId",
                table: "DonorOrganQueries");

            migrationBuilder.DropColumn(
                name: "DonorOrganQueryId",
                table: "PatientOrganQueries");

            migrationBuilder.DropColumn(
                name: "PatientOrganQueryId",
                table: "DonorOrganQueries");

            migrationBuilder.DropColumn(
                name: "Results",
                table: "DonorMedicalExams");

            migrationBuilder.DropColumn(
                name: "ScheduledAt",
                table: "DonorMedicalExams");

            migrationBuilder.AddColumn<int>(
                name: "TransplantOrganId",
                table: "PatientOrganQueries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "DonorMedicalExams",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "When",
                table: "DonorMedicalExams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PatientOrganQueries_TransplantOrganId",
                table: "PatientOrganQueries",
                column: "TransplantOrganId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorOrganQueries_TransplantOrganId",
                table: "DonorOrganQueries",
                column: "TransplantOrganId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientOrganQueries_TransplantOrgans_TransplantOrganId",
                table: "PatientOrganQueries",
                column: "TransplantOrganId",
                principalTable: "TransplantOrgans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
