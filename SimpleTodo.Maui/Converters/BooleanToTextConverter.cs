using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTodo.Maui.Converters
{
    public class BooleanToTextConverter : IValueConverter
    {
        public class BooleanToImageConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is bool isCompleted)
                {
                    var parameters = parameter.ToString().Split(',');
                    return isCompleted ? parameters[0] : parameters[1];
                }

                return null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                var parameters = parameter.ToString().Split(',');
                return boolValue ? parameters[0] : parameters[1];
            }

            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
