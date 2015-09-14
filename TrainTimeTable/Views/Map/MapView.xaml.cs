using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.WindowsCommon.Views;
using TrainTimeTable.Shared.Messages;
using TrainTimeTable.Shared.Models.Map;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TrainTimeTable.Views.Map
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapView : MvxWindowsPage
    {
        private MvxSubscriptionToken _token;
        private MapManager _mapMapManager;
        private MvxSubscriptionToken _token2;

        public MapView()
        {
            this.InitializeComponent();
            var messenger=Mvx.Resolve<IMvxMessenger>();
            _mapMapManager=new MapManager(this.MapControl);
           _token= messenger.Subscribe<AllStationWithLocationMessage>(OnGeoListReceived);
            _token2 = messenger.Subscribe<SetMyPositionMessage>(SetMyPositionOnMap);
        }

        private void SetMyPositionOnMap(SetMyPositionMessage obj)
        {
            _mapMapManager.SetMyPosition(obj.Position);

        }

        public MapManager MapManager
        {
            get { return _mapMapManager; }
            set
            {
                _mapMapManager = value;
                
            }
        }

        private void OnGeoListReceived(AllStationWithLocationMessage obj)
        {
            _mapMapManager.AddStationsToMap(obj.Stations);
        }

       
    }
}
