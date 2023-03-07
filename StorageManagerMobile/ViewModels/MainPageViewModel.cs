using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly GestioneMagazzinoContext context;

        private List<Page> pages = new List<Page>();
        public List<Page> Pages
        {
            get { return pages; }
            set { pages = value; NotifyPropertyChanged(); }
        }

        public FlyoutMenuViewModel MenuViewModel;
        public IngredientsViewModel IngredientsViewModel;

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
                Type PageType = Type.GetType("MauiApp1.Views." + PageName);
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
