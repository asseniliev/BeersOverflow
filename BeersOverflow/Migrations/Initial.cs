using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreDemo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abv = table.Column<double>(type: "float", nullable: false),
                    StyleId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beers_Styles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Styles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beers_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeerId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rating_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Styles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Special Ale" },
                    { 2, "English Porter" },
                    { 3, "Indian Pale Ale" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsAdmin", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { 1, null, null, true, null, null, "admin" },
                    { 2, null, null, false, null, null, "gosho" },
                    { 3, null, null, false, null, null, "pesho" }
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Abv", "CreatedById", "Name", "StyleId" },
                values: new object[] { 1, 4.5999999999999996, 2, "Glarus English Ale", 1 });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Abv", "CreatedById", "Name", "StyleId" },
                values: new object[] { 2, 5.0, 2, "Rhombus Porter", 2 });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Abv", "CreatedById", "Name", "StyleId" },
                values: new object[] { 3, 6.5999999999999996, 3, "Opasen Char", 3 });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "Id", "BeerId", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 3, 5 },
                    { 2, 1, 2, 2 },
                    { 3, 2, 3, 1 },
                    { 4, 2, 2, 3 },
                    { 5, 3, 3, 5 },
                    { 6, 3, 2, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_CreatedById",
                table: "Beers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Beers_StyleId",
                table: "Beers",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_BeerId",
                table: "Rating",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_UserId",
                table: "Rating",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropTable(
                name: "Styles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
