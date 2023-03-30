using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StorageManagerMobile.ViewModels.Popup
{
    public class AddIngredientToOrderListViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);
        private static readonly CategoryIngredientListsRepository CategoryIngredientListsRepository = new CategoryIngredientListsRepository(context);

        private List<Ingredient> AllIngredients { get; set; }

        private OrderCategory cat;
        public OrderCategory Cat { get { return cat; } set { cat = value; NotifyPropertyChanged(); } }

        private Ingredient ingredient;
        public Ingredient Ingredient { get { return ingredient; } set { ingredient = value; NotifyPropertyChanged(); } }

        private ObservableCollection<Ingredient> ingredients;
        public ObservableCollection<Ingredient> Ingredients { get { return ingredients; } set { ingredients = value;NotifyPropertyChanged(); } }

        public AddIngredientToOrderListViewModel(OrderCategory cat)
        {
            AllIngredients = IngredientsRepository.GetAll().ToList();
            Ingredients = new ObservableCollection<Ingredient>(AllIngredients);
            Cat = cat;
        }

        public ICommand PerformSearch => new Command<string>((query) =>
        {
            Search(query);
        });

        public void Search(string query)
        {
            List<Ingredient> results = new List<Ingredient>();
            if (query == "")
            {
                results = AllIngredients;
            }
            else
            {
                foreach (Ingredient Ingredient in AllIngredients)
                {
                    if (Ingredient.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || Ingredient.Category.Contains(query, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(Ingredient);
                    }
                }
            }

            results.Sort((l, r) => l.Name.CompareTo(r.Name));
            Ingredients = new ObservableCollection<Ingredient>(results);
            //CollapseExpanders();
        }
    }
}
