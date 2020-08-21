using Ookii.Dialogs.Wpf;
using System.Windows;
using TuneReportViewer.Model;
using System.Collections.Generic;
using TuneReportViewer.View;
using System;
using System.Windows.Documents;
using LiveCharts;
using LiveCharts.Wpf;

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

        List<QQQTuneReport> qqqTRList;

        private void btn_SearchClicked(object sender, RoutedEventArgs e)
        {
            DataReader dataReader = new DataReader();
            qqqTRList = dataReader.MainProgram(txtBox_FolderPath.Text);
            PopulateData();
        }

        private void btn_ExportClicked(object sender, RoutedEventArgs e)
        {
        }

        private void PopulateData()
        {
            List<TableData> filteredData = new List<TableData>();
            if (qqqTRList != null)
            {
                for (int i = 0; i < qqqTRList.Count; i++)
                {
                    // This tune report should be skipped if the checkbox is checked
                    if (chkBox_Filter.IsChecked == true && qqqTRList[i].passStatus == false)
                    {
                        continue;
                    } else
                    {
                        if (qqqTRList[i].passStatus == true)
                        {
                            if (radBtn_Pos.IsChecked == true && qqqTRList[i].posemv == 0)
                            {
                                continue;
                            } else if (radBtn_Neg.IsChecked == true && qqqTRList[i].negemv == 0) {
                                continue;
                            }
                        }
                    }

                    TableData newTableDataLine = new TableData();

                    newTableDataLine.tuneDateTime = qqqTRList[i].tuneDateTime;
                    newTableDataLine.passStatus = qqqTRList[i].passStatus;
                    newTableDataLine.tuneType = qqqTRList[i].tuneType;
                    newTableDataLine.posemv = qqqTRList[i].posemv;
                    newTableDataLine.negemv = qqqTRList[i].negemv;

                    if (radBtn_Pos.IsChecked == true)
                    {
                        if (radBtn_Res1.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = qqqTRList[i].positivestandardms1mass1Ab;
                            newTableDataLine.ms1_2 = qqqTRList[i].positivestandardms1mass2Ab;
                            newTableDataLine.ms1_3 = qqqTRList[i].positivestandardms1mass3Ab;
                            newTableDataLine.ms1_4 = qqqTRList[i].positivestandardms1mass4Ab;
                            newTableDataLine.ms1_5 = qqqTRList[i].positivestandardms1mass5Ab;
                            newTableDataLine.ms1_6 = qqqTRList[i].positivestandardms1mass6Ab;
                            newTableDataLine.ms2_1 = qqqTRList[i].positivestandardms2mass1Ab;
                            newTableDataLine.ms2_2 = qqqTRList[i].positivestandardms2mass2Ab;
                            newTableDataLine.ms2_3 = qqqTRList[i].positivestandardms2mass3Ab;
                            newTableDataLine.ms2_4 = qqqTRList[i].positivestandardms2mass4Ab;
                            newTableDataLine.ms2_5 = qqqTRList[i].positivestandardms2mass5Ab;
                            newTableDataLine.ms2_6 = qqqTRList[i].positivestandardms2mass6Ab;
                        } else if (radBtn_Res2.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = qqqTRList[i].positivewidems1mass1Ab;
                            newTableDataLine.ms1_2 = qqqTRList[i].positivewidems1mass2Ab;
                            newTableDataLine.ms1_3 = qqqTRList[i].positivewidems1mass3Ab;
                            newTableDataLine.ms1_4 = qqqTRList[i].positivewidems1mass4Ab;
                            newTableDataLine.ms1_5 = qqqTRList[i].positivewidems1mass5Ab;
                            newTableDataLine.ms1_6 = qqqTRList[i].positivewidems1mass6Ab;
                            newTableDataLine.ms2_1 = qqqTRList[i].positivewidems2mass1Ab;
                            newTableDataLine.ms2_2 = qqqTRList[i].positivewidems2mass2Ab;
                            newTableDataLine.ms2_3 = qqqTRList[i].positivewidems2mass3Ab;
                            newTableDataLine.ms2_4 = qqqTRList[i].positivewidems2mass4Ab;
                            newTableDataLine.ms2_5 = qqqTRList[i].positivewidems2mass5Ab;
                            newTableDataLine.ms2_6 = qqqTRList[i].positivewidems2mass6Ab;
                        } else if (radBtn_Res3.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = qqqTRList[i].positivewidestms1mass1Ab;
                            newTableDataLine.ms1_2 = qqqTRList[i].positivewidestms1mass2Ab;
                            newTableDataLine.ms1_3 = qqqTRList[i].positivewidestms1mass3Ab;
                            newTableDataLine.ms1_4 = qqqTRList[i].positivewidestms1mass4Ab;
                            newTableDataLine.ms1_5 = qqqTRList[i].positivewidestms1mass5Ab;
                            newTableDataLine.ms1_6 = qqqTRList[i].positivewidestms1mass6Ab;
                            newTableDataLine.ms2_1 = qqqTRList[i].positivewidestms2mass1Ab;
                            newTableDataLine.ms2_2 = qqqTRList[i].positivewidestms2mass2Ab;
                            newTableDataLine.ms2_3 = qqqTRList[i].positivewidestms2mass3Ab;
                            newTableDataLine.ms2_4 = qqqTRList[i].positivewidestms2mass4Ab;
                            newTableDataLine.ms2_5 = qqqTRList[i].positivewidestms2mass5Ab;
                            newTableDataLine.ms2_6 = qqqTRList[i].positivewidestms2mass6Ab;
                        }
                    } else
                    {
                        if (radBtn_Res1.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = qqqTRList[i].negativestandardms1mass1Ab;
                            newTableDataLine.ms1_2 = qqqTRList[i].negativestandardms1mass2Ab;
                            newTableDataLine.ms1_3 = qqqTRList[i].negativestandardms1mass3Ab;
                            newTableDataLine.ms1_4 = qqqTRList[i].negativestandardms1mass4Ab;
                            newTableDataLine.ms1_5 = qqqTRList[i].negativestandardms1mass5Ab;
                            newTableDataLine.ms1_6 = qqqTRList[i].negativestandardms1mass6Ab;
                            newTableDataLine.ms2_1 = qqqTRList[i].negativestandardms2mass1Ab;
                            newTableDataLine.ms2_2 = qqqTRList[i].negativestandardms2mass2Ab;
                            newTableDataLine.ms2_3 = qqqTRList[i].negativestandardms2mass3Ab;
                            newTableDataLine.ms2_4 = qqqTRList[i].negativestandardms2mass4Ab;
                            newTableDataLine.ms2_5 = qqqTRList[i].negativestandardms2mass5Ab;
                            newTableDataLine.ms2_6 = qqqTRList[i].negativestandardms2mass6Ab;
                        }
                        else if (radBtn_Res2.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = qqqTRList[i].negativewidems1mass1Ab;
                            newTableDataLine.ms1_2 = qqqTRList[i].negativewidems1mass2Ab;
                            newTableDataLine.ms1_3 = qqqTRList[i].negativewidems1mass3Ab;
                            newTableDataLine.ms1_4 = qqqTRList[i].negativewidems1mass4Ab;
                            newTableDataLine.ms1_5 = qqqTRList[i].negativewidems1mass5Ab;
                            newTableDataLine.ms1_6 = qqqTRList[i].negativewidems1mass6Ab;
                            newTableDataLine.ms2_1 = qqqTRList[i].negativewidems2mass1Ab;
                            newTableDataLine.ms2_2 = qqqTRList[i].negativewidems2mass2Ab;
                            newTableDataLine.ms2_3 = qqqTRList[i].negativewidems2mass3Ab;
                            newTableDataLine.ms2_4 = qqqTRList[i].negativewidems2mass4Ab;
                            newTableDataLine.ms2_5 = qqqTRList[i].negativewidems2mass5Ab;
                            newTableDataLine.ms2_6 = qqqTRList[i].negativewidems2mass6Ab;
                        }
                        else if (radBtn_Res3.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = qqqTRList[i].negativewidestms1mass1Ab;
                            newTableDataLine.ms1_2 = qqqTRList[i].negativewidestms1mass2Ab;
                            newTableDataLine.ms1_3 = qqqTRList[i].negativewidestms1mass3Ab;
                            newTableDataLine.ms1_4 = qqqTRList[i].negativewidestms1mass4Ab;
                            newTableDataLine.ms1_5 = qqqTRList[i].negativewidestms1mass5Ab;
                            newTableDataLine.ms1_6 = qqqTRList[i].negativewidestms1mass6Ab;
                            newTableDataLine.ms2_1 = qqqTRList[i].negativewidestms2mass1Ab;
                            newTableDataLine.ms2_2 = qqqTRList[i].negativewidestms2mass2Ab;
                            newTableDataLine.ms2_3 = qqqTRList[i].negativewidestms2mass3Ab;
                            newTableDataLine.ms2_4 = qqqTRList[i].negativewidestms2mass4Ab;
                            newTableDataLine.ms2_5 = qqqTRList[i].negativewidestms2mass5Ab;
                            newTableDataLine.ms2_6 = qqqTRList[i].negativewidestms2mass6Ab;
                        }
                    }

                    filteredData.Add(newTableDataLine);
                }
                lvUsers.ItemsSource = filteredData;
            }
        }

        class TableData
        {
            public DateTime tuneDateTime { get; set; }
            public bool passStatus { get; set; }
            public string tuneType { get; set; }
            public int posemv { get; set; }
            public int negemv { get; set; }
            public float ms1_1 { get; set; }
            public float ms1_2 { get; set; }
            public float ms1_3 { get; set; }
            public float ms1_4 { get; set; }
            public float ms1_5 { get; set; }
            public float ms1_6 { get; set; }
            public float ms2_1 { get; set; }
            public float ms2_2 { get; set; }
            public float ms2_3 { get; set; }
            public float ms2_4 { get; set; }
            public float ms2_5 { get; set; }
            public float ms2_6 { get; set; }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            PopulateData();
        }
    }
}
