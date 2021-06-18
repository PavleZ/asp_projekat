using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataAccess.Migrations
{
    public partial class ratingadjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostRatings_Ratings_RatingId",
                table: "PostRatings");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostRatings",
                table: "PostRatings");

            migrationBuilder.DropIndex(
                name: "IX_PostRatings_RatingId",
                table: "PostRatings");

            migrationBuilder.RenameColumn(
                name: "RatingId",
                table: "PostRatings",
                newName: "Rating");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostRatings",
                table: "PostRatings",
                columns: new[] { "PostId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostRatings",
                table: "PostRatings");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "PostRatings",
                newName: "RatingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostRatings",
                table: "PostRatings",
                columns: new[] { "PostId", "UserId", "RatingId" });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostRatings_RatingId",
                table: "PostRatings",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostRatings_Ratings_RatingId",
                table: "PostRatings",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
