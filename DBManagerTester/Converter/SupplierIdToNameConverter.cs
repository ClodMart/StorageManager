using DBManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DBManagerTester.Converter
{
    public class SupplierIdToNameConverter : IValueConverter
    {
        private GestioneMagazzinoContext context = new GestioneMagazzinoContext(); 

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return context.Suppliers.FirstOrDefault(x => x.Id == (int)value).SupplierName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
