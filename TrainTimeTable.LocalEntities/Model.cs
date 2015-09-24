using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

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

    }
}

