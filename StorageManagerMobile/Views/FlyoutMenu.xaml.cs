namespace StorageManagerMobile.Views;

public partial class FlyoutMenu : ContentPage
{
	public FlyoutMenu()
	{
		InitializeComponent();
	}
    private void Button_Clicked(object sender, EventArgs e)
    {
        string label = ((Button)sender).Text;
        Element Parent = ((Button)sender).Parent;
        while (Parent is not MainPage)
        {
            Parent = Parent.Parent;
        }
        ((MainPage)Parent).ChangePage(label);
    }
}