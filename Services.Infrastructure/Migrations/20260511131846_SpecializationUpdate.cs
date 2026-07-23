using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SpecializationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SpecializationId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Services_SpecializationId",
                table: "Services",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Specializations_SpecializationId",
                table: "Services",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Specializations_SpecializationId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_SpecializationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Services");
        }
    }
}
