using CommunityToolkit.Maui.Views;
using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.DataModels;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels.Add;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Groupings;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Popup;

namespace StorageManagerMobile.Views.AddPages;

public partial class AddIngredient : ContentPage
{


    private SupplierSelectionViewModel VM = new SupplierSelectionViewModel();

    public AddIngredient()
	{
		InitializeComponent();
	}

    private async Task OpenSupplierSelectionPopupAsync()
    {
        var popup = new SupplierSelection();
        popup.BindingContext = VM;
        var result = await this.ShowPopupAsync(popup);
        if (result==null)
        {
            await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
            //result = await this.ShowPopupAsync(popup);
        }
        else
        {
            IngredientsFormat res = (IngredientsFormat)result;
            ((AddIngredientViewModel)BindingContext).Formats.Add(res);
            VM = new SupplierSelectionViewModel();
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        OpenSupplierSelectionPopupAsync();
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        CheckAvailabilityAsync();
        Navigation.PopAsync();
    }

    private async Task CheckAvailabilityAsync()
    {
        long Exists = ((AddIngredientViewModel)BindingContext).CheckIfExists();
        if (Exists != 0)
        {
            bool answer = await DisplayAlert("L'ingrediente già esiste con lo stesso nome", "Vuoi aggiungere questi fornitori all'ingrediente", "Yes", "No");
            if(answer)
            {
                ((AddIngredientViewModel)BindingContext).SaveFormats(Exists);
            }
            else
            {
                return;
            }
        }
        else
        {
            long ID = ((AddIngredientViewModel)BindingContext).SaveIngredient();
            ((AddIngredientViewModel)BindingContext).SaveFormats(ID);
        }
            ((AddIngredientViewModel)BindingContext).SaveChanges();

    }

    private void DeleteFormat_Clicked(object sender, EventArgs e)
    {
        IngredientsFormat actual = (IngredientsFormat)((ImageButton)sender).BindingContext;
        ((AddIngredientViewModel)BindingContext).Formats.Remove(actual);
    }
}