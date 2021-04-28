using Ookii.Dialogs.Wpf;
using System.Windows;
using TuneReportViewer.Model;
using System.Collections.Generic;
using TuneReportViewer.View;
using System;
using System.Windows.Documents;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Events;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;
using System.Data;
using System.Collections;

namespace TuneReportViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Load Main Window and initialize SeriesCollection
        public MainWindow()
        {
            InitializeComponent();
            txtBox_FolderPath.Text = "C:\\Users\\chripage\\OneDrive - Agilent Technologies\\Side Projects\\Visual Studio\\tune_report_viewer\\Old Versions\\Tune Report Viewer\\3_Example Data\\G6410B";
            SeriesCollection = new SeriesCollection();
        }
        
        /// <summary>
        /// Method for Browse Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_BrowseClicked(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Please select a folder.";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
                System.Windows.MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            if ((bool)dialog.ShowDialog(this))
                txtBox_FolderPath.Text = dialog.SelectedPath;
        }

        // Set properties for use between various methods
        List<QQQTuneReport> qqqTRList;
        List<TableData> filteredData = new List<TableData>();
        List<TableData> filteredChart = new List<TableData>();

        /// <summary>
        /// Method for handling the Search Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SearchClicked(object sender, RoutedEventArgs e)
        {
            // Run methods to search for Tune Reports
            DataReader dataReader = new DataReader();
            qqqTRList = dataReader.MainProgram(txtBox_FolderPath.Text);

            // Update the table and charts
            UpdateTableandChart();
        }

        /// <summary>
        /// Method for handling the Export button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExportClicked(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Class that is a minimum version of the QQQ Tune Report class
        /// This is the data set that will be tabulated and charted
        /// </summary>
        class TableData : IComparable<TableData>
        {
            public DateTime tuneDateTime { get; set; }
            public bool passStatus { get; set; }
            public string tuneType { get; set; }
            public double posemv { get; set; }
            public double negemv { get; set; }
            public double ms1_1 { get; set; }
            public double ms1_2 { get; set; }
            public double ms1_3 { get; set; }
            public double ms1_4 { get; set; }
            public double ms1_5 { get; set; }
            public double ms1_6 { get; set; }
            public double ms2_1 { get; set; }
            public double ms2_2 { get; set; }
            public double ms2_3 { get; set; }
            public double ms2_4 { get; set; }
            public double ms2_5 { get; set; }
            public double ms2_6 { get; set; }
            public bool includeInChart { get; set; }

            public int CompareTo(TableData compareTableData)
            {
                if(compareTableData == null)
                {
                    return 1;
                } else
                {
                    return this.tuneDateTime.CompareTo(compareTableData.tuneDateTime);
                }
                throw new NotImplementedException();
            }
            
            public bool Equals(TableData other)
            {
                if (other == null)
                {
                    return false;
                }
                return (this.tuneDateTime.Equals(other.tuneDateTime));
            }
            
        }

        /// <summary>
        /// Filter the returned data based on the Chart and Table Filter Options
        /// </summary>
        /// <param name="NaN"></param>
        /// <returns>
        /// Returns a table where null data is either NaN or Zeroed. When NaN is set to true then null
        /// values are returned as Double.NaN. This data is for use with the Charts. Whereas Zeroed
        /// data can be string formatted out in the Grid Table. Double.NaN cannot be string formatted out
        /// </returns>
        List<TableData> FilterData(bool NaN)
        {
            // Create a new list that will ultimately be returned
            List<TableData> returnTable = new List<TableData>();
            // Check whether the main data set is not null
            if (qqqTRList != null)
            {
                for (int i = 0; i < qqqTRList.Count; i++)
                {
                    // Determine whether the current tune report should be included in the filtered
                    // dataset or whether it should be filtered out based on polarity or pass status
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

                    // Filter out if this was a Chart Filtered dataset and the Report was
                    // chosen to not be included in the chart
                    if (NaN == true && qqqTRList[i].includeInChart == false)
                    {
                        continue;
                    }

                    // If the dataset is to be included then initialize a new line of data
                    TableData newTableDataLine = new TableData();

                    // Set the newTableDataLine properties based on the Tune Reports Properties
                    newTableDataLine.tuneDateTime = qqqTRList[i].tuneDateTime;
                    newTableDataLine.passStatus = qqqTRList[i].passStatus;
                    newTableDataLine.tuneType = qqqTRList[i].tuneType;
                    newTableDataLine.posemv = NaN ? (qqqTRList[i].posemv == 0 ? double.NaN : qqqTRList[i].posemv) : qqqTRList[i].posemv;
                    newTableDataLine.negemv = NaN ? (qqqTRList[i].negemv == 0 ? double.NaN : qqqTRList[i].negemv) : qqqTRList[i].negemv;
                    newTableDataLine.includeInChart = qqqTRList[i].includeInChart;

                    if (radBtn_Pos.IsChecked == true)
                    {
                        if (radBtn_Res1.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = NaN ? (qqqTRList[i].positivestandardms1mass1Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms1mass1Ab) : qqqTRList[i].positivestandardms1mass1Ab;
                            newTableDataLine.ms1_2 = NaN ? (qqqTRList[i].positivestandardms1mass2Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms1mass2Ab) : qqqTRList[i].positivestandardms1mass2Ab;
                            newTableDataLine.ms1_3 = NaN ? (qqqTRList[i].positivestandardms1mass3Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms1mass3Ab) : qqqTRList[i].positivestandardms1mass3Ab;
                            newTableDataLine.ms1_4 = NaN ? (qqqTRList[i].positivestandardms1mass4Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms1mass4Ab) : qqqTRList[i].positivestandardms1mass4Ab;
                            newTableDataLine.ms1_5 = NaN ? (qqqTRList[i].positivestandardms1mass5Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms1mass5Ab) : qqqTRList[i].positivestandardms1mass5Ab;
                            newTableDataLine.ms1_6 = NaN ? (qqqTRList[i].positivestandardms1mass6Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms1mass6Ab) : qqqTRList[i].positivestandardms1mass6Ab;
                            newTableDataLine.ms2_1 = NaN ? (qqqTRList[i].positivestandardms2mass1Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms2mass1Ab) : qqqTRList[i].positivestandardms2mass1Ab;
                            newTableDataLine.ms2_2 = NaN ? (qqqTRList[i].positivestandardms2mass2Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms2mass2Ab) : qqqTRList[i].positivestandardms2mass2Ab;
                            newTableDataLine.ms2_3 = NaN ? (qqqTRList[i].positivestandardms2mass3Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms2mass3Ab) : qqqTRList[i].positivestandardms2mass3Ab;
                            newTableDataLine.ms2_4 = NaN ? (qqqTRList[i].positivestandardms2mass4Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms2mass4Ab) : qqqTRList[i].positivestandardms2mass4Ab;
                            newTableDataLine.ms2_5 = NaN ? (qqqTRList[i].positivestandardms2mass5Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms2mass5Ab) : qqqTRList[i].positivestandardms2mass5Ab;
                            newTableDataLine.ms2_6 = NaN ? (qqqTRList[i].positivestandardms2mass6Ab == 0 ? double.NaN : qqqTRList[i].positivestandardms2mass6Ab) : qqqTRList[i].positivestandardms2mass6Ab;
                        } else if (radBtn_Res2.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = NaN ? (qqqTRList[i].positivewidems1mass1Ab == 0 ? double.NaN : qqqTRList[i].positivewidems1mass1Ab) : qqqTRList[i].positivewidems1mass1Ab;
                            newTableDataLine.ms1_2 = NaN ? (qqqTRList[i].positivewidems1mass2Ab == 0 ? double.NaN : qqqTRList[i].positivewidems1mass2Ab) : qqqTRList[i].positivewidems1mass2Ab;
                            newTableDataLine.ms1_3 = NaN ? (qqqTRList[i].positivewidems1mass3Ab == 0 ? double.NaN : qqqTRList[i].positivewidems1mass3Ab) : qqqTRList[i].positivewidems1mass3Ab;
                            newTableDataLine.ms1_4 = NaN ? (qqqTRList[i].positivewidems1mass4Ab == 0 ? double.NaN : qqqTRList[i].positivewidems1mass4Ab) : qqqTRList[i].positivewidems1mass4Ab;
                            newTableDataLine.ms1_5 = NaN ? (qqqTRList[i].positivewidems1mass5Ab == 0 ? double.NaN : qqqTRList[i].positivewidems1mass5Ab) : qqqTRList[i].positivewidems1mass5Ab;
                            newTableDataLine.ms1_6 = NaN ? (qqqTRList[i].positivewidems1mass6Ab == 0 ? double.NaN : qqqTRList[i].positivewidems1mass6Ab) : qqqTRList[i].positivewidems1mass6Ab;
                            newTableDataLine.ms2_1 = NaN ? (qqqTRList[i].positivewidems2mass1Ab == 0 ? double.NaN : qqqTRList[i].positivewidems2mass1Ab) : qqqTRList[i].positivewidems2mass1Ab;
                            newTableDataLine.ms2_2 = NaN ? (qqqTRList[i].positivewidems2mass2Ab == 0 ? double.NaN : qqqTRList[i].positivewidems2mass2Ab) : qqqTRList[i].positivewidems2mass2Ab;
                            newTableDataLine.ms2_3 = NaN ? (qqqTRList[i].positivewidems2mass3Ab == 0 ? double.NaN : qqqTRList[i].positivewidems2mass3Ab) : qqqTRList[i].positivewidems2mass3Ab;
                            newTableDataLine.ms2_4 = NaN ? (qqqTRList[i].positivewidems2mass4Ab == 0 ? double.NaN : qqqTRList[i].positivewidems2mass4Ab) : qqqTRList[i].positivewidems2mass4Ab;
                            newTableDataLine.ms2_5 = NaN ? (qqqTRList[i].positivewidems2mass5Ab == 0 ? double.NaN : qqqTRList[i].positivewidems2mass5Ab) : qqqTRList[i].positivewidems2mass5Ab;
                            newTableDataLine.ms2_6 = NaN ? (qqqTRList[i].positivewidems2mass6Ab == 0 ? double.NaN : qqqTRList[i].positivewidems2mass6Ab) : qqqTRList[i].positivewidems2mass6Ab;
                        }
                        else if (radBtn_Res3.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = NaN ? (qqqTRList[i].positivewidestms1mass1Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms1mass1Ab) : qqqTRList[i].positivewidestms1mass1Ab;
                            newTableDataLine.ms1_2 = NaN ? (qqqTRList[i].positivewidestms1mass2Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms1mass2Ab) : qqqTRList[i].positivewidestms1mass2Ab;
                            newTableDataLine.ms1_3 = NaN ? (qqqTRList[i].positivewidestms1mass3Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms1mass3Ab) : qqqTRList[i].positivewidestms1mass3Ab;
                            newTableDataLine.ms1_4 = NaN ? (qqqTRList[i].positivewidestms1mass4Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms1mass4Ab) : qqqTRList[i].positivewidestms1mass4Ab;
                            newTableDataLine.ms1_5 = NaN ? (qqqTRList[i].positivewidestms1mass5Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms1mass5Ab) : qqqTRList[i].positivewidestms1mass5Ab;
                            newTableDataLine.ms1_6 = NaN ? (qqqTRList[i].positivewidestms1mass6Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms1mass6Ab) : qqqTRList[i].positivewidestms1mass6Ab;
                            newTableDataLine.ms2_1 = NaN ? (qqqTRList[i].positivewidestms2mass1Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms2mass1Ab) : qqqTRList[i].positivewidestms2mass1Ab;
                            newTableDataLine.ms2_2 = NaN ? (qqqTRList[i].positivewidestms2mass2Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms2mass2Ab) : qqqTRList[i].positivewidestms2mass2Ab;
                            newTableDataLine.ms2_3 = NaN ? (qqqTRList[i].positivewidestms2mass3Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms2mass3Ab) : qqqTRList[i].positivewidestms2mass3Ab;
                            newTableDataLine.ms2_4 = NaN ? (qqqTRList[i].positivewidestms2mass4Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms2mass4Ab) : qqqTRList[i].positivewidestms2mass4Ab;
                            newTableDataLine.ms2_5 = NaN ? (qqqTRList[i].positivewidestms2mass5Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms2mass5Ab) : qqqTRList[i].positivewidestms2mass5Ab;
                            newTableDataLine.ms2_6 = NaN ? (qqqTRList[i].positivewidestms2mass6Ab == 0 ? double.NaN : qqqTRList[i].positivewidestms2mass6Ab) : qqqTRList[i].positivewidestms2mass6Ab;
                        }
                    } else
                    {
                        if (radBtn_Res1.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = NaN ? (qqqTRList[i].negativestandardms1mass1Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms1mass1Ab) : qqqTRList[i].negativestandardms1mass1Ab;
                            newTableDataLine.ms1_2 = NaN ? (qqqTRList[i].negativestandardms1mass2Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms1mass2Ab) : qqqTRList[i].negativestandardms1mass2Ab;
                            newTableDataLine.ms1_3 = NaN ? (qqqTRList[i].negativestandardms1mass3Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms1mass3Ab) : qqqTRList[i].negativestandardms1mass3Ab;
                            newTableDataLine.ms1_4 = NaN ? (qqqTRList[i].negativestandardms1mass4Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms1mass4Ab) : qqqTRList[i].negativestandardms1mass4Ab;
                            newTableDataLine.ms1_5 = NaN ? (qqqTRList[i].negativestandardms1mass5Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms1mass5Ab) : qqqTRList[i].negativestandardms1mass5Ab;
                            newTableDataLine.ms1_6 = NaN ? (qqqTRList[i].negativestandardms1mass6Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms1mass6Ab) : qqqTRList[i].negativestandardms1mass6Ab;
                            newTableDataLine.ms2_1 = NaN ? (qqqTRList[i].negativestandardms2mass1Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms2mass1Ab) : qqqTRList[i].negativestandardms2mass1Ab;
                            newTableDataLine.ms2_2 = NaN ? (qqqTRList[i].negativestandardms2mass2Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms2mass2Ab) : qqqTRList[i].negativestandardms2mass2Ab;
                            newTableDataLine.ms2_3 = NaN ? (qqqTRList[i].negativestandardms2mass3Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms2mass3Ab) : qqqTRList[i].negativestandardms2mass3Ab;
                            newTableDataLine.ms2_4 = NaN ? (qqqTRList[i].negativestandardms2mass4Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms2mass4Ab) : qqqTRList[i].negativestandardms2mass4Ab;
                            newTableDataLine.ms2_5 = NaN ? (qqqTRList[i].negativestandardms2mass5Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms2mass5Ab) : qqqTRList[i].negativestandardms2mass5Ab;
                            newTableDataLine.ms2_6 = NaN ? (qqqTRList[i].negativestandardms2mass6Ab == 0 ? double.NaN : qqqTRList[i].negativestandardms2mass6Ab) : qqqTRList[i].negativestandardms2mass6Ab;
                        }
                        else if (radBtn_Res2.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = NaN ? (qqqTRList[i].negativewidems1mass1Ab == 0 ? double.NaN : qqqTRList[i].negativewidems1mass1Ab) : qqqTRList[i].negativewidems1mass1Ab;
                            newTableDataLine.ms1_2 = NaN ? (qqqTRList[i].negativewidems1mass2Ab == 0 ? double.NaN : qqqTRList[i].negativewidems1mass2Ab) : qqqTRList[i].negativewidems1mass2Ab;
                            newTableDataLine.ms1_3 = NaN ? (qqqTRList[i].negativewidems1mass3Ab == 0 ? double.NaN : qqqTRList[i].negativewidems1mass3Ab) : qqqTRList[i].negativewidems1mass3Ab;
                            newTableDataLine.ms1_4 = NaN ? (qqqTRList[i].negativewidems1mass4Ab == 0 ? double.NaN : qqqTRList[i].negativewidems1mass4Ab) : qqqTRList[i].negativewidems1mass4Ab;
                            newTableDataLine.ms1_5 = NaN ? (qqqTRList[i].negativewidems1mass5Ab == 0 ? double.NaN : qqqTRList[i].negativewidems1mass5Ab) : qqqTRList[i].negativewidems1mass5Ab;
                            newTableDataLine.ms1_6 = NaN ? (qqqTRList[i].negativewidems1mass6Ab == 0 ? double.NaN : qqqTRList[i].negativewidems1mass6Ab) : qqqTRList[i].negativewidems1mass6Ab;
                            newTableDataLine.ms2_1 = NaN ? (qqqTRList[i].negativewidems2mass1Ab == 0 ? double.NaN : qqqTRList[i].negativewidems2mass1Ab) : qqqTRList[i].negativewidems2mass1Ab;
                            newTableDataLine.ms2_2 = NaN ? (qqqTRList[i].negativewidems2mass2Ab == 0 ? double.NaN : qqqTRList[i].negativewidems2mass2Ab) : qqqTRList[i].negativewidems2mass2Ab;
                            newTableDataLine.ms2_3 = NaN ? (qqqTRList[i].negativewidems2mass3Ab == 0 ? double.NaN : qqqTRList[i].negativewidems2mass3Ab) : qqqTRList[i].negativewidems2mass3Ab;
                            newTableDataLine.ms2_4 = NaN ? (qqqTRList[i].negativewidems2mass4Ab == 0 ? double.NaN : qqqTRList[i].negativewidems2mass4Ab) : qqqTRList[i].negativewidems2mass4Ab;
                            newTableDataLine.ms2_5 = NaN ? (qqqTRList[i].negativewidems2mass5Ab == 0 ? double.NaN : qqqTRList[i].negativewidems2mass5Ab) : qqqTRList[i].negativewidems2mass5Ab;
                            newTableDataLine.ms2_6 = NaN ? (qqqTRList[i].negativewidems2mass6Ab == 0 ? double.NaN : qqqTRList[i].negativewidems2mass6Ab) : qqqTRList[i].negativewidems2mass6Ab;
                        }
                        else if (radBtn_Res3.IsChecked == true)
                        {
                            newTableDataLine.ms1_1 = NaN ? (qqqTRList[i].negativewidestms1mass1Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms1mass1Ab) : qqqTRList[i].negativewidestms1mass1Ab;
                            newTableDataLine.ms1_2 = NaN ? (qqqTRList[i].negativewidestms1mass2Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms1mass2Ab) : qqqTRList[i].negativewidestms1mass2Ab;
                            newTableDataLine.ms1_3 = NaN ? (qqqTRList[i].negativewidestms1mass3Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms1mass3Ab) : qqqTRList[i].negativewidestms1mass3Ab;
                            newTableDataLine.ms1_4 = NaN ? (qqqTRList[i].negativewidestms1mass4Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms1mass4Ab) : qqqTRList[i].negativewidestms1mass4Ab;
                            newTableDataLine.ms1_5 = NaN ? (qqqTRList[i].negativewidestms1mass5Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms1mass5Ab) : qqqTRList[i].negativewidestms1mass5Ab;
                            newTableDataLine.ms1_6 = NaN ? (qqqTRList[i].negativewidestms1mass6Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms1mass6Ab) : qqqTRList[i].negativewidestms1mass6Ab;
                            newTableDataLine.ms2_1 = NaN ? (qqqTRList[i].negativewidestms2mass1Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms2mass1Ab) : qqqTRList[i].negativewidestms2mass1Ab;
                            newTableDataLine.ms2_2 = NaN ? (qqqTRList[i].negativewidestms2mass2Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms2mass2Ab) : qqqTRList[i].negativewidestms2mass2Ab;
                            newTableDataLine.ms2_3 = NaN ? (qqqTRList[i].negativewidestms2mass3Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms2mass3Ab) : qqqTRList[i].negativewidestms2mass3Ab;
                            newTableDataLine.ms2_4 = NaN ? (qqqTRList[i].negativewidestms2mass4Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms2mass4Ab) : qqqTRList[i].negativewidestms2mass4Ab;
                            newTableDataLine.ms2_5 = NaN ? (qqqTRList[i].negativewidestms2mass5Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms2mass5Ab) : qqqTRList[i].negativewidestms2mass5Ab;
                            newTableDataLine.ms2_6 = NaN ? (qqqTRList[i].negativewidestms2mass6Ab == 0 ? double.NaN : qqqTRList[i].negativewidestms2mass6Ab) : qqqTRList[i].negativewidestms2mass6Ab;
                        }
                    }

                    // Add this populated line of data to the list to be returned
                    returnTable.Add(newTableDataLine);
                }

                // Return the filtered dataset
                return returnTable;

            }

            // This should not be run. The method will either breakout or return by this point
            return null;

        }

        // Various properties that are used in setting the chart
        public SeriesCollection SeriesCollection { get; set; }
        public DateTime[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        /// <summary>
        /// This method populates the LiveChart
        /// </summary>
        private void PopulateChart()
        {

            // Check that there is filtered data to chart
            if (filteredChart.Count != 0)
            {
                // Set the x axis labels
                Labels = filteredChart.Select(x => x.tuneDateTime).ToArray();
                this.Formatter = value => new DateTime((long)value * TimeSpan.FromDays(1).Ticks).ToString("yyyy-MM-dd HH:mm:ss");

                // Check if the EMV data should be plotted
                if (chkBox_EMV.IsChecked == true)
                {
                    if (radBtn_Pos.IsChecked == true)
                    {
                        SeriesCollection.Add(
                            new LineSeries
                            {
                                Title = "Positive EMV",
                                Values = new ChartValues<double>(filteredChart.Select(x => x.posemv).ToArray()),
                                Fill = Brushes.Transparent,
                                Stroke = Brushes.LightBlue,
                                ScalesYAt = 1
                            }
                        );
                    } else {
                        SeriesCollection.Add(
                            new LineSeries
                            {
                                Title = "Negative EMV",
                                Values = new ChartValues<double>(filteredChart.Select(x => x.negemv).ToArray()),
                                Fill = Brushes.Transparent,
                                Stroke = Brushes.LightBlue,
                                ScalesYAt = 1
                            }
                        );
                    }
                }

                // Check if the Abundance data should be plotted
                if (chkBox_Abundance.IsChecked == true)
                {
                    if (radBtn_Mass1.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_1",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_1).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.LightSkyBlue
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_1",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_1).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.DarkGray
                        });
                    }
                    if (radBtn_Mass2.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_2",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_2).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.LightSkyBlue
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_2",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_2).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.DarkGray
                        });
                    }
                    if (radBtn_Mass3.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_3",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_3).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.LightSkyBlue
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_3",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_3).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.DarkGray
                        });
                    }
                    if (radBtn_Mass4.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_4",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_4).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.LightSkyBlue
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_4",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_4).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.DarkGray
                        });
                    }
                    if (radBtn_Mass5.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_5",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_5).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.LightSkyBlue
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_5",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_5).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.DarkGray
                        });
                    }
                    if (radBtn_Mass6.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_6",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_6).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.LightSkyBlue
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_6",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_6).ToArray()),
                            Fill = Brushes.Transparent,
                            Stroke = Brushes.DarkGray
                        });
                    }
                }


                DataContext = this;

            }

        }

        /// <summary>
        /// Method to update the Table based on the various filtering buttons 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            UpdateTableandChart();
        }

        /// <summary>
        /// Method to update the Chart based on the various filtering buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphFilter_Click(object sender, RoutedEventArgs e)
        {
            UpdateTableandChart();
        }

        /// <summary>
        /// Method to update the Table and Chart based on the various filtering buttons
        /// </summary>
        private void UpdateTableandChart()
        {
            UpdateTable();
            UpdateChart();
        }

        private void UpdateTable()
        {
            if (qqqTRList != null)
            {
                // Return and tabulate a filtered List of TableData with null values set to Zeroes
                // Zeroes can be String Formatted in the Grid Data
                filteredData = FilterData(false);
                filteredData.Sort();
                DataTable.ItemsSource = filteredData;
                ICollectionView view = CollectionViewSource.GetDefaultView(filteredData);
                view.Refresh();
            }
        }

        private void UpdateChart()
        {
            if (qqqTRList != null)
            {
                // Return and chart a filtered List of TableData with null values set to Double.NaN
                // Double.NaN is handled by LiveChart, whereas it cannot handle Zeroes natively.
                SeriesCollection.Clear();
                filteredChart = FilterData(true);
                filteredChart.Sort();
                PopulateChart();
            }
        }

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        /// <summary>
        /// Method to handle Grid View Column Header clicking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as System.Windows.Data.Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        /// <summary>
        /// Sort method
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="direction"></param>
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(DataTable.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }


        private void Include_Clicked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            TableData item = (TableData)cb.DataContext;
            for (int i = 0; i < qqqTRList.Count; i++)
            {
                if (qqqTRList[i].tuneDateTime == item.tuneDateTime)
                {
                    qqqTRList[i].includeInChart = item.includeInChart;
                    break;
                }
            }

            UpdateChart();

        }

        private void Chart_OnDataHover(object sender, ChartPoint p)
        {
            Console.WriteLine("[EVENT] you hovered over " + p.X + ", " + p.Y);
        }

    }
}
