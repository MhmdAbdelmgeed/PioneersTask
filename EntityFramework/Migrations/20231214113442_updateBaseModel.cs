using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class updateBaseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleterUserId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Goods",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleterUserId",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Goods",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Goods",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Goods");
        }
    }
}
