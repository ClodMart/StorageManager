using CommunityToolkit.Maui.Storage;
using DBManager.Models;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.Views.Orders;

namespace StorageManagerMobile.Views;

public partial class NewOrder : ContentPage
{
    IFileSaver fileSaver;
	public NewOrder()
	{
		InitializeComponent();
	}

    private void EditIngredientGroup_Clicked(object sender, EventArgs e)
    {
        OrderCategory Currrent = (OrderCategory)((ImageButton)sender).BindingContext;
        OrderCategoryDetails newPage = new OrderCategoryDetails();
        newPage.BindingContext = new OrderCategoryDetailsViewModel(Currrent.Id);
        Navigation.PushAsync(newPage);
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        OrderCategoryDetails newPage = new OrderCategoryDetails();
        Navigation.PushAsync(newPage);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //fileSaver = this.Handler.MauiContext.Services.GetServices<IFileSaver>().FirstOrDefault();
        CreateOrder newPage = new CreateOrder();
        OrderCategory Currrent = (OrderCategory)((Button)sender).BindingContext;
        CreateOrderViewModel VM = new CreateOrderViewModel(Currrent);
        newPage.BindingContext = VM;
        Navigation.PushAsync(newPage);
    }
}