using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsViewModel _viewModel;

    // constructor
    public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // methoden
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
            _viewModel.ChangeAvatarCommand.Execute(null);
        }
        else
        {
            return;
        }
    }
}