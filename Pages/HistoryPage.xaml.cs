using HabitTrackerApp.Services;

namespace HabitTrackerApp.Pages;

public partial class HistoryPage : ContentPage
{
    private readonly DatabaseService _db;

    public HistoryPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var items = await _db.GetAllAsync();
        HabitList.ItemsSource = items;
        GoodCountLabel.Text = items.Count(h => h.IsGoodHabit).ToString();
        BadCountLabel.Text = items.Count(h => !h.IsGoodHabit).ToString();
    }
}
