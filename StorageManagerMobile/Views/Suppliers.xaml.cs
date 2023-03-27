using DBManager.Models;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.ViewModels.Add;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.Views.Suppliers;

namespace StorageManagerMobile.Views;

public partial class SuppliersPage : ContentPage
{
	public SuppliersPage()
	{
		InitializeComponent();
	}

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((SuppliersViewModel)BindingContext).Search(e.NewTextValue);
    }

    private void AddSupplier_Clicked(object sender, EventArgs e)
    {
        AddSuppliers newPage = new AddSuppliers();
        newPage.BindingContext = new AddSupplierViewModel();
        Navigation.PushAsync(newPage);
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ((SuppliersViewModel)BindingContext).RefreshList();
    }

    private void EditSupplier_Clicked(object sender, EventArgs e)
    {
        Supplier Sup = (Supplier)((ImageButton)sender).BindingContext;
        SupplierDetail newPage = new SupplierDetail();
        newPage.BindingContext = new SupplierDetailsViewModel(Sup);
        Navigation.PushAsync(newPage);
    }
}