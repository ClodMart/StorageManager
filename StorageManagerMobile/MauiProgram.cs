using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using StorageManagerMobile.Views.Orders;

namespace StorageManagerMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Platforms.Android.DangerousAndroidMessageHandlerEmitter.Register();
            Platforms.Android.DangerousTrustProvider.Register();    
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();


            builder.Services.AddSingleton(FileSaver.Default);
            builder.Services.AddTransient<OrderSelector>();
            return builder.Build();
        }
    }
}