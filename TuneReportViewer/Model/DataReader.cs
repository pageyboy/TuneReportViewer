using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuneReportViewer.Model
{
    class DataReader
    {
        public void MainProgram()
        {

            // Create an object of type CustomClass.
            QQQTuneReport tReport = new QQQTuneReport("C:\\Users\\chripage\\OneDrive - Agilent Technologies\\Side Projects\\Visual Studio\\tune_report_viewer\\Old Versions\\Tune Report Viewer\\3_Example Data\\G6410B\\Autotune_20160305_112548\\QQQTuneReport.xml");

            tReport.ReadQQQReport();
            tReport.printStandardTuneReport();
        }
    }
}
