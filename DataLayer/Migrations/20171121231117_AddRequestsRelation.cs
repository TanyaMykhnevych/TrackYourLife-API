using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class AddRequestsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorRequests_PatientRequests_PatientRequestId",
                table: "DonorRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientRequests_DonorRequests_DonorRequestId",
                table: "PatientRequests");

            migrationBuilder.DropIndex(
                name: "IX_PatientRequests_DonorRequestId",
                table: "PatientRequests");

            migrationBuilder.DropIndex(
                name: "IX_DonorRequests_PatientRequestId",
                table: "DonorRequests");

            migrationBuilder.DropColumn(
                name: "DonorRequestId",
                table: "PatientRequests");

            migrationBuilder.DropColumn(
                name: "PatientRequestId",
                table: "DonorRequests");

            migrationBuilder.CreateTable(
                name: "RequestsRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonorRequestId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PatientRequestId = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestsRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestsRelations_DonorRequests_DonorRequestId",
                        column: x => x.DonorRequestId,
                        principalTable: "DonorRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestsRelations_PatientRequests_PatientRequestId",
                        column: x => x.PatientRequestId,
                        principalTable: "PatientRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestsRelations_DonorRequestId",
                table: "RequestsRelations",
                column: "DonorRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestsRelations_PatientRequestId",
                table: "RequestsRelations",
                column: "PatientRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestsRelations_DonorRequestId_PatientRequestId",
                table: "RequestsRelations",
                columns: new[] { "DonorRequestId", "PatientRequestId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestsRelations");

            migrationBuilder.AddColumn<int>(
                name: "DonorRequestId",
                table: "PatientRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientRequestId",
                table: "DonorRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientRequests_DonorRequestId",
                table: "PatientRequests",
                column: "DonorRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorRequests_PatientRequestId",
                table: "DonorRequests",
                column: "PatientRequestId",
                unique: true,
                filter: "[PatientRequestId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorRequests_PatientRequests_PatientRequestId",
                table: "DonorRequests",
                column: "PatientRequestId",
                principalTable: "PatientRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRequests_DonorRequests_DonorRequestId",
                table: "PatientRequests",
                column: "DonorRequestId",
                principalTable: "DonorRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
