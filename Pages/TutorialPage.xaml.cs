namespace HabitTrackerApp.Pages;

public partial class TutorialPage : ContentPage
{
    // YouTube video ID — change to any video. Embed restrictions don't matter,
    // we open it in the YouTube app or browser instead of embedding.
    private const string VideoId = "DY15PQBUuvQ";

    public TutorialPage()
    {
        InitializeComponent();

        // Load the video's official YouTube thumbnail
        VideoThumbnail.Source = ImageSource.FromUri(
            new Uri($"https://img.youtube.com/vi/{VideoId}/maxresdefault.jpg"));
    }

    private async void OnPlayTapped(object sender, TappedEventArgs e)
    {
        var url = $"https://www.youtube.com/watch?v={VideoId}";
        try
        {
            await Launcher.Default.OpenAsync(new Uri(url));
        }
        catch (Exception)
        {
            await DisplayAlert("Cannot open video", "Please make sure you have a browser or YouTube app installed.", "OK");
        }
    }
}
