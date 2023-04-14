using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using PdfSharp.Maui;
using PdfSharpCore;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using StorageManagerMobile.Services;

namespace StorageManagerMobile.Views.Orders;

public partial class ExportPreview : ContentPage
{
    private static readonly EZFontResolver EZFontResolver_fontResolver = EZFontResolver.Get;

    public ExportPreview()
	{
		InitializeComponent();
        GlobalFontSettings.FontResolver = EZFontResolver_fontResolver;

    }

    async Task SaveFile(CancellationToken cancellationToken, PdfDocument document)
    {
        using var stream = new MemoryStream();
        document.Save(stream, false);
        var fileSaverResult = await FileSaver.Default.SaveAsync("test.pdf", stream, cancellationToken);
        if (fileSaverResult.IsSuccessful)
        {
            await Toast.Make($"The file was saved successfully to location: {fileSaverResult.FilePath}").Show(cancellationToken);
        }
        else
        {
            await Toast.Make($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}").Show(cancellationToken);
        }
    }

    async Task PickFolder(CancellationToken cancellationToken)
    {
        var result = await FolderPicker.Default.PickAsync(cancellationToken);
        if (result.IsSuccessful)
        {
            await Toast.Make($"The folder was picked: Name - {result.Folder.Name}, Path - {result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
        }
        else
        {
            await Toast.Make($"The folder was not picked with error: {result.Exception.Message}").Show(cancellationToken);
        }
    }

    private async Task<FileStream> GetResourcePathAsync()
    {
        FileStream stream = await FileSystem.OpenAppPackageFileAsync("OpenSans - Regular.ttf") as FileStream;
        return stream;
    }

    private void ExportButton_Clicked(object sender, EventArgs e)
    {
        //var stream = await Microsoft.Maui.Essentials.FileSystem.OpenAppPackageFileAsync(filePath);
        FileStream stream = GetResourcePathAsync().Result;        
        EZFontResolver_fontResolver.AddFont("OpenSans-Regular", PdfSharpCore.Drawing.XFontStyle.Regular, stream.Name, true, true); 

        View pdf = Preview;
        //GlobalFontSettings.FontResolver = new FontResolver();
        PdfManager pdfManager = new PdfManager();
        //Navigation.PushAsync(pdf);
        PdfDocument document = pdfManager.GeneratePdfFromView(pdf, PageOrientation.Portrait, PageSize.A4, PdfStyle.PlatformSpecific);
         //pdfManager.GeneratePdfFromView(pdf);
        //PdfDocument document = pdfManager.GeneratePdfFromView(pdf, PageOrientation.Portrait, PageSize.A4, PdfStyle.PlatformSpecific);
        SaveFile(default, pdfManager.GeneratePdfFromView(pdf));
    }
}