using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using TestProject.Models;

namespace TestProjectMigrations
{
    [ContextType(typeof(LocalDatabaseContext))]
    partial class LocalDatabaseMigration
    {
        public override string Id
        {
            get { return "20150915214200_LocalDatabaseMigration"; }
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

                    b.Property<int?>("FromId");

                    b.Property<int?>("ToId");

                    b.Key("Id");
                });

            builder.Entity("TestProject.Models.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Ecr");

                    b.Property<long>("ExpressCode");

                    b.Property<string>("ImageSourceUri");

                    b.Property<string>("StationName");

                    b.Key("Id");
                });

            builder.Entity("TestProject.Models.FavoriteTrainPath", b =>
                {
                    b.Reference("TestProject.Models.Station")
                        .InverseCollection()
                        .ForeignKey("FromId");

                    b.Reference("TestProject.Models.Station")
                        .InverseCollection()
                        .ForeignKey("ToId");
                });
        }
    }
}
