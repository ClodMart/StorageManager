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
    public class IngredientSelectionViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(context);
        private static readonly IngredientsFormatsRepository ingredientsFormatsRepository = new IngredientsFormatsRepository(context);
        private static readonly ProductCompositionsRepository ProductCompositionsRepository = new ProductCompositionsRepository(context);

        private List<Ingredient> AllIngredients { get; set; }

        private Product product;
        public Product Product { get { return product; } set { product = value; NotifyPropertyChanged(); } }

        private Ingredient ingredient;
        public Ingredient Ingredient { get { return ingredient; } set { ingredient = value; NotifyPropertyChanged(); } }

        private ObservableCollection<Ingredient> ingredients;
        public ObservableCollection<Ingredient> Ingredients { get { return ingredients; } set { ingredients = value; NotifyPropertyChanged(); } }

        private double quantity;
        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; NotifyPropertyChanged(); }
        }

        public IngredientSelectionViewModel(Product prod)
        {
            AllIngredients = IngredientsRepository.GetAll().ToList();
            Ingredients = new ObservableCollection<Ingredient>(AllIngredients);
            Product = prod;
        }

        public IngredientSelectionViewModel()
        {
            AllIngredients = IngredientsRepository.GetAll().ToList();
            Ingredients = new ObservableCollection<Ingredient>(AllIngredients);
            Product = new Product();
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
        }

        public double GetCost()
        {
            double cost = 0;
            IngredientsFormat format = ingredientsFormatsRepository.GetFormatsFromIngredientId(Ingredient.Id).FirstOrDefault(x => x.IsDefault);
            if (format != null) 
            {
                cost = (double)format.Cost * Quantity;
            }
            return cost;
        }
    }
}
