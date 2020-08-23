using Ookii.Dialogs.Wpf;
using System.Windows;
using TuneReportViewer.Model;
using System.Collections.Generic;
using TuneReportViewer.View;
using System;
using System.Windows.Documents;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

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
            SeriesCollection = new SeriesCollection();
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
        List<TableData> filteredData = new List<TableData>();
        List<TableData> filteredChart = new List<TableData>();

        private void btn_SearchClicked(object sender, RoutedEventArgs e)
        {
            DataReader dataReader = new DataReader();
            qqqTRList = dataReader.MainProgram(txtBox_FolderPath.Text);
            UpdateTableandChart();
        }

        private void btn_ExportClicked(object sender, RoutedEventArgs e)
        {
        }

        List<TableData> FilterData(bool NaN)
        {
            List<TableData> returnTable = new List<TableData>();
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
                    newTableDataLine.posemv = NaN ? (qqqTRList[i].posemv == 0 ? double.NaN : qqqTRList[i].posemv) : qqqTRList[i].posemv;
                    newTableDataLine.negemv = NaN ? (qqqTRList[i].negemv == 0 ? double.NaN : qqqTRList[i].negemv) : qqqTRList[i].negemv;

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

                    returnTable.Add(newTableDataLine);
                }

                return returnTable;

            }

            return null;

        }

        public SeriesCollection SeriesCollection { get; set; }
        public DateTime[] Labels { get; set; }
        public Func<DateTime, string> Formatter { get; set; }

        private void PopulateChart()
        {

            if (filteredData.Count != 0)
            {
                Labels = filteredChart.Select(x => x.tuneDateTime).ToArray();
                Formatter = value => value.ToString("s");

                if (chkBox_EMV.IsChecked == true)
                {
                    if (radBtn_Pos.IsChecked == true)
                    {
                        SeriesCollection.Add(
                            new LineSeries
                            {
                                Title = "Positive EMV",
                                Values = new ChartValues<double>(filteredChart.Select(x => x.posemv).ToArray())
                            }
                        );
                    } else {
                        SeriesCollection.Add(
                            new LineSeries
                            {
                                Title = "Negative EMV",
                                Values = new ChartValues<double>(filteredChart.Select(x => x.negemv).ToArray())
                            }
                        );
                    }
                }

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
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_2).ToArray())
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_2",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_2).ToArray()),

                        });
                    }
                    if (radBtn_Mass3.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_3",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_3).ToArray())
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_3",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_3).ToArray())
                        });
                    }
                    if (radBtn_Mass4.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_4",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_4).ToArray())
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_4",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_4).ToArray())
                        });
                    }
                    if (radBtn_Mass5.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_5",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_5).ToArray())
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_5",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_5).ToArray())
                        });
                    }
                    if (radBtn_Mass6.IsChecked == true)
                    {
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS1_6",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms1_6).ToArray())
                        });
                        SeriesCollection.Add(
                        new LineSeries
                        {
                            Title = "MS2_6",
                            Values = new ChartValues<double>(filteredChart.Select(x => x.ms2_6).ToArray())
                        });
                    }
                }


                DataContext = this;

            }

        }

        class TableData
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
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            UpdateTableandChart();
        }

        private void GraphFilter_Click(object sender, RoutedEventArgs e)
        {
            SeriesCollection.Clear();
            filteredChart = FilterData(true);
            PopulateChart();
        }

        private void UpdateTableandChart()
        {
            filteredData = FilterData(false);
            DataTable.ItemsSource = filteredData;
            ICollectionView view = CollectionViewSource.GetDefaultView(filteredData);
            view.Refresh();

            SeriesCollection.Clear();
            filteredChart = FilterData(true);
            PopulateChart();

        }
    }
}
