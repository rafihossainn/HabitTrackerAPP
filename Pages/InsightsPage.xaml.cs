using HabitTrackerApp.Services;

namespace HabitTrackerApp.Pages;

public class HabitTip
{
    public string HabitName { get; set; } = "";
    public string Category { get; set; } = "";
    public string TipText { get; set; } = "";
    public string ActionSteps { get; set; } = "";
}

public partial class InsightsPage : ContentPage
{
    private readonly DatabaseService _db;

    // Category-based tips library
    private static readonly Dictionary<string, (string tip, string steps)> CategoryTips = new()
    {
        ["Exercise"] = (
            "Skipping exercise weakens your cardiovascular system and reduces energy levels over time.",
            "• Start with just 10 minutes a day\n• Schedule workouts like meetings\n• Find a workout buddy for accountability\n• Try walking, cycling, or dancing"),
        ["Diet & Nutrition"] = (
            "Poor diet choices increase risk of obesity, diabetes, and heart disease.",
            "• Meal prep on Sundays to avoid fast food\n• Swap soda for water or herbal tea\n• Add one vegetable to every meal\n• Read food labels before buying"),
        ["Sleep"] = (
            "Poor sleep impairs memory, mood, and immune function.",
            "• Set a consistent bedtime alarm\n• No screens 1 hour before bed\n• Keep your room cool and dark\n• Avoid caffeine after 2 PM"),
        ["Screen Time"] = (
            "Excessive screen time causes eye strain, anxiety, and disrupted sleep.",
            "• Use app timers (Android Digital Wellbeing)\n• Put your phone in another room at night\n• Take a 20-second eye break every 20 minutes\n• Replace scroll time with a hobby"),
        ["Mental Health"] = (
            "Neglecting mental health leads to burnout, anxiety, and depression.",
            "• Journal for 5 minutes each morning\n• Practice deep breathing exercises\n• Talk to someone you trust\n• Limit news consumption to once a day"),
        ["Smoking / Vaping"] = (
            "Smoking damages your lungs, heart, and increases cancer risk significantly.",
            "• Set a firm quit date and tell someone\n• Use nicotine patches or gum as a bridge\n• Identify your triggers and avoid them\n• Call a quit line or join a support group"),
        ["Alcohol"] = (
            "Excessive alcohol damages your liver, brain, and disrupts relationships.",
            "• Track every drink with an app\n• Set a weekly limit and stick to it\n• Swap evening drinks for sparkling water\n• Find a sober activity to replace the habit"),
        ["Productivity"] = (
            "Procrastination and poor focus reduce your output and increase stress.",
            "• Use the Pomodoro technique (25 min on, 5 min off)\n• Write your top 3 tasks the night before\n• Silence notifications during work blocks\n• Break big tasks into 15-minute chunks"),
        ["Social"] = (
            "Isolation or toxic social habits affect your mental and emotional health.",
            "• Schedule one meaningful conversation weekly\n• Limit time with draining people\n• Join a club or community group\n• Practice active listening in conversations"),
        ["General"] = (
            "This habit may be negatively affecting your daily well-being.",
            "• Identify what triggers this habit\n• Replace it with a healthier alternative\n• Track your progress daily\n• Reward yourself for every week you improve"),
    };

    public InsightsPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var all = await _db.GetAllAsync();
        var badHabits = all.Where(h => !h.IsGoodHabit).ToList();

        NoBadPanel.IsVisible = badHabits.Count == 0;

        var tips = badHabits
            .GroupBy(h => h.Name, StringComparer.OrdinalIgnoreCase)
            .Select(g =>
            {
                var habit = g.First();
                var category = habit.Category ?? "General";
                var (tip, steps) = CategoryTips.TryGetValue(category, out var t)
                    ? t
                    : CategoryTips["General"];

                return new HabitTip
                {
                    HabitName = habit.Name,
                    Category = $"Category: {category}  •  Logged {g.Count()}x",
                    TipText = tip,
                    ActionSteps = steps
                };
            })
            .ToList();

        TipsList.ItemsSource = tips;
    }
}
