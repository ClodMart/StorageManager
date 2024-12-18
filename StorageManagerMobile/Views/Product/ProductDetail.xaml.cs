using CommunityToolkit.Maui.Views;
using DBManager.Models;
using DevExpress.Data.Browsing;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Popup;
using StorageManagerMobile.Views.Product.Popup;

namespace StorageManagerMobile.Views.Product;

public partial class ProductDetail : ContentPage
{
    IngredientSelectionViewModel VM = new IngredientSelectionViewModel();

    public ProductDetail()
	{
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async Task OpenCompositionSelectionPopupAsync()
    {
        var popup = new IngredientSelection();
        VM = new IngredientSelectionViewModel(((ProductDetailsViewModel)BindingContext).Product);
        popup.BindingContext = VM;
        var result = await this.ShowPopupAsync(popup);
        if (result == null)
        {
            await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
        }
        else
        {
            ProductComposition res = (ProductComposition)result;
            ((ProductDetailsViewModel)BindingContext).AddComposition(res);
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        OpenCompositionSelectionPopupAsync();
    }

    private void DeleteFormat_Clicked(object sender, EventArgs e)
    {

    }
}