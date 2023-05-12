using StorageManagerMobile.ViewModels.Components;

namespace StorageManagerMobile.Views.CustomControl;

public partial class OrderItem : VerticalStackLayout
{
	public OrderItem()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		((OrderItemViewModel)BindingContext).Ingredient.Selected = !((OrderItemViewModel)BindingContext).Ingredient.Selected;
        ((OrderItemViewModel)BindingContext).UpdateListing();
    }

    private void NumberPicker_TextChanged(object sender, TextChangedEventArgs e)
    {        
        ((OrderItemViewModel)BindingContext).UpdateListing();
    }
}