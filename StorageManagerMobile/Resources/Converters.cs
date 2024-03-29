﻿using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.Resources
{
    public class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Colors.Green;
            }
            else
            {
                return Colors.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((decimal)value == 0)
            {
                return Colors.Transparent;
            }
            else if ((decimal)value > 0)
            {
                return Colors.Red;
            }
            else
            {
                return Colors.Green;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Colors.OrangeRed;
            }
            else
            {
                return Colors.LightGreen;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToHeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return 70;
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToImageConverterLightmode : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "arrowdown_lightmode.svg" : "arrowup_lightmode.svg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToImageConverterDarkMode : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "arrowdown_darkmode.svg" : "arrowup_darkmode.svg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IntRounder : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((decimal)value, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class DoubleRounder : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class IdToSupplierConverter : IValueConverter
    {
        private readonly StorageManagerDBContext dbContext = DBService.Instance.DbContext;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return dbContext.Suppliers.FirstOrDefault(x => x.Id.Equals((long)value)).SupplierName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IdToProductConverter : IValueConverter
    {
        private readonly StorageManagerDBContext dbContext = DBService.Instance.DbContext;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return dbContext.Products.FirstOrDefault(x => x.Id.Equals((long)value)).ProductName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IdToIngredientConverter : IValueConverter
    {
        private readonly StorageManagerDBContext dbContext = DBService.Instance.DbContext;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return dbContext.Ingredients.FirstOrDefault(x => x.Id.Equals((long)value)).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsUsedValueConverter : IValueConverter
    {
        private readonly StorageManagerDBContext dbContext = DBService.Instance.DbContext;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Out = dbContext.IsUsedValues.FirstOrDefault(x => x.Id.Equals((long)value)).Description;
            //if(Out.Contains(" "))
            //{
            //    Out = Out.Replace(" ", "\n");
            //}
            //else if (Out.Length > 5)
            //{
            //    Out = Out.Substring(0, 5);
            //}
            return Out;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                long Out = dbContext.IsUsedValues.FirstOrDefault(x => x.Description.Equals((string)value)).Id;
                return Out;
            }
            else
            {
                return null;
            }           
        }
    }

    public class DatetimeToDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            if (value != null)
            {
                return ((DateOnly)value);
            }
            else
            {
                return "NoOrderDoneYet";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BoolReverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IngredientFormatToPriceColor : IValueConverter
    {
        private readonly StorageManagerDBContext dbContext = DBService.Instance.DbContext;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IngredientsFormat inf = dbContext.IngredientsFormats.FirstOrDefault(x=>x.Id == (long)value);
            if (((DateOnly.FromDateTime(DateTime.Now)).DayNumber - (inf.LastPriceChange ?? DateOnly.MinValue).DayNumber) < 30)
            {
                if (inf.CostDifference == 0)
                {
                    return Colors.Transparent;
                }
                else if (inf.CostDifference > 0)
                {
                    return Colors.Red;
                }
                else
                {
                    return Colors.Green;
                }
            }
            else
            {
                return Colors.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class SupplierChecker : IValueConverter
    {
        private readonly StorageManagerDBContext dbContext = DBService.Instance.DbContext;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return "Seleziona Fornitore";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}
