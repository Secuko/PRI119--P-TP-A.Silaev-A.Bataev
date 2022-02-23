using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class OperationStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_SearchRequests_SearchRequestId",
                table: "Operations");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SearchRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SearchRequestId",
                table: "Operations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_SearchRequests_SearchRequestId",
                table: "Operations",
                column: "SearchRequestId",
                principalTable: "SearchRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_SearchRequests_SearchRequestId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SearchRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "SearchRequestId",
                table: "Operations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_SearchRequests_SearchRequestId",
                table: "Operations",
                column: "SearchRequestId",
                principalTable: "SearchRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
