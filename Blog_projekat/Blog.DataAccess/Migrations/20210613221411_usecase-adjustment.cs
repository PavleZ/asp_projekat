using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataAccess.Migrations
{
    public partial class usecaseadjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_UseCases_UseCaseId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUseCases_UseCases_UseCaseId",
                table: "UserUseCases");

            migrationBuilder.DropTable(
                name: "UseCases");

            migrationBuilder.DropIndex(
                name: "IX_Logs_UseCaseId",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "UseCaseId",
                table: "Logs",
                newName: "UseCaseName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseCaseName",
                table: "Logs",
                newName: "UseCaseId");

            migrationBuilder.CreateTable(
                name: "UseCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseCases", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UseCaseId",
                table: "Logs",
                column: "UseCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UseCases_Name",
                table: "UseCases",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_UseCases_UseCaseId",
                table: "Logs",
                column: "UseCaseId",
                principalTable: "UseCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUseCases_UseCases_UseCaseId",
                table: "UserUseCases",
                column: "UseCaseId",
                principalTable: "UseCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
