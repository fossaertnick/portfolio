using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Domain.Services;

namespace Mde.Project.Mobile
{
    public partial class App : Application
    {
        private readonly AppOptionsService _optionsService;
        public App()
        {
            InitializeComponent();

            _optionsService = new AppOptionsService();
            _optionsService.LoadSavedTheme();

            MainPage = new AppShell();
        }
    }
}