using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Messenger;
using TrainTimeTable.Shared.Models.Map;

namespace TrainTimeTable.Shared.Messages
{
    public class AllStationWithLocationMessage:MvxMessage
    {
        private readonly IEnumerable<StationModel> _stations;

        public AllStationWithLocationMessage(object sender, IEnumerable<StationModel> stations)
            :base(sender)
        {
            _stations = stations;
        }

        public IEnumerable<StationModel> Stations
        {
            get { return _stations; }
        }
    }
}
