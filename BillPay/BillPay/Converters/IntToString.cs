using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BillPay.Converters
{
    class IntToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inInt = (int) value;
            return inInt.ToString();
        } 

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inString = (string)value;
            return int.Parse(inString);
        }
    }
}
