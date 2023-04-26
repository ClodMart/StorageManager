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
        //((ProductDetailsViewModel)BindingContext).SaveAll();
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
            //result = await this.ShowPopupAsync(popup);
        }
        else
        {
            //ProductComposition Out = new ProductComposition();
            ProductComposition res = (ProductComposition)result;
            ((ProductDetailsViewModel)BindingContext).AddComposition(res);
            //Out.IngredientId = res.Id;
            //Out.ProductId= ((ProductDetailsViewModel)BindingContext).
            //res.Ingredient = ((IngredientDetailViewModel)BindingContext).Title;
            //res.IngredientId = ((IngredientDetailViewModel)BindingContext).Title.Id;
            //((ProductDetailsViewModel)BindingContext).SaveIngredientFormat(res);
            //VM = new SupplierSelectionViewModel(((IngredientDetailViewModel)BindingContext).Title);
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        OpenCompositionSelectionPopupAsync();
    }

    private void DeleteFormat_Clicked(object sender, EventArgs e)
    {
        //IngredientsFormat actual = (IngredientsFormat)((ImageButton)sender).BindingContext;
        //((IngredientDetailViewModel)BindingContext).RemoveIngredientFormat(actual);
        //((IngredientDetailViewModel)BindingContext).Ingredients.Remove(actual);
    }
}