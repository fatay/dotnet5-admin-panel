using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammersBlog.Data.Migrations
{
    public partial class CreateInitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(480)", maxLength: 480, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(480)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(480)", maxLength: 480, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuthor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 225, DateTimeKind.Local).AddTicks(7730), "C# Programlama Dili İle İlgili En Güncel Bilgiler", true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 225, DateTimeKind.Local).AddTicks(7745), "C#", "C# Blog Kategorisi" },
                    { 2, "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 225, DateTimeKind.Local).AddTicks(7765), "C++ Programlama Dili İle İlgili En Güncel Bilgiler", true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 225, DateTimeKind.Local).AddTicks(7766), "C++", "C++ Blog Kategorisi" },
                    { 3, "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 225, DateTimeKind.Local).AddTicks(7771), "JavaScript Programlama Dili İle İlgili En Güncel Bilgiler", true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 225, DateTimeKind.Local).AddTicks(7773), "JavaScript", "JavaScript Blog Kategorisi" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[] { 1, "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 232, DateTimeKind.Local).AddTicks(353), "Admin Rolü, tüm yetkilere sahiptir.", true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 232, DateTimeKind.Local).AddTicks(526), "Admin", "Admin Rolüdür." });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedByName", "ModifiedDate", "Note", "PasswordHash", "Picture", "RoleId", "Username" },
                values: new object[] { 1, "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 251, DateTimeKind.Local).AddTicks(9411), "Admin Kullanıcısı", "fataycomputers@gmail.com", "Fatih", true, false, "Aydin", "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 251, DateTimeKind.Local).AddTicks(9429), "Admin kullanıcısıdır.", new byte[] { 48, 49, 57, 50, 48, 50, 51, 97, 55, 98, 98, 100, 55, 51, 50, 53, 48, 53, 49, 54, 102, 48, 54, 57, 100, 102, 49, 56, 98, 53, 48, 48 }, "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU", 1, "fatay" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "CommentId", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 1, 1, 1, 0, " Lorem ipsum dolor sit amet, consectetur adipiscing elit. \r\n                             Suspendisse sagittis blandit faucibus. Praesent mollis posuere vulputate. \r\n                             Pellentesque mollis risus varius nisl facilisis venenatis. \r\n                             Sed varius ante lorem, tristique varius mauris mollis eget. \r\n                             Ut dictum velit ut iaculis placerat. Donec dignissim tortor non orci dapibus lobortis. \r\n                             Proin vitae convallis metus, ut fermentum lacus. Etiam condimentum tristique finibus. \r\n                             Fusce convallis, ligula eget cursus imperdiet, turpis ante iaculis turpis, at fermentum purus risus non ante. \r\n                             Nunc sagittis nulla mattis metus interdum tempor.", "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 220, DateTimeKind.Local).AddTicks(8651), new DateTime(2021, 5, 30, 13, 34, 47, 220, DateTimeKind.Local).AddTicks(5774), true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 221, DateTimeKind.Local).AddTicks(78), "C# 9.0 ve .NET5 Yenilikleri", "Fatih Aydin", "C# 9.0 ve .NET5 Yenilikleri", "C#, C# 9.0, .NET5, .NET Core, .NET Framework", "default.jpg", "C# 9.0 ve .NET5 Yenilikleri", 1, 80 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "CommentId", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 2, 2, 1, 0, " Lorem ipsum dolor sit amet, consectetur adipiscing elit. \r\n                             Suspendisse sagittis blandit faucibus. Praesent mollis posuere vulputate. \r\n                             Pellentesque mollis risus varius nisl facilisis venenatis. \r\n                             Sed varius ante lorem, tristique varius mauris mollis eget. \r\n                             Ut dictum velit ut iaculis placerat. Donec dignissim tortor non orci dapibus lobortis. \r\n                             Proin vitae convallis metus, ut fermentum lacus. Etiam condimentum tristique finibus. \r\n                             Fusce convallis, ligula eget cursus imperdiet, turpis ante iaculis turpis, at fermentum purus risus non ante. \r\n                             Nunc sagittis nulla mattis metus interdum tempor.", "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 221, DateTimeKind.Local).AddTicks(2881), new DateTime(2021, 5, 30, 13, 34, 47, 221, DateTimeKind.Local).AddTicks(2879), true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 221, DateTimeKind.Local).AddTicks(2882), "C++ 11 ve 19 Yenilikleri", "Fatih Aydin", "C++ 11 ve 19 Yenilikleri", "C++, Object Oriented Programming, C++ 11, QT", "default.jpg", "C++ 11 ve 19 Yenilikleri", 1, 100 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "CommentId", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 3, 3, 1, 0, " Lorem ipsum dolor sit amet, consectetur adipiscing elit. \r\n                             Suspendisse sagittis blandit faucibus. Praesent mollis posuere vulputate. \r\n                             Pellentesque mollis risus varius nisl facilisis venenatis. \r\n                             Sed varius ante lorem, tristique varius mauris mollis eget. \r\n                             Ut dictum velit ut iaculis placerat. Donec dignissim tortor non orci dapibus lobortis. \r\n                             Proin vitae convallis metus, ut fermentum lacus. Etiam condimentum tristique finibus. \r\n                             Fusce convallis, ligula eget cursus imperdiet, turpis ante iaculis turpis, at fermentum purus risus non ante. \r\n                             Nunc sagittis nulla mattis metus interdum tempor.", "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 221, DateTimeKind.Local).AddTicks(2890), new DateTime(2021, 5, 30, 13, 34, 47, 221, DateTimeKind.Local).AddTicks(2888), true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 221, DateTimeKind.Local).AddTicks(2892), "ECMASCRIPT ve JavaScript ES6", "Fatih Aydin", "ECMASCRIPT ve JavaScript ES6", "JavScript, ES6, ECMAScript", "default.jpg", "JavaScipt ES6 Nedir?", 1, 2940 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 1, 1, "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 228, DateTimeKind.Local).AddTicks(8748), true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 228, DateTimeKind.Local).AddTicks(8768), "C# Makale Yorumu", "Yorum satırı - 1" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 2, 2, "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 228, DateTimeKind.Local).AddTicks(8785), true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 228, DateTimeKind.Local).AddTicks(8787), "C++ Makale Yorumu", "Yorum satırı - 2" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[] { 3, 3, "InitialCreate", new DateTime(2021, 5, 30, 13, 34, 47, 228, DateTimeKind.Local).AddTicks(8793), true, false, "InitialModify", new DateTime(2021, 5, 30, 13, 34, 47, 228, DateTimeKind.Local).AddTicks(8795), "JavaScript Makale Yorumu", "Yorum satırı - 3" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
