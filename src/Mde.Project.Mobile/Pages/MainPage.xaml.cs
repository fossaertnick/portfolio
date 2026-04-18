using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages;

public partial class MainPage : ContentPage
{
    // constructor
    public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}