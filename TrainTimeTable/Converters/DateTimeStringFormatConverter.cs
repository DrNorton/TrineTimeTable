using System;
using Windows.UI.Xaml.Data;

namespace TrainTimeTable.Converters
{
    public class DateTimeStringFormatConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return null;
            var date = (DateTime) value;
            return date.ToString((string) parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
