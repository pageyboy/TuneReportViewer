using Ookii.Dialogs.Wpf;
using System.Windows;
using TuneReportViewer.Model;

namespace TuneReportViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtBox_FolderPath.Text = "C:\\Users\\chripage\\OneDrive - Agilent Technologies\\Side Projects\\Visual Studio\\tune_report_viewer\\Old Versions\\Tune Report Viewer\\3_Example Data\\G6410B";
        }

        private void btn_BrowseClicked(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Please select a folder.";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            if ((bool)dialog.ShowDialog(this))
                txtBox_FolderPath.Text = dialog.SelectedPath;
        }

        private void btn_SearchClicked(object sender, RoutedEventArgs e)
        {
            DataReader myClass = new DataReader();
            myClass.MainProgram();
        }

        private void btn_ExportClicked(object sender, RoutedEventArgs e)
        {
        }
    }
}
