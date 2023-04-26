namespace StorageManagerMobile.Views.Product.Popup;
using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.ViewModels.Popup;

public partial class IngredientSelection : Popup
{
	public IngredientSelection()
	{
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {
        ProductComposition Out = new ProductComposition();
        Out.Ingredient = ((IngredientSelectionViewModel)BindingContext).Ingredient;
        Out.IngredientId = ((IngredientSelectionViewModel)BindingContext).Ingredient.Id;
        Out.ProductId = ((IngredientSelectionViewModel)BindingContext).Product.Id;
        Out.Product = ((IngredientSelectionViewModel)BindingContext).Product;
        Out.Quantity = ((IngredientSelectionViewModel)BindingContext).Quantity;
        Out.Cost = ((IngredientSelectionViewModel)BindingContext).GetCost();
        //Ingredient Out = ((IngredientSelectionViewModel)BindingContext).Ingredient;

        Close(Out);
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((IngredientSelectionViewModel)BindingContext).Search(e.NewTextValue);
    }
}