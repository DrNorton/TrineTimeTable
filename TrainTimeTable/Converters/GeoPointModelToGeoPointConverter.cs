using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Data;
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.Shared.Models.Map;


namespace TrainTimeTable.Converters
{
    public class GeoPointModelToGeoPointConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is PositionDto)
            {
                var val = value as PositionDto;
                return
                    new Geopoint(new BasicGeoposition()
                    {
                        Latitude = (double) val.Latitude,
                        Longitude = (double) val.Longitude
                    });
            }
            else
            {
                var val = value as GeoModel;
                return
                    new Geopoint(new BasicGeoposition()
                    {
                        Latitude = (double)val.Latitude,
                        Longitude = (double)val.Longitude
                    });
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var val = value as Geopoint;
            return new PositionDto() {Longitude = val.Position.Longitude,Latitude = val.Position.Latitude};
        }
    }


}
