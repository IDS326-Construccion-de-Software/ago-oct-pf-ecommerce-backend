using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentGatewayFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:orderstatus", "pending,processing,completed,cancelled")
                .Annotation("Npgsql:Enum:paymentstatus", "pending,completed,failed,refunded")
                .Annotation("Npgsql:Enum:typeCategory", "snacks,pastry,frozen,drinks,fish,meats,dairy,pets,babies,bakery,deliMeats")
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categories_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "paymentmethods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    metadata = table.Column<string>(type: "json", nullable: true),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("paymentmethods_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cellphone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: true),
                    directions = table.Column<string>(type: "json", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    numidentification = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    categoryid = table.Column<Guid>(type: "uuid", nullable: false),
                    brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("products_pkey", x => x.id);
                    table.ForeignKey(
                        name: "products_categoryid_fkey",
                        column: x => x.categoryid,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    addressid = table.Column<Guid>(type: "uuid", nullable: true),
                    total = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    placedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orders_pkey", x => x.id);
                    table.ForeignKey(
                        name: "orders_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shoppingcart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shoppingcart_pkey", x => x.id);
                    table.ForeignKey(
                        name: "shoppingcart_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productimages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    productid = table.Column<Guid>(type: "uuid", nullable: false, comment: "productId"),
                    url = table.Column<string>(type: "text", nullable: false),
                    isprimary = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("productimages_pkey", x => x.id);
                    table.ForeignKey(
                        name: "productimages_productid_fkey",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    orderid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    issuedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    total = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    tax = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValueSql: "0"),
                    url = table.Column<string>(type: "text", nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("invoices_pkey", x => x.id);
                    table.ForeignKey(
                        name: "invoices_orderid_fkey",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "invoices_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderitems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    orderid = table.Column<Guid>(type: "uuid", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: false),
                    unitprice = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orderitems_pkey", x => x.id);
                    table.ForeignKey(
                        name: "orderitems_orderid_fkey",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "orderitems_productid_fkey",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cartitems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    cartid = table.Column<Guid>(type: "uuid", nullable: false),
                    productid = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    addedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cartitems_pkey", x => x.id);
                    table.ForeignKey(
                        name: "cartitems_cartid_fkey",
                        column: x => x.cartid,
                        principalTable: "shoppingcart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "cartitems_productid_fkey",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    orderid = table.Column<Guid>(type: "uuid", nullable: true),
                    invoiceid = table.Column<Guid>(type: "uuid", nullable: true),
                    paymentmethodid = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    transactionreference = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AuthCode = table.Column<string>(type: "text", nullable: true),
                    ResponseCode = table.Column<string>(type: "text", nullable: true),
                    GatewayErrorMessage = table.Column<string>(type: "text", nullable: true),
                    PaymentStatus = table.Column<string>(type: "text", nullable: false),
                    paidat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payments_pkey", x => x.id);
                    table.ForeignKey(
                        name: "payments_invoiceid_fkey",
                        column: x => x.invoiceid,
                        principalTable: "invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "payments_orderid_fkey",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "payments_paymentmethodid_fkey",
                        column: x => x.paymentmethodid,
                        principalTable: "paymentmethods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "payments_userid_fkey",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cartitems_cartid",
                table: "cartitems",
                column: "cartid");

            migrationBuilder.CreateIndex(
                name: "IX_cartitems_productid",
                table: "cartitems",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_orderid",
                table: "invoices",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_userid",
                table: "invoices",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_orderid",
                table: "orderitems",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_productid",
                table: "orderitems",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_userid",
                table: "orders",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_payments_invoiceid",
                table: "payments",
                column: "invoiceid");

            migrationBuilder.CreateIndex(
                name: "IX_payments_orderid",
                table: "payments",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_payments_paymentmethodid",
                table: "payments",
                column: "paymentmethodid");

            migrationBuilder.CreateIndex(
                name: "IX_payments_userid",
                table: "payments",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_productimages_productid",
                table: "productimages",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_products_categoryid",
                table: "products",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingcart_userid",
                table: "shoppingcart",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "users_email_key",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartitems");

            migrationBuilder.DropTable(
                name: "orderitems");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "productimages");

            migrationBuilder.DropTable(
                name: "shoppingcart");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "paymentmethods");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
