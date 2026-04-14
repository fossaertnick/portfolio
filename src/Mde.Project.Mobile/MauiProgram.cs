using CommunityToolkit.Maui;
using Mde.Project.Mobile.Pages;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Controls.Maps;
using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Locations.Mock;
using Mde.Project.Mobile.ViewModels;

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
            Routing.RegisterRoute("manual", typeof(ManualPage));
            Routing.RegisterRoute("list", typeof(ListPage));
            Routing.RegisterRoute("add", typeof(CreateOrUpdatePage));
            Routing.RegisterRoute("details", typeof(DetailsPage));
            Routing.RegisterRoute("map", typeof(MapPage));

            builder.Services.AddSingleton<ListPage>();
            builder.Services.AddTransient<ListViewModel>();

            builder.Services.AddSingleton<DetailsPage>();
            builder.Services.AddTransient<DetailsViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            builder.Services.AddSingleton<MapPage>();
            builder.Services.AddTransient<MapViewModel>();

            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddTransient<SettingsViewModel>();

            builder.Services.AddSingleton<CreateOrUpdatePage>();
            builder.Services.AddTransient<CreateOrUpdateViewModel>();

            builder.Services.AddTransient<IKnownLocationService, MockLocationService>();

            return builder.Build();
        }
    }
}
