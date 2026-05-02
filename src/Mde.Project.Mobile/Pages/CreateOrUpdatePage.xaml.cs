using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Mde.Project.Mobile.ViewModels;
namespace Mde.Project.Mobile.Pages;

public partial class CreateOrUpdatePage : ContentPage
{
    private readonly CreateOrUpdateViewModel _viewModel;

    // fields
    private bool _loaded;

    // constructor
    public CreateOrUpdatePage(CreateOrUpdateViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // methoden
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
		    _viewModel.CancelCommand.Execute("cancel");
        }
        else
        {
            return;
        }
    }
    private async void MediaChoice_Clicked(object sender, EventArgs e)
    {
        string action = await Application.Current.MainPage.DisplayActionSheet
                (
                    "Kies optie",
                    "Annuleer",
                    null,
                    "Camera",
                    "Galerij"
                );

        switch (action)
        {
            case "Camera":
                _viewModel.TakePhotoCommand.Execute(null);
                break;
            case "Galerij":
                _viewModel.PickPhotoCommand.Execute(null);
                break;
        }
    }
}