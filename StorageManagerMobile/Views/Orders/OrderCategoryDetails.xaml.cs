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
            long res = ((OrderCategoryDetailsViewModel)BindingContext).AddCategoryIngredient((Ingredient)result);
            if (res == -1)
            {
                await DisplayAlert("Alert", "Ingrediente già aggiunto alla lista", "OK");
            }
            else
            {
                ((OrderCategoryDetailsViewModel)BindingContext).RefreshIngredientList();
            }            
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
        //((OrderCategoryDetailsViewModel)BindingContext).SaveNewCategory();
        ((OrderCategoryDetailsViewModel)((Button)sender).BindingContext).SaveNewCategory();
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        CategoryIngredientList current = (CategoryIngredientList)(((ImageButton)sender).BindingContext);
        ((OrderCategoryDetailsViewModel)BindingContext).RemoveCategoryIngredient(current);
        ((OrderCategoryDetailsViewModel)BindingContext).RefreshIngredientList();
    }
}