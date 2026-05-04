using Microsoft.Maui.Handlers;

namespace HabitTrackerApp;

public class CustomWebViewHandler : WebViewHandler
{
    protected override Android.Webkit.WebView CreatePlatformView()
    {
        var webView = base.CreatePlatformView();
        // Spoof a real Chrome browser so YouTube allows embedding
        webView.Settings.JavaScriptEnabled = true;
        webView.Settings.DomStorageEnabled = true;
        webView.Settings.MediaPlaybackRequiresUserGesture = false;
        webView.Settings.UserAgentString =
            "Mozilla/5.0 (Linux; Android 13; Pixel 7 Build/TQ3A.230901.001) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/120.0.6099.230 Mobile Safari/537.36";
        return webView;
    }
}
