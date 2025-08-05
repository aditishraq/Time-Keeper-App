using System;
using System.Windows.Forms;

namespace TimekeeperApp
{
    public partial class EditTimeEntryForm : Form
    {
        public TimeEntry TimeEntry { get; private set; }

        private Label lblDescription = null!;
        private TextBox txtDescription = null!;
        private Label lblStartTime = null!;
        private DateTimePicker dtpStartDate = null!;
        private DateTimePicker dtpStartTime = null!;
        private Label lblEndTime = null!;
        private DateTimePicker dtpEndDate = null!;
        private DateTimePicker dtpEndTime = null!;
        private Label lblDuration = null!;
        private Label lblDurationValue = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;

        public EditTimeEntryForm(TimeEntry timeEntry)
        {
            TimeEntry = new TimeEntry
            {
                StartTime = timeEntry.StartTime,
                EndTime = timeEntry.EndTime,
                Duration = timeEntry.Duration,
                Description = timeEntry.Description
            };

            InitializeComponent();
            LoadTimeEntry();
        }

        private void InitializeComponent()
        {
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblStartTime = new Label();
            dtpStartDate = new DateTimePicker();
            dtpStartTime = new DateTimePicker();
            lblEndTime = new Label();
            dtpEndDate = new DateTimePicker();
            dtpEndTime = new DateTimePicker();
            lblDuration = new Label();
            lblDurationValue = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();

            // Form
            Text = "Edit Time Entry";
            Size = new Size(400, 280);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            // lblDescription
            lblDescription.Location = new Point(12, 15);
            lblDescription.Size = new Size(80, 23);
            lblDescription.Text = "Description:";

            // txtDescription
            txtDescription.Location = new Point(100, 12);
            txtDescription.Size = new Size(270, 23);
            txtDescription.TabIndex = 0;

            // lblStartTime
            lblStartTime.Location = new Point(12, 50);
            lblStartTime.Size = new Size(80, 23);
            lblStartTime.Text = "Start Time:";

            // dtpStartDate
            dtpStartDate.Location = new Point(100, 47);
            dtpStartDate.Size = new Size(130, 23);
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.TabIndex = 1;
            dtpStartDate.ValueChanged += DateTime_ValueChanged;

            // dtpStartTime
            dtpStartTime.Location = new Point(240, 47);
            dtpStartTime.Size = new Size(100, 23);
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.ShowUpDown = true;
            dtpStartTime.TabIndex = 2;
            dtpStartTime.ValueChanged += DateTime_ValueChanged;

            // lblEndTime
            lblEndTime.Location = new Point(12, 85);
            lblEndTime.Size = new Size(80, 23);
            lblEndTime.Text = "End Time:";

            // dtpEndDate
            dtpEndDate.Location = new Point(100, 82);
            dtpEndDate.Size = new Size(130, 23);
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.TabIndex = 3;
            dtpEndDate.ValueChanged += DateTime_ValueChanged;

            // dtpEndTime
            dtpEndTime.Location = new Point(240, 82);
            dtpEndTime.Size = new Size(100, 23);
            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.ShowUpDown = true;
            dtpEndTime.TabIndex = 4;
            dtpEndTime.ValueChanged += DateTime_ValueChanged;

            // lblDuration
            lblDuration.Location = new Point(12, 120);
            lblDuration.Size = new Size(80, 23);
            lblDuration.Text = "Duration:";

            // lblDurationValue
            lblDurationValue.Location = new Point(100, 120);
            lblDurationValue.Size = new Size(200, 23);
            lblDurationValue.Text = "00:00:00";

            // btnSave
            btnSave.Location = new Point(190, 180);
            btnSave.Size = new Size(80, 30);
            btnSave.Text = "Save";
            btnSave.TabIndex = 5;
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;

            // btnCancel
            btnCancel.Location = new Point(290, 180);
            btnCancel.Size = new Size(80, 30);
            btnCancel.Text = "Cancel";
            btnCancel.TabIndex = 6;
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            btnCancel.DialogResult = DialogResult.Cancel;

            // Add controls to form
            Controls.AddRange(new Control[] {
                lblDescription, txtDescription,
                lblStartTime, dtpStartDate, dtpStartTime,
                lblEndTime, dtpEndDate, dtpEndTime,
                lblDuration, lblDurationValue,
                btnSave, btnCancel
            });

            CancelButton = btnCancel;
            AcceptButton = btnSave;

            ResumeLayout(false);
            PerformLayout();
        }

        private void LoadTimeEntry()
        {
            txtDescription.Text = TimeEntry.Description;
            dtpStartDate.Value = TimeEntry.StartTime.Date;
            dtpStartTime.Value = TimeEntry.StartTime;
            dtpEndDate.Value = TimeEntry.EndTime.Date;
            dtpEndTime.Value = TimeEntry.EndTime;
            UpdateDuration();
        }

        private void DateTime_ValueChanged(object? sender, EventArgs e)
        {
            UpdateDuration();
        }

        private void UpdateDuration()
        {
            try
            {
                var startDateTime = dtpStartDate.Value.Date + dtpStartTime.Value.TimeOfDay;
                var endDateTime = dtpEndDate.Value.Date + dtpEndTime.Value.TimeOfDay;

                if (endDateTime > startDateTime)
                {
                    var duration = endDateTime - startDateTime;
                    lblDurationValue.Text = $"{duration:hh\\:mm\\:ss}";
                    lblDurationValue.ForeColor = System.Drawing.Color.Black;
                    btnSave.Enabled = true;
                }
                else
                {
                    lblDurationValue.Text = "Invalid: End time must be after start time";
                    lblDurationValue.ForeColor = System.Drawing.Color.Red;
                    btnSave.Enabled = false;
                }
            }
            catch
            {
                lblDurationValue.Text = "Invalid date/time";
                lblDurationValue.ForeColor = System.Drawing.Color.Red;
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                var startDateTime = dtpStartDate.Value.Date + dtpStartTime.Value.TimeOfDay;
                var endDateTime = dtpEndDate.Value.Date + dtpEndTime.Value.TimeOfDay;

                if (endDateTime <= startDateTime)
                {
                    MessageBox.Show("End time must be after start time.", "Invalid Time Range",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TimeEntry.Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? "Session" : txtDescription.Text.Trim();
                TimeEntry.StartTime = startDateTime;
                TimeEntry.EndTime = endDateTime;
                TimeEntry.Duration = endDateTime - startDateTime;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving time entry: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}