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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'101', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Deleted", "Description", "Name", "Price", "Stock", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #1", "Product #1", 36.985656091471043, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #73", "Product #73", 615.51964873006557, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #72", "Product #72", 87.606644512902321, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #71", "Product #71", 98.303680343694836, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #70", "Product #70", 76.306234678396137, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #69", "Product #69", 231.07280849063432, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #68", "Product #68", 394.12613953190208, 2, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #67", "Product #67", 593.36050256032524, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #66", "Product #66", 276.55602869417334, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #65", "Product #65", 405.21200553197974, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #64", "Product #64", 44.525887508190188, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #63", "Product #63", 430.50256567332087, 2, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #62", "Product #62", 647.98284162440473, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #61", "Product #61", 22.779631870230489, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #60", "Product #60", 48.709492952893257, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #59", "Product #59", 28.329693688233242, 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #58", "Product #58", 9.4782721761047242, 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #57", "Product #57", 183.98418210632363, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #56", "Product #56", 308.44092162253378, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #55", "Product #55", 486.60821787575645, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #54", "Product #54", 16.282590713483557, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #53", "Product #53", 306.9458003765651, 2, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #74", "Product #74", 404.34821957971349, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #52", "Product #52", 85.886621727555351, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #75", "Product #75", 447.03978526361277, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #77", "Product #77", 202.88293228618937, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 98, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #98", "Product #98", 783.66393151863667, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 97, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #97", "Product #97", 277.24270158877721, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 96, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #96", "Product #96", 186.85227870561755, 2, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #95", "Product #95", 73.642321442087336, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 94, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #94", "Product #94", 20.863248947944143, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 93, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #93", "Product #93", 218.90468777106364, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 92, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #92", "Product #92", 627.98725065122699, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 91, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #91", "Product #91", 504.51121649914944, 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 90, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #90", "Product #90", 116.27146381245529, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 89, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #89", "Product #89", 534.05982207230284, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 88, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #88", "Product #88", 129.80728208916599, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 87, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #87", "Product #87", 36.813036111562063, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 86, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #86", "Product #86", 593.30496047311692, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 85, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #85", "Product #85", 214.77075264918187, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #84", "Product #84", 337.29492045626739, 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 83, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #83", "Product #83", 0.93091542317108966, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 82, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #82", "Product #82", 38.217318122376369, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 81, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #81", "Product #81", 466.87778152612867, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #80", "Product #80", 296.30411063707623, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 79, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #79", "Product #79", 283.63224708877141, 3, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 78, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #78", "Product #78", 194.17291770790374, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 76, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #76", "Product #76", 90.731710603801403, 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #51", "Product #51", 109.16345235107626, 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #50", "Product #50", 282.62153022253028, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #49", "Product #49", 167.2211015444347, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #22", "Product #22", 93.435096130908988, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #21", "Product #21", 450.4549298949795, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #20", "Product #20", 2.5046700008700928, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #19", "Product #19", 223.00626515923358, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #18", "Product #18", 124.94723547852935, 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #17", "Product #17", 342.91045746901563, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #16", "Product #16", 52.195072293838052, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #15", "Product #15", 138.54025251164111, 2, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #14", "Product #14", 207.34922189421448, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #13", "Product #13", 200.60565771749506, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #12", "Product #12", 589.9082399718036, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #11", "Product #11", 76.30138369570551, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #10", "Product #10", 94.475551779603364, 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #9", "Product #9", 432.86544877331028, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #8", "Product #8", 155.62790826877017, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #7", "Product #7", 5.348051466675499, 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #6", "Product #6", 4.6883207767681778, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #5", "Product #5", 0.85180458373008505, 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #4", "Product #4", 31.51580519299759, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #3", "Product #3", 154.8568547260281, 3, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #2", "Product #2", 851.32247519740952, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #23", "Product #23", 380.04858676439551, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #24", "Product #24", 308.81146111703083, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #25", "Product #25", 59.719521465115029, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #26", "Product #26", 601.77559539292736, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #48", "Product #48", 100.24006114725026, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #47", "Product #47", 317.68407507132929, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #46", "Product #46", 32.834978116133705, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #45", "Product #45", 700.59562882343107, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #44", "Product #44", 205.21298673432926, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #43", "Product #43", 448.27365553857464, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #42", "Product #42", 46.569919555713383, 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #41", "Product #41", 225.13416969828967, 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #40", "Product #40", 108.90157009889444, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #39", "Product #39", 134.74828233884102, 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 99, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #99", "Product #99", 174.15821688489905, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #38", "Product #38", 494.41971102003924, 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #36", "Product #36", 361.9328800295167, 9, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #35", "Product #35", 194.45858075956747, 8, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #34", "Product #34", 341.71458506850274, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #33", "Product #33", 387.87485957232997, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #32", "Product #32", 688.20696517229408, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #31", "Product #31", 74.640651651956446, 4, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #30", "Product #30", 446.77273121698425, 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #29", "Product #29", 303.67425814814595, 1, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #28", "Product #28", 29.705942834590534, 6, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #27", "Product #27", 146.58037557060848, 7, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #37", "Product #37", 75.484058437628704, 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Description for product #100", "Product #100", 28.656266870282714, 5, "system", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
