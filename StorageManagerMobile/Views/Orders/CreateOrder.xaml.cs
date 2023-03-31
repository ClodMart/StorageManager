using DBManager.Models;
using StorageManagerMobile.ViewModels;

namespace StorageManagerMobile.Views.Orders;

public partial class CreateOrder : ContentPage
{
	public CreateOrder()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (((CreateOrderViewModel)BindingContext).ItemsMissed)
        {
            DisplayAlertAsync();
        }
    }

    private async Task DisplayAlertAsync()
    {
        await DisplayAlert("Ingredienti non caricati", "Alcuni ingredienti non hanno un fornitore di default quindi non sono stati caricati", "Ok");
    }

    private void SelectItem_Clicked(object sender, EventArgs e)
    {
        OrderItem Current = (OrderItem)((Grid)sender).BindingContext;
        ((CreateOrderViewModel)BindingContext).SelectItem(Current);
    }

    private void DeselectItem_Clicked(object sender, EventArgs e)
    {
        OrderItem Current = (OrderItem)((Grid)sender).BindingContext;
        ((CreateOrderViewModel)BindingContext).DeselectItem(Current);
    }

    private void NumberPicker_Focused(object sender, FocusEventArgs e)
    {
        OrderItem Current = (OrderItem)((Picker)sender).BindingContext;
        Current.IsFocused = false;
    }
}