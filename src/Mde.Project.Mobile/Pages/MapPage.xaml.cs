using Microsoft.Maui.Maps;

namespace Mde.Project.Mobile.Pages;

public partial class MapPage : ContentPage
{
	public MapPage()
	{
		InitializeComponent();
	}

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("add");
    }

    private async void AvatarButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//settingsPage");
    }

    private async void ListButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("list");
    }
}