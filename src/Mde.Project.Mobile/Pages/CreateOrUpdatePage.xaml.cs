using Mde.Project.Mobile.Core.Services.Interfaces;
using Mde.Project.Mobile.ViewModels;
using System.ComponentModel;
namespace Mde.Project.Mobile.Pages;

public partial class CreateOrUpdatePage : ContentPage
{
    private readonly CreateOrUpdateViewModel _viewModel;

    // constructor
    public CreateOrUpdatePage(CreateOrUpdateViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    // methoden
    private async void MediaChoice_Clicked(object sender, EventArgs e)
    {
        string action = await Application.Current.MainPage.DisplayActionSheet
                (
                    "Choose option",
                    "Cancel",
                    null,
                    "Camera",
                    "Library"
                );

        switch (action)
        {
            case "Camera":
                _viewModel.TakePhotoCommand.Execute(null);
                break;
            case "Library":
                _viewModel.PickPhotoCommand.Execute(null);
                break;
            case "Video":
                _viewModel.TakeVideoCommand.Execute(null);
                break;
        }
    }
    private void Mic_Clicked(object sender, EventArgs e)
    {
        _viewModel.StartSpeech();
    }
}