using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenge.Data.Migrations
{
    /// <inheritdoc />
    public partial class SyncWithTeamChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "productimages_productid_fkey",
                table: "productimages");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "numidentification",
                table: "users",
                newName: "NumIdentification");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "directions",
                table: "users",
                newName: "Directions");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "cellphone",
                table: "users",
                newName: "Cellphone");

            migrationBuilder.RenameColumn(
                name: "birthdate",
                table: "users",
                newName: "Birthdate");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "shoppingcart",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "shoppingcart",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "shoppingcart",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "shoppingcart",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingcart_userid",
                table: "shoppingcart",
                newName: "IX_shoppingcart_UserId");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "products",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "products",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "categoryid",
                table: "products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "brand",
                table: "products",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_products_categoryid",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "productimages",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "productimages",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "order",
                table: "productimages",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "isprimary",
                table: "productimages",
                newName: "IsPrimary");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "productimages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_productimages_productid",
                table: "productimages",
                newName: "IX_productimages_ProductId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "payments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "payments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "transactionreference",
                table: "payments",
                newName: "TransactionReference");

            migrationBuilder.RenameColumn(
                name: "paymentmethodid",
                table: "payments",
                newName: "PaymentMethodId");

            migrationBuilder.RenameColumn(
                name: "paidat",
                table: "payments",
                newName: "PaidAt");

            migrationBuilder.RenameColumn(
                name: "orderid",
                table: "payments",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "invoiceid",
                table: "payments",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "payments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "payments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "payments",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_payments_userid",
                table: "payments",
                newName: "IX_payments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_paymentmethodid",
                table: "payments",
                newName: "IX_payments_PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_orderid",
                table: "payments",
                newName: "IX_payments_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_invoiceid",
                table: "payments",
                newName: "IX_payments_InvoiceId");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "paymentmethods",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "provider",
                table: "paymentmethods",
                newName: "Provider");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "paymentmethods",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "metadata",
                table: "paymentmethods",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "paymentmethods",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "orders",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "orders",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "placedat",
                table: "orders",
                newName: "PlacedAt");

            migrationBuilder.RenameColumn(
                name: "addressid",
                table: "orders",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "orders",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_userid",
                table: "orders",
                newName: "IX_orders_UserId");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "orderitems",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "unitprice",
                table: "orderitems",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "subtotal",
                table: "orderitems",
                newName: "Subtotal");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "orderitems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "orderitems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "orderid",
                table: "orderitems",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "orderitems",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_orderitems_productid",
                table: "orderitems",
                newName: "IX_orderitems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_orderitems_orderid",
                table: "orderitems",
                newName: "IX_orderitems_OrderId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "invoices",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "invoices",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "invoices",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "invoices",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "tax",
                table: "invoices",
                newName: "Tax");

            migrationBuilder.RenameColumn(
                name: "orderid",
                table: "invoices",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "issuedat",
                table: "invoices",
                newName: "IssuedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "invoices",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_invoices_userid",
                table: "invoices",
                newName: "IX_invoices_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_invoices_orderid",
                table: "invoices",
                newName: "IX_invoices_OrderId");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "categories",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "categories",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "cartitems",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "cartitems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "cartitems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "cartid",
                table: "cartitems",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "addedat",
                table: "cartitems",
                newName: "AddedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "cartitems",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_cartitems_productid",
                table: "cartitems",
                newName: "IX_cartitems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_cartitems_cartid",
                table: "cartitems",
                newName: "IX_cartitems_CartId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "users",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "users",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "shoppingcart",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "shoppingcart",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "products",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "products",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "productimages",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "productId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "payments",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaidAt",
                table: "payments",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "payments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "paymentmethods",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "orders",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PlacedAt",
                table: "orders",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "orderitems",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "invoices",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssuedAt",
                table: "invoices",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "categories",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "categories",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "cartitems",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedAt",
                table: "cartitems",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AddForeignKey(
                name: "productimages_productid_fkey",
                table: "productimages",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "productimages_productid_fkey",
                table: "productimages");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "NumIdentification",
                table: "users",
                newName: "numidentification");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Directions",
                table: "users",
                newName: "directions");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Cellphone",
                table: "users",
                newName: "cellphone");

            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "users",
                newName: "birthdate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "shoppingcart",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "shoppingcart",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "shoppingcart",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "shoppingcart",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingcart_UserId",
                table: "shoppingcart",
                newName: "IX_shoppingcart_userid");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "products",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "products",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products",
                newName: "categoryid");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "products",
                newName: "brand");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "products",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "products",
                newName: "IX_products_categoryid");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "productimages",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "productimages",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "productimages",
                newName: "order");

            migrationBuilder.RenameColumn(
                name: "IsPrimary",
                table: "productimages",
                newName: "isprimary");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "productimages",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_productimages_ProductId",
                table: "productimages",
                newName: "IX_productimages_productid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "payments",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "payments",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "TransactionReference",
                table: "payments",
                newName: "transactionreference");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "payments",
                newName: "paymentmethodid");

            migrationBuilder.RenameColumn(
                name: "PaidAt",
                table: "payments",
                newName: "paidat");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "payments",
                newName: "orderid");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "payments",
                newName: "invoiceid");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "payments",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "payments",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "payments",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_payments_UserId",
                table: "payments",
                newName: "IX_payments_userid");

            migrationBuilder.RenameIndex(
                name: "IX_payments_PaymentMethodId",
                table: "payments",
                newName: "IX_payments_paymentmethodid");

            migrationBuilder.RenameIndex(
                name: "IX_payments_OrderId",
                table: "payments",
                newName: "IX_payments_orderid");

            migrationBuilder.RenameIndex(
                name: "IX_payments_InvoiceId",
                table: "payments",
                newName: "IX_payments_invoiceid");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "paymentmethods",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "Provider",
                table: "paymentmethods",
                newName: "provider");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "paymentmethods",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "paymentmethods",
                newName: "metadata");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "paymentmethods",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "orders",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "orders",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "orders",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "PlacedAt",
                table: "orders",
                newName: "placedat");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "orders",
                newName: "addressid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orders",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_UserId",
                table: "orders",
                newName: "IX_orders_userid");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "orderitems",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "orderitems",
                newName: "unitprice");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "orderitems",
                newName: "subtotal");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "orderitems",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "orderitems",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "orderitems",
                newName: "orderid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orderitems",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_orderitems_ProductId",
                table: "orderitems",
                newName: "IX_orderitems_productid");

            migrationBuilder.RenameIndex(
                name: "IX_orderitems_OrderId",
                table: "orderitems",
                newName: "IX_orderitems_orderid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "invoices",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "invoices",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "invoices",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "invoices",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "Tax",
                table: "invoices",
                newName: "tax");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "invoices",
                newName: "orderid");

            migrationBuilder.RenameColumn(
                name: "IssuedAt",
                table: "invoices",
                newName: "issuedat");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "invoices",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_invoices_UserId",
                table: "invoices",
                newName: "IX_invoices_userid");

            migrationBuilder.RenameIndex(
                name: "IX_invoices_OrderId",
                table: "invoices",
                newName: "IX_invoices_orderid");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "categories",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "categories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "categories",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "cartitems",
                newName: "updatedat");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "cartitems",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "cartitems",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "cartitems",
                newName: "cartid");

            migrationBuilder.RenameColumn(
                name: "AddedAt",
                table: "cartitems",
                newName: "addedat");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cartitems",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_cartitems_ProductId",
                table: "cartitems",
                newName: "IX_cartitems_productid");

            migrationBuilder.RenameIndex(
                name: "IX_cartitems_CartId",
                table: "cartitems",
                newName: "IX_cartitems_cartid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "shoppingcart",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "shoppingcart",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "products",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<Guid>(
                name: "productid",
                table: "productimages",
                type: "uuid",
                nullable: false,
                comment: "productId",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "payments",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "paidat",
                table: "payments",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "payments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "paymentmethods",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "placedat",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "orderitems",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "invoices",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "issuedat",
                table: "invoices",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedat",
                table: "cartitems",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "addedat",
                table: "cartitems",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AddForeignKey(
                name: "productimages_productid_fkey",
                table: "productimages",
                column: "productid",
                principalTable: "products",
                principalColumn: "id");
        }
    }
}
