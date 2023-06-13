using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StorageManagerMobile.ViewModels
{
    public class OrderSelectorViewModel :BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly CategoryIngredientListsRepository CategoryIngredientsRepository = new CategoryIngredientListsRepository(context);

        private OrderCategory cat;
        public OrderCategory Cat
        {
            get { return cat; }
            set { cat = value; NotifyPropertyChanged(); }
        }

        private List<CategoryIngredientList> allIngredientsList;
        public List<CategoryIngredientList> AllIngredientsList { get { return allIngredientsList; } set { allIngredientsList = value; NotifyPropertyChanged(); } }

        private ObservableCollection<OrderIngredient> selectedIngredients;
        public ObservableCollection<OrderIngredient> SelectedIngredients { get { return selectedIngredients;} set { selectedIngredients = value;NotifyPropertyChanged(); } }

        private ObservableCollection<OrderIngredient> selectableIngredients;
        public ObservableCollection<OrderIngredient> SelectableIngredients { get { return selectableIngredients;} set { selectableIngredients = value;NotifyPropertyChanged(); } }

        public OrderSelectorViewModel() 
        {
            Cat = new OrderCategory();
            AllIngredientsList = new List<CategoryIngredientList>();
            SelectedIngredients = new ObservableCollection<OrderIngredient>();
            SelectableIngredients=new ObservableCollection<OrderIngredient>();
        }

        public OrderSelectorViewModel(OrderCategory Current)
        {
            Cat = Current;
            long CategoryId = Current.Id;
            List<CategoryIngredientList> CategoryIngredients = CategoryIngredientsRepository.GetFromCategory_Id(CategoryId).ToList();
            AllIngredientsList = new List<CategoryIngredientList>();
            List<OrderIngredient> SelectedList = new List<OrderIngredient>();
            List<OrderIngredient> SelectableList = new List<OrderIngredient>();
            foreach (CategoryIngredientList x in CategoryIngredients)
            {
                AllIngredientsList.Add(x);
                if (x.Selected)
                {
                    SelectedList.Add(new OrderIngredient(x));
                }
                else
                {
                    SelectableList.Add(new OrderIngredient(x));
                }
            }
            SelectedIngredients = new ObservableCollection<OrderIngredient>(SelectedList.OrderBy(x=>x.Ingredient.Category));
            SelectableIngredients = new ObservableCollection<OrderIngredient>(SelectableList.OrderBy(x => x.Ingredient.Category));
        }

        public ICommand Select => new Command<OrderIngredient>((Current) =>
        {
            SelectMethod(Current);
        });

        public void SelectMethod(OrderIngredient ToSelect)
        {
            SelectableIngredients.Remove(ToSelect);
            SelectedIngredients.Add(ToSelect);
            SelectedIngredients.OrderBy(x => x.Ingredient.Category);
            CategoryIngredientList selected = AllIngredientsList.FirstOrDefault(x => x.IngredientId == ToSelect.Ingredient.Id);
            selected.Selected = true;
            AllIngredientsList.FirstOrDefault(x => x.IngredientId == ToSelect.Ingredient.Id).Selected= true;
            CategoryIngredientsRepository.Update(selected);
            
        }

        public ICommand Deseclect => new Command<OrderIngredient>((Current) =>
        {
            DeselectMethod(Current);
        });

        public void DeselectMethod(OrderIngredient ToSelect)
        {
            SelectableIngredients.Add(ToSelect);
            SelectedIngredients.Remove(ToSelect);
            SelectableIngredients.OrderBy(x => x.Ingredient.Category);
            CategoryIngredientList selected = AllIngredientsList.FirstOrDefault(x => x.IngredientId == ToSelect.Ingredient.Id);
            selected.Selected = false;
            AllIngredientsList.FirstOrDefault(x => x.IngredientId == ToSelect.Ingredient.Id).Selected = false;
            CategoryIngredientsRepository.Update(selected);            
        }
    }
}
