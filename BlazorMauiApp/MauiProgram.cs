using BlazorMauiApp.Services;
using Microsoft.Extensions.Logging;

namespace BlazorMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<StudentService>();
            // Register TimerService as a singleton
            builder.Services.AddSingleton<TimerService>(provider =>
            {
                var dispatcher = provider.GetRequiredService<IDispatcher>();
                return new TimerService(dispatcher);
            });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
