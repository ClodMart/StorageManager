using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Add;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Suppliers.Popup;

namespace StorageManagerMobile.Views.Suppliers;

public partial class AddSuppliers : ContentPage
{
    private FormatSelectionViewModel VM = new FormatSelectionViewModel();

    public AddSuppliers()
	{
		InitializeComponent();
	}

    private void Salva_Clicked(object sender, EventArgs e)
    {
		if( ((AddSupplierViewModel)BindingContext).SaveSupplier() > 0)
        {
            Navigation.PopAsync();
        }
        else
        {
            DisplayAlert("Attenzione", "Impossibile salvare il fornitore, prego inserire nuovi dati", "Ok");
        }
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
            res.Supplier = ((AddSupplierViewModel)BindingContext).Supplier;
            res.SupplierId = ((AddSupplierViewModel)BindingContext).Supplier.Id;
            ((AddSupplierViewModel)BindingContext).SaveIngredientFormat(res);
            VM = new FormatSelectionViewModel();
        }
    }

    private void AddFormat_Clicked(object sender, EventArgs e)
    {
        OpenFormatSelectionPopupAsync();
    }
}