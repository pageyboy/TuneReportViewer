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
        public string tuneType;
        public DateTime tuneDateTime;
        public polarity positive;
        public polarity negative;
    }

    public struct polarity
    {
        public resolution standard;
        public resolution wide;
        public resolution widest;
    }

    public struct resolution
    {
        public mzAb mass1;
        public mzAb mass2;
        public mzAb mass3;
        public mzAb mass4;
        public mzAb mass5;
        public mzAb mass6;
    }
    public struct mzAb
    {
        public float mzExpected;
        public int abundance;
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

            XmlDocument doc = new XmlDocument();
            doc.Load(fName);
            Console.WriteLine(doc.DocumentElement.OuterXml);
            XmlNode root = doc.LastChild;
            if(root.HasChildNodes)
            {
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    if ((root.ChildNodes[i].Name == "QQQTuneInfo") && root.ChildNodes[i].HasChildNodes == true)
                    {
                        XmlNode QQQTuneInfo = root.ChildNodes[i];
                        for (int x = 0; x < QQQTuneInfo.ChildNodes.Count; x++)
                        {
                            Console.WriteLine(QQQTuneInfo.ChildNodes[x].Name);
                            switch (QQQTuneInfo.ChildNodes[x].Name)
                            {
                                case "TuneReportType": aTest.tuneType = QQQTuneInfo.ChildNodes[x].InnerText; break;
                                default: break;
                            }
                        }
                    }
                }
            }

            this.test = aTest;

        }

        // Instance Constructor.
        public QQQTuneReport(string fName)
        {
            this.fName = fName;
        }

    }

}
