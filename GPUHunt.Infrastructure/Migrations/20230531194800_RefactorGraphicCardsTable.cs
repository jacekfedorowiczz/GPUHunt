using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPUHunt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorGraphicCardsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LowestPriceStore",
                table: "GraphicCards",
                newName: "LowestPriceStore_Name");

            migrationBuilder.RenameColumn(
                name: "HighestPriceStore",
                table: "GraphicCards",
                newName: "HighestPriceStore_Name");

            migrationBuilder.AddColumn<int>(
                name: "HighestPriceStore_Id",
                table: "GraphicCards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LowestPriceStore_Id",
                table: "GraphicCards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighestPriceStore_Id",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "LowestPriceStore_Id",
                table: "GraphicCards");

            migrationBuilder.RenameColumn(
                name: "LowestPriceStore_Name",
                table: "GraphicCards",
                newName: "LowestPriceStore");

            migrationBuilder.RenameColumn(
                name: "HighestPriceStore_Name",
                table: "GraphicCards",
                newName: "HighestPriceStore");
        }
    }
}
