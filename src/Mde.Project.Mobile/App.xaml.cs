using Mde.Project.Mobile.Domain.Services;
using Mde.Project.Mobile.Domain.Services.Interfaces;

namespace Mde.Project.Mobile
{
    public partial class App : Application
    {
        private readonly IDeviceSystemService _optionsService;
        private readonly IServiceProvider _service;

        // constructor
        public App(IDeviceSystemService optionsService, IServiceProvider service)
        {
            InitializeComponent();

            _optionsService = optionsService;
            _service = service;

            _optionsService.LoadSavedTheme();

            MainPage = _service.GetRequiredService<AppShell>();
        }
    }
}