using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.ViewModels;
using Microsoft.Maui.Controls.Maps;

namespace Mde.Project.Mobile.Pages;

public partial class MapPage : ContentPage
{
    private readonly IMapService _mapService;
    private readonly IGeoCodingService _geoCoding;
    private readonly MapViewModel _viewModel;

    // constructor
    public MapPage(MapViewModel viewModel, IMapService mapService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _mapService = mapService;

        // nu geef ik 'MapView' door zodat deze weet dat hij de 
        _mapService.Initialize(MapView);

        _mapService.OnPinClicked += (id) =>
            {
                Dispatcher.Dispatch(() =>
                {
                    _viewModel.PinClickedCommand.Execute(id);
                });
            };
    }

    // methoden
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadMapAsync();
        RenderMap();
    }
    private void RenderMap()
    {
        if(_viewModel.CurrentLocation != null)
        {
            _mapService.MoveTo(_viewModel.CurrentLocation);
        }
        _mapService.SetPins(_viewModel.Locations);
    }

    public async void MapView_MapClicked(object sender, MapClickedEventArgs e)
    {
        await _viewModel.MapClickedCreateMemoria(e.Location);
    }
}