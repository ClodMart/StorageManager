using StorageManagerMobile.CustomComponents.ViewModels;

namespace StorageManagerMobile.CustomComponents;

public partial class IngredientViewer : ContentView
{
	public double Height = 0;
	public IngredientViewer()
	{
		InitializeComponent();
	}

    private void ContentView_Loaded(object sender, EventArgs e)
    {
		Height= 100* (((IngredientViewerViewModel)BindingContext).Ingredients.Count + 1);
    }
}