using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.Resources
{
    public class PageLink
    {
        public string Label { get; set; }
        public string Page { get; set; }

        public PageLink(string label, string page)
        {
            Label = label; Page = page;
        }
    }

    public class QuantityObject
    {
        public int QuantityNeeded { get; set; }
        public int ActualQuantity { get; set; }

        public QuantityObject(int quantityNeeded, int actualQuantity) 
        {
            QuantityNeeded = quantityNeeded;
            ActualQuantity = actualQuantity;
        }
    }

    public static class UsedValues
    {
       private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
       private static readonly IsUsedValuesRepository IsUsedRepository = new IsUsedValuesRepository(context);

       public static readonly ImmutableList<IsUsedValue> IsUsedValues = IsUsedRepository.GetAll().ToImmutableList();
    }
}
