using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages;

public partial class ListPage : ContentPage
{
    private readonly ListViewModel _viewModel;

    // constructor
    public ListPage(ListViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // methoden
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.InitializeLocationCommand.Execute(null);
    }
}