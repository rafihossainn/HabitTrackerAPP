using Microsoft.Maui.Controls.Shapes;

namespace HabitTrackerApp.Pages;

public partial class OnboardingPage : ContentPage
{
    private int _current = 0;
    private VisualElement[]? _slides;
    private Ellipse[]? _dots;

    public OnboardingPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Safe to access named elements here — page is fully loaded
        _slides ??= [Slide0, Slide1, Slide2, Slide3];
        _dots   ??= [Dot0, Dot1, Dot2, Dot3];
    }

    private async void OnNextTapped(object sender, EventArgs e)
    {
        if (_slides is null) return;

        if (_current < 3)
            await GoToSlide(_current + 1);
        else
            await FinishOnboarding();
    }

    private async void OnSkipTapped(object sender, EventArgs e)
        => await FinishOnboarding();

    private async Task GoToSlide(int index)
    {
        if (_slides is null || _dots is null) return;

        await _slides[_current].FadeTo(0, 180);
        _slides[_current].IsVisible = false;

        _current = index;

        for (int i = 0; i < _dots.Length; i++)
        {
            _dots[i].Fill = new SolidColorBrush(
                i == _current ? Color.FromArgb("#7C5CFF") : Color.FromArgb("#2F2F44"));
            _dots[i].WidthRequest  = i == _current ? 12 : 10;
            _dots[i].HeightRequest = i == _current ? 12 : 10;
        }

        NextLabel.Text = _current == 3 ? "🚀  Get Started" : "Next  →";
        NextBtn.BackgroundColor = _current == 3
            ? Color.FromArgb("#00D9A3")
            : Color.FromArgb("#7C5CFF");

        _slides[_current].Opacity = 0;
        _slides[_current].IsVisible = true;
        await _slides[_current].FadeTo(1, 220);
    }

    private async Task FinishOnboarding()
    {
        Preferences.Set("onboarding_done", true);
        await Navigation.PopModalAsync(animated: false);
    }
}
