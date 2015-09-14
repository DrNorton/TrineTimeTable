using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using TrainTimeTable.LocalDbEntities;

namespace TrainTimeTableMigrations
{
    [ContextType(typeof(BloggingContext))]
    partial class BloggingContextModelSnapshot : ModelSnapshot
    {
        public override void BuildModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815");

            builder.Entity("TrainTimeTable.LocalDbEntities.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url")
                        .Required();

                    b.Key("BlogId");
                });

            builder.Entity("TrainTimeTable.LocalDbEntities.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogId");

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.Key("PostId");
                });

            builder.Entity("TrainTimeTable.LocalDbEntities.Post", b =>
                {
                    b.Reference("TrainTimeTable.LocalDbEntities.Blog")
                        .InverseCollection()
                        .ForeignKey("BlogId");
                });
        }
    }
}
