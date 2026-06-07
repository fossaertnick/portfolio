using CommunityToolkit.Maui.Views;

namespace Mde.Project.Mobile.Pages;

public partial class ImagePopup : Popup
{
	public ImagePopup(string path)
	{
		InitializeComponent();
        PopupImage.Source = path;
	}
}