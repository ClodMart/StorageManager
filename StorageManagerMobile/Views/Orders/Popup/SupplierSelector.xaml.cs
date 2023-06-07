namespace StorageManagerMobile.Views.Orders.Popup;
using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.ViewModels.Popup;

public partial class SupplierSelector : Popup
{
	public SupplierSelector()
	{
		InitializeComponent();
	}

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (((SupplierSelectorViewModel)BindingContext).Selected != ((SupplierSelectorViewModel)BindingContext).OriginalSelected)
        {
            Supplier Out = ((SupplierSelectorViewModel)BindingContext).Selected;
            Close(Out);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Supplier Out = ((SupplierSelectorViewModel)BindingContext).Selected;
        Close(Out);
    }

    private void Popup_Opened(object sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
    {
        ((SupplierSelectorViewModel)BindingContext).Selected = ((SupplierSelectorViewModel)BindingContext).OriginalSelected;
    }
}