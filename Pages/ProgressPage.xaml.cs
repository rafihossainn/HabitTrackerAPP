using HabitTrackerApp.Services;
using Microsoft.Maui.Graphics;

namespace HabitTrackerApp.Pages;

public partial class ProgressPage : ContentPage
{
    private readonly DatabaseService _db;
    private readonly RingDrawable _drawable = new();
    private const int WeeklyGoal = 7;

    public ProgressPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
        RingView.Drawable = _drawable;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var logged = await _db.ThisWeekCountAsync();
        var total = await _db.TotalMinutesAsync();
        LoggedLabel.Text = logged.ToString();
        MinutesLabel.Text = total.ToString();

        var target = Math.Min(1.0, logged / (double)WeeklyGoal);
        await AnimateTo(target);
    }

    private async void OnReplay(object sender, EventArgs e)
    {
        var logged = await _db.ThisWeekCountAsync();
        var target = Math.Min(1.0, logged / (double)WeeklyGoal);
        _drawable.Progress = 0;
        RingView.Invalidate();
        PercentLabel.Text = "0%";
        await AnimateTo(target);
    }

    private Task AnimateTo(double target)
    {
        var tcs = new TaskCompletionSource<bool>();
        var anim = new Animation(v =>
        {
            _drawable.Progress = v;
            RingView.Invalidate();
            PercentLabel.Text = $"{(int)Math.Round(v * 100)}%";
        }, 0, target, Easing.CubicOut);

        anim.Commit(this, "RingAnim", length: 1400, finished: (_, _) => tcs.SetResult(true));
        return tcs.Task;
    }
}

public class RingDrawable : IDrawable
{
    public double Progress { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var cx = dirtyRect.Center.X;
        var cy = dirtyRect.Center.Y;
        var radius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2f - 28f;
        var stroke = 22f;

        // Track
        canvas.StrokeColor = Color.FromArgb("#2F2F44");
        canvas.StrokeSize = stroke;
        canvas.StrokeLineCap = LineCap.Round;
        canvas.DrawCircle(cx, cy, radius);

        if (Progress <= 0) return;

        // Progress arc with gradient-like 2-tone
        canvas.StrokeColor = Color.FromArgb("#7C5CFF");
        canvas.StrokeSize = stroke;
        canvas.StrokeLineCap = LineCap.Round;

        var sweep = (float)(360.0 * Progress);
        var rect = new RectF(cx - radius, cy - radius, radius * 2, radius * 2);
        // MAUI DrawArc: angles in degrees, 0 is east, counter-clockwise positive.
        // Start at top (-90 -> in MAUI's convention 90), sweep clockwise: clockwise=true.
        canvas.DrawArc(rect, 90, 90 - sweep, true, false);

        // Accent dot at the head
        var rad = (90 - sweep) * Math.PI / 180.0;
        var hx = cx + (float)(radius * Math.Cos(rad));
        var hy = cy - (float)(radius * Math.Sin(rad));
        canvas.FillColor = Color.FromArgb("#00D9A3");
        canvas.FillCircle(hx, hy, stroke / 2f + 2);
    }
}
