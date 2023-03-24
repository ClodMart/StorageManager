namespace StorageManagerMobile.Views.Popup;
using CommunityToolkit.Maui.Views;
using StorageManagerMobile.Resources;

public partial class QuantityPopup : Popup
{
	public QuantityPopup()
	{
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {
		int QNeeded = -1;
		int ActualQ = -1;

		if(QuantityNeeded.Text != null)
		{
			try
			{
				QNeeded = int.Parse(QuantityNeeded.Text);
			}
			catch 
			{
				QNeeded= -1;
			}
		}
		else
		{
            try
            {
                QNeeded = int.Parse(QuantityNeeded.Placeholder);
            }
            catch
            {
                QNeeded = -1;
            }
        }
        if (ActualQuantity.Text != null)
        {
            try
            {
                ActualQ = int.Parse(ActualQuantity.Text);
            }
            catch
            {
                ActualQ = -1;
            }
        }
        else
        {
            try
            {
                ActualQ = int.Parse(ActualQuantity.Placeholder);
            }
            catch
            {
                ActualQ = -1;
            }
        }
		QuantityObject Out = new QuantityObject(QNeeded, ActualQ);
		Close(Out);
    }
}