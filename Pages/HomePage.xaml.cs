using HabitTrackerApp.Services;

namespace HabitTrackerApp.Pages;

public partial class HomePage : ContentPage
{
    private readonly DatabaseService _db;

    public HomePage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var count = await _db.ThisWeekCountAsync();
        WeekCountLabel.Text = count == 1 ? "1 habit this week" : $"{count} habits this week";
    }

    private async void OnProgressClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//progress");

    private async void OnLogClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//log");

    private async void OnHistoryClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//history");

    private async void OnInsightsClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//insights");

    private async void OnTutorialClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//tutorial");
}
