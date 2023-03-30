using CommunityToolkit.Maui.Views;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.Views;
using System.Reflection;

namespace StorageManagerMobile
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = new MainPageViewModel();
                this.Detail = new NavigationPage(new HomePage());
            }
            catch (Exception ex) when (ex.Message.Contains("Failed to connect"))
            {
                DisplayAlert("Connessione fallita", "E' fallita la connessione al database, controllare la configurazione.\nL'applicazione verrà chiusa.", "Ok");
                Application.Current.Quit();
            }            
        }

        public void ChangePage(string label)
        {
            string PageName = ((MainPageViewModel)BindingContext).MenuList.Find(x => x.Label == label).Page;
            string type = Assembly.GetCallingAssembly().GetName().Name + ".Views." + PageName;
            Type PageType = Type.GetType(type);
            Page newPage = ((MainPageViewModel)BindingContext).Pages.Find(x => x.GetType() == PageType);
            this.Detail = new NavigationPage(newPage);
            this.IsPresented = false;
        }

        private void FlyoutPage_Loaded(object sender, EventArgs e)
        {
            this.IsPresented = true;
        }
    }
}