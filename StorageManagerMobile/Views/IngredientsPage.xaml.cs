using CommunityToolkit.Maui.Views;
using DBManager.Interfacce;
using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.Services;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.ViewModels.Add;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Groupings;
using StorageManagerMobile.ViewModels.Popup;
using StorageManagerMobile.Views.AddPages;
using StorageManagerMobile.Views.DetailPage;
using StorageManagerMobile.Views.Ingredients.Popup;
using StorageManagerMobile.Views.Popup;
using System.Collections.ObjectModel;

namespace StorageManagerMobile.Views;

public partial class IngredientsPage : ContentPage
{
    private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
    private bool refreshView = false;
    private bool IsChanging = false;

    public IngredientsPage()
	{
		InitializeComponent();
    }

    #region UsedList
    #region ListManagement

    #region Search

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(e.NewTextValue == "")
        {
            ((IngredientsViewModel)BindingContext).SearchEmpty();
        }        
    }

    #endregion

    #region Filters

    private void Filter_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        ((IngredientsViewModel)BindingContext).FilterList(((RadioButton)sender).StyleId.ToString());

    }

    #endregion

    #region RefreshList

    private void Refresher_Refreshing(object sender, EventArgs e)
    {
        RefreshList();
        Refresher.IsRefreshing = false;
    }

    public void RefreshList()
    {
        ((IngredientsViewModel)BindingContext).RefreshIngredientList();        
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        RefreshList();
        RefreshUnusedList();
    }

    #endregion
    #endregion

    #region Actions

    #region EditMethods
    private async Task EditQuantityAsync(object sender, EventArgs e)
    {
        List<Ingredient> updated = new List<Ingredient>();
        IngredientViewerViewModel Current = (IngredientViewerViewModel)((Button)sender).BindingContext;
        Ingredient Title = Current.Title;
        var popup = new QuantityPopup();
        popup.BindingContext = new QuantityPopupViewModel(Title.Name, Title.Category, Title.QuantityNeeded, Title.ActualQuantity);
        var result = await this.ShowPopupAsync(popup);
        if (result != null)
        {
            while (((QuantityObject)result).ActualQuantity == -1 || ((QuantityObject)result).QuantityNeeded == -1)
            {
                await DisplayAlert("Attenzione", "Valori non validi, I valori devono essere interi senza virgola", "OK");
                result = await this.ShowPopupAsync(popup);
            }

            Current.Title.QuantityNeeded = ((QuantityObject)result).QuantityNeeded;
            Current.Title.ActualQuantity = ((QuantityObject)result).ActualQuantity;
            ((IngredientsViewModel)BindingContext).UpdateIngredientList(Current);
            ((IngredientsViewModel)BindingContext).RefreshIngredientList();
            //    updated.Add(Current.Title);

            //Current.Title = new ObservableCollection<Ingredient>(updated);
        }
    }

    private void EditQuantity_Clicked(object sender, EventArgs e)
    {
        EditQuantityAsync(sender, e);
    }

    private void EditIngredientGroup_Clicked(object sender, EventArgs e)
    {
        IngredientViewerViewModel Current = (IngredientViewerViewModel)((ImageButton)sender).BindingContext;
        IngredientDetailViewModel VM = new IngredientDetailViewModel(Current);
        IngredientDetail Edit = new IngredientDetail();
        Edit.BindingContext = VM;
        Navigation.PushAsync(Edit);
    }

    private void SetDefault_Clicked(object sender, CheckedChangedEventArgs e)
    {
        IngredientsFormat Selected = (IngredientsFormat)((RadioButton)sender).BindingContext;
        IngredientViewerViewModel Current = ((IngredientsViewModel)BindingContext).IngredientList.FirstOrDefault(x => x.Title.Id == Selected.IngredientId);
        if (refreshView)
        {
            foreach (IngredientsFormat x in Current.Ingredients)
            {
                if (x.Id != Selected.Id)
                {
                    x.IsDefault = false;
                }
            }
            Current.SaveIngredients();
            refreshView = false;
        }
        else
        {
            refreshView = true;
        }
    }

    #endregion

    #region Delete

    private void Delete_Clicked(object sender, EventArgs e)
    {
        IngredientsFormat Selected = (IngredientsFormat)((ImageButton)sender).BindingContext;
        IngredientViewerViewModel Current = ((IngredientsViewModel)BindingContext).IngredientList.FirstOrDefault(x => x.Title.Id == Selected.IngredientId);
        if (Current.DeleteIngredient(Selected) == 0)
        {
            ((IngredientsViewModel)BindingContext).DeleteIngredients(Current);
        }
        //RefreshList();
        ((IngredientsViewModel)BindingContext).NotifyUIChange();
    }

    #endregion

    #endregion

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        AddIngredient NewPage = new AddIngredient();
        AddIngredientViewModel VM = new AddIngredientViewModel();
        NewPage.BindingContext = VM;
        Navigation.PushAsync(NewPage);
    }
    #endregion
    //private void Expander_ExpandedChanged(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    //{
    //    InvalidateMeasure();
    //}

    #region UnusedList

    private void UnusedFilters_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        ((IngredientsViewModel)BindingContext).FilterListUnused(((RadioButton)sender).Content.ToString());
    }

    private async void SetDefaultUnused_Clicked(object sender, CheckedChangedEventArgs e)
    {
            IngredientsFormat Selected = (IngredientsFormat)((RadioButton)sender).BindingContext;
            IngredientViewerViewModel Current = ((IngredientsViewModel)BindingContext).NotUsedIngredientList.FirstOrDefault(x => x.Title.Id == Selected.IngredientId);
            Current.ModelChanged += Current_ModelChanged;
            if (refreshView)
            {
                foreach (IngredientsFormat x in Current.Ingredients)
                {
                    if (x.Id != Selected.Id)
                    {
                        x.IsDefault = false;
                    }
                }
                Current.SaveIngredients();
                refreshView = false;
            }
            else
            {
                refreshView = true;
            }      
    }

    private void Current_ModelChanged(object sender, EventArgs e)
    {
        //((IngredientsViewModel)BindingContext).RefreshIngredientList();
        //((IngredientsViewModel)BindingContext).RefreshUnusedIngredientList();
        ((IngredientViewerViewModel)sender).ModelChanged-= Current_ModelChanged;
    }

    private void DeleteUnused_Clicked(object sender, EventArgs e)
    {
        IngredientsFormat Selected = (IngredientsFormat)((ImageButton)sender).BindingContext;
        IngredientViewerViewModel Current = ((IngredientsViewModel)BindingContext).NotUsedIngredientList.FirstOrDefault(x => x.Title.Id == Selected.IngredientId);
        if (Current.DeleteIngredient(Selected) == 0)
        {
            ((IngredientsViewModel)BindingContext).DeleteIngredients(Current);
        }
        //RefreshUnusedList();
        ((IngredientsViewModel)BindingContext).NotifyUIChange();
    }

    public void RefreshUnusedList()
    {
        ((IngredientsViewModel)BindingContext).RefreshUnusedIngredientList();
    }

    private void RefresherUnusing_Refreshing(object sender, EventArgs e)
    {
        RefreshUnusedList();
        RefresherUnused.IsRefreshing = false;
    }
    #endregion

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        ((IngredientsViewModel)BindingContext).RefreshIngredientList();
        ((IngredientsViewModel)BindingContext).RefreshUnusedIngredientList();
    }

    private async Task EditIsUsedAsync(object sender, EventArgs e)
    {
        List<Ingredient> updated = new List<Ingredient>();
        IngredientViewerViewModel Current = (IngredientViewerViewModel)((Button)sender).BindingContext;
        Ingredient Title = Current.Title;
        var popup = new IsUsedSelection();
        popup.BindingContext = new IsUsedSelectionViewModel(Title.IsUsedValueNavigation);
        var result = await this.ShowPopupAsync(popup);
        if (result != null)
        {
            Current.UpdateIsUsedValues((IsUsedValue)result);
            ((IngredientsViewModel)BindingContext).UpdateIngredientList(Current);
            ((IngredientsViewModel)BindingContext).RefreshIngredientList();
            ((IngredientsViewModel)BindingContext).RefreshUnusedIngredientList();
            //((IngredientsViewModel)BindingContext).NotifyUIChange();
            //    updated.Add(Current.Title);

            //Current.Title = new ObservableCollection<Ingredient>(updated);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        EditIsUsedAsync(sender, e);
    }

    private void EditCost_Clicked(object sender, EventArgs e)
    {
        OpenEditCost(sender, e);
    }

    private async Task OpenEditCost(object sender, EventArgs e)
    {
        IngredientsFormat Current = (IngredientsFormat)((Button)sender).BindingContext;
        var popup = new CostChange();
        popup.BindingContext = new CostChangeViewModel(Current);
        var result = await this.ShowPopupAsync(popup);
        RefreshList();
        RefreshUnusedList();
        //((IngredientsViewModel)BindingContext).NotifyUIChange();
    }

    private void searchBarNotUsed_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue == "")
        {
            ((IngredientsViewModel)BindingContext).UnusedSearchEmpty();
        }
    }

    //private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    if (e.OldTextValue != null)
    //    {
    //        IngredientsFormat actual = (IngredientsFormat)((Entry)sender).BindingContext;
    //        if (!IsChanging)
    //        {                
    //            actual.ChangePrice(decimal.Parse(e.NewTextValue));
    //            IsChanging = true;
    //            IngredientsFormatsRepository.Update(actual);
    //        }
    //        else
    //        {
    //            IsChanging = false;
    //        }
    //    }
    //}
}