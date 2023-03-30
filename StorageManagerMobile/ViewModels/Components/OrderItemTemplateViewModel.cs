using DBManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Components
{
    public class OrderItemTemplateViewModel : BaseViewModel
    {
        private int numberOfItems;
        public int NumberOfItems
        {
            get { return numberOfItems; }
            set { numberOfItems = value; NotifyPropertyChanged(); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; NotifyPropertyChanged(); }
        }
        private IngredientsFormat item;
        public IngredientsFormat Item
        {
            get { return item; }
            set { item = value; NotifyPropertyChanged(); }
        }

        private List<int> numbers;
        public List<int> Numbers
        {
            get { return numbers; }
            set { numbers = value; NotifyPropertyChanged(); }
        }

        public OrderItemTemplateViewModel (IngredientsFormat item)
        {
            Item= item;
            NumberOfItems= 1;
            IsSelected= false;
            for(int i = 1; i<11; i++)
            {
                Numbers.Add(i);
            }
        }
    }
}
