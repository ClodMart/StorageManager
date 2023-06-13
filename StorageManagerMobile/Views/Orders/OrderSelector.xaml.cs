using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Orders.Popup;
using System.Diagnostics;
using System.Drawing;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace StorageManagerMobile.Views.Orders;

public partial class OrderSelector : ContentPage
{

	public OrderSelector()
	{
		InitializeComponent();
	}

    private void Select_Tapped(object sender, TappedEventArgs e)
    {
        ((OrderSelectorViewModel)BindingContext).SelectMethod((OrderIngredient)((Grid)sender).BindingContext); 
    }

    private void Deselect_Tapped(object sender, TappedEventArgs e)
    {
        ((OrderSelectorViewModel)BindingContext).DeselectMethod((OrderIngredient)((Grid)sender).BindingContext);
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        ((OrderIngredient)((Picker)sender).BindingContext).NotifyChanges();
        ((OrderIngredient)((Picker)sender).BindingContext).UpdateCategoryIngredient();
        ((Picker)sender).Unfocus();  
    }

    private void ExportButton_Clicked(object sender, EventArgs e)
    {
        HttpRequestAsync();
    }

    private async Task HttpRequestAsync()
    {
        string Username = "Prins";
        string PW = "Prins123!";
        string CategoryId = ((OrderSelectorViewModel)BindingContext).Cat.Id.ToString();
        HttpClient client = new HttpClient();
        Uri uri = new Uri(string.Format("https://10.147.18.219:5024/api/ExportOrder/{0}/{1}/GetOrderById/{2}", Username, PW, CategoryId));
        try
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            //string SavePath = FolderPicker.PickAsync();
            var stream = response.Content.ReadAsStreamAsync();
            FileSaver.SaveAsync("Order" + DateOnly.FromDateTime(DateTime.Now).ToString().Replace("/", "-") + ".xls",stream.Result , cancellationToken:default);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
    }

    private async Task EditIsUsedAsync(object sender, EventArgs e)
    {
        List<Ingredient> updated = new List<Ingredient>();
        OrderIngredient Current = (OrderIngredient)((Button)sender).BindingContext;
        Ingredient Title = Current.Ingredient;
        var popup = new SupplierSelector();
        popup.BindingContext = new SupplierSelectorViewModel(Title, Current.SelectedSupplier);
        var result = await this.ShowPopupAsync(popup);
        if (result != null)
        {
            Current.SelectedSupplier = ((Supplier)result);
            //((IngredientsViewModel)BindingContext).UpdateIngredientList(Current);
            //((IngredientsViewModel)BindingContext).RefreshIngredientList();
            //((IngredientsViewModel)BindingContext).RefreshUnusedIngredientList();
            //    updated.Add(Current.Title);

            //Current.Title = new ObservableCollection<Ingredient>(updated);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        EditIsUsedAsync(sender, e);
    }
}