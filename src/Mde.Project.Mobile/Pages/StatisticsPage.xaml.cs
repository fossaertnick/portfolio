using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages;

public partial class StatisticsPage : ContentPage
{
	private readonly StatisticsViewModel _viewModel;

	// constructor
	public StatisticsPage(StatisticsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

    // methoden
    protected override void OnAppearing()
    {
        base.OnAppearing();
		_viewModel.InitializeCommand.Execute(null);
    }
}