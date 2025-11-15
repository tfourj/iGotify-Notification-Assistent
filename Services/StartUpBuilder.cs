using System.Reflection;

namespace iGotify_Notification_Assist.Services;

public class StartUpBuilder : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return builder =>
        {
            // Log version on startup
            var version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "Unknown";
            Console.WriteLine($"========================================");
            Console.WriteLine($"iGotify Notification Assist");
            Console.WriteLine($"Version: {version}");
            Console.WriteLine($"========================================");
            
            // Create GotifyInstance after starting of the API
            var gss = GotifySocketService.getInstance();
            gss.Init();
            if (gss.isInit)
                gss.Start();

            next(builder);
        };
    }
}