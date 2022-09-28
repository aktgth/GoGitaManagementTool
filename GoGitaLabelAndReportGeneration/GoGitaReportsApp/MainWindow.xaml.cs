using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoGitaReportsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProgressBarWindow pbw = null;
        private ObservableCollection<StatusItem> srcItems;
        private ObservableCollection<StatusItem> destItems;
        private string selectedReport;
        private string reportFilePath;
        private string deliveryType;
        private DateTime? startDateTime;
        private DateTime? endDateTime;
        public MainWindow()
        {
            InitializeComponent();
            srcItems = new ObservableCollection<StatusItem>();
            destItems = new ObservableCollection<StatusItem>();
            srcItems.Add(new StatusItem() { StatusType = "Pending payment" });
            srcItems.Add(new StatusItem() { StatusType = "Processing" });
            srcItems.Add(new StatusItem() { StatusType = "On hold" });
            srcItems.Add(new StatusItem() { StatusType = "Completed" });
            srcItems.Add(new StatusItem() { StatusType = "Cancelled" });
            srcItems.Add(new StatusItem() { StatusType = "Refunded" });
            srcItems.Add(new StatusItem() { StatusType = "Failed" });
            srcItems.Add(new StatusItem() { StatusType = "Draft" });

            srcListView.ItemsSource = srcItems;
            destListView.ItemsSource = destItems;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(srcListView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("StatusType", ListSortDirection.Ascending));
            selectedReport = "Order Address Labels";
            deliveryType = "Delivery";
        }

        private void SrcItem_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem;
            if (listViewItem != null && e.LeftButton == MouseButtonState.Pressed)
            {
                //MessageBox.Show("Selected item content is: " + ((StatusItem)listViewItem.Content).StatusType.ToString());
                string statusType = ((StatusItem)listViewItem.Content).StatusType.ToString();
                DragDropEffects dragDropEffects =  DragDrop.DoDragDrop(listViewItem,
                                     new DataObject(DataFormats.Serializable, statusType),
                                     DragDropEffects.Copy | DragDropEffects.Move);
                if(dragDropEffects == DragDropEffects.None)
                {
                    if (!srcItems.Any(item => item.StatusType == statusType))
                        srcItems.Add(new StatusItem() { StatusType = statusType });
                }
            }
        }

        private void SrcItemList_Drop(object sender, DragEventArgs e)
        {
            string statusType = e.Data.GetData(DataFormats.Serializable).ToString();
            if(!srcItems.Any(item => item.StatusType == statusType))
                srcItems.Add(new StatusItem() { StatusType = statusType });
        }

        private void SrcItemList_DragOver(object sender, DragEventArgs e)
        {
            string statusType = e.Data.GetData(DataFormats.Serializable).ToString();
            if (!srcItems.Any(item => item.StatusType == statusType))
                srcItems.Add(new StatusItem() { StatusType = statusType });
        }

        private void SrcItemList_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void SrcItemList_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void SrcItemList_DragLeave(object sender, DragEventArgs e)
        {
            string statusType = e.Data.GetData(DataFormats.Serializable).ToString();
            if (srcItems.Any(item => item.StatusType == statusType))
                srcItems.Remove(srcItems.Where(item => item.StatusType == statusType).First());
        }

        private void DestItem_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem;
            if (listViewItem != null && e.LeftButton == MouseButtonState.Pressed)
            {
                string statusType = ((StatusItem)listViewItem.Content).StatusType.ToString();
                DragDropEffects dragDropEffects = DragDrop.DoDragDrop(listViewItem,
                                     new DataObject(DataFormats.Serializable, statusType),
                                     DragDropEffects.Copy | DragDropEffects.Move);

                if (dragDropEffects == DragDropEffects.None)
                {
                    if (!destItems.Any(item => item.StatusType == statusType))
                        destItems.Add(new StatusItem() { StatusType = statusType });
                }
            }
        }

        private void DestItemList_Drop(object sender, DragEventArgs e)
        {
            string statusType = e.Data.GetData(DataFormats.Serializable).ToString();
            if (!destItems.Any(item => item.StatusType == statusType))
                destItems.Add(new StatusItem() { StatusType = statusType });
        }

        private void DestItemList_DragOver(object sender, DragEventArgs e)
        {
            string statusType = e.Data.GetData(DataFormats.Serializable).ToString();
            if (!destItems.Any(item => item.StatusType == statusType))
                destItems.Add(new StatusItem() { StatusType = statusType });
        }

        private void DestItemList_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void DestItemList_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void DestItemList_DragLeave(object sender, DragEventArgs e)
        {
            string statusType = e.Data.GetData(DataFormats.Serializable).ToString();
            if (destItems.Any(item => item.StatusType == statusType))
                destItems.Remove(destItems.Where(item => item.StatusType == statusType).First());
        }

        private void ReportRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            selectedReport = radioButton.Content.ToString();
        }

        private void DeliveryRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            deliveryType = radioButton.Content.ToString();
        }

        private void datePickerFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            if (datePicker == null)
                return;
            startDateTime = datePicker.SelectedDate;
        }

        private void datePickerTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            if (datePicker == null)
                return;
            endDateTime = datePicker.SelectedDate;
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            var buttonClick = sender as Button;
            if (destItems.Count <= 0 || !startDateTime.HasValue || !endDateTime.HasValue)
            {
                MessageBox.Show("Make sure all the fields are filled!", "Go Gita Reports Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Docx file (*.docx)|*.docx|All files (*.*)|(*.*)|C# file (*.cs)|*.cs";
            //logic to use all the variables
            if (saveFileDialog.ShowDialog() == true)
            {
                MessageBox.Show("The filepath to be saved is: " + saveFileDialog.FileName);
                
                reportFilePath = saveFileDialog.FileName;
                try
                {
                    // Using background worker to asynchronously run work method.
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerReportsProgress = true;
                    worker.DoWork += GenerateReportAsync;
                    worker.ProgressChanged += worker_ProgressChanged;
                    worker.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }

                
                
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Notifying the progress bar window of the current progress.
            pbw.UpdateProgress(e.ProgressPercentage);
        }

        private void GenerateReportAsync(object sender, DoWorkEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(reportFilePath);
            DateTime createdTime = fileInfo.CreationTimeUtc;
            Dispatcher.Invoke(() =>
            {
                // Disabling parent window controls while the work is being done.
                pnlMainGrid.IsEnabled = false;
                //Launch the progress bar window using Show()
                pbw = new ProgressBarWindow();
                pbw.Show();
            });
            OrderReportUtils.GenerateReport(selectedReport, reportFilePath, deliveryType, destItems.Select(a => a.StatusType).ToList(),
                startDateTime.Value, endDateTime.Value);

            (sender as BackgroundWorker).ReportProgress(100);
            Dispatcher.Invoke(() =>
            {
                //Disabling parent window controls while the work is being done.
                pnlMainGrid.IsEnabled = true; 
                pbw.Close();
            });
            if (File.Exists(reportFilePath) && createdTime.CompareTo(DateTime.UtcNow) < 0)
                MessageBox.Show("Generate action completed and file is saved at the path: " + reportFilePath
                    , "Go Gita Reports Success", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Generate action incompelete and some issue happened!", "Go Gita Reports Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public class StatusItem
    {
        public string StatusType { get; set; }
    }
}
