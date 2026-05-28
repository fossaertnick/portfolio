using Mde.Project.Mobile.Domain.Services;

namespace Mde.Project.Mobile
{
    public partial class App : Application
    {
        private readonly AppOptionsService _optionsService;
        private readonly JwtService _jwtService;

        // constructor
        public App(AppOptionsService optionsService, JwtService jwtService)
        {
            InitializeComponent();

            _optionsService = optionsService;
            _jwtService = jwtService;

            _jwtService.EnsureDeviceId();
            _optionsService.LoadSavedTheme();
            _ = _jwtService.EnsureTokenAsync();

            MainPage = new AppShell();
        }
    }
}