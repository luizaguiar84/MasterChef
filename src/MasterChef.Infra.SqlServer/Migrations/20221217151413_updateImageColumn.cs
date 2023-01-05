using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterChef.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class updateImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Recipes",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Recipes",
                newName: "Picture");
        }
    }
}
