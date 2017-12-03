using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DataLayer.Migrations
{
    public partial class OrganDeliveryInfo_RemoveUselessFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganDeliveryInfos_UserInfos_DonorId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganDeliveryInfos_Clinics_FromClinicId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganDeliveryInfos_UserInfos_PatientId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganDeliveryInfos_Clinics_ToClinicId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_OrganDeliveryInfos_DonorId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_OrganDeliveryInfos_FromClinicId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_OrganDeliveryInfos_PatientId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_OrganDeliveryInfos_ToClinicId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropColumn(
                name: "FromClinicId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropColumn(
                name: "StartTransportTime",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrganDeliveryInfos");

            migrationBuilder.DropColumn(
                name: "ToClinicId",
                table: "OrganDeliveryInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonorId",
                table: "OrganDeliveryInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FromClinicId",
                table: "OrganDeliveryInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "OrganDeliveryInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTransportTime",
                table: "OrganDeliveryInfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrganDeliveryInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToClinicId",
                table: "OrganDeliveryInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrganDeliveryInfos_DonorId",
                table: "OrganDeliveryInfos",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganDeliveryInfos_FromClinicId",
                table: "OrganDeliveryInfos",
                column: "FromClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganDeliveryInfos_PatientId",
                table: "OrganDeliveryInfos",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganDeliveryInfos_ToClinicId",
                table: "OrganDeliveryInfos",
                column: "ToClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganDeliveryInfos_UserInfos_DonorId",
                table: "OrganDeliveryInfos",
                column: "DonorId",
                principalTable: "UserInfos",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganDeliveryInfos_Clinics_FromClinicId",
                table: "OrganDeliveryInfos",
                column: "FromClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganDeliveryInfos_UserInfos_PatientId",
                table: "OrganDeliveryInfos",
                column: "PatientId",
                principalTable: "UserInfos",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganDeliveryInfos_Clinics_ToClinicId",
                table: "OrganDeliveryInfos",
                column: "ToClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
