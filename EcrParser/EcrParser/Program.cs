﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Encoder = System.Text.Encoder;

namespace EcrParser
{
    class Program
    {
        private static List<CsvModel> _coord;
        private static List<WikiStationModel> _wikiImages;

        static  void Main(string[] args)
        {
            //InsertToBlob();
           LoadStationsAndEcrFromWiki();
            Go();
            Console.ReadLine();
        }

        private static void LoadStationsAndEcrFromWiki()
        {
            var resultList=new List<WikiStationModel>();
            var client = new WebClient();
            client.Encoding=Encoding.UTF8;
            var html = client.DownloadString("https://ru.wikipedia.org/wiki/Список_железнодорожных_станций_и_платформ_Москвы");
             var htmlDoc = new HtmlAgilityPack.HtmlDocument();
             htmlDoc.LoadHtml(html);
             var table = htmlDoc.DocumentNode.DescendantsAndSelf().Where(x => x.Name.Contains("table")).Where(y => y.Attributes.Any(z => z.Name == "class" && z.Value == "standard")==true).ToList();
            foreach (var singleTable in table)
            {
                var rows=singleTable.DescendantsAndSelf().Where(x => x.Name == "tr").ToList();
                
                foreach (var row in rows)
                {
                    var count = 0;
                    var columns=row.DescendantsAndSelf().Where(x => x.Name == "td").ToList();
                  
                    if (columns != null)
                    {
               
                             var wiki=new WikiStationModel();
                        try
                        {


                            for (int i = 0; i < columns.Count; i++)
                            {
                                if (i == 0)
                                {
                                    var image = columns[i].DescendantsAndSelf().FirstOrDefault(x => x.Name == "img");
                                    wiki.FileName = image.Attributes.FirstOrDefault(x => x.Name == "alt").Value;
                                }

                                if (i == 4)
                                {
                                    var href =
                                        columns[i].DescendantsAndSelf().FirstOrDefault(y => y.Name == "a").InnerText;
                                    wiki.Ecr = long.Parse(href);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            continue;;
                        }
                        resultList.Add(wiki);
                        count++;
                    }
                }
            }
            var test = 0;
            _wikiImages=resultList.Where(x => x.Ecr != 0 && x.FileName!=null).ToList();
            foreach (var image in _wikiImages)
            {
                try
                {
                    var url = String.Format("https://commons.wikimedia.org/wiki/File:{0}?uselang=ru", image.FileName);
                    var img = new WebClient().DownloadString(url);
                    var doc2 = new HtmlDocument();
                    doc2.LoadHtml(img);
                    var imgDiv =
                        doc2.DocumentNode.DescendantsAndSelf()
                            .Where(x => x.Name == "div")
                            .Where(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "fullImageLink") == true)
                            .FirstOrDefault();
                    var href = imgDiv.DescendantsAndSelf().FirstOrDefault(x => x.Name == "a");
                    var url2 = href.Attributes.FirstOrDefault(x => x.Name == "href").Value;
                    image.Url = url2;
                    image.ByteImage=new WebClient().DownloadData(image.Url);
                    image.Thumb = GetSmallImage(image.ByteImage);
                    test++;
                    Debug.WriteLine(test);
                }
                catch (Exception e)
                {
                    continue;
                }
             
            }
      


        }

        public static async Task Go()
        {
            var parsed = await Parse();
            Insert(parsed);
         
        }

        public static string InsertToBlob(string name,byte[] image)
        {
            var storageAccount =
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=projectimages;AccountKey=7Yej0KKzAJZHyTFSMQnap8HB1yEJNYkJqQLXwfH9HM5Op4kcEM3LZpKRW9+sKf8rWv7JatXc8bhcUq12CaqR8g==");
            var blobClient = storageAccount.CreateCloudBlobClient();
         // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();
            container.SetPermissions(
                    new BlobContainerPermissions
                    {
                        PublicAccess =
                            BlobContainerPublicAccessType.Blob
                    }); 
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            using (var fileStream = new MemoryStream(image))
            {
                blockBlob.UploadFromStream(fileStream);
            }
            return String.Format("http://projectimages.blob.core.windows.net/images/{0}", name);
        }

        private static void Insert(List<EcrModel> parsed)
        {
            try
            {
                var test = parsed.Select(x => x.StationImageUrl).Distinct();
                var context = new traintimetable_dbEntities();
                  AddTypes(test);
                var count = 0;
                foreach (var ecrModel in parsed)
                {

                    var newStation = new Station()
                    {
                        Ecr = ecrModel.Ecr,
                        ExpressCode = ecrModel.ExpressCode,
                        OpenStreetMapNode = ecrModel.OpenStreetMapNode,
                        StationName = ecrModel.StationName,
                        StationType = context.StationTypes.FirstOrDefault(x => x.Name == ecrModel.StationImageUrl),
                        OpenStreetMapUrl = ecrModel.OpenStreetMapUrl,
                        

                    };
                    if (ecrModel.Position != null)
                    {
                        newStation.Position = new Position()
                        {
                            Latitude = ecrModel.Position.Latitude,
                            Longitude = ecrModel.Position.Longitude
                        };
                    }
                    else
                    {
                        newStation.Position = new Position()
                        {
                            Latitude = 0,
                            Longitude = 0
                        };
                    }
                    try
                    {
                        var wikiImages = _wikiImages.FirstOrDefault(x => x.Ecr == ecrModel.Ecr);
                        if (wikiImages != null && wikiImages.ByteImage != null)
                        {
                            var urlblob = InsertToBlob(wikiImages.FileName, wikiImages.ByteImage);
                            var smallBlob = InsertToBlob("thumb_" + wikiImages.FileName, wikiImages.Thumb);
                            newStation.Image=new Image();
                            newStation.Image.FullImageUrl = urlblob;
                            newStation.Image.ThumbUrl = smallBlob;
                        }
                        else
                        {
                            var url =
                                string.Format(
                                    "http://dev.virtualearth.net/REST/v1/Imagery/Map/Road/{0},{1}/17?mapSize=800,800&key=l6AOGVgaf0DIlLuOEOum~ysMXqmfV9te9mCzpSy1QHA~AsNLrgOgd3BHryIGeusH0s4--NnOwoiAT6Pc56ilxJPflOrSOF1CN1h0BaimBW1g&AB&pp={0},{1};;★",
                                    ecrModel.Position.Latitude.ToString().Replace(",", "."), ecrModel.Position.Longitude.ToString().Replace(",", "."));

                            var url2 =
                              string.Format(
                                  "http://dev.virtualearth.net/REST/v1/Imagery/Map/Road/{0},{1}/17?mapSize=300,300&key=l6AOGVgaf0DIlLuOEOum~ysMXqmfV9te9mCzpSy1QHA~AsNLrgOgd3BHryIGeusH0s4--NnOwoiAT6Pc56ilxJPflOrSOF1CN1h0BaimBW1g&AB&pp={0},{1};;★",
                                  ecrModel.Position.Latitude.ToString().Replace(",", "."), ecrModel.Position.Longitude.ToString().Replace(",", "."));
                            var img = new WebClient().DownloadData(url);
                            var thumb = new WebClient().DownloadData(url2);
                            if (img != null)
                            {
                                var urlblob = InsertToBlob(ecrModel.StationName+".jpg", img);
                                var smallBlob = InsertToBlob("thumb_"+ecrModel.StationName + ".jpg", thumb);

                                newStation.Image = new Image();
                                newStation.Image.FullImageUrl = urlblob;
                                newStation.Image.ThumbUrl = smallBlob;
                            }

                        }
                    }
                    catch (Exception d)
                    {
                        
                    }
                    context.Stations.Add(newStation);
                    context.SaveChanges();
                    Console.WriteLine(count++);
                }
            }
            catch (Exception e)
            {
                
            }
           
        }

        private static void AddTypes(IEnumerable<string> types )
        {
            var context = new traintimetable_dbEntities();
            foreach (var t in types)
            {
                context.StationTypes.Add(new StationType() { Name = t });
                context.SaveChanges();
            }
           
        
        
        }

        public static byte[] GetSmallImage(byte[] image)
        {
          
                    // Create a new stream and read in the data. Where this.Data = the Data Column
            using (MemoryStream stream = new MemoryStream(image))
                    {
                        // Create the original image using the stream data
                        using (var img = System.Drawing.Image.FromStream(stream))
                        {
                            // Get the image dimensions
                            double width = img.Width;
                            double height = img.Height;

                            // Only resize if the image is larger than the thumbnail size
                            if (height > 150)
                            {
                                // Find the aspect ratio so that we don't distort the image during reduction
                                double aspectRatio = width / height;

                                // Set a fixed height for your thumbnails
                                height = 300;

                                // Get the width based on the height and aspect ratio to get the correct scale
                                width = height * aspectRatio;
                            }

                            // Create a new image based on the original and new size
                            using (Bitmap bmp = new Bitmap(img, (int)width, (int)height))
                            {
                                using (MemoryStream smallImage = new MemoryStream())
                                {
                                    // Save the thumbnail image and return as a byte array
                                    bmp.Save(smallImage, ImageFormat.Png);
                                    return smallImage.ToArray();
                                }
                            }
                        }
                    }
                
               
            
        }

        private static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(image, new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            return resizedImage;
        } 

        static async Task<List<EcrModel>> Parse()
        {
            _coord=new List<CsvModel>();
            using (var csvReader=new StreamReader("Coordinates.txt",Encoding.UTF8))
            {
                try
                {
                    string line;
                    while ((line = csvReader.ReadLine()) != null)
                    {
                        var parts = line.Split(new char[] { ';' });
                        var csv = new CsvModel();
                        csv.Esr = long.Parse(parts[0].Replace("\"", ""));
                        Console.WriteLine(parts[4]);
                       
                        csv.Lat = double.Parse(parts[4].Replace(".", ",").Replace("\"", ""));
                        csv.Long = double.Parse(parts[5].Replace(".", ",").Replace("\"", ""));
                        _coord.Add(csv);
                    }
                 
                   
                }
                catch (Exception e)
                {
                    
                    throw;
                }
             
            }

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            using (var stremReader=new StreamReader("Text.txt"))
            {
                var str = await stremReader.ReadToEndAsync();
                htmlDoc.LoadHtml(str);
                var table = htmlDoc.DocumentNode.SelectSingleNode("//tbody");
                var resultList=new List<EcrModel>();
                var test = 0;
                foreach (var row in table.SelectNodes("tr"))
                {
                 
                    var result = new EcrModel();
                    var rows = row.SelectNodes("td");
                     if (rows != null)
                     {
                         var count = 0;
                       foreach (var column in rows )
                       {
                           switch (count)
                           {
                                case 0:
                                    ParseEcrExpress(column, result);
                                    break;

                                case 1:
                                    ParseNameStantion(column,result);
                                    break;

                                case 2:
                                   if (result.StationName == "Егорьевск 1")
                                   {
                                       
                                   }
                                   ParseOpenStreetMap(column, result);
                                    break;
                                   
                                case 5:
                                   result.RegionId = 0;
                                   break;
                           }
                         
                           count++;
                          
                          
                       }
                        resultList.Add(result);
                    }
                    test++;
                    Console.WriteLine(test);

                }

                foreach (var res in resultList)
                {
                    Console.WriteLine(res.StationName);
                }

                return resultList;
            }
        
        }

        private static void ParseOpenStreetMap(HtmlNode column, EcrModel result)
        {
            try
            {
                if (column.InnerText != "\r\n      &nbsp;    ")
                {
                    var img = column.ChildNodes.FirstOrDefault(x => x.Name == "div");
                    if (img != null)
                    {
                        var hregs = img.ChildNodes.Where(x => x.Name == "a").ToList();
                        var href = hregs.FirstOrDefault();

                        var test = href.Attributes.FirstOrDefault(x => x.Name == "href").Value;
                        result.OpenStreetMapUrl = test;
                        result.OpenStreetMapNode = long.Parse(test.Replace(@"http://www.openstreetmap.org/browse/node/", "").Replace(@"http://www.openstreetmap.org/browse/way/",""));
                        result.Position=new Geometry();
                        if (result.StationName.ToLower()=="крюково")
                        {
                            
                        }
                        
                        var geom=_coord.FirstOrDefault(x => x.Esr.Equals(result.Ecr));
                        if (geom != null)
                        {
                            result.Position.Latitude = geom.Lat;
                            result.Position.Longitude = geom.Long;
                        }
                        else
                        {
                          //Ищем по имени

                        }
                        
                       
                    }
                 }
                
            }
            catch (Exception e)
            {
                
            }
           
            
        }

        private static void ParseNameStantion(HtmlNode column, EcrModel result)
        {
            try
            {
                result.StationImageUrl = column.DescendantsAndSelf().FirstOrDefault(x => x.Name == "img").Attributes.FirstOrDefault().Value;
                result.StationName = column.InnerText.Replace("\r\n      &nbsp;", "").Replace("  ", "");
            }
            catch (Exception e)
            {
                
            }
            


        }

        private static void ParseEcrExpress(HtmlNode column,EcrModel model)
        {
            var hrefs = column.DescendantsAndSelf().Where(x => x.Name == "a").ToList();
            var index = 0;
            foreach (var hy in hrefs)
            {
                if (hy.InnerText != "")
                {
                    if (index == 0)
                    {
                        model.Ecr = long.Parse(hy.InnerText);
                    }
                    else
                    {
                        model.ExpressCode = long.Parse(hy.InnerText);
                    }
                    index++;
                }
               
            }
        }
    }
}
