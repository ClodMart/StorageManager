using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.DataModels.DBDataModel;
using StorageManagerMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StorageManagerMobile.ViewModels.Popup
{
    public class CostChangeViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        //private static readonly IngredientsFormatsRepository ingredientsFormatsRepository = new IngredientsFormatsRepository(context);
        private static readonly DataApiIngredientsGateaway IngredientsGateway = new DataApiIngredientsGateaway();

        private IngredientsFormat format;
        public IngredientsFormat Format { get { return format; } set { format = value; NotifyPropertyChanged(); } }

        private decimal? newCost;
        public decimal? NewCost { get { return newCost; } set { newCost = value; NotifyPropertyChanged(); } }

        public CostChangeViewModel(IngredientsFormat format)
        {
            Format = format;
            NewCost = format.Cost;
        }

        public void UpdateCost()
        {
            Format.ChangePrice(NewCost ?? 0);
        }

        public ICommand SaveModifications => new Command(() =>
        {
            SaveModificationsMethod();
        });

        public void SaveModificationsMethod()
        {
            UpdateCost();
            IngredientsGateway.UpdateFormat(new IngredientFormatTemplate(Format));
            //ingredientsFormatsRepository.Update(Format);
        }
    }
}
