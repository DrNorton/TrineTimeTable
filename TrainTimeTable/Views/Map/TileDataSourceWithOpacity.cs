using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;

namespace TrainTimeTable.Views.Map
{
    public class TileDataSourceWithOpacity : CustomMapTileDataSource
    {
        #region Private Properties

        private string _tileUrl;
        private byte _opacity;

        #endregion

        #region Constructor

        public TileDataSourceWithOpacity(string tileUrl, byte opacity)
        {
            _tileUrl = tileUrl;
            _opacity = opacity;
            this.BitmapRequested += BitmapRequestedHandler;
        }

        #endregion

        #region Public Properties

        public bool IsWMS { get; set; }

        #endregion

        #region Private Methods

        private async void BitmapRequestedHandler(CustomMapTileDataSource sender, MapTileBitmapRequestedEventArgs args)
        {
            var deferral = args.Request.GetDeferral();

            try
            {
                using (var imgStream = await GetTileAsStreamAsync(args.X, args.Y, args.ZoomLevel))
                {
                    //http://msdn.microsoft.com/en-us/library/windows/apps/xaml/jj709936.aspx
                    var memStream = imgStream.AsRandomAccessStream();
                    var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(memStream);

                    var pixelProvider = await decoder.GetPixelDataAsync(
                        Windows.Graphics.Imaging.BitmapPixelFormat.Rgba8,
                        Windows.Graphics.Imaging.BitmapAlphaMode.Straight,
                        new Windows.Graphics.Imaging.BitmapTransform() {ScaledWidth = 256,ScaledHeight = 256},
                        Windows.Graphics.Imaging.ExifOrientationMode.RespectExifOrientation,
                        Windows.Graphics.Imaging.ColorManagementMode.ColorManageToSRgb);

                    var pixels = pixelProvider.DetachPixelData();

                    var width = 256;
                    var height = 256;

                    Parallel.For(0, height, (i) =>
                    {
                        for (var j = 0; j < width; j++)
                        {
                            var idx = (i * height + j) * 4 + 3; // Alpha channel Index (RGBA)

                            //Only change the opacity of a pixel if it isn't transparent
                            if (pixels[idx] != 0)
                            {
                                pixels[idx] = _opacity;
                            }
                        }
                    });

                    //http://msdn.microsoft.com/en-US/library/windows/apps/xaml/dn632728.aspx
                    var randomAccessStream = new InMemoryRandomAccessStream();
                    var outputStream = randomAccessStream.GetOutputStreamAt(0);
                    var writer = new DataWriter(outputStream);
                    writer.WriteBytes(pixels);
                    await writer.StoreAsync();
                    await writer.FlushAsync();

                    args.Request.PixelData = RandomAccessStreamReference.CreateFromStream(randomAccessStream);
                }
            }
            catch { }

            deferral.Complete();
        }

        private Task<MemoryStream> GetTileAsStreamAsync(int x, int y, int zoom)
        {
            var tcs = new TaskCompletionSource<MemoryStream>();

            var quadkey = TileXYZoomToQuadKey(x, y, zoom);

            string url;

            if (IsWMS)
            {
                double north, south, east, west;
                TileXYZoomToBBox(x, y, zoom, out north, out south, out east, out west);
                url = _tileUrl.Replace("{boundingbox}", string.Format("{0:N5},{1:N5},{2:N5},{3:N5}", west, south, east, north));
            }
            else
            {
                url = _tileUrl.Replace("{x}", x.ToString()).Replace("{y}", y.ToString()).Replace("{zoomlevel}", zoom.ToString()).Replace("{quadkey}", quadkey);
            }

            var request = HttpWebRequest.Create(url);
            request.BeginGetResponse(async (a) =>
            {
                var r = (HttpWebRequest)a.AsyncState;
                HttpWebResponse response = (HttpWebResponse)r.EndGetResponse(a);

                using (var s = response.GetResponseStream())
                {
                    var ms = new MemoryStream();
                    await s.CopyToAsync(ms);
                    ms.Position = 0;
                    tcs.SetResult(ms);
                }
            }, request);

            return tcs.Task;
        }

        private string TileXYZoomToQuadKey(int tileX, int tileY, int zoom)
        {
            var quadKey = new StringBuilder();
            for (int i = zoom; i > 0; i--)
            {
                char digit = '0';
                int mask = 1 << (i - 1);
                if ((tileX & mask) != 0)
                {
                    digit++;
                }
                if ((tileY & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey.Append(digit);
            }
            return quadKey.ToString();
        }

        private void TileXYZoomToBBox(int x, int y, int zoom, out double north, out double south, out double east, out double west)
        {
            double mapSize = Math.Pow(2, zoom);

            west = ((x * 360) / mapSize) - 180;
            east = (((x + 1) * 360) / mapSize) - 180;

            double efactor = Math.Exp((0.5 - y / mapSize) * 4 * Math.PI);
            north = (Math.Asin((efactor - 1) / (efactor + 1))) * (180 / Math.PI);

            efactor = Math.Exp((0.5 - (y + 1) / mapSize) * 4 * Math.PI);
            south = (Math.Asin((efactor - 1) / (efactor + 1))) * (180 / Math.PI);
        }

        #endregion
    }
}
