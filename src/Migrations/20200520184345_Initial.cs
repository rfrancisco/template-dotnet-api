using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjectName.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Deleted", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { -1, false, "", "Product #1", 215.93966390329396, 8 },
                    { -73, false, "", "Product #73", 494.87235224939531, 5 },
                    { -72, false, "", "Product #72", 159.071797985617, 5 },
                    { -71, false, "", "Product #71", 77.217189542584677, 1 },
                    { -70, false, "", "Product #70", 52.717702135777898, 8 },
                    { -69, false, "", "Product #69", 110.33887133949383, 2 },
                    { -68, false, "", "Product #68", 100.02500687540741, 2 },
                    { -67, false, "", "Product #67", 43.084442854432595, 6 },
                    { -66, false, "", "Product #66", 87.826087929227427, 3 },
                    { -65, false, "", "Product #65", 38.335537136688615, 9 },
                    { -64, false, "", "Product #64", 137.23689522931207, 3 },
                    { -63, false, "", "Product #63", 633.99844441981907, 6 },
                    { -62, false, "", "Product #62", 760.3438510887064, 3 },
                    { -61, false, "", "Product #61", 297.28224122677102, 1 },
                    { -60, false, "", "Product #60", 88.497980426297516, 6 },
                    { -59, false, "", "Product #59", 92.026941061963768, 3 },
                    { -58, false, "", "Product #58", 479.15207074869051, 4 },
                    { -57, false, "", "Product #57", 5.4103662233847967, 1 },
                    { -56, false, "", "Product #56", 562.62804546702091, 6 },
                    { -55, false, "", "Product #55", 246.02968955041362, 5 },
                    { -54, false, "", "Product #54", 662.10350051061414, 6 },
                    { -53, false, "", "Product #53", 7.708889704062087, 8 },
                    { -74, false, "", "Product #74", 18.891989522097628, 8 },
                    { -52, false, "", "Product #52", 22.479106414354924, 5 },
                    { -75, false, "", "Product #75", 34.11006590542852, 8 },
                    { -77, false, "", "Product #77", 306.73564825148117, 7 },
                    { -98, false, "", "Product #98", 32.335787512518365, 3 },
                    { -97, false, "", "Product #97", 177.73698264348181, 4 },
                    { -96, false, "", "Product #96", 33.489185878303452, 6 },
                    { -95, false, "", "Product #95", 48.65831371427435, 9 },
                    { -94, false, "", "Product #94", 430.25998604449444, 9 },
                    { -93, false, "", "Product #93", 13.355281910558826, 4 },
                    { -92, false, "", "Product #92", 6.6245321103485919, 1 },
                    { -91, false, "", "Product #91", 403.37909162201879, 7 },
                    { -90, false, "", "Product #90", 34.800885179918673, 4 },
                    { -89, false, "", "Product #89", 21.635369800792716, 7 },
                    { -88, false, "", "Product #88", 99.974790231312994, 4 },
                    { -87, false, "", "Product #87", 353.10276746987495, 9 },
                    { -86, false, "", "Product #86", 122.12783722771697, 8 },
                    { -85, false, "", "Product #85", 810.58676840625094, 2 },
                    { -84, false, "", "Product #84", 266.00528048165387, 4 },
                    { -83, false, "", "Product #83", 44.278787693138604, 2 },
                    { -82, false, "", "Product #82", 144.12886181107206, 1 },
                    { -81, false, "", "Product #81", 9.7404334953708744, 7 },
                    { -80, false, "", "Product #80", 218.69187159402847, 6 },
                    { -79, false, "", "Product #79", 31.537496778898639, 9 },
                    { -78, false, "", "Product #78", 568.39458982292308, 8 },
                    { -76, false, "", "Product #76", 143.6104251461152, 7 },
                    { -51, false, "", "Product #51", 724.02526687925001, 1 },
                    { -50, false, "", "Product #50", 126.05140751136999, 7 },
                    { -49, false, "", "Product #49", 40.776269016683223, 8 },
                    { -22, false, "", "Product #22", 451.20038103461286, 1 },
                    { -21, false, "", "Product #21", 345.45434685910789, 8 },
                    { -20, false, "", "Product #20", 281.91596419686266, 2 },
                    { -19, false, "", "Product #19", 501.61375575028995, 2 },
                    { -18, false, "", "Product #18", 522.83045235408019, 8 },
                    { -17, false, "", "Product #17", 6.2728425293568719, 3 },
                    { -16, false, "", "Product #16", 32.35410206408897, 7 },
                    { -15, false, "", "Product #15", 7.6061924601002566, 2 },
                    { -14, false, "", "Product #14", 32.335398496284803, 5 },
                    { -13, false, "", "Product #13", 795.4163215958589, 7 },
                    { -12, false, "", "Product #12", 30.120070278700474, 9 },
                    { -11, false, "", "Product #11", 214.24760202143696, 3 },
                    { -10, false, "", "Product #10", 226.69940769146172, 9 },
                    { -9, false, "", "Product #9", 661.20404083151561, 8 },
                    { -8, false, "", "Product #8", 3.7527416179667887, 9 },
                    { -7, false, "", "Product #7", 109.70660716141883, 5 },
                    { -6, false, "", "Product #6", 203.33743729225239, 7 },
                    { -5, false, "", "Product #5", 585.98804495809043, 4 },
                    { -4, false, "", "Product #4", 51.789082955470818, 5 },
                    { -3, false, "", "Product #3", 51.30898750541219, 7 },
                    { -2, false, "", "Product #2", 164.68056481549542, 5 },
                    { -23, false, "", "Product #23", 170.50483840354943, 2 },
                    { -24, false, "", "Product #24", 350.37042451015225, 5 },
                    { -25, false, "", "Product #25", 73.028415103921859, 1 },
                    { -26, false, "", "Product #26", 20.122413418778411, 7 },
                    { -48, false, "", "Product #48", 107.00087242154446, 6 },
                    { -47, false, "", "Product #47", 673.13744740473919, 8 },
                    { -46, false, "", "Product #46", 174.61910297843588, 2 },
                    { -45, false, "", "Product #45", 442.60120265772622, 9 },
                    { -44, false, "", "Product #44", 159.33157131417263, 8 },
                    { -43, false, "", "Product #43", 263.49396209860868, 7 },
                    { -42, false, "", "Product #42", 261.48105153882926, 6 },
                    { -41, false, "", "Product #41", 227.86650538624568, 4 },
                    { -40, false, "", "Product #40", 112.16473163672011, 8 },
                    { -39, false, "", "Product #39", 245.49416911112803, 7 },
                    { -99, false, "", "Product #99", 499.71650944078181, 8 },
                    { -38, false, "", "Product #38", 80.33589071935782, 1 },
                    { -36, false, "", "Product #36", 726.63799853606054, 6 },
                    { -35, false, "", "Product #35", 326.57595184751597, 4 },
                    { -34, false, "", "Product #34", 66.417288077258178, 4 },
                    { -33, false, "", "Product #33", 142.44710012220179, 8 },
                    { -32, false, "", "Product #32", 239.08226833636047, 5 },
                    { -31, false, "", "Product #31", 15.433354601000136, 6 },
                    { -30, false, "", "Product #30", 144.55851454919136, 8 },
                    { -29, false, "", "Product #29", 476.4736253146425, 7 },
                    { -28, false, "", "Product #28", 48.714803137683681, 2 },
                    { -27, false, "", "Product #27", 207.4954969396328, 7 },
                    { -37, false, "", "Product #37", 621.42806778449005, 7 },
                    { -100, false, "", "Product #100", 254.05734611957212, 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
