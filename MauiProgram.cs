using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace HabitTrackerApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts => { });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<Services.DatabaseService>();
        builder.Services.AddSingleton<Pages.HomePage>();
        builder.Services.AddSingleton<Pages.ProgressPage>();
        builder.Services.AddSingleton<Pages.LogPage>();
        builder.Services.AddSingleton<Pages.HistoryPage>();
        builder.Services.AddSingleton<Pages.InsightsPage>();
        builder.Services.AddSingleton<Pages.TutorialPage>();

        return builder.Build();
    }
}
