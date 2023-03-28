using DBManager.Models;


namespace StorageManagerMobile.Views.Suppliers.Popup;
using CommunityToolkit.Maui.Views;

public partial class AddFormat : Popup
{
	public AddFormat()
	{
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {
        bool Failed = false;
        IngredientsFormat Out = new IngredientsFormat();

        if (IngredientPicker.SelectedItem != null)
        {
            Out.IngredientId = ((Ingredient)IngredientPicker.SelectedItem).Id;
            Out.Ingredient = (Ingredient)IngredientPicker.SelectedItem;
        }
        else
        {
            Failed = true;
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
        if (Failed)
        {
            Close(null);
        }
        else
        {
            Close(Out);
        }
    }
}