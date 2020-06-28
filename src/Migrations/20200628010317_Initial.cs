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
                    { 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #1", "Product #1", 52.31045976854417, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #73", "Product #73", 36.238745672739455, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #72", "Product #72", 522.50120110926275, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #71", "Product #71", 741.46677750789877, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #70", "Product #70", 458.40247740801544, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #69", "Product #69", 446.98579801385557, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #68", "Product #68", 121.92822031766559, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #67", "Product #67", 290.0254012225314, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #66", "Product #66", 504.32282781895384, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #65", "Product #65", 270.40230794223135, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #64", "Product #64", 646.30490008103891, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #63", "Product #63", 556.39885795647228, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #62", "Product #62", 20.596080674136097, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #61", "Product #61", 17.665726327181666, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #60", "Product #60", 186.29129857490366, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #59", "Product #59", 135.09805496134703, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #58", "Product #58", 855.8960652881749, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #57", "Product #57", 11.693403774729653, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #56", "Product #56", 419.6294949267197, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #55", "Product #55", 25.587433495366682, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #54", "Product #54", 69.639896494168738, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #53", "Product #53", 51.645723330623348, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #74", "Product #74", 25.113644327555619, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #52", "Product #52", 626.4176101137034, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #75", "Product #75", 13.348695254581372, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #77", "Product #77", 128.0444901455401, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 98, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #98", "Product #98", 688.28242574738454, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 97, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #97", "Product #97", 179.80706792036401, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 96, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #96", "Product #96", 317.89037571609504, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #95", "Product #95", 16.004112472759612, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 94, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #94", "Product #94", 269.99275828245692, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 93, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #93", "Product #93", 113.0488448166516, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 92, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #92", "Product #92", 368.60046606911368, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 91, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #91", "Product #91", 701.05762502600328, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 90, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #90", "Product #90", 623.96912414858548, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 89, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #89", "Product #89", 327.9548068232624, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 88, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #88", "Product #88", 396.66991489784317, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 87, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #87", "Product #87", 139.91334086373138, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 86, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #86", "Product #86", 454.18519778092633, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 85, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #85", "Product #85", 34.460516546136006, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #84", "Product #84", 440.64551864967007, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 83, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #83", "Product #83", 429.32941727960923, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 82, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #82", "Product #82", 34.578907962226729, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 81, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #81", "Product #81", 610.01264599431897, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #80", "Product #80", 210.13741056441208, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 79, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #79", "Product #79", 97.291592842569386, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 78, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #78", "Product #78", 18.247000979374629, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 76, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #76", "Product #76", 233.35514603804572, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #51", "Product #51", 537.38406230387466, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #50", "Product #50", 30.423130761097713, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #49", "Product #49", 25.08946658255973, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #22", "Product #22", 120.94498580272543, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #21", "Product #21", 79.961964733880933, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #20", "Product #20", 532.60467138448951, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #19", "Product #19", 418.87005629896652, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #18", "Product #18", 145.12832783960195, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #17", "Product #17", 192.06622867475554, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #16", "Product #16", 81.154866048206983, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #15", "Product #15", 207.76785511372978, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #14", "Product #14", 82.796750278583147, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #13", "Product #13", 154.61785065085527, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #12", "Product #12", 934.49188045528331, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #11", "Product #11", 79.666317035754361, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #10", "Product #10", 489.81546770679557, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #9", "Product #9", 500.12135413480985, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #8", "Product #8", 77.92570708502349, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #7", "Product #7", 707.06673254587997, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #6", "Product #6", 33.741850157148136, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #5", "Product #5", 779.95758779065568, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #4", "Product #4", 32.107147961904829, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #3", "Product #3", 288.57385102779318, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #2", "Product #2", 134.13288418954838, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #23", "Product #23", 744.05855924126627, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #24", "Product #24", 125.81836507041864, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #25", "Product #25", 448.92609555689904, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #26", "Product #26", 177.24312174936904, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #48", "Product #48", 260.40264800861604, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #47", "Product #47", 124.86475204576961, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #46", "Product #46", 413.69809761536214, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #45", "Product #45", 281.41500027636761, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #44", "Product #44", 180.79710310129315, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #43", "Product #43", 37.11332770302581, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #42", "Product #42", 410.89394459216572, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #41", "Product #41", 1.9699412556225162, 7, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #40", "Product #40", 320.95426727875798, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #39", "Product #39", 432.44235089954094, 8, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 99, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #99", "Product #99", 19.731082663746125, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #38", "Product #38", 267.24109141865796, 1, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #36", "Product #36", 122.03110268015, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #35", "Product #35", 221.14216357383046, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #34", "Product #34", 357.44564731346708, 6, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #33", "Product #33", 26.860235576918459, 9, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #32", "Product #32", 202.93302202547576, 5, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #31", "Product #31", 210.46839632022585, 3, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #30", "Product #30", 155.2606864624008, 4, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #29", "Product #29", 294.90683928500249, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #28", "Product #28", 59.539478668728599, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #27", "Product #27", 360.09464063453237, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #37", "Product #37", 111.42373533985752, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #100", "Product #100", 5.858275482365058, 2, "unknown", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
