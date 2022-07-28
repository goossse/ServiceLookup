using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceLookup.DAL.Migrations
{
    public partial class EditReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "ReviewCriterias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewCriterias_ReviewId",
                table: "ReviewCriterias",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ConditionId",
                table: "Requests",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Conditions_ConditionId",
                table: "Requests",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewCriterias_Reviews_ReviewId",
                table: "ReviewCriterias",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Conditions_ConditionId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewCriterias_Reviews_ReviewId",
                table: "ReviewCriterias");

            migrationBuilder.DropIndex(
                name: "IX_ReviewCriterias_ReviewId",
                table: "ReviewCriterias");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ConditionId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "ReviewCriterias");
        }
    }
}
