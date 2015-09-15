using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Infrastructure;

namespace TrainTimeTable.LocalDbEntities.Migrations
{
    [ContextType(typeof(LocalDatabaseContext))]
    partial class DatabaseMigration:Migration
    {
        public override string Id
        {
            get { return "20150914205632_Migration"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13815"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815");

            builder.Entity("TestProject.Models.FavoriteTrainPath", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FromStationCode");

                    b.Property<long>("ToStationCode");

                    b.Key("Id");
                });
        }
    }
}
