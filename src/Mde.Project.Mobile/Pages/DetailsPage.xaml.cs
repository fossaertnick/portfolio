using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages;

public partial class DetailsPage : ContentPage
{
    private readonly DetailsViewModel _viewModel;

    // constructor
    public DetailsPage(DetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // methoden
    private async void DeleteMemoriaButton_Clicked(object sender, EventArgs e)
    {
        bool bevestiging = await DisplayAlertAsync(
            "Bevestigen",
            "Weet je zeker dat je je memória wilt veranderen?",
            "Ja",
            "Nee"
            );

        if (bevestiging)
        {
            var buttonPress = sender as Button;
            if (buttonPress?.BindingContext is Guid specificMemoriaId)
            {
                _viewModel.DeleteMemoriaCommand.Execute(specificMemoriaId);
            }
        }
        else
        {
            return;
        }
    }
}