using SQLite;

namespace HabitTrackerApp.Models;

public class Habit
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    public DateTime Date { get; set; }

    public bool IsGoodHabit { get; set; } = true;

    public string Category { get; set; } = "General";

    [Ignore]
    public string DateDisplay => Date.ToString("MMM d, yyyy");

    [Ignore]
    public string DurationDisplay => $"{DurationMinutes} min";

    [Ignore]
    public string TypeLabel => IsGoodHabit ? "✅ Good" : "⚠️ Bad";

    [Ignore]
    public string TypeColorHex => IsGoodHabit ? "#00D9A3" : "#FF5C7A";

    [Ignore]
    public string CardAccentHex => IsGoodHabit ? "#1A3D35" : "#3D1A22";
}
