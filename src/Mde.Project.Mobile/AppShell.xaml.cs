namespace Mde.Project.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void ManualPage_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("manual");
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            bool bevestiging = await DisplayAlertAsync(
                                "Bevestiging",
                                "Weet je zeker dat je wilt uitloggen?",
                                "Ja",
                                "Nee"
);

            if (bevestiging)
            {
                await Shell.Current.GoToAsync("//mainPage");
            }
            else
            {
                return;
            }
        }
    }
}
