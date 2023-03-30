using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Orders.Popup;

namespace StorageManagerMobile.Views.Orders;

public partial class OrderCategoryDetails : ContentPage
{

	public OrderCategoryDetails()
	{
		InitializeComponent();
	}

    private async Task OpenIngredientAddPopupAsync()
    {
        var popup = new AddIngredientToOrderList();
        AddIngredientToOrderListViewModel VM = new AddIngredientToOrderListViewModel(((OrderCategoryDetailsViewModel)BindingContext).Cat);
        popup.BindingContext = VM;
        var result = await this.ShowPopupAsync(popup);
        if (result == null)
        {
            await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
            //result = await this.ShowPopupAsync(popup);
        }
        else
        {
            ((OrderCategoryDetailsViewModel)BindingContext).AddCategoryIngredient((Ingredient)result);
            ((OrderCategoryDetailsViewModel)BindingContext).RefreshIngredientList();
        }
    }

    private void AddIngredient_Clicked(object sender, EventArgs e)
    {
        OpenIngredientAddPopupAsync();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ((OrderCategoryDetailsViewModel)BindingContext).RefreshIngredientList();
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        ((OrderCategoryDetailsViewModel)BindingContext).SaveNewCategory();
    }
}