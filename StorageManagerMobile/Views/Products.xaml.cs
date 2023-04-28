using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.ViewModels;
using StorageManagerMobile.ViewModels.Details;
using StorageManagerMobile.ViewModels.Groupings;
using StorageManagerMobile.Views.Product;

namespace StorageManagerMobile.Views;

public partial class Products : ContentPage
{

    private bool refreshView = false;

    public Products()
	{
		InitializeComponent();
	}

    #region ListManagement

    #region Search

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        //((IngredientsViewModel)BindingContext).Search(e.NewTextValue);
    }

    #endregion

    #region Filters

    private void Filter_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        //((IngredientsViewModel)BindingContext).FilterList(((RadioButton)sender).StyleId.ToString());

    }

    #endregion

    #region RefreshList

    private void Refresher_Refreshing(object sender, EventArgs e)
    {
        RefreshList();
        //Refresher.IsRefreshing = false;
    }

    public void RefreshList()
    {
       ((ProductsViewModel)BindingContext).RefreshIngredientList();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        //Refresher.IsRefreshing = true;
    }

    #endregion
    #endregion


    #region Actions

    #region EditMethods
    private async Task EditQuantityAsync(object sender, EventArgs e)
    {
        //List<Ingredient> updated = new List<Ingredient>();
        //IngredientViewerViewModel Current = (IngredientViewerViewModel)((Button)sender).BindingContext;
        //Ingredient Title = Current.Title;
        //var popup = new QuantityPopup();
        //popup.BindingContext = new QuantityPopupViewModel(Title.Name, Title.Category, Title.QuantityNeeded, Title.ActualQuantity);
        //var result = await this.ShowPopupAsync(popup);
        //if (result != null)
        //{
        //    while (((QuantityObject)result).ActualQuantity == -1 || ((QuantityObject)result).QuantityNeeded == -1)
        //    {
        //        await DisplayAlert("Attenzione", "Valori non validi, I valori devono essere interi senza virgola", "OK");
        //        result = await this.ShowPopupAsync(popup);
        //    }

        //    Current.Title.QuantityNeeded = ((QuantityObject)result).QuantityNeeded;
        //    Current.Title.ActualQuantity = ((QuantityObject)result).ActualQuantity;
        //    ((IngredientsViewModel)BindingContext).UpdateIngredientList(Current.Title);
        //    ((IngredientsViewModel)BindingContext).RefreshIngredientList();
        //}
    }

    private void EditQuantity_Clicked(object sender, EventArgs e)
    {
        //EditQuantityAsync(sender, e);
    }

    private void EditIngredientGroup_Clicked(object sender, EventArgs e)
    {
        ProductViewerViewModel Current = (ProductViewerViewModel)((ImageButton)sender).BindingContext;
        ProductDetailsViewModel VM = new ProductDetailsViewModel(Current);
        ProductDetail Edit = new ProductDetail();
        Edit.BindingContext = VM;
        Navigation.PushAsync(Edit);
    }

    private void SetDefault_Clicked(object sender, CheckedChangedEventArgs e)
    {
        //IngredientsFormat Selected = (IngredientsFormat)((RadioButton)sender).BindingContext;
        //IngredientViewerViewModel Current = ((IngredientsViewModel)BindingContext).IngredientList.FirstOrDefault(x => x.Title.Id == Selected.IngredientId);
        //if (refreshView)
        //{
        //    foreach (IngredientsFormat x in Current.Ingredients)
        //    {
        //        if (x.Id != Selected.Id)
        //        {
        //            x.IsDefault = false;
        //        }
        //    }
        //    Current.SaveIngredients();
        //    refreshView = false;
        //}
        //else
        //{
        //    refreshView = true;
        //}
    }
    #endregion

    #region Delete

    private void Delete_Clicked(object sender, EventArgs e)
    {
        ProductComposition Selected = (ProductComposition)((ImageButton)sender).BindingContext;
        ProductViewerViewModel Current = ((ProductsViewModel)BindingContext).ProductList.FirstOrDefault(x => x.Title.Id == Selected.ProductId);
        if (Current.DeleteComposition(Selected) == 0)
        {
           // ((IngredientsViewModel)BindingContext).DeleteIngredients(Current);
        }
        RefreshList();
    }

    #endregion

    #endregion

    private void Expander_ExpandedChanged(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {
        InvalidateMeasure();
    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        //AddIngredient NewPage = new AddIngredient();
        //AddIngredientViewModel VM = new AddIngredientViewModel();
        //NewPage.BindingContext = VM;
        //Navigation.PushAsync(NewPage);
    }
}