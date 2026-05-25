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
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.Refresh();
    }
}