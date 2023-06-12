using CommunityToolkit.Maui.Core.Views;
using DBManager.Interfacce;
using DBManager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels.Add
{
    public class AddSupplierViewModel : BaseViewModel
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly SuppliersRepository SuppliersRepository = new SuppliersRepository(context);
        private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);



        private Supplier supplier;
        public Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; NotifyPropertyChanged(); }
        }

        public AddSupplierViewModel()
        {
            Supplier= new Supplier();
        }

        public long SaveSupplier()
        {
            if(Supplier != null && Supplier.SupplierName != null)
            {
                long SupId = SuppliersRepository.Add(Supplier);
                IngredientsFormatsRepository.AddAll(Supplier.IngredientsFormats.ToList());
                return SupId;
            }
            else
            {
                //await DisplayAlert("Attenzione", "Impossibile salvare il fornitore, prego inserire nuovi dati", "Ok");
                return -1;
            }
        }

        public void SaveIngredientFormat(IngredientsFormat Ingredient)
        {
            Supplier.IngredientsFormats.Add(Ingredient);

            //Update View
            Supplier New = Supplier;
            Supplier= new Supplier();
            Supplier = New;

            //IngredientsFormatsRepository.Add(Ingredient);
            //context.SaveChanges();
            //GetSupplierFromDB();
        }
    }
}
