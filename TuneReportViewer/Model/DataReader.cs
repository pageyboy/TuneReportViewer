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
            QQQTuneReport tReport = new QQQTuneReport("testFileName");

            tReport.ReadQQQReport();
            Console.WriteLine(tReport.test);
            Console.WriteLine(tReport.fName);

        }
    }
}
