using StorageManagerMobile.ViewModels.Add;

namespace StorageManagerMobile.Views.Suppliers;

public partial class AddSuppliers : ContentPage
{
	public AddSuppliers()
	{
		InitializeComponent();
	}

    private void Salva_Clicked(object sender, EventArgs e)
    {
		((AddSupplierViewModel)BindingContext).SaveSupplier();
		Navigation.PopAsync();
    }
}