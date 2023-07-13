using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class sasasa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sorular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KonuId = table.Column<int>(type: "int", nullable: false),
                    SoruText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CevapA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CevapB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CevapC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CevapD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CevapE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogruCevap = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sorular_Konular_KonuId",
                        column: x => x.KonuId,
                        principalTable: "Konular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sorular_KonuId",
                table: "Sorular",
                column: "KonuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Sorular");
        }
    }
}
