using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjectRootNamespace.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'101', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Deleted", "Description", "Name", "Price", "Stock", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #1", "Product #1", 52.260178781701335, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #73", "Product #73", 251.36914875189271, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #72", "Product #72", 68.280275354292371, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #71", "Product #71", 297.85257233905259, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #70", "Product #70", 55.594217924212209, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #69", "Product #69", 23.392898764178575, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #68", "Product #68", 63.972743727254098, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #67", "Product #67", 557.39487191168348, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #66", "Product #66", 362.41341957562298, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #65", "Product #65", 177.4676310566569, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #64", "Product #64", 5.2633875465315709, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #63", "Product #63", 186.02751743794769, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #62", "Product #62", 464.624694536731, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #61", "Product #61", 609.71257755240072, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #60", "Product #60", 494.4064075217612, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #59", "Product #59", 4.2750797468587196, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #58", "Product #58", 11.113275320321915, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #57", "Product #57", 244.20309430183988, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #56", "Product #56", 30.022567937626764, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #55", "Product #55", 181.27364554036112, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #54", "Product #54", 12.108975342991284, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #53", "Product #53", 715.15179954336577, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #74", "Product #74", 178.1001944179182, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #52", "Product #52", 217.09630021876484, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #75", "Product #75", 156.11078624898138, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #77", "Product #77", 280.11088218451982, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 98, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #98", "Product #98", 260.16160685576574, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 97, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #97", "Product #97", 56.859291980443196, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 96, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #96", "Product #96", 171.57096640512856, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #95", "Product #95", 597.93684613375774, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 94, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #94", "Product #94", 459.48041990188904, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 93, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #93", "Product #93", 69.719333040397302, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 92, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #92", "Product #92", 45.894778886760946, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 91, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #91", "Product #91", 21.731507765935508, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 90, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #90", "Product #90", 422.23627537267106, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 89, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #89", "Product #89", 92.797564564644162, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 88, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #88", "Product #88", 110.71632107520304, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 87, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #87", "Product #87", 576.49527616542548, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 86, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #86", "Product #86", 293.42840813259983, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 85, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #85", "Product #85", 105.88399931131117, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #84", "Product #84", 124.08046227883568, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 83, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #83", "Product #83", 119.02947764798508, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 82, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #82", "Product #82", 26.010980085474895, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 81, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #81", "Product #81", 123.73384353086996, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #80", "Product #80", 71.465091099713504, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 79, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #79", "Product #79", 238.59146274001873, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 78, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #78", "Product #78", 495.14041323407565, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 76, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #76", "Product #76", 206.55610127679822, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #51", "Product #51", 660.82413764336331, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #50", "Product #50", 19.755481030212476, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #49", "Product #49", 645.2805915499481, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #22", "Product #22", 30.964904068487186, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #21", "Product #21", 82.119740531835575, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #20", "Product #20", 73.152853944875687, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #19", "Product #19", 241.45489282508143, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #18", "Product #18", 551.0116507816183, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #17", "Product #17", 7.4212433450954229, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #16", "Product #16", 8.4488516554463899, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #15", "Product #15", 189.84868898887592, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #14", "Product #14", 12.065702267021733, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #13", "Product #13", 315.36200660297737, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #12", "Product #12", 442.63617874245915, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #11", "Product #11", 340.35718049963805, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #10", "Product #10", 74.980149698900121, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #9", "Product #9", 107.33120677775294, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #8", "Product #8", 581.07150105762832, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #7", "Product #7", 127.13465692900803, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #6", "Product #6", 437.53304026906056, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #5", "Product #5", 104.91185397604102, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #4", "Product #4", 242.57839392059407, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #3", "Product #3", 366.64043816860789, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #2", "Product #2", 366.65403055802642, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #23", "Product #23", 426.58989766919512, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #24", "Product #24", 26.38267374429045, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #25", "Product #25", 69.907074054194183, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #26", "Product #26", 39.618601412334762, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #48", "Product #48", 557.57392709030489, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #47", "Product #47", 250.00597851584013, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #46", "Product #46", 184.15474657581873, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #45", "Product #45", 79.140032678442097, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #44", "Product #44", 382.26134290092688, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #43", "Product #43", 296.50324009941107, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #42", "Product #42", 527.78464479827539, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #41", "Product #41", 7.0547414166176425, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #40", "Product #40", 233.05958755829352, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #39", "Product #39", 153.25932911795533, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 99, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #99", "Product #99", 564.88380528841344, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #38", "Product #38", 98.721300608814374, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #36", "Product #36", 215.25985731988209, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #35", "Product #35", 25.592236476294808, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #34", "Product #34", 583.55912169840144, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #33", "Product #33", 231.77403937269656, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #32", "Product #32", 277.18264882135327, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #31", "Product #31", 105.71184289348862, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #30", "Product #30", 54.759144207350516, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #29", "Product #29", 2.7212577065086263, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #28", "Product #28", 662.36586472409124, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #27", "Product #27", 417.99173386860252, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #37", "Product #37", 160.86357049730773, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #100", "Product #100", 683.60557012753827, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
