using CommunityToolkit.Maui.Views;
using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Groupings;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.DetailPage;
using StorageManagerMobile.Views.Popup;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StorageManagerMobile.Views;

public partial class Ingredients : ContentPage
{
    private System.Diagnostics.Stopwatch Timer= new System.Diagnostics.Stopwatch();
    private bool ButtonPressed = false;
    private Thread ButtonTimer;

    public Ingredients()
	{
		InitializeComponent();
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((IngredientsViewModel)BindingContext).Search(e.NewTextValue);
        
    }

    private void Filter_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        ((IngredientsViewModel)BindingContext).FilterList(((RadioButton)sender).StyleId.ToString());

    }

    private void Expander_ExpandedChanged(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {
        InvalidateMeasure();
    }


    private async Task EditQuantityAsync(object sender, EventArgs e)
    {
        List<Ingredient> updated = new List<Ingredient>();
        IngredientViewerViewModel Current =(IngredientViewerViewModel)((Button)sender).BindingContext;
        Ingredient Title = Current.Title;
        var popup = new QuantityPopup();
        popup.BindingContext = new QuantityPopupViewModel(Title.Ingredient1, Title.Category, Title.QuantityNeeded, Title.ActualQuantity);
        var result = await this.ShowPopupAsync(popup);
        if (result != null)
        {
            while (((QuantityObject)result).ActualQuantity == -1 || ((QuantityObject)result).QuantityNeeded == -1)
            {
                await DisplayAlert("Attenzione", "Valori non validi, I valori devono essere interi senza virgola", "OK");
                result = await this.ShowPopupAsync(popup);
            }
            foreach (Ingredient x in Current.Ingredients)
            {
                x.QuantityNeeded = ((QuantityObject)result).QuantityNeeded;
                x.ActualQuantity = ((QuantityObject)result).ActualQuantity;
                ((IngredientsViewModel)BindingContext).UpdateIngredientList(x);
                updated.Add(x);
            }
            Current.Ingredients = new ObservableCollection<Ingredient>(updated);
        }
    }

    private void EditQuantity_Clicked(object sender, EventArgs e)
    {
        EditQuantityAsync(sender, e);
    }

    private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
        Navigation.PushAsync(new IngredientDetail());
    }

    private void LongPressGestureRecognizer_LongPressed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new IngredientDetail());
    }

    private void EditIngredientGroup_Clicked(object sender, EventArgs e)
    {
        IngredientViewerViewModel Current = (IngredientViewerViewModel)((ImageButton)sender).BindingContext;
        IngredientDetailViewModel VM = new IngredientDetailViewModel(Current);
        IngredientDetail Edit = new IngredientDetail();
        Edit.BindingContext= VM;
        Navigation.PushAsync(Edit);
        //((IngredientsViewModel)BindingContext).IngredientList.Clear();
    }

    private void Refresher_Refreshing(object sender, EventArgs e)
    {
        RefreshList();
        Refresher.IsRefreshing= false;
    }
    public void RefreshList()
    {
        ((IngredientsViewModel)BindingContext).RefreshIngredientList();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        Refresher.IsRefreshing = true;
    }

    private void SetDefault_Clicked(object sender, EventArgs e)
    {
      Ingredient Selected = (Ingredient)((ImageButton)sender).BindingContext;
      IngredientViewerViewModel Current = ((IngredientsViewModel)BindingContext).IngredientList.FirstOrDefault(x => x.Title.Ingredient1 == Selected.Ingredient1);
     
        foreach (Ingredient x in Current.Ingredients)
        {
            if (x.Id == Selected.Id)
            {
                x.IsDefault = true;
            }
            else
            {
                x.IsDefault = false;
            }
        }
        Current.SaveIngredients();
        ((IngredientsViewModel)BindingContext).RefreshIngredientList();
    }
}