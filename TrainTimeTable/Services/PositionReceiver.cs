using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.Shared.Services;

namespace TrainTimeTable.Services
{
    public class PositionReceiver:IPositionReceiver
    {
        public async Task<PositionDto> GetCurrentCoordinates()
        {
            var myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            return new PositionDto() {Latitude = myGeocoordinate.Latitude, Longitude = myGeocoordinate.Longitude};
        }
    }
}
