using StorageManagerMobile.Resources;
using StorageManagerMobile.ViewModels;

namespace StorageManagerMobile.Views.Orders;

public partial class OrderSelector : ContentPage
{
	public OrderSelector()
	{
		InitializeComponent();
	}

    private void Select_Tapped(object sender, TappedEventArgs e)
    {
        ((OrderSelectorViewModel)BindingContext).SelectMethod((OrderIngredient)((Grid)sender).BindingContext); 
    }

    private void Deselect_Tapped(object sender, TappedEventArgs e)
    {
        ((OrderSelectorViewModel)BindingContext).DeselectMethod((OrderIngredient)((Grid)sender).BindingContext);
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        ((OrderIngredient)((Picker)sender).BindingContext).NotifyChanges();
        ((OrderIngredient)((Picker)sender).BindingContext).UpdateCategoryIngredient();
        ((Picker)sender).Unfocus();  
    }
}