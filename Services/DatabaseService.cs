using HabitTrackerApp.Models;
using SQLite;

namespace HabitTrackerApp.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _db;

    private async Task InitAsync()
    {
        if (_db is not null) return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "habits.db3");
        _db = new SQLiteAsyncConnection(dbPath,
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        await _db.CreateTableAsync<Habit>();
    }

    public async Task<int> AddAsync(Habit habit)
    {
        await InitAsync();
        return await _db!.InsertAsync(habit);
    }

    public async Task<List<Habit>> GetAllAsync()
    {
        await InitAsync();
        return await _db!.Table<Habit>().OrderByDescending(h => h.Date).ToListAsync();
    }

    public async Task<int> DeleteAsync(Habit habit)
    {
        await InitAsync();
        return await _db!.DeleteAsync(habit);
    }

    public async Task<int> CountAsync()
    {
        await InitAsync();
        return await _db!.Table<Habit>().CountAsync();
    }

    public async Task<int> TotalMinutesAsync()
    {
        await InitAsync();
        var all = await _db!.Table<Habit>().ToListAsync();
        return all.Sum(h => h.DurationMinutes);
    }

    public async Task<int> ThisWeekCountAsync()
    {
        await InitAsync();
        var weekAgo = DateTime.Today.AddDays(-6);
        return await _db!.Table<Habit>().Where(h => h.Date >= weekAgo).CountAsync();
    }
}
