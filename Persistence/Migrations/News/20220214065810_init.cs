using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.News
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ShowInMainPage = table.Column<bool>(type: "bit", nullable: false),
                    HeadTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SummaryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsType = table.Column<int>(type: "int", nullable: false),
                    NewsPriority = table.Column<int>(type: "int", nullable: false),
                    PublishedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoNews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowInMainPage = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PublishedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoNews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoNews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ShowInMainPage = table.Column<bool>(type: "bit", nullable: false),
                    PublishedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoNews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsAttachment_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsNewsCategory",
                columns: table => new
                {
                    NewsCategoriesId = table.Column<int>(type: "int", nullable: false),
                    NewsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsNewsCategory", x => new { x.NewsCategoriesId, x.NewsId });
                    table.ForeignKey(
                        name: "FK_NewsNewsCategory_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsNewsCategory_NewsCategory_NewsCategoriesId",
                        column: x => x.NewsCategoriesId,
                        principalTable: "NewsCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsCategoryPhotoNews",
                columns: table => new
                {
                    NewsCategoriesId = table.Column<int>(type: "int", nullable: false),
                    PhotoNewsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategoryPhotoNews", x => new { x.NewsCategoriesId, x.PhotoNewsId });
                    table.ForeignKey(
                        name: "FK_NewsCategoryPhotoNews_NewsCategory_NewsCategoriesId",
                        column: x => x.NewsCategoriesId,
                        principalTable: "NewsCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsCategoryPhotoNews_PhotoNews_PhotoNewsId",
                        column: x => x.PhotoNewsId,
                        principalTable: "PhotoNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoNewsAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoNewsId = table.Column<int>(type: "int", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoNewsAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoNewsAttachment_PhotoNews_PhotoNewsId",
                        column: x => x.PhotoNewsId,
                        principalTable: "PhotoNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsCategoryVideoNews",
                columns: table => new
                {
                    NewsCategoriesId = table.Column<int>(type: "int", nullable: false),
                    VideoNewsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategoryVideoNews", x => new { x.NewsCategoriesId, x.VideoNewsId });
                    table.ForeignKey(
                        name: "FK_NewsCategoryVideoNews_NewsCategory_NewsCategoriesId",
                        column: x => x.NewsCategoriesId,
                        principalTable: "NewsCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsCategoryVideoNews_VideoNews_VideoNewsId",
                        column: x => x.VideoNewsId,
                        principalTable: "VideoNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoNewsAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoNewsId = table.Column<int>(type: "int", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoNewsAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoNewsAttachment_VideoNews_VideoNewsId",
                        column: x => x.VideoNewsId,
                        principalTable: "VideoNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsAttachment_NewsId",
                table: "NewsAttachment",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategoryPhotoNews_PhotoNewsId",
                table: "NewsCategoryPhotoNews",
                column: "PhotoNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategoryVideoNews_VideoNewsId",
                table: "NewsCategoryVideoNews",
                column: "VideoNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsNewsCategory_NewsId",
                table: "NewsNewsCategory",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoNewsAttachment_PhotoNewsId",
                table: "PhotoNewsAttachment",
                column: "PhotoNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoNewsAttachment_VideoNewsId",
                table: "VideoNewsAttachment",
                column: "VideoNewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsAttachment");

            migrationBuilder.DropTable(
                name: "NewsCategoryPhotoNews");

            migrationBuilder.DropTable(
                name: "NewsCategoryVideoNews");

            migrationBuilder.DropTable(
                name: "NewsNewsCategory");

            migrationBuilder.DropTable(
                name: "PhotoNewsAttachment");

            migrationBuilder.DropTable(
                name: "VideoNewsAttachment");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "NewsCategory");

            migrationBuilder.DropTable(
                name: "PhotoNews");

            migrationBuilder.DropTable(
                name: "VideoNews");
        }
    }
}
