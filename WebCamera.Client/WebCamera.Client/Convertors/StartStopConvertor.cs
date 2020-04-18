using System;
using System.Globalization;
using Xamarin.Forms;

namespace WebCamera.Client.UI.Convertors
{
    public class StartStopConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cast = value as bool?;
            if (cast != null)
            {
                if (cast.Value == true)
                {
                    return "Stop";
                }
                else
                {
                    return "Start";
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
