using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Cirrious.MvvmCross.ViewModels;
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.Shared.Models.Map;

namespace TrainTimeTable.Views.Map
{
    public class MapManager:MvxNotifyPropertyChanged
    {
        private readonly MapControl _mapControl;
        private readonly MapItemsControl _popupItemsControl;
        private ObservableCollection<StationModel> _positions; 

        public MapManager(MapControl mapControl)
        {
            _mapControl = mapControl;
            
          
            _mapControl.Loaded += _mapControl_Loaded;
            _positions =new ObservableCollection<StationModel>();
            _mapControl.MapElementClick += _mapControl_MapElementClick;
            _mapControl.CenterChanged += _mapControl_CenterChanged;
            

        }

        private void _mapControl_CenterChanged(MapControl sender, object args)
        {
            if (args != null)
            {
                
            }
        }

        private void _mapControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // CreateLayer();
             CreateOverlayMethod();
        }

        private void CreateLayer()
        {
            var dataSource = new HttpMapTileDataSource(
                "http://a.tile.openstreetmap.org/{zoomlevel}/{x}/{y}.png");
            var ts = new MapTileSource(dataSource)
            {
                Layer = MapTileLayer.BackgroundReplacement,
                IsTransparencyEnabled = true
            };
            _mapControl.TileSources.Add(ts);
        }

        private void CreateOverlayMethod()
        {
            var dataSource = new TileDataSourceWithOpacity(
              "http://a.tiles.openrailwaymap.org/standard/{zoomlevel}/{x}/{y}.png", 255);

            var ts = new MapTileSource(dataSource)
            {
                IsTransparencyEnabled = true,IsFadingEnabled = true,ZoomLevelRange =new MapZoomLevelRange() { Max = 20,Min = 15.5}
            };
            _mapControl.TileSources.Add(ts);
        }

       

        // Create the custom tiles.
        // This example creates red tiles that are partially opaque.
        private async Task<RandomAccessStreamReference> CreateBitmapAsStreamAsync(string url)
        {
            var ass = await new HttpClient().GetByteArrayAsync(new Uri(url));
           
            // Create RandomAccessStream from byte array
            InMemoryRandomAccessStream randomAccessStream =
                new InMemoryRandomAccessStream();
            IOutputStream outputStream = randomAccessStream.GetOutputStreamAt(0);
            DataWriter writer = new DataWriter(outputStream);
            writer.WriteBytes(ass);
            await writer.StoreAsync();
            await writer.FlushAsync();
            return RandomAccessStreamReference.CreateFromStream(randomAccessStream);

        }

    

        public ObservableCollection<StationModel> Positions
        {
            get { return _positions; }
            set
            {
                _positions = value;
                base.RaisePropertyChanged(()=>Positions);
            }
        }

        private void _mapControl_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapElement=args.MapElements.FirstOrDefault() as MapIcon;
          _mapControl.Center=new Geopoint(new BasicGeoposition() {Latitude = mapElement.Location.Position.Latitude,Longitude = mapElement.Location.Position.Longitude});
            Positions.Clear();
            Positions.Add(new StationModel() {StationName = mapElement.Title,Position = new GeoModel() {Latitude = mapElement.Location.Position.Latitude,Longitude = mapElement.Location.Position.Longitude} });

        }

    

        public void AddStationsToMap(IEnumerable<StationModel> stations)
        {
            foreach (var stationModel in stations)
            {
                if (stationModel.Position.Latitude != null && stationModel.Position.Longitude != null)
                    AddSingleStation(stationModel.StationName,(double)stationModel.Position.Latitude, (double)stationModel.Position.Longitude);
            }
        }

        private void AddSingleStation(string station,double latitude, double longitude)
        {
            var mapIcon = new MapIcon();
            mapIcon.Location = new Geopoint(new BasicGeoposition()
            {
                Latitude = latitude,
                Longitude = longitude
            });

            mapIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon.Title = station;
        
            _mapControl.MapElements.Add(mapIcon);
        }

        public void SetMyPosition(PositionDto position)
        {
            var mapIcon = new MapIcon();
          
            mapIcon.Location = new Geopoint(new BasicGeoposition()
            {
                Latitude = (double)position.Latitude,
                Longitude = (double)position.Longitude
            });
            mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Pins/my_location.png"));
       
        
            _mapControl.MapElements.Add(mapIcon);
        }
    }
}
