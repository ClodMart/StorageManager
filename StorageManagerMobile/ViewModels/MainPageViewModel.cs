using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.Services;
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
        #region Properties
        private readonly StorageManagerDBContext context = DBService.Instance.DbContext;

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

        private SuppliersViewModel suppliersViewModel;
        public SuppliersViewModel SuppliersViewModel
        {
            get { return suppliersViewModel; }
            set { suppliersViewModel = value;
            NotifyPropertyChanged();}
        }

        private IngredientsViewModel ingredientsViewModel;
        public IngredientsViewModel IngredientsViewModel
        {
            get { return ingredientsViewModel; }
            set { ingredientsViewModel = value; NotifyPropertyChanged(); }
        }

        private NewOrderViewModel newOrderViewModel;
        public NewOrderViewModel NewOrderViewModel
        {
            get { return newOrderViewModel; }
            set { newOrderViewModel = value; NotifyPropertyChanged(); }
        }

        private ProductsViewModel productsViewModel;
        public ProductsViewModel ProductsViewModel
        {
            get { return productsViewModel; }
            set { productsViewModel = value; NotifyPropertyChanged(); }
        }

        private List<PageLink> menuList;
        public List<PageLink> MenuList { get { return menuList; } set { menuList = value; NotifyPropertyChanged(); } }

        private List<Ingredient> ingredientList;
        public List<Ingredient> IngredientList
        {
            get { return ingredientList; }
            set { ingredientList = value; NotifyPropertyChanged(); }
        }

        #endregion

        public MainPageViewModel()
        {
            MenuList = new List<PageLink>()
            {
                new PageLink("Home","HomePage"),
                new PageLink("Fornitori", "SuppliersPage"),
                new PageLink("Ingredienti", "Ingredients"),
                new PageLink("Prodotti", "Products"),
                new PageLink("Nuovo Ordine", "NewOrder"), 
            };
            MenuViewModel = new FlyoutMenuViewModel(MenuList.Select(x => x.Label).ToList());

            //IngredientList = context.Ingredients.ToList();
            IngredientsViewModel = new IngredientsViewModel();
            SuppliersViewModel = new SuppliersViewModel();
            NewOrderViewModel = new NewOrderViewModel();
            ProductsViewModel = new ProductsViewModel();

            InizializeModel();

        }

        //Inizialize the fyoutmenu viewmodel
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
                else if(newPage is SuppliersPage)
                {
                    newPage.BindingContext = SuppliersViewModel;
                }
                else if(newPage is NewOrder)
                {
                    newPage.BindingContext = NewOrderViewModel;
                }
                else if (newPage is Products)
                {
                    newPage.BindingContext = ProductsViewModel;
                }
                Pages.Add(newPage);
            }
        }
    }
}
