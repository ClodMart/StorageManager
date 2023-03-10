using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly GestioneMagazzinoContext context = new GestioneMagazzinoContext();

        private List<Page> pages = new List<Page>();
        public List<Page> Pages
        {
            get { return pages; }
            set { pages = value; NotifyPropertyChanged(); }
        }

        private FlyoutMenuViewModel menuViewModel;
        public FlyoutMenuViewModel MenuViewModel
        {
            get { return menuViewModel; }
            set { menuViewModel = value; NotifyPropertyChanged();}
        }

        private IngredientsViewModel ingredientsViewModel;
        public IngredientsViewModel IngredientsViewModel
        {
            get { return ingredientsViewModel; }
            set { ingredientsViewModel = value; NotifyPropertyChanged(); }
        }

        private List<PageLink> menuList;
        public List<PageLink> MenuList { get { return menuList; } set { menuList = value; NotifyPropertyChanged(); } }

        private List<Ingredient> ingredientList;
        public List<Ingredient> IngredientList
        {
            get { return ingredientList; }
            set { ingredientList = value; NotifyPropertyChanged(); }
        }

        public MainPageViewModel()
        {
            MenuList = new List<PageLink>()
            {
                new PageLink("Home","HomePage"),
                new PageLink("Ingredienti", "Ingredients"),
                new PageLink("Prodotti", "PaginaProdotti"),
                new PageLink("Fornitori", "PaginaFornitori"),
            };
            MenuViewModel = new FlyoutMenuViewModel(MenuList.Select(x => x.Label).ToList());

            IngredientList = context.Ingredients.ToList();
            IngredientsViewModel = new IngredientsViewModel(IngredientList);

            
            InizializeModel();

        }

        private void InizializeModel()
        {
            foreach (PageLink x in MenuList)
            {
                string PageName = x.Page;
                Type PageType = Type.GetType(Assembly.GetCallingAssembly().GetName().Name +".Views." + PageName);
                Page newPage;
                try
                {
                    newPage = (Page)Activator.CreateInstance(PageType);
                }
                catch
                {
                    break;
                }

                if (newPage is Ingredients)
                {
                    newPage.BindingContext = IngredientsViewModel;
                }
                Pages.Add(newPage);
            }
        }
    }
}
