using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class OrganDeliveryInfo_RemoveEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganDataSnapshots_OrganDeliveryInfos_OrganDeliveryId",
                table: "OrganDataSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_TransplantOrgans_OrganDeliveryInfos_OrganDeliveryInfoId",
                table: "TransplantOrgans");

            migrationBuilder.DropTable(
                name: "OrganDeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_TransplantOrgans_OrganDeliveryInfoId",
                table: "TransplantOrgans");

            migrationBuilder.DropIndex(
                name: "IX_OrganDataSnapshots_OrganDeliveryId",
                table: "OrganDataSnapshots");

            migrationBuilder.DropColumn(
                name: "OrganDeliveryInfoId",
                table: "TransplantOrgans");

            migrationBuilder.DropColumn(
                name: "OrganDeliveryId",
                table: "OrganDataSnapshots");

            migrationBuilder.AddColumn<int>(
                name: "TransplantOrganId",
                table: "OrganDataSnapshots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrganDataSnapshots_TransplantOrganId",
                table: "OrganDataSnapshots",
                column: "TransplantOrganId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganDataSnapshots_TransplantOrgans_TransplantOrganId",
                table: "OrganDataSnapshots",
                column: "TransplantOrganId",
                principalTable: "TransplantOrgans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganDataSnapshots_TransplantOrgans_TransplantOrganId",
                table: "OrganDataSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_OrganDataSnapshots_TransplantOrganId",
                table: "OrganDataSnapshots");

            migrationBuilder.DropColumn(
                name: "TransplantOrganId",
                table: "OrganDataSnapshots");

            migrationBuilder.AddColumn<int>(
                name: "OrganDeliveryInfoId",
                table: "TransplantOrgans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganDeliveryId",
                table: "OrganDataSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrganDeliveryInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    TransplantOrganId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganDeliveryInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransplantOrgans_OrganDeliveryInfoId",
                table: "TransplantOrgans",
                column: "OrganDeliveryInfoId",
                unique: true,
                filter: "[OrganDeliveryInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrganDataSnapshots_OrganDeliveryId",
                table: "OrganDataSnapshots",
                column: "OrganDeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganDataSnapshots_OrganDeliveryInfos_OrganDeliveryId",
                table: "OrganDataSnapshots",
                column: "OrganDeliveryId",
                principalTable: "OrganDeliveryInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransplantOrgans_OrganDeliveryInfos_OrganDeliveryInfoId",
                table: "TransplantOrgans",
                column: "OrganDeliveryInfoId",
                principalTable: "OrganDeliveryInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
