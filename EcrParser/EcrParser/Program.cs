using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace EcrParser
{
    class Program
    {
        private static List<CsvModel> _coord;

        static void Main(string[] args)
        {
            Parse();
            Console.ReadLine();
        }

        static async Task Parse()
        {
            _coord=new List<CsvModel>();
            using (var csvReader=new StreamReader("Coordinates.txt"))
            {
                try
                {
                    string line;
                    while ((line = csvReader.ReadLine()) != null)
                    {
                        var parts = line.Split(new char[] { ';' });
                        var csv = new CsvModel();
                        csv.Esr = parts[0].Replace("\"", "");
                        Console.WriteLine(parts[4]);
                        if (csv.Esr == "210930")
                        {
                            
                        }
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
                        var geom=_coord.FirstOrDefault(x => x.Esr.Equals(result.Ecr));
                        if (geom != null)
                        {
                            result.Position.Latitude = geom.Lat;
                            result.Position.Longitude = geom.Long;
                        }
                        else
                        {
                            Console.WriteLine("t");
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
                        model.Ecr = hy.InnerText;
                    }
                    else
                    {
                        model.ExpressCode = hy.InnerText;
                    }
                    index++;
                }
               
            }
        }
    }
}
