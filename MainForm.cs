using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimekeeperApp
{
    public partial class MainForm : Form
    {
        private List<TimeEntry> timeEntries;
        private DateTime? startTime;
        private bool isTracking;

        private Button btnStart = null!;
        private Button btnStop = null!;
        private Label lblCurrentSession = null!;
        private Label lblStatus = null!;
        private DataGridView dgvTimeEntries = null!;
        private Button btnExportExcel = null!;
        private Button btnClearAll = null!;
        private TextBox txtDescription = null!;
        private Label lblDescription = null!;

        public MainForm()
        {
            InitializeComponent();
            timeEntries = new List<TimeEntry>();
            isTracking = false;
            LoadTimeEntries();
            UpdateDisplay();
        }

        private void InitializeComponent()
        {
            btnStart = new Button();
            btnStop = new Button();
            lblCurrentSession = new Label();
            lblStatus = new Label();
            dgvTimeEntries = new DataGridView();
            btnExportExcel = new Button();
            btnClearAll = new Button();
            txtDescription = new TextBox();
            lblDescription = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTimeEntries).BeginInit();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 50);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(100, 40);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(125, 50);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(100, 40);
            btnStop.TabIndex = 1;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // lblCurrentSession
            // 
            lblCurrentSession.Location = new Point(12, 100);
            lblCurrentSession.Name = "lblCurrentSession";
            lblCurrentSession.Size = new Size(400, 23);
            lblCurrentSession.TabIndex = 2;
            lblCurrentSession.Text = "No active session";
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(240, 50);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(200, 40);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Ready to start";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvTimeEntries
            // 
            dgvTimeEntries.AllowUserToAddRows = false;
            dgvTimeEntries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTimeEntries.ColumnHeadersHeight = 40;
            dgvTimeEntries.Location = new Point(12, 130);
            dgvTimeEntries.Name = "dgvTimeEntries";
            dgvTimeEntries.ReadOnly = true;
            dgvTimeEntries.RowHeadersWidth = 72;
            dgvTimeEntries.Size = new Size(760, 350);
            dgvTimeEntries.TabIndex = 4;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(12, 490);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(120, 30);
            btnExportExcel.TabIndex = 5;
            btnExportExcel.Text = "Export to Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.Location = new Point(145, 490);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(100, 30);
            btnClearAll.TabIndex = 6;
            btnClearAll.Text = "Clear All";
            btnClearAll.UseVisualStyleBackColor = true;
            btnClearAll.Click += btnClearAll_Click;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(100, 12);
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Enter description (optional)";
            txtDescription.Size = new Size(300, 35);
            txtDescription.TabIndex = 7;
            // 
            // lblDescription
            // 
            lblDescription.Location = new Point(12, 12);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(80, 23);
            lblDescription.TabIndex = 8;
            lblDescription.Text = "Description:";
            // 
            // MainForm
            // 
            ClientSize = new Size(776, 542);
            Controls.Add(btnStart);
            Controls.Add(btnStop);
            Controls.Add(lblCurrentSession);
            Controls.Add(lblStatus);
            Controls.Add(dgvTimeEntries);
            Controls.Add(btnExportExcel);
            Controls.Add(btnClearAll);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Time Keeper";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTimeEntries).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnStart_Click(object? sender, EventArgs e)
        {
            startTime = DateTime.Now;
            isTracking = true;

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            txtDescription.Enabled = false;

            lblStatus.Text = "Tracking...";
            UpdateCurrentSession();
        }

        private void btnStop_Click(object? sender, EventArgs e)
        {
            if (startTime.HasValue)
            {
                var endTime = DateTime.Now;
                var duration = endTime - startTime.Value;

                var entry = new TimeEntry
                {
                    StartTime = startTime.Value,
                    EndTime = endTime,
                    Duration = duration,
                    Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? "Session" : txtDescription.Text
                };

                timeEntries.Add(entry);
                SaveTimeEntries();
                UpdateDisplay();

                startTime = null;
                isTracking = false;

                btnStart.Enabled = true;
                btnStop.Enabled = false;
                txtDescription.Enabled = true;
                txtDescription.Clear();

                lblStatus.Text = "Session completed";
                lblCurrentSession.Text = "No active session";
            }
        }

        private void UpdateCurrentSession()
        {
            if (isTracking && startTime.HasValue)
            {
                var elapsed = DateTime.Now - startTime.Value;
                lblCurrentSession.Text = $"Current session: {elapsed:hh\\:mm\\:ss} (Started: {startTime.Value:yyyy-MM-dd HH:mm:ss})";

                // Update every second
                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 1000;
                timer.Tick += (s, e) =>
                {
                    if (isTracking && startTime.HasValue)
                    {
                        var currentElapsed = DateTime.Now - startTime.Value;
                        lblCurrentSession.Text = $"Current session: {currentElapsed:hh\\:mm\\:ss} (Started: {startTime.Value:yyyy-MM-dd HH:mm:ss})";
                    }
                    else
                    {
                        ((System.Windows.Forms.Timer)s!).Stop();
                    }
                };
                timer.Start();
            }
        }

        private void UpdateDisplay()
        {
            dgvTimeEntries.DataSource = null;
            dgvTimeEntries.DataSource = timeEntries.OrderByDescending(t => t.StartTime).ToList();

            if (dgvTimeEntries.Columns.Count > 0)
            {
                dgvTimeEntries.Columns["StartTime"].HeaderText = "Start Time";
                dgvTimeEntries.Columns["EndTime"].HeaderText = "End Time";
                dgvTimeEntries.Columns["Duration"].HeaderText = "Duration";
                dgvTimeEntries.Columns["Description"].HeaderText = "Description";

                dgvTimeEntries.Columns["StartTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                dgvTimeEntries.Columns["EndTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                dgvTimeEntries.Columns["Duration"].DefaultCellStyle.Format = @"hh\:mm\:ss";
            }
        }

        private void btnExportExcel_Click(object? sender, EventArgs e)
        {
            if (timeEntries.Count == 0)
            {
                MessageBox.Show("No time entries to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            saveDialog.DefaultExt = "csv";
            saveDialog.FileName = $"TimeEntries_{DateTime.Now:yyyyMMdd}.csv";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportToCsv(saveDialog.FileName);
                    MessageBox.Show($"Time entries exported successfully to {saveDialog.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting data: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToCsv(string fileName)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Description,Start Date,Start Time,End Date,End Time,Duration Hours,Duration Minutes,Total Minutes");

            foreach (var entry in timeEntries.OrderBy(t => t.StartTime))
            {
                var durationHours = (int)entry.Duration.TotalHours;
                var durationMinutes = entry.Duration.Minutes;
                var totalMinutes = (int)entry.Duration.TotalMinutes;

                csv.AppendLine($"\"{entry.Description}\"," +
                              $"{entry.StartTime:yyyy-MM-dd}," +
                              $"{entry.StartTime:HH:mm:ss}," +
                              $"{entry.EndTime:yyyy-MM-dd}," +
                              $"{entry.EndTime:HH:mm:ss}," +
                              $"{durationHours}," +
                              $"{durationMinutes}," +
                              $"{totalMinutes}");
            }

            File.WriteAllText(fileName, csv.ToString());
        }

        private void btnClearAll_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to clear all time entries? This cannot be undone.",
                                       "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                timeEntries.Clear();
                SaveTimeEntries();
                UpdateDisplay();
            }
        }

        private void SaveTimeEntries()
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(timeEntries, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText("timeentries.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadTimeEntries()
        {
            try
            {
                if (File.Exists("timeentries.json"))
                {
                    var json = File.ReadAllText("timeentries.json");
                    timeEntries = System.Text.Json.JsonSerializer.Deserialize<List<TimeEntry>>(json) ?? new List<TimeEntry>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                timeEntries = new List<TimeEntry>();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isTracking)
            {
                var result = MessageBox.Show("You have an active session. Do you want to stop it before closing?",
                                           "Active Session", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    btnStop_Click(this, EventArgs.Empty);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }

    public class TimeEntry
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; } = "";
    }
}