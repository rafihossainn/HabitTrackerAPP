namespace HabitTrackerApp.Pages;

public partial class TutorialPage : ContentPage
{
    // Atomic Habits summary video — swap the ID for any YouTube video you like
    private const string VideoId = "PZ7lDrwYdZc";

    public TutorialPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        VideoWebView.Source = new HtmlWebViewSource { Html = BuildHtml(VideoId) };
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        VideoWebView.Source = new HtmlWebViewSource
        {
            Html = "<html><body style='background:#000'></body></html>"
        };
    }

    private static string BuildHtml(string videoId) =>
        "<html><head>" +
        "<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
        "<style>*{margin:0;padding:0}body{background:#000}iframe{width:100%;height:100vh;border:none}</style>" +
        "</head><body>" +
        "<iframe src='https://www.youtube.com/embed/" + videoId + "?playsinline=1&rel=0' " +
        "allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' " +
        "allowfullscreen></iframe>" +
        "</body></html>";
}
