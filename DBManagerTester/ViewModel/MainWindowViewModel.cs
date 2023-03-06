using DBManager.Interfacce;
using DBManager.Models;
using DBManagerTester.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DBManagerTester.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static readonly GestioneMagazzinoContext dbContext = new GestioneMagazzinoContext();
        private readonly DrinkIngredientsRepository DrinkIngredientsRepository = new DrinkIngredientsRepository(dbContext);
        private readonly IngredientsRepository IngredientsRepository = new IngredientsRepository(dbContext);
        private readonly MenusRepository MenusRepository = new MenusRepository(dbContext);
        private readonly MenuPreparationsRepository MenuPreparationsRepository = new MenuPreparationsRepository(dbContext);
        private readonly SuppliersRepository SuppliersRepository = new SuppliersRepository(dbContext);
        private readonly UseMaterialsRepository UseMaterialsRepository = new UseMaterialsRepository(dbContext);

        private ObservableCollection<string> displayData { get; set; }
        public ObservableCollection<string> DisplayData
        {
            get { return displayData; }
            set { displayData = value; NotifyPropertyChanged(); }
        }

        public TypeOfData ImportType { get; set; } = TypeOfData.DrinkIngredient;
        public TypeOfData ShowType { get; set; } = TypeOfData.DrinkIngredient;


        public MainWindowViewModel()
        {
            
            DisplayData = new ObservableCollection<string>();
        }

        public void ImportCSV(string uri)
        {
            string result = "";
            switch (ImportType)
            {
                case TypeOfData.DrinkIngredient:
                    result = DrinkIngredientsRepository.InsertFromCSV(uri);
                    break;
                case TypeOfData.Ingredient:
                    result = IngredientsRepository.InsertFromCSV(uri);
                    break;
                case TypeOfData.Menu:
                    result = MenusRepository.InsertFromCSV(uri);
                    break;
                case TypeOfData.MenuPreparation:
                    result = MenuPreparationsRepository.InsertFromCSV(uri);
                    break;
                case TypeOfData.Supplier:
                    result = SuppliersRepository.InsertFromCSV(uri);
                    break;
                case TypeOfData.UseMaterial:
                    result = UseMaterialsRepository.InsertFromCSV(uri);
                    break;
            }
            
            if (result != "Succeded")
            {
                MessageBox.Show("Error importing data: " + result);
            }
            else
            {
                MessageBox.Show("Import " + ImportType + " Succesful !");
            }
        }

        public void UpdateData()
        {
            List<string> data = new List<string>();
            switch (ShowType)
            {
                case TypeOfData.DrinkIngredient:
                    foreach (DrinkIngredient x in DrinkIngredientsRepository.GetAll())
                    {
                        data.Add(x.ToString());
                    }
                    break;
                case TypeOfData.Ingredient:
                    foreach (Ingredient x in IngredientsRepository.GetAll())
                    {
                        data.Add(x.ToString());
                    }
                    break;
                case TypeOfData.Menu:
                    foreach (Menu x in MenusRepository.GetAll())
                    {
                        data.Add(x.ToString());
                    }
                    break;
                case TypeOfData.MenuPreparation:
                    foreach (MenuPreparation x in MenuPreparationsRepository.GetAll())
                    {
                        data.Add(x.ToString());
                    }
                    break;
                case TypeOfData.Supplier:
                    foreach (Supplier x in SuppliersRepository.GetAll())
                    {
                        data.Add(x.ToString());
                    }
                    break;
                case TypeOfData.UseMaterial:
                    foreach (UseMaterial x in UseMaterialsRepository.GetAll())
                    {
                        data.Add(x.ToString());
                    }
                    break;
            }

            DisplayData = new ObservableCollection<string>(data);
        }

    }
}
