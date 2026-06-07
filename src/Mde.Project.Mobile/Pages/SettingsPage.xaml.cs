using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Domain.Services;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsViewModel _viewModel;
    private readonly IDeviceSystemService _colorOptions;

    // constructor
    public SettingsPage(SettingsViewModel viewModel, IDeviceSystemService options)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _colorOptions = options;
    }

    // methoden
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.Refresh();
    }
    public void Apply(ColorChoice choice)
    {
        Preferences.Set(Constants.ColorChoice, (int)choice);
        _colorOptions.ApplyColorTheme(choice);
    }
    private void SystemColor_Clicked(object sender, EventArgs e)
    {
        Apply(ColorChoice.Sober);
    }
    private void GreenColor_Clicked(object sender, EventArgs e)
    {
        Apply(ColorChoice.Green);

    }
    private void BlueColor_Clicked(object sender, EventArgs e)
    {
        Apply(ColorChoice.Blue);

    }
    private void PurpleColor_Clicked(object sender, EventArgs e)
    {
        Apply(ColorChoice.Purple);
    }
}