using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
namespace Mde.Project.Mobile.Pages;

public partial class CreateOrUpdatePage : ContentPage
{
	public CreateOrUpdatePage()
	{
		InitializeComponent();
	}

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        bool bevestiging = await DisplayAlertAsync(
            "Bevestigen",
            "Weet je zeker dat je wilt annuleren?",
            "Ja",
            "Nee"
        );

        if (bevestiging)
        {
		    await Shell.Current.GoToAsync("list");
        }
        else
        {
            return;
        }
    }

    private async void CreateButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("list");
        await Toast.Make("Nieuwe memoria aangemaakt").Show();
    }
}