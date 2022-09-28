using System.Windows;

namespace GoGitaReportsApp
{
    public partial class ProgressBarWindow : Window
    {
        public ProgressBarWindow()
        {
            InitializeComponent();
        }
        public void UpdateProgress(int percentage)
        {
            // When progress is reported, update the progress bar control.
            pbStatus.Value = percentage;

            // When progress reaches 100%, close the progress bar window.
            if (percentage == 100)
            {
                Close();
            }
        }
    }
}