using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using StorageManagerMobile.Views.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Details
{
    internal class OrderCategoryDetailsViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly CategoryIngredientListsRepository CategoryIngredientListsRepository = new CategoryIngredientListsRepository(context);
        private static readonly OrderCategoriesRepository OrderCategoriesRepository = new OrderCategoriesRepository(context);

        private OrderCategory cat;
        public OrderCategory Cat { get { return cat; } set { cat = value; NotifyPropertyChanged(); } }

        private bool newcategory;
        public bool NewCategory
        {
            get { return newcategory; }
            set { newcategory = value; NotifyPropertyChanged();}
        }

        private string title;
        public string Title 
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged(); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<CategoryIngredientList> ingredientList;
        public ObservableCollection<CategoryIngredientList> IngredientList 
        { 
            get { return ingredientList;} 
            set { ingredientList = value; NotifyPropertyChanged(); } 
        }

        public OrderCategoryDetailsViewModel()
        {
            NewCategory = true;
            Cat.Id = OrderCategoriesRepository.GetNextId();
            IngredientList = new ObservableCollection<CategoryIngredientList>(new List<CategoryIngredientList>());
        }

        public OrderCategoryDetailsViewModel(long CategoryId)
        {
            Cat = OrderCategoriesRepository.GetById(CategoryId);
            Title = Cat.Name; 
            Description = Cat.Description;
            NewCategory = false;
            IngredientList = new ObservableCollection<CategoryIngredientList>(CategoryIngredientListsRepository.GetFromCategory_Id(CategoryId));
        }

        public void RefreshIngredientList()
        {
            IngredientList.Clear();
                IngredientList = new ObservableCollection<CategoryIngredientList>(CategoryIngredientListsRepository.GetFromCategory_Id(Cat.Id));          
        }

        public void AddCategoryIngredient(Ingredient Ing)
        {
            CategoryIngredientList Ingredient = new CategoryIngredientList();
            Ingredient.CategoryId = Cat.Id;
            Ingredient.IngredientId = Ing.Id;
            Ingredient.Ingredient = Ing;
            Ingredient.Category = Cat;

            CategoryIngredientListsRepository.Add(Ingredient);
        }

        public void SaveNewCategory()
        {
            Cat.Name = Title;
            Cat.Description = Description;
            OrderCategoriesRepository.Add(Cat);
        }

    }
}
