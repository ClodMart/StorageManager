using StorageManagerMobile.ViewModels.Details;

namespace StorageManagerMobile.Views.DetailPage;

public partial class IngredientDetail : ContentPage
{
	public IngredientDetail()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		//IsUsedPicker.ItemsSource = ((IngredientDetailViewModel)BindingContext).PickerValues;
		//foreach(string x in ((IngredientDetailViewModel)BindingContext).PickerValues)
		//{
  //          IsUsedPicker.Items.Add(x);
  //      }
		//IsUsedPicker.SelectedItem = ((IngredientDetailViewModel)BindingContext).IsUsedValues.FirstOrDefault(x => x.Id == ((IngredientDetailViewModel)BindingContext).Title.IsUsed).Description;
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}