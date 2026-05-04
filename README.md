# Habit Tracker — CS ETIC 25-Hour Certificate Project

A .NET MAUI mobile app for tracking daily habits. Built for the CS ETIC 2025-26 certificate project.

## How requirements are met

| Requirement | Where |
| --- | --- |
| Built with .NET MAUI (Android + iOS) | `HabitTrackerApp.csproj` targets `net8.0-android` and `net8.0-ios` |
| Data-related animated image | `Pages/ProgressPage.xaml(.cs)` — animated progress ring drawn on a `GraphicsView` that animates from 0% to your weekly habit completion |
| Form with 3 fields saved to a permanent local database | `Pages/LogPage.xaml(.cs)` — captures Habit Name, Duration (min), Date and saves to SQLite via `Services/DatabaseService.cs` |
| Browse / load saved data on a separate page | `Pages/HistoryPage.xaml(.cs)` — `CollectionView` reads from SQLite |
| Embedded video on its own page | `Pages/TutorialPage.xaml(.cs)` — uses `CommunityToolkit.Maui.MediaElement` |
| Each requirement on its own page | 5 pages: Home, Progress, Log Habit, History, Tutorial |
| Easy navigation | `AppShell.xaml` flyout menu, plus quick-action buttons on Home |
| Consistent color scheme | Defined in `Resources/Styles/Colors.xaml`, applied via shared styles in `Resources/Styles/Styles.xaml` |

## Project structure

```
HabitTrackerApp/
  HabitTrackerApp.csproj
  MauiProgram.cs            # DI registration, MAUI builder
  App.xaml(.cs)             # App entry, loads styles
  AppShell.xaml(.cs)        # Flyout navigation
  Models/
    Habit.cs                # SQLite-mapped model
  Services/
    DatabaseService.cs      # SQLite CRUD + aggregate queries
  Pages/
    HomePage                # Landing page with quick actions
    ProgressPage            # Animated weekly-progress ring
    LogPage                 # 3-field form, saves to SQLite
    HistoryPage             # Browse/load all entries from SQLite
    TutorialPage            # Embedded MediaElement video
  Resources/
    Styles/                 # Colors.xaml + Styles.xaml
    AppIcon/, Splash/, Images/, Fonts/, Raw/
  Platforms/
    Android/, iOS/
```

## How to run (Visual Studio)

1. Install **Visual Studio 2022 (Community is fine)** with the **".NET Multi-platform App UI development"** workload checked.
2. Open `HabitTrackerApp.csproj`.
3. In the Solution Configuration dropdown, choose **Android Emulator** (or **iOS Simulator** on macOS) and pick a target device.
4. Press **F5** to build and run.

The first build will restore NuGet packages: `Microsoft.Maui.Controls`, `sqlite-net-pcl`, `CommunityToolkit.Maui`, `CommunityToolkit.Maui.MediaElement`.

## Color scheme

| Token | Value | Use |
| --- | --- | --- |
| Primary | `#7C5CFF` | Buttons, headers, ring fill |
| Accent | `#00D9A3` | Highlights, ring head dot, durations |
| BgDeep | `#13131F` | Page background |
| BgCard | `#252537` | Cards |
| TextPrimary | `#FFFFFF` | Headings |
| TextSecondary | `#B4B4C8` | Body |

## Notes

- **Video**: `TutorialPage` plays a sample MP4 from a public URL. To embed a local video, drop an `.mp4` into `Resources/Raw/` and change `Source` to `embed://yourfile.mp4`.
- **Database location**: SQLite file `habits.db3` is stored at `FileSystem.AppDataDirectory` (per-app sandbox). Data persists across app launches.
- **Animation**: The progress ring uses `Animation` + a custom `IDrawable` on `GraphicsView`. It re-runs automatically when the page appears and via the **Replay Animation** button.
