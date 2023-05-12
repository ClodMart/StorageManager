namespace StorageManagerMobile.Views.Ingredients.Popup;
using CommunityToolkit.Maui.Views;
using StorageManagerMobile.Resources;

public partial class CostChange : Popup
{
	public CostChange()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}