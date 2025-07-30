# ⏱️ Time Keeper App

A simple, clean desktop application for tracking time sessions with real-time monitoring and Excel export functionality.

![Time Keeper App](https://via.placeholder.com/800x500/2563eb/ffffff?text=Time+Keeper+App+Screenshot)

## ✨ Features

- **One-Click Time Tracking** - Start and stop timing sessions with simple buttons
- **Real-Time Display** - Watch your session time update live as you work
- **Session Descriptions** - Add optional descriptions to categorize your activities
- **Automatic Data Persistence** - All sessions are automatically saved locally
- **Excel Export** - Export your time data to CSV format for analysis in Excel
- **Clean Interface** - Minimalist design focused on productivity
- **Self-Contained** - No installation required, runs as a single executable

## 🖥️ Screenshots

### Main Interface
- Clean, intuitive design with Start/Stop buttons
- Real-time session timer display
- Data grid showing all past sessions
- Optional description field for each session

### Export Features
- Export all time entries to CSV format
- Includes separate date/time columns for easy Excel filtering
- Duration provided in multiple formats (hours, minutes, total minutes)

## 🚀 Quick Start

### Download & Run
1. Download the latest release from the [Releases](../../releases) page
2. Extract the ZIP file to your desired location
3. Double-click `TimekeeperApp.exe` to run
4. No installation required!

### Building from Source

#### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Windows 10/11 (for Windows Forms)

#### Clone and Build
```bash
git clone https://github.com/yourusername/timekeeper-app.git
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

## 🎯 Use Cases

- **Freelance Work** - Track billable hours for different clients
- **Study Sessions** - Monitor time spent on different subjects
- **Project Management** - Log time on various project tasks
- **Personal Productivity** - Track focused work periods
- **Time Auditing** - Understand how you spend your time

## 🔧 Development

### Adding Features
The application is structured with clean separation of concerns:
- UI controls and layout in `InitializeComponent()`
- Event handlers for user interactions
- Data persistence through JSON serialization
- CSV export functionality

### Contributing
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Support

If you encounter any issues or have suggestions:
- Open an [Issue](../../issues) on GitHub
- Provide details about your system and the problem
- Include screenshots if helpful

## 🗺️ Roadmap

- [ ] Dark mode theme
- [ ] Multiple timer support
- [ ] Categories and tags for sessions
- [ ] Time goals and notifications
- [ ] Weekly/monthly reports
- [ ] Database export options
- [ ] System tray integration

## 👨‍💻 Author

**[Your Name]** - [Your GitHub Profile](https://github.com/yourusername)

## 🙏 Acknowledgments

- Built with .NET 8.0 and Windows Forms
- Icon designed using [mention your icon source]
- Inspired by the need for simple, effective time tracking

---

⭐ Star this repository if you find it helpful!
