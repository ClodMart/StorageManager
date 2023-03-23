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
        bool Failed = false;
        IngredientsFormat Out = new IngredientsFormat();

        if (SupplierPicker.SelectedItem != null)
        {
                Out.SupplierId = ((Supplier)SupplierPicker.SelectedItem).Id;
                Out.Supplier = (Supplier)SupplierPicker.SelectedItem;            
        }
        else
        {
            Failed= true;
        }
        if (SizeKg.Text != null)
        {
            try
            {
                Out.SizeKg = decimal.Parse(SizeKg.Text);
            }
            catch
            {
                Failed = true;
            }
        }
        else
        {
            Failed = true;
        }
        if (SizeUnits.Text != null)
        {
            try
            {
                Out.SizeUnit = int.Parse(SizeUnits.Text);
            }
            catch
            {
                Failed = true;
            }
        }
        else
        {
            Failed = true;
        }
        if (Cost.Text != null)
        {
            try
            {
                Out.Cost = decimal.Parse(Cost.Text);
            }
            catch
            {
                Failed = true;
            }
        }
        else
        {
            Failed = true;
        }
        if(Failed == true)
        {
            Close(null);
        }
        else
        {
            Close(Out);
        }
    }
}