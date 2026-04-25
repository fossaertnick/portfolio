using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Maps;

namespace Mde.Project.Mobile.Pages;

public partial class MapPage : ContentPage
{
    private readonly IMemoriaService _memoriaService;
    private readonly MapViewModel _viewModel;
	public MapPage(MapViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;

        _viewModel.Locations.CollectionChanged += (s, e) => { UpdatePins(); };
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var location = await _viewModel.ReadyMapService();
        await _viewModel.LoadExistingMemoriaAsync();

        if(location != null)
        {
            MapView.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(1)));
        }

        UpdatePins();
    }
    private void UpdatePins()
    {
        MapView.Pins.Clear();

        foreach(var memoria in _viewModel.Locations)
        {
            MapView.Pins.Add(new Pin
            {
                Label = memoria.Name,
                Location = new Location(memoria.Latitude, memoria.Longitude)
            });
        }
    }
}