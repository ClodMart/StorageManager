using CommunityToolkit.Maui.Views;
using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Popup;

namespace StorageManagerMobile.Views.DetailPage;

public partial class IngredientDetail : ContentPage
{
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
        popup.BindingContext = new SupplierSelectionViewModel(((IngredientDetailViewModel)BindingContext).Title);
        var result = await this.ShowPopupAsync(popup);
        while(result == null)
        {
            await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
            result = await this.ShowPopupAsync(popup);
        }
        IngredientsFormat res = (IngredientsFormat)result;
        res.Ingredient = ((IngredientDetailViewModel)BindingContext).Title;
        res.IngredientId = ((IngredientDetailViewModel)BindingContext).Title.Id;
        ((IngredientDetailViewModel)BindingContext).SaveIngredientFormat(res);
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        OpenSupplierSelectionPopupAsync();
    }
}