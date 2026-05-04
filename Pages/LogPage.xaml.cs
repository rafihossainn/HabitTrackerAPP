using HabitTrackerApp.Models;
using HabitTrackerApp.Services;

namespace HabitTrackerApp.Pages;

public partial class LogPage : ContentPage
{
    private readonly DatabaseService _db;
    private bool _isGoodHabit = true;

    public LogPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
        DatePick.Date = DateTime.Today;
    }

    private void OnGoodTapped(object sender, EventArgs e)
    {
        _isGoodHabit = true;
        GoodBorder.BackgroundColor = Color.FromArgb("#1A3D35");
        GoodBorder.Stroke = new SolidColorBrush(Color.FromArgb("#00D9A3"));
        BadBorder.BackgroundColor = Color.FromArgb("#252537");
        BadBorder.Stroke = new SolidColorBrush(Color.FromArgb("#2F2F44"));
    }

    private void OnBadTapped(object sender, EventArgs e)
    {
        _isGoodHabit = false;
        BadBorder.BackgroundColor = Color.FromArgb("#3D1A22");
        BadBorder.Stroke = new SolidColorBrush(Color.FromArgb("#FF5C7A"));
        GoodBorder.BackgroundColor = Color.FromArgb("#252537");
        GoodBorder.Stroke = new SolidColorBrush(Color.FromArgb("#2F2F44"));
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var name = NameEntry.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlert("Missing field", "Please enter a habit name.", "OK");
            return;
        }

        if (!int.TryParse(DurationEntry.Text, out var minutes) || minutes <= 0)
        {
            await DisplayAlert("Invalid duration", "Enter a positive number of minutes.", "OK");
            return;
        }

        var category = CategoryPicker.SelectedItem?.ToString() ?? "General";

        var habit = new Habit
        {
            Name = name,
            DurationMinutes = minutes,
            Date = DatePick.Date,
            IsGoodHabit = _isGoodHabit,
            Category = category
        };

        await _db.AddAsync(habit);

        var typeWord = _isGoodHabit ? "good habit" : "bad habit";
        StatusLabel.TextColor = _isGoodHabit
            ? Color.FromArgb("#00D9A3")
            : Color.FromArgb("#FF5C7A");
        StatusLabel.Text = $"Saved \"{habit.Name}\" as a {typeWord}!";

        NameEntry.Text = string.Empty;
        DurationEntry.Text = string.Empty;
        DatePick.Date = DateTime.Today;
        CategoryPicker.SelectedIndex = -1;
        OnGoodTapped(this, EventArgs.Empty);
    }

    private async void OnHistoryClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//history");
}
