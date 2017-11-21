using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class RenameQueriesToRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorMedicalExams_DonorOrganQueries_DonorOrganQueryId",
                table: "DonorMedicalExams");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorOrganQueries_PatientOrganQueries_PatientOrganQueryId",
                table: "DonorOrganQueries");

            migrationBuilder.DropTable(
                name: "PatientOrganQueries");

            migrationBuilder.DropTable(
                name: "DonorOrganQueries");

            migrationBuilder.DropIndex(
                name: "IX_DonorMedicalExams_DonorOrganQueryId",
                table: "DonorMedicalExams");

            migrationBuilder.DropColumn(
                name: "DonorOrganQueryId",
                table: "DonorMedicalExams");

            migrationBuilder.AddColumn<int>(
                name: "DonorRequestId",
                table: "DonorMedicalExams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PatientRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonorRequestId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganInfoId = table.Column<int>(type: "int", nullable: false),
                    PatientInfoId = table.Column<int>(type: "int", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientRequests_OrganInfos_OrganInfoId",
                        column: x => x.OrganInfoId,
                        principalTable: "OrganInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientRequests_UserInfos_PatientInfoId",
                        column: x => x.PatientInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonorRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonorInfoId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganInfoId = table.Column<int>(type: "int", nullable: false),
                    PatientRequestId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransplantOrganId = table.Column<int>(type: "int", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorRequests_UserInfos_DonorInfoId",
                        column: x => x.DonorInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorRequests_OrganInfos_OrganInfoId",
                        column: x => x.OrganInfoId,
                        principalTable: "OrganInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorRequests_PatientRequests_PatientRequestId",
                        column: x => x.PatientRequestId,
                        principalTable: "PatientRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorRequests_TransplantOrgans_TransplantOrganId",
                        column: x => x.TransplantOrganId,
                        principalTable: "TransplantOrgans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonorMedicalExams_DonorRequestId",
                table: "DonorMedicalExams",
                column: "DonorRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorRequests_DonorInfoId",
                table: "DonorRequests",
                column: "DonorInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorRequests_OrganInfoId",
                table: "DonorRequests",
                column: "OrganInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorRequests_PatientRequestId",
                table: "DonorRequests",
                column: "PatientRequestId",
                unique: true,
                filter: "[PatientRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DonorRequests_TransplantOrganId",
                table: "DonorRequests",
                column: "TransplantOrganId",
                unique: true,
                filter: "[TransplantOrganId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRequests_DonorRequestId",
                table: "PatientRequests",
                column: "DonorRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRequests_OrganInfoId",
                table: "PatientRequests",
                column: "OrganInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRequests_PatientInfoId",
                table: "PatientRequests",
                column: "PatientInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorMedicalExams_DonorRequests_DonorRequestId",
                table: "DonorMedicalExams",
                column: "DonorRequestId",
                principalTable: "DonorRequests",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorMedicalExams_DonorRequests_DonorRequestId",
                table: "DonorMedicalExams");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorRequests_PatientRequests_PatientRequestId",
                table: "DonorRequests");

            migrationBuilder.DropTable(
                name: "PatientRequests");

            migrationBuilder.DropTable(
                name: "DonorRequests");

            migrationBuilder.DropIndex(
                name: "IX_DonorMedicalExams_DonorRequestId",
                table: "DonorMedicalExams");

            migrationBuilder.DropColumn(
                name: "DonorRequestId",
                table: "DonorMedicalExams");

            migrationBuilder.AddColumn<int>(
                name: "DonorOrganQueryId",
                table: "DonorMedicalExams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PatientOrganQueries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DonorOrganQueryId = table.Column<int>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    OrganInfoId = table.Column<int>(nullable: false),
                    PatientInfoId = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientOrganQueries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientOrganQueries_OrganInfos_OrganInfoId",
                        column: x => x.OrganInfoId,
                        principalTable: "OrganInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientOrganQueries_UserInfos_PatientInfoId",
                        column: x => x.PatientInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonorOrganQueries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DonorInfoId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    OrganInfoId = table.Column<int>(nullable: false),
                    PatientOrganQueryId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TransplantOrganId = table.Column<int>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorOrganQueries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonorOrganQueries_UserInfos_DonorInfoId",
                        column: x => x.DonorInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorOrganQueries_OrganInfos_OrganInfoId",
                        column: x => x.OrganInfoId,
                        principalTable: "OrganInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorOrganQueries_PatientOrganQueries_PatientOrganQueryId",
                        column: x => x.PatientOrganQueryId,
                        principalTable: "PatientOrganQueries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorOrganQueries_TransplantOrgans_TransplantOrganId",
                        column: x => x.TransplantOrganId,
                        principalTable: "TransplantOrgans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonorMedicalExams_DonorOrganQueryId",
                table: "DonorMedicalExams",
                column: "DonorOrganQueryId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorOrganQueries_DonorInfoId",
                table: "DonorOrganQueries",
                column: "DonorInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorOrganQueries_OrganInfoId",
                table: "DonorOrganQueries",
                column: "OrganInfoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_PatientOrganQueries_DonorOrganQueryId",
                table: "PatientOrganQueries",
                column: "DonorOrganQueryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientOrganQueries_OrganInfoId",
                table: "PatientOrganQueries",
                column: "OrganInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientOrganQueries_PatientInfoId",
                table: "PatientOrganQueries",
                column: "PatientInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorMedicalExams_DonorOrganQueries_DonorOrganQueryId",
                table: "DonorMedicalExams",
                column: "DonorOrganQueryId",
                principalTable: "DonorOrganQueries",
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
    }
}
