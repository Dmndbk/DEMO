using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_RepairRequests
{
    public class ValueToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = (string)value;
            switch (input)
            {
                case "Высокий":
                    return Brushes.IndianRed;
                case "Средний":
                    return Brushes.LightBlue;
                case "Низкий":
                    return Brushes.LightGreen;
                default:
                    return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
