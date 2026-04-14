using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Mde.Project.Mobile.ViewModels;
namespace Mde.Project.Mobile.Pages;

public partial class CreateOrUpdatePage : ContentPage
{
    private readonly CreateOrUpdateViewModel _viewModel;

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
		    _viewModel.SpecificActionCommand.Execute("cancel");
        }
        else
        {
            return;
        }
    }
}