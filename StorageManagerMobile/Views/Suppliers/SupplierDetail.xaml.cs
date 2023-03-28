using CommunityToolkit.Maui.Views;
using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Suppliers.Popup;
using System.Diagnostics;
using System.Text;

namespace StorageManagerMobile.Views.Suppliers;

public partial class SupplierDetail : ContentPage
{
    private FormatSelectionViewModel VM = new FormatSelectionViewModel();

	public SupplierDetail()
	{
		InitializeComponent();
	}

    private async void Elimina_Clicked(object sender, EventArgs e)
    {
        if (((SupplierDetailsViewModel)BindingContext).Supplier.IngredientsFormats.Count > 0)
        {
            bool answer = await DisplayAlert("Fornitore usato !\n", "\nIl fornitore è usato da: " + ((SupplierDetailsViewModel)BindingContext).Supplier.IngredientsFormats.Count() + " Ingredienti. \n\nProseguendo queste entry verranno eliminate, sei sicuro ?", "Si", "No");
            if (answer)
            {
                ((SupplierDetailsViewModel)BindingContext).DeleteSupplier();
            }
            else
            {
                //Do Nothing
            }
        }
        else
        {
            ((SupplierDetailsViewModel)BindingContext).DeleteSupplier();
        }
        Navigation.PopAsync();
    }

    private void Salva_Clicked(object sender, EventArgs e)
    {
        ((SupplierDetailsViewModel)BindingContext).SaveSupplier();
        Navigation.PopAsync();
    }

    private void DeleteFormat_Clicked(object sender, EventArgs e)
    {
        IngredientsFormat Current = (IngredientsFormat)((ImageButton)sender).BindingContext;
        ((SupplierDetailsViewModel)BindingContext).DeleteFormat(Current);
    }

    private async Task OpenFormatSelectionPopupAsync()
    {
        var popup = new AddFormat();
        if (VM == null)
        {
            VM = new FormatSelectionViewModel();
        }
        popup.BindingContext = VM;
        var result = await this.ShowPopupAsync(popup);
        if (result == null)
        {
            await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
        }
        else
        {
            IngredientsFormat res = (IngredientsFormat)result;
            res.Supplier = ((SupplierDetailsViewModel)BindingContext).Supplier;
            res.SupplierId = ((SupplierDetailsViewModel)BindingContext).Supplier.Id;
            ((SupplierDetailsViewModel)BindingContext).SaveIngredientFormat(res);
            VM = new FormatSelectionViewModel();
        }
    }

    private void AddFormat_Clicked(object sender, EventArgs e)
    {
        OpenFormatSelectionPopupAsync();
    }
}