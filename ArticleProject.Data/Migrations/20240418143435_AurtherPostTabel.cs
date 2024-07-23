using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AurtherPostTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autherPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutherId = table.Column<int>(type: "int", nullable: false),
                    AuthoresId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autherPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_autherPost_Authores_AuthoresId",
                        column: x => x.AuthoresId,
                        principalTable: "Authores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_autherPost_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_autherPost_AuthoresId",
                table: "autherPost",
                column: "AuthoresId");

            migrationBuilder.CreateIndex(
                name: "IX_autherPost_CategoryId",
                table: "autherPost",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "autherPost");
        }
    }
}
