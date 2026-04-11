namespace Mde.Project.Mobile.Pages;

[QueryProperty(nameof(Test), "tryout")]
public partial class DetailsPage : ContentPage
{
	// testproperty
	public string Test 
	{
		get { return Test; }
		set
		{
			lblTest.Text = value;
		}
	}
	public DetailsPage()
	{
		InitializeComponent();
	}
}