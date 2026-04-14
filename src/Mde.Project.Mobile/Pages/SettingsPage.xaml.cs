namespace Mde.Project.Mobile.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void AvatarButton_Clicked(object sender, EventArgs e)
    {
        bool bevestiging = await DisplayAlertAsync(
                "Bevestigen",
                "Weet je zeker dat je je avatar wilt veranderen?",
                "Ja",
                "Nee"
);

        if (bevestiging)
        {
            await Shell.Current.GoToAsync("//settingsPage");
        }
        else
        {
            return;
        }
    }
}