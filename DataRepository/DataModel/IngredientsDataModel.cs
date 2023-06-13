namespace DataRepository.DataModel
{
    public class IngredientsDataModel
    {


    }

    private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
    private static readonly IngredientsFormatsRepository IngredientsFormatsRepository = new IngredientsFormatsRepository(context);

    private List<IngredientsFormat> AllFormats = new List<IngredientsFormat>();

    private bool isExpanded = false;
    public bool IsExpanded
    {
        get { return isExpanded; }
        set { isExpanded = value; NotifyPropertyChanged(); }
    }

    private Ingredient title;
    public Ingredient Title
    {
        get { return title; }
        set { title = value; NotifyPropertyChanged(); CalcQuantityDisplay(); }
    }
    private ObservableCollection<IngredientsFormat> ingredients;
    public ObservableCollection<IngredientsFormat> Ingredients
    {
        get { return ingredients; }
        set
        {
            ingredients = value; NotifyPropertyChanged();
            //    UpdateIngredientList();
        }
    }

    private string quantityDisplay;
    public string QuantityDisplay
    {
        get { return quantityDisplay; }
        set { quantityDisplay = value; NotifyPropertyChanged(); }
    }
}
