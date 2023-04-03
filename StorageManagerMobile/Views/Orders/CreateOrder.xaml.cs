using DBManager.Models;
using StorageManagerMobile.ViewModels;
using System.Text;

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
            DisplayAlertAsync(((CreateOrderViewModel)BindingContext).Missed) ;
        }
    }

    private async Task DisplayAlertAsync(List<Ingredient> IgList)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Alcuni ingredienti non hanno un fornitore di default quindi non sono stati caricati:");
        foreach(Ingredient x in IgList)
        {
            sb.AppendLine("- " + x.Name+", "+x.Category);
        }
        await DisplayAlert("Ingredienti non caricati", sb.ToString(), "Ok");
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

    //private void NumberPicker_Focused(object sender, FocusEventArgs e)
    //{
    //    OrderItem Current = (OrderItem)((Picker)sender).BindingContext;
    //    Current.IsFocused = false;
    //}

    private void NumberPicker_TextChanged(object sender, TextChangedEventArgs e)
    {
        OrderItem Current = (OrderItem)((Entry)sender).BindingContext;
        ((CreateOrderViewModel)BindingContext).UpdateListing(Current);
    }
}