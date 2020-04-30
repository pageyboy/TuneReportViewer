using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuneReportViewer.Model
{

    public struct fullReport
    {
        public bool polarity;
        public bool tuneType;
    }

    public class QQQTuneReport
    {
        // Class members.
        //
        // Property.
        public string fName;
        public fullReport test;

        // Method
        public void ReadQQQReport()
        {
            fullReport aTest = new fullReport();
            aTest.polarity = true;
            this.test = aTest;
        }

        // Instance Constructor.
        public QQQTuneReport(string fName)
        {
            this.fName = fName;
        }

    }

}
