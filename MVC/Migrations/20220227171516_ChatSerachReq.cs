using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class ChatSerachReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SearchRequestId",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SearchRequestId",
                table: "Chats",
                column: "SearchRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_SearchRequests_SearchRequestId",
                table: "Chats",
                column: "SearchRequestId",
                principalTable: "SearchRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_SearchRequests_SearchRequestId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_SearchRequestId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SearchRequestId",
                table: "Chats");
        }
    }
}
