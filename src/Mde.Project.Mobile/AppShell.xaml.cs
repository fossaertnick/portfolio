using Mde.Project.Mobile.Domain.Services;

namespace Mde.Project.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly AppCloser _closer;

        // constructor
        public AppShell()
        {
            InitializeComponent();
            _closer = new AppCloser();
        }

        // methoden
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

            _closer.Close();
        }
    }
}
