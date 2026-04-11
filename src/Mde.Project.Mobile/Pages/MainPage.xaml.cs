namespace Mde.Project.Mobile.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("add");
    }

    private async void ListButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("list");
    }

    private async void AvatarButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//settingsPage");
    }
}