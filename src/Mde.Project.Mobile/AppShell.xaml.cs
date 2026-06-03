using Mde.Project.Mobile.Domain.Services.Interfaces;

namespace Mde.Project.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly IDeviceSystemService _toClose;

        // constructor
        public AppShell(IDeviceSystemService toClose)
        {
            InitializeComponent();
            _toClose = toClose;
        }

        // methoden
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void ManualPage_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ManualPage));
        }
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            bool bevestiging = await DisplayAlertAsync(
                                "Confirmation",
                                "Are you sure you want to close the app?",
                                "Yes",
                                "No"
);

            if (!bevestiging) return;

            _toClose.Close();
        }

    }
}
