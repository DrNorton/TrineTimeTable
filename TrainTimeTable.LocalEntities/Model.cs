using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace TrainTimeTable.LocalEntities
{
    public class DatabaseInitializator
    {
        public string Path { get; set; }


    }

   

    public class FavoriteTrainPath
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Station))]
        public int FromId { get; set; }

        [ManyToOne("FromId")]
        public  Station From { get; set; }

        [ForeignKey(typeof(Station))]
        public int ToId { get; set; }

        [ManyToOne("ToId")]
        public  Station To { get; set; }
       
    }

    public class Station
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public long Ecr { get; set; }
        public long ExpressCode { get; set; }
        public string StationName { get; set; }
        public string ImageSourceUri { get; set; }

        [ForeignKey(typeof(Position))]
        public int PositionId { get; set; }

        [ManyToOne("PositionId", CascadeOperations = CascadeOperation.All)]
        public Position Position { get; set; }

        [ForeignKey(typeof(Image))]
        public int ImageId { get; set; }

        [ManyToOne("ImageId", CascadeOperations = CascadeOperation.All)]
        public Image Image { get; set; }

    }

    public class Position
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

    public  class Image
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ThumbUrl { get; set; }
        public string FullImageUrl { get; set; }

       
    }

}

