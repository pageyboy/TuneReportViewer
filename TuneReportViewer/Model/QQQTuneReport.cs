using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

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

            XElement yourNode = XElement.Load(fName);
            XElement test = yourNode.XPathSelectElement("//QQQTuneInfo[TuneReportType='Autotune'");

        }

        // Instance Constructor.
        public QQQTuneReport(string fName)
        {
            this.fName = fName;
        }

    }

}
