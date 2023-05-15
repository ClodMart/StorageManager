using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Add;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Product.Popup;

namespace StorageManagerMobile.Views.Product;

public partial class AddProduct : ContentPage
{
    IngredientSelectionViewModel VM = new IngredientSelectionViewModel();

    public AddProduct()
	{
		InitializeComponent();
	}

    private async Task OpenCompositionSelectionPopupAsync()
    {
        var popup = new IngredientSelection();
        VM = new IngredientSelectionViewModel(((AddProductViewModel)BindingContext).Product);
        popup.BindingContext = VM;
        var result = await this.ShowPopupAsync(popup);
        if (result == null)
        {
            await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
        }
        else
        {
            ProductComposition res = (ProductComposition)result;
            ((AddProductViewModel)BindingContext).AddComposition(res);
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        OpenCompositionSelectionPopupAsync();
    }

    private void DeleteFormat_Clicked(object sender, EventArgs e)
    {

    }
    private void Save_Clicked(object sender, EventArgs e)
    {
        if (((AddProductViewModel)BindingContext).Product.ProductName != null && ((AddProductViewModel)BindingContext).Product.ProductCategory != null && ((AddProductViewModel)BindingContext).Product.ProductPrice != null)
        {
            Navigation.PopAsync();
        }
        else
        {
            DisplayAlertAsync();
        }            
    }

    private async Task DisplayAlertAsync()
    {
        await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
    }
}