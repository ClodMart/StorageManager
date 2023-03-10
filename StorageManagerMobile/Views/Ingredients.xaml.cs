using StorageManagerMobile.CustomComponents;
using StorageManagerMobile.ViewModels;

namespace StorageManagerMobile.Views;

public partial class Ingredients : ContentPage
{
    private double H;
	public Ingredients()
	{
		InitializeComponent();
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void Filters_Clicked(object sender, EventArgs e)
    {

    }

    private void Filter_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }

    private void ListView_Refreshing(object sender, EventArgs e)
    {

    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private void AddMaterial_Clicked(object sender, EventArgs e)
    {

    }

    private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        InvalidateMeasure();
        //this.GetOrCreateRenderer();
        //IngredientViewer Current = ((IngredientViewer)e.CurrentSelection.FirstOrDefault());
        //IngredientViewer Prev = ((IngredientViewer)e.PreviousSelection.FirstOrDefault());
        //Current.HeightRequest = ((IngredientViewer)e.CurrentSelection.FirstOrDefault()).Height;
        //Prev.HeightRequest = 100;
    }

    private void Row_SizeChanged(object sender, EventArgs e)
    {
        ((IView)((IngredientViewer)sender)).InvalidateMeasure();
        ((IView)((IngredientViewer)sender)).InvalidateArrange();
        ((IView)List).InvalidateMeasure();
        ((IView)List).InvalidateArrange();
        //((IngredientsViewModel)BindingContext).NotifyPropertyChanged("IngredientList");
        //Dispatcher.Dispatch(() =>
        //(this as IView).InvalidateArrange());
        //List.IsVisible=false;
        //this.Dispatcher.Dispatch(() =>
        //{

        //    //List.IsVisible = true;
        //    Element Parent = this.Parent;
        //    while (Parent is not MainPage)
        //    {
        //        Parent = Parent.Parent;
        //    }
        //    ((MainPage)Parent).RefreshPage();

        //});

    }

    private void List_SizeChanged(object sender, EventArgs e)
    {
        Element Parent = this.Parent;
        while (Parent is not MainPage)
        {
            Parent = Parent.Parent;
        }
        if (H > 0)
        {
            List.HeightRequest = H;
        }
        
        ((MainPage)Parent).ForceLayout();        
    }
}