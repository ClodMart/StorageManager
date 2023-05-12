using GemBox.Spreadsheet;

namespace StorageManagerMobile
{
    public partial class App : Application
    {
        public App()
        {
          InitializeComponent();
          MainPage = new MainPage();
          SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }
    }
}