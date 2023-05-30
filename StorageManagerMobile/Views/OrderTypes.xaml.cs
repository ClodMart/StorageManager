using CommunityToolkit.Maui.Storage;
using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.Views.Orders;

namespace StorageManagerMobile.Views;

public partial class OrderTypes : ContentPage
{
    public OrderTypes()
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
        //CreateOrder newPage = new CreateOrder();
        //OrderCategory Currrent = (OrderCategory)((Button)sender).BindingContext;
        //CreateOrderViewModel VM = new CreateOrderViewModel(Currrent);
        //newPage.BindingContext = VM;
        //Navigation.PushAsync(newPage);

        OrderSelector newPage = new OrderSelector();
        OrderCategory Currrent = (OrderCategory)((Button)sender).BindingContext;
        OrderSelectorViewModel VM = new OrderSelectorViewModel(Currrent);
        newPage.BindingContext = VM;
        Navigation.PushAsync(newPage);

        //OrderManager newPage = new OrderManager();
        //OrderCategory Currrent = (OrderCategory)((Button)sender).BindingContext;
        ////List<OrdersList> List = OrderLists.GetListByCategory(Currrent.Id);
        //OrderManagerViewModel VM = new OrderManagerViewModel(Currrent);
        //newPage.BindingContext = VM;
        //Navigation.PushAsync(newPage);
    }
}