using System;
using Windows.UI.Xaml.Data;

namespace TrainTimeTable.Converters
{
    public class SecundsToMinutesConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var d = (double) value;
            return String.Format("{0} м", d/60);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
