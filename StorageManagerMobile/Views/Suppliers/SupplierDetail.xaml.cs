using StorageManagerMobile.ViewModels.Details;

namespace StorageManagerMobile.Views.Suppliers;

public partial class SupplierDetail : ContentPage
{
	public SupplierDetail()
	{
		InitializeComponent();
	}

    private void Elimina_Clicked(object sender, EventArgs e)
    {
        ((SupplierDetailsViewModel)BindingContext).DeleteSupplier();
        Navigation.PopAsync();
    }

    private void Salva_Clicked(object sender, EventArgs e)
    {
        ((SupplierDetailsViewModel)BindingContext).SaveSupplier();
        Navigation.PopAsync();
    }
}