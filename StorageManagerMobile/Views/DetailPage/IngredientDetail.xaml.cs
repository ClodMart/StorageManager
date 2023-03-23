using CommunityToolkit.Maui.Views;
using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Popup;

namespace StorageManagerMobile.Views.DetailPage;

public partial class IngredientDetail : ContentPage
{
    SupplierSelectionViewModel VM;

    public IngredientDetail()
	{
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }

    private async Task OpenSupplierSelectionPopupAsync()
    {
        var popup = new SupplierSelection();
        if(VM == null)
        {
            VM = new SupplierSelectionViewModel(((IngredientDetailViewModel)BindingContext).Title);
        }
        popup.BindingContext = VM;
        var result = await this.ShowPopupAsync(popup);
        if(result == null)
        {
            await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
            //result = await this.ShowPopupAsync(popup);
        }
        else
        {
            IngredientsFormat res = (IngredientsFormat)result;
            res.Ingredient = ((IngredientDetailViewModel)BindingContext).Title;
            res.IngredientId = ((IngredientDetailViewModel)BindingContext).Title.Id;
            ((IngredientDetailViewModel)BindingContext).SaveIngredientFormat(res);
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        OpenSupplierSelectionPopupAsync();
    }
}