using CommunityToolkit.Maui.Views;
using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.Popup;

namespace StorageManagerMobile.Views.DetailPage;

public partial class IngredientDetail : ContentPage
{
    SupplierSelectionViewModel VM;
    private bool IsChanging = false;

    public IngredientDetail()
	{
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {
        //foreach(IngredientsFormat x in ((IngredientDetailViewModel)BindingContext).Ingredients)
        //{
        //    ((IngredientDetailViewModel)BindingContext).
        //}
        //((IngredientDetailViewModel)BindingContext).SaveModificationsMethod();
        Navigation.PopAsync();
    }

    private async Task OpenSupplierSelectionPopupAsync()
    {
        var popup = new SupplierSelection();
        if(VM == null)
        {
            VM = new SupplierSelectionViewModel(((IngredientDetailViewModel)BindingContext).Title);
        }
        popup.BindingContext = VM;
        var result = await this.ShowPopupAsync(popup);
        if(result == null)
        {
            await DisplayAlert("Attenzione", "Valori non validi, Selezionare tutti I valori", "OK");
            //result = await this.ShowPopupAsync(popup);
        }
        else
        {
            IngredientsFormat res = (IngredientsFormat)result;
            res.Ingredient = ((IngredientDetailViewModel)BindingContext).Title;
            res.IngredientId = ((IngredientDetailViewModel)BindingContext).Title.Id;
            ((IngredientDetailViewModel)BindingContext).SaveIngredientFormat(res);
            VM = new SupplierSelectionViewModel(((IngredientDetailViewModel)BindingContext).Title);
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        OpenSupplierSelectionPopupAsync();
    }

    private void DeleteFormat_Clicked(object sender, EventArgs e)
    {
        IngredientsFormat actual = (IngredientsFormat)((ImageButton)sender).BindingContext;
        ((IngredientDetailViewModel)BindingContext).RemoveIngredientFormat(actual);
        ((IngredientDetailViewModel)BindingContext).Ingredients.Remove(actual);
    }

    private void Cost_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.OldTextValue != null)
        {
            if (!IsChanging)
            {
                IngredientsFormat actual = (IngredientsFormat)((Entry)sender).BindingContext;
                actual.ChangePrice(decimal.Parse(e.NewTextValue));
                IsChanging = true;
            }
            else
            {
                IsChanging = false;
            }
        }
    }
}