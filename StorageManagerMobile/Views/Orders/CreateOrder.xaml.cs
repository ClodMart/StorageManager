using DBManager.Models;
using PdfSharp.Maui;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.Views.Orders.ExportTemplate;
using System.Text;
using PdfSharpCore;
using CommunityToolkit.Maui.Storage;
using System.IO;
using System.Threading;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Maui.Controls.PlatformConfiguration;
using CommunityToolkit.Maui.Alerts;

namespace StorageManagerMobile.Views.Orders;

public partial class CreateOrder : ContentPage
{
	public CreateOrder()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (((CreateOrderViewModel)BindingContext).ItemsMissed)
        {
            DisplayAlertAsync(((CreateOrderViewModel)BindingContext).Missed) ;
        }
    }

    private async Task DisplayAlertAsync(List<Ingredient> IgList)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Alcuni ingredienti non hanno un fornitore di default quindi non sono stati caricati:");
        foreach(Ingredient x in IgList)
        {
            sb.AppendLine("- " + x.Name+", "+x.Category);
        }
        await DisplayAlert("Ingredienti non caricati", sb.ToString(), "Ok");
    }

    private void SelectItem_Clicked(object sender, EventArgs e)
    {
        OrderItem Current = (OrderItem)((Grid)sender).BindingContext;
        ((CreateOrderViewModel)BindingContext).SelectItem(Current);
    }

    private void DeselectItem_Clicked(object sender, EventArgs e)
    {
        OrderItem Current = (OrderItem)((Grid)sender).BindingContext;
        ((CreateOrderViewModel)BindingContext).DeselectItem(Current);
    }

    private void NumberPicker_TextChanged(object sender, TextChangedEventArgs e)
    {
        OrderItem Current = (OrderItem)((Entry)sender).BindingContext;
        ((CreateOrderViewModel)BindingContext).UpdateListing(Current);
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

    private void ExportButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ExportPreview());
    }
}