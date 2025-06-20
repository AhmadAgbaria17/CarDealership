using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetbackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Person",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Person",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
