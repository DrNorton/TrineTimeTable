using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using TrainTimeTable.ApiClient.Facade;
using TrainTimeTable.ApiClient.Models;
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.Shared.Messages;
using TrainTimeTable.Shared.Models.Map;
using TrainTimeTable.Shared.Services;
using TrainTimeTable.Shared.ViewModels.Base;

namespace TrainTimeTable.Shared.ViewModels.Map
{
    public class MapViewModel:LoadingScreen
    {
        private readonly IApiFacade _apiFacade;
        private readonly IMvxMessenger _messenger;
        private readonly IPositionReceiver _positionReceiver;
        private List<StationResponse> _allStations;
        private PositionDto _centerPoint;
        private double _zoomLevel=14;
        private List<MapPanelItem> _mapPanelItems;

        public MapViewModel()
        {
            
        }

        public MapViewModel(IApiFacade apiFacade, IMvxMessenger messenger,IPositionReceiver positionReceiver)
        {
            _apiFacade = apiFacade;
            _messenger = messenger;
            _positionReceiver = positionReceiver;
            _centerPoint=new PositionDto()
            {
                //Geopoint for Moscow
                Latitude = 55.7522200,
                Longitude = 37.6155600
            };
            LoadStations();
            AddMapItems();
            ShowMyLocationOnTheMap();
        }

        private void AddMapItems()
        {
            _mapPanelItems=new List<MapPanelItem>();
            _mapPanelItems.Add(new MapPanelItem() {Label = "Приблизить",Symbol = 57609,Command = new MvxCommand(()=>ZoomPlus())});
            _mapPanelItems.Add(new MapPanelItem() { Label = "Отдалить", Symbol = 57608, Command = new MvxCommand(() => ZoomMinus()) });
            _mapPanelItems.Add(new MapPanelItem() { Label = "Положение", Symbol = 57810,Command = new MvxCommand(()=>ShowMe())});

        }

        private async void ShowMe()
        {
            var position = await _positionReceiver.GetCurrentCoordinates();
            _messenger.Publish(new SetMyPositionMessage(this, position));
        }

        private async void ShowMyLocationOnTheMap()
        {
            var position=await _positionReceiver.GetCurrentCoordinates();
            _messenger.Publish(new SetMyPositionMessage(this, position));
        }

        private void ZoomMinus()
        {
            ZoomLevel=ZoomLevel-0.3;
        }

        private void ZoomPlus()
        {
            ZoomLevel = ZoomLevel + 0.3;
        }


        public List<StationResponse> AllStations
        {
            get { return _allStations; }
            set
            {
                _allStations = value;
                base.RaisePropertyChanged(()=>AllStations);
            }
        }

        public PositionDto CenterPoint
        {
            get { return _centerPoint; }
            set
            {
                _centerPoint = value;
                base.RaisePropertyChanged(()=>CenterPoint);
            }
        }

        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                _zoomLevel = value;
                Debug.WriteLine("Zoom : {0}",value);
                base.RaisePropertyChanged(()=>ZoomLevel);
            }
        }

        public List<MapPanelItem> MapPanelItems
        {
            get { return _mapPanelItems; }
            set
            {
                _mapPanelItems = value;
                base.RaisePropertyChanged(()=>MapPanelItems);
            }
        }

        private async void LoadStations()
        {
            var response = await _apiFacade.GetAllStationsCoordinates();
            var stations = response.Result;
            foreach (var station in stations)
            {
                station.ImageSourceUri = new Uri("ms-appx:///Assets/Pins/mappin.png", UriKind.RelativeOrAbsolute);
            }
            AllStations = stations;
            _messenger.Publish(new AllStationWithLocationMessage(this,AllStations.Select(x=>new StationModel() {Ecr = x.Ecr,ExpressCode = x.ExpressCode,ImageSourceUri = x.ImageSourceUri,Position = new GeoModel() {Latitude = x.Position.Latitude,Longitude = x.Position.Longitude},StationName = x.StationName})));
        }
    }
}
