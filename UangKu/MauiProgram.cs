using CommunityToolkit.Maui;
using InputKit.Handlers;
using Microsoft.Extensions.Logging;

namespace UangKu
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddInputKitHandlers();
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-brands-400.ttf", "FABrands");
                    fonts.AddFont("fa-regular-400.ttf", "FARegular");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}