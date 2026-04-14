namespace Mde.Project.Mobile.Pages;

public partial class ListPage : ContentPage
{
    // property (testdata)
    public List<string> MyCollectionView  { get; set; }

    // constructor
	public ListPage()
	{
		InitializeComponent();

        // testdata binden aan mijn property
        MyCollectionView = new List<string> 
        {
            "Item 1", 
            "Item 2", 
            "Item 3",
            "Item 4",
            "Item 5",
            "Item 6",
            "Item 7",
            "Item 8",
            "Item 9",
        }; 
        BindingContext = this;
    }

    // methoden
    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("add");
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var border = (Border)sender;
        string clickedValue = border.BindingContext as string;

        await Shell.Current.GoToAsync($"details?tryout={clickedValue}");
    }

    private async void AvatarButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//settingsPage");
    }

    private async void MapButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("map");
    }

    private async void Details_Clicked(object sender, EventArgs e)
    {
        var border = (Border)sender;
        string clickedValue = border.BindingContext as string;

        await Shell.Current.GoToAsync($"details?tryout={clickedValue}");
    }
}