using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;

namespace TrainTimeTable.LocalDbEntities.Migrations
{
    public partial class DatabaseMigration : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Station",
                columns: table => new
                {
                    Id = table.Column(type: "INTEGER", nullable: false),
                      
                    Ecr = table.Column(type: "INTEGER", nullable: false),
                    ExpressCode = table.Column(type: "INTEGER", nullable: false),
                    ImageSourceUri = table.Column(type: "TEXT", nullable: true),
                    StationName = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.Id);
                });
            migration.CreateTable(
                name: "FavoriteTrainPath",
                columns: table => new
                {
                    Id = table.Column(type: "INTEGER", nullable: false),
                       
                    FromId = table.Column(type: "INTEGER", nullable: true),
                    ToId = table.Column(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteTrainPath", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteTrainPath_Station_FromId",
                        columns: x => x.FromId,
                        referencedTable: "Station",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoriteTrainPath_Station_ToId",
                        columns: x => x.ToId,
                        referencedTable: "Station",
                        referencedColumn: "Id");
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("FavoriteTrainPath");
            migration.DropTable("Station");
        }
    }
}
