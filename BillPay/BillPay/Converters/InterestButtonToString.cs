using BillPay.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BillPay.Converters
{
    class InterestButtonToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var button = value as InterestButton;

            if (button != null)
                return $"Tip({button.Amount})";
            else
                return "";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var amount = (string)value;
                var interestButton = new InterestButton() { Amount = amount };
                return interestButton;
            }
            else
                return new InterestButton();
        }
    }
}
