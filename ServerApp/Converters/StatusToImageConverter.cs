using System;
using System.Globalization;
using System.Windows.Data;

namespace ServerApp.Converters
{
    /// <summary>
    /// Converter class
    /// </summary>
    class StatusToImageConverter : IValueConverter
    {
        /// <summary>
        /// Gives the path to the icon depending on the status.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (bool.TryParse(value.ToString(), out var res))
                return res ? "../Resources/online.png" : "../Resources/offline.png";

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
