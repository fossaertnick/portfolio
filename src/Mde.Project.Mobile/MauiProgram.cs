#if ANDROID
using Mde.Project.Mobile.Platforms.Android;
using Plugin.LocalNotification;
#elif WINDOWS
using Mde.Project.Mobile.Platforms.Windows;
using Microsoft.UI.Xaml;
using Microsoft.UI.Dispatching;
#endif

using CommunityToolkit.Maui;
using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Services;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Mde.Project.Mobile.Domain.Services;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using Mde.Project.Mobile.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;


namespace Mde.Project.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement(false);
#if ANDROID
            builder.UseLocalNotification();
            builder.UseMauiMaps();
#endif
            builder.ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("icomoon.ttf", "appIcons");
                });

            // SQLITE
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
            builder.Services.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseSqlite($"Filename={dbPath}");
            });

#if DEBUG

            // UNDERLINES TEXT INPUT
            builder.ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
                {
                    handler.PlatformView.Background = null;
                });
                EditorHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
                {
                    handler.PlatformView.Background = null;
                });
                DatePickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
                {
                    handler.PlatformView.Background = null;
                });
                TimePickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
                {
                    handler.PlatformView.Background = null;
                });
                SearchBarHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
                {
                    handler.PlatformView.Background = null;
                });

#endif
            });


            builder.Logging.AddDebug();
#endif

            // ROUTES
            Routing.RegisterRoute(nameof(ManualPage), typeof(ManualPage));
            Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
            Routing.RegisterRoute(nameof(CreateOrUpdatePage), typeof(CreateOrUpdatePage));
            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
            Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));

            // PAGES/MVVM MODELLEN
            builder.Services.AddTransient<ListPage>();
            builder.Services.AddTransient<ListViewModel>();

            builder.Services.AddTransient<DetailsPage>();
            builder.Services.AddTransient<DetailsViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            builder.Services.AddTransient<MapPage>();
            builder.Services.AddTransient<MapViewModel>();

            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<SettingsViewModel>();

            builder.Services.AddTransient<CreateOrUpdatePage>();
            builder.Services.AddTransient<CreateOrUpdateViewModel>();

            builder.Services.AddTransient<StatisticsPage>();
            builder.Services.AddTransient<StatisticsViewModel>();

            builder.Services.AddTransient<ManualPage>();
            builder.Services.AddTransient<ManualViewModel>();

            // SERVICES
            builder.Services.AddScoped<IMemoriaService, MemoriaService>();
            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IStatisticService, StatisticService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<ILocalMemoriaCache, LocalMemoriaCache>();

            builder.Services.AddSingleton<IDeviceSystemService, DeviceSystemService>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<ISourceOfTruthService, SourceOfTruthService>();
#if ANDROID
            builder.Services.AddSingleton<ISpeechToTextService, AndroidSpeechToTextService>();
            builder.Services.AddSingleton<IPushNotificationService, AndroidNotificationsService>();
            builder.Services.AddScoped<IMapService, AndroidMapService>();
#elif WINDOWS
            builder.Services.AddSingleton<ISpeechToTextService, WindowsSpeechToTextService>();
            builder.Services.AddSingleton<IPushNotificationService, WindowsNotificationsService>();
            builder.Services.AddScoped<IMapService, WindowsMapService>();
#endif
            // API CONNECTION
            builder.Services.AddHttpClient(Constants.MemoriaClientName,
                config => config.BaseAddress = new Uri(Constants.MemoriaApiUrl));
            
            var app = builder.Build();

            // SEEDING
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
                    using var dbContext = dbContextFactory.CreateDbContext();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database initialization failed: {ex}");
                }
            }

            return app;
        }
    }
}
