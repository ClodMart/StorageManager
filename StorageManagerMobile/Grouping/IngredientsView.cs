using DBManager.Models;
using StorageManagerMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace StorageManagerMobile.Grouping
{

    public class IngredientsView : ObservableCollection<Ingredient>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Ingredient Title { get; set; }
        private ObservableCollection<Ingredient> ingredients;
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; NotifyPropertyChanged(); }
        }

        private readonly List<Ingredient> items;

        private void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool showlist;
        public bool ShowList
        {
            get { return showlist; }
            set { showlist = value; NotifyPropertyChanged(); }
        }

        public ICommand ChangeVisibility => new Command(() =>
        {
            ChangeVIs();
        });


        public IngredientsView(Ingredient title, ObservableCollection<Ingredient> ing)
        {
            items = ing.ToList();
            ShowList = false;
            Title = title;
        }

        private void ChangeVIs()
        {
            ShowList = !ShowList;
            if (showlist)
            {
                Ingredients = new ObservableCollection<Ingredient>(items);
            }
            else
            {
                Ingredients.Clear();
            }
            

        }
    }
}
