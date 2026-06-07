using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages;

public partial class ManualPage : ContentPage
{
	private readonly ManualViewModel _ViewModel;

	// constructor
	public ManualPage(ManualViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_ViewModel = viewModel;
	}

    // methoden
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		_ViewModel.InitializeCommand.Execute(null);
    }
}