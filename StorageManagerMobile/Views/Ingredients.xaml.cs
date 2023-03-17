using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.CustomComponents;
using StorageManagerMobile.CustomComponents.ViewModels;
using StorageManagerMobile.Resources;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.DetailPage;
using StorageManagerMobile.Views.Popup;
using System.Collections.ObjectModel;

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

    private void Button_Pressed(object sender, EventArgs e)
    {
        ButtonTimer = new Thread(new ThreadStart(TimeButton));
        ButtonPressed =true;
        Timer.Start();        
    }

    private void TimeButton()
    {
        while (ButtonPressed)
        {
            if (Timer.ElapsedMilliseconds > 2000)
            {
                Navigation.PushAsync(new IngredientDetail());
                ButtonPressed = false;
            }
            else if (!ButtonPressed)
            {
                break;
            }
        }
    }

    private void Button_Released(object sender, EventArgs e)
    {
        Timer.Stop();
        Timer.Reset();
        ButtonPressed = false;
    }
}