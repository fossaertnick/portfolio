using CommunityToolkit.Maui;
using Mde.Project.Mobile.Domain.Locations.Mock;
using Mde.Project.Mobile.Domain.Services;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using Mde.Project.Mobile.ViewModels;
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
                .UseMauiMaps()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement(false)  
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG

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
                
#endif
            });


            builder.Logging.AddDebug();
#endif
            Routing.RegisterRoute(nameof(ManualPage), typeof(ManualPage));
            Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
            Routing.RegisterRoute(nameof(CreateOrUpdatePage), typeof(CreateOrUpdatePage));
            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
            Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));

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

            builder.Services.AddSingleton<IMemoriaService, MemoriaService>();
            builder.Services.AddSingleton<IMediaService, MediaService>();
            builder.Services.AddSingleton<ILocationService, LocationService>();
            builder.Services.AddSingleton<ISeedingService, SeedingService>();
            builder.Services.AddSingleton<IStatisticService, StatisticService>();
            builder.Services.AddSingleton<IManualService, ManualService>();

            if (OperatingSystem.IsWindows())
            {
                builder.Services.AddSingleton<IMapService, WindowsMapService>();
            }
            else
            {
                builder.Services.AddSingleton<IMapService, AndroidMapService>();
            }

            builder.Services.AddHttpClient<IGeoCodingService, GoogleGeoCodingService>();

            return builder.Build();
        }
    }
}
