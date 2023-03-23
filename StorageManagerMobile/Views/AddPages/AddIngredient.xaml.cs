using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Add;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Popup;

namespace StorageManagerMobile.Views.AddPages;

public partial class AddIngredient : ContentPage
{
    private SupplierSelectionViewModel VM = new SupplierSelectionViewModel();

    public AddIngredient()
	{
		InitializeComponent();
	}

    private async Task OpenSupplierSelectionPopupAsync()
    {
        var popup = new SupplierSelection();
        popup.BindingContext = VM;
        var result = await this.ShowPopupAsync(popup);
        if (result==null)
        {
            await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
            //result = await this.ShowPopupAsync(popup);
        }
        else
        {
            IngredientsFormat res = (IngredientsFormat)result;
            ((AddIngredientViewModel)BindingContext).Formats.Add(res);
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        OpenSupplierSelectionPopupAsync();
    }
}