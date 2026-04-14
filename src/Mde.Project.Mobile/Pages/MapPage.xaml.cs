using Mde.Project.Mobile.ViewModels;
using Microsoft.Maui.Maps;

namespace Mde.Project.Mobile.Pages;

public partial class MapPage : ContentPage
{
	public MapPage(MapViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}