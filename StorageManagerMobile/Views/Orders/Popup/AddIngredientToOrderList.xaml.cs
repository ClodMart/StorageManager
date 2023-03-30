namespace StorageManagerMobile.Views.Orders.Popup;

using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Popup;

public partial class AddIngredientToOrderList : Popup
{
	public AddIngredientToOrderList()
	{
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {
        Ingredient Out = ((AddIngredientToOrderListViewModel)BindingContext).Ingredient;
        Close(Out);
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((AddIngredientToOrderListViewModel)BindingContext).Search(e.NewTextValue);
    }
}