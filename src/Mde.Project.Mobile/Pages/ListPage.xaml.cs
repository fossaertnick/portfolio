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
        _viewModel.InitializeMemoriaCommand.Execute(null);
        base.OnAppearing();
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool bevestiging = await DisplayAlertAsync(
            "Bevestigen",
            "Weet je zeker dat je je memória wilt veranderen?",
            "Ja",
            "Nee"
            );

        if (bevestiging)
        {
            var swipeItem = sender as SwipeItem;
            if(swipeItem?.BindingContext is Memoria specificMemoria) 
            {
                _viewModel.DeleteMemoriaCommand.Execute(specificMemoria);
            }
        }
        else
        {
            return;
        }
    }
}