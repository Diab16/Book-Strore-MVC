using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Book_Store_MVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Books_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Books_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BookOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Book_Id = table.Column<int>(type: "int", nullable: false),
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookOrder_Books_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BookOrder_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling" },
                    { 2, "George R.R. Martin" },
                    { 3, "J.R.R. Tolkien" },
                    { 4, "Agatha Christie" },
                    { 5, "Stephen King" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Fiction" },
                    { 3, "Mystery" },
                    { 4, "Horror" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Maple St", "alice@example.com", "Alice Johnson", "123-456-7890" },
                    { 2, "456 Oak St", "bob@example.com", "Bob Smith", "987-654-3210" }
                });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bloomsbury Publishing" },
                    { 2, "Bantam Books" },
                    { 3, "HarperCollins" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "AuthorName", "CategoryId", "DatePublished", "Description", "ImageUrl", "Language", "Price", "PublisherId", "PublisherName", "Title" },
                values: new object[,]
                {
                    { 1, 1, "J.K. Rowling", 1, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasy novel about a young wizard's journey.", "url_to_image_1", "English", 19.99f, 1, "Bloomsbury Publishing", "Harry Potter and the Philosopher's Stone" },
                    { 2, 2, "George R.R. Martin", 1, new DateTime(1996, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "First book in the epic fantasy series A Song of Ice and Fire.", "url_to_image_2", "English", 29.99f, 2, "Bantam Books", "A Game of Thrones" },
                    { 3, 3, "J.R.R. Tolkien", 1, new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "A fantasy novel and prequel to The Lord of the Rings.", "url_to_image_3", "English", 14.99f, 3, "HarperCollins", "The Hobbit" },
                    { 4, 4, "Agatha Christie", 3, new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A mystery novel featuring detective Hercule Poirot.", "url_to_image_4", "English", 12.99f, 1, "Bloomsbury Publishing", "Murder on the Orient Express" },
                    { 5, 5, "Stephen King", 4, new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "A horror novel about a haunted hotel.", "url_to_image_5", "English", 15.99f, 2, "Bantam Books", "The Shining" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 8, 15, 41, 21, 723, DateTimeKind.Local).AddTicks(2173) },
                    { 2, 2, new DateTime(2024, 8, 8, 15, 41, 21, 723, DateTimeKind.Local).AddTicks(2221) }
                });

            migrationBuilder.InsertData(
                table: "BookOrder",
                columns: new[] { "Id", "Book_Id", "Order_Id", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 2, 1, 1 },
                    { 3, 3, 2, 1 },
                    { 4, 4, 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookOrder_Book_Id",
                table: "BookOrder",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrder_Order_Id",
                table: "BookOrder",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookOrder");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
