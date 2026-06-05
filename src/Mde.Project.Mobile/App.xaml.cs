using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Plugin.LocalNotification;
using System.Diagnostics;

namespace Mde.Project.Mobile
{
    public partial class App : Application
    {
        private readonly IDeviceSystemService _optionsService;
        private readonly IServiceProvider _service;
        private readonly IPushNotificationService _toUserService;
        private readonly IAuthService _authService;

        // constructor
        public App(IDeviceSystemService optionsService, IServiceProvider service, IPushNotificationService toUserService, IAuthService authService)
        {
            InitializeComponent();

            _optionsService = optionsService;
            _service = service;
            _authService = authService;
            _toUserService = toUserService;
    
            _optionsService.LoadSavedTheme();

            MainPage = _service.GetRequiredService<AppShell>();
        }

        // methoden
        protected override async void OnStart()
        {
            base.OnStart();

            _ = InitializeAsync();
            _ = InitializeNotification();
        }

        // ondersteunende methoden
        public async Task InitializeNotification()
        {
            try
            {
                if(DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    await _toUserService.InitializeNotifications();
                }
                else // (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    if (!Preferences.ContainsKey(Constants.NotificationPermission))
                    {
                        var granted = await LocalNotificationCenter.Current.RequestNotificationPermission();
                        Preferences.Set(Constants.NotificationPermission, granted);
                        if (granted) await _toUserService.InitializeNotifications();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        private async Task InitializeAsync()
        {
            await _authService.AuthenticateDeviceAsync();
        }
    }
}