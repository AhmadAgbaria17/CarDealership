using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetbackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCarDealerShipsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Person_PersonId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Car",
                newName: "Year");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_PersonId",
                table: "Car",
                newName: "IX_Car_PersonId");

            migrationBuilder.AddColumn<int>(
                name: "CarDealerShipId",
                table: "Car",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Engine",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fuel",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HorsePower",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mileage",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModelName",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CarDealerShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDealerShips", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarDealerShipId",
                table: "Car",
                column: "CarDealerShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarDealerShips_CarDealerShipId",
                table: "Car",
                column: "CarDealerShipId",
                principalTable: "CarDealerShips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Person_PersonId",
                table: "Car",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarDealerShips_CarDealerShipId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Person_PersonId",
                table: "Car");

            migrationBuilder.DropTable(
                name: "CarDealerShips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_CarDealerShipId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "CarDealerShipId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Engine",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Fuel",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "HorsePower",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "ModelName",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Cars",
                newName: "Model");

            migrationBuilder.RenameIndex(
                name: "IX_Car_PersonId",
                table: "Cars",
                newName: "IX_Cars_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Person_PersonId",
                table: "Cars",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
