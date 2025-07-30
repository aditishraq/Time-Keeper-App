# ⏱️ Time Keeper App

A simple, clean desktop application for tracking time sessions with real-time monitoring and Excel export functionality.

<img src="https://github.com/aditishraq/Time-Keeper-App/blob/main/main_page.png" width="600" alt="Time Keeper App Screenshot">


## Features

- Start and stop timer with simple buttons
- Real-time display of current session
- Add descriptions to your time entries
- All data saved automatically
- Export to CSV for Excel
- Clean, minimal interface
- Single executable file - no installation needed

## How to Use

Track your time with simple start/stop buttons. Add optional descriptions to remember what you worked on. Export your data to CSV when you need reports.

## 🚀 Quick Start

### Building from Source

#### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Windows 10/11 (for Windows Forms)

#### Clone and Build
```bash
git clone https://github.com/aditishraq/Time-Keeper-App.git
cd timekeeper-app
dotnet build
dotnet run
```

#### Create Self-Contained Executable
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## 📊 Data Export Format

The CSV export includes the following columns:
- **Description** - Your session description
- **Start Date** - Date when session started (YYYY-MM-DD)
- **Start Time** - Time when session started (HH:MM:SS)
- **End Date** - Date when session ended (YYYY-MM-DD)
- **End Time** - Time when session ended (HH:MM:SS)
- **Duration Hours** - Whole hours worked
- **Duration Minutes** - Additional minutes worked
- **Total Minutes** - Total session duration in minutes

## 🛠️ Technical Details

- **Framework**: .NET 8.0 Windows Forms
- **Language**: C# 12
- **Data Storage**: Local JSON file (`timeentries.json`)
- **Export Format**: CSV (Excel compatible)
- **Platform**: Windows 10/11

## 📁 Project Structure

```
TimekeeperApp/
├── MainForm.cs          # Main application window and logic
├── Program.cs           # Application entry point
├── TimekeeperApp.csproj # Project configuration
├── favicon.ico          # Application icon
└── README.md           # This file
```


## 🔧 Development

### Adding Features
The application is structured with clean separation of concerns:
- UI controls and layout in `InitializeComponent()`
- Event handlers for user interactions
- Data persistence through JSON serialization
- CSV export functionality



## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

