namespace StorageManagerMobile.Views.Popup;

using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Popup;

public partial class SupplierSelection : Popup
{
	public SupplierSelection()
	{
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {
        IngredientsFormat Out = new IngredientsFormat();

        if (SupplierPicker.SelectedItem != null)
        {
            try
            {
                Out.SupplierId = ((Supplier)SupplierPicker.SelectedItem).Id;
                Out.Supplier = (Supplier)SupplierPicker.SelectedItem;
            }
            catch
            {
            }
        }
        if (SizeKg.Text != null)
        {
            try
            {
                Out.SizeKg = decimal.Parse(SizeKg.Text);
            }
            catch
            {
            }
        }
        if (SizeUnits.Text != null)
        {
            try
            {
                Out.SizeUnit = int.Parse(SizeUnits.Text);
            }
            catch
            {
            }
        }
        if (Cost.Text != null)
        {
            try
            {
                Out.Cost = decimal.Parse(Cost.Text);
            }
            catch
            {
            }
        }
        if(Out.Equals(new IngredientsFormat()))
        {
            Close(null);
        }
        else
        {
            Close(Out);
        }
    }
}