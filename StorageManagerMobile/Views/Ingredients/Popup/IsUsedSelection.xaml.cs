namespace StorageManagerMobile.Views.Ingredients.Popup;
using CommunityToolkit.Maui.Views;
using DBManager.Models;
using StorageManagerMobile.Resources;
using StorageManagerMobile.ViewModels.Popup;

public partial class IsUsedSelection : Popup
{
	public IsUsedSelection()
	{
		InitializeComponent();
	}

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(((IsUsedSelectionViewModel)BindingContext).Selected != ((IsUsedSelectionViewModel)BindingContext).OriginalSelected)
        {
            IsUsedValue Out = ((IsUsedSelectionViewModel)BindingContext).Selected;
            Close(Out);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        IsUsedValue Out = ((IsUsedSelectionViewModel)BindingContext).Selected;
        Close(Out);
    }

    private void Popup_Opened(object sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
    {
        ((IsUsedSelectionViewModel)BindingContext).Selected = ((IsUsedSelectionViewModel)BindingContext).OriginalSelected;
    }
}