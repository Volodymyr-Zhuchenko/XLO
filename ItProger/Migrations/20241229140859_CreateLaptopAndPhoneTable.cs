using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItProger.Migrations
{
    /// <inheritdoc />
    public partial class CreateLaptopAndPhoneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SrcImage",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SrcImage",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SrcImage",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "SrcImage",
                table: "Laptops");
        }
    }
}
