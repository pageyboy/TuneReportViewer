using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Globalization;

namespace TuneReportViewer.Model
{
    public class QQQTuneReport
    {
        // Class members.
        //
        // Property.
        public string folderPath { get; set; }
        public fullReport report { get; set; }
        private string tuneReportFilePath { get; set; }
        public bool passStatus { get; set; }
        public DateTime tuneDateTime { get; set; }
        public string tuneType { get; set; }

        public class fullReport
        {
            public polarity positive { get; set; }
            public polarity negative { get; set; }
        }

        public class polarity
        {
            public bool polarityPerformed { get; set; }
            public resolution standard { get; set; }
            public resolution wide { get; set; }
            public resolution widest { get; set; }
        }

        public class resolution
        {
            public mzAb ms1mass1 { get; set; }
            public mzAb ms1mass2 { get; set; }
            public mzAb ms1mass3 { get; set; }
            public mzAb ms1mass4 { get; set; }
            public mzAb ms1mass5 { get; set; }
            public mzAb ms1mass6 { get; set; }
            public mzAb ms2mass1 { get; set; }
            public mzAb ms2mass2 { get; set; }
            public mzAb ms2mass3 { get; set; }
            public mzAb ms2mass4 { get; set; }
            public mzAb ms2mass5 { get; set; }
            public mzAb ms2mass6 { get; set; }
        }

        public class mzAb
        {
            public float mzExpected { get; set; }
            public float abundance { get; set; }
        }

        // Method

        private bool CheckStatus()
        {
            // All passed Check/Autotunes will have a QQQTuneReport.xml within the folder. If there is no file it has failed for one reason or another
            // Probably a good place to determine whether the file is a Check/Autotune and the date/time it was created.
            // Prefer to read from QQQTuneReport.xml file but if this is missing then this is the only way.

            passStatus = File.Exists(tuneReportFilePath);
            string[] dirs = this.folderPath.Split('\\');
            string trDirectory = dirs[dirs.Length - 1];
            string[] trComps = trDirectory.Split('_');
            if (trComps.Length != 3)
            {
                return false;
            }
            this.tuneType = trComps[0];
            string dateTimeString = trComps[1] + trComps[2];
            try
            {
                DateTime temp;
                DateTime.TryParseExact(dateTimeString, new[] { "yyyyMMddHHmmss" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out temp);
                this.tuneDateTime = temp;
            }
            catch (Exception)
            {
                return false;
            }
            return passStatus;

        }

        public void ReadQQQReport()
        {

            if (!CheckStatus())
            {
                return;
            }

            // Initialize report and set polarityPerformed flags to false by default. If the nodes aren't observed then they weren't run and so are false.

            fullReport report = new fullReport();
            polarity positive = new polarity();
            polarity negative = new polarity();
            negative.polarityPerformed = false;
            positive.polarityPerformed = false;

            // Create new instance of the document and navigate to the Positive and Negative node.
            // Possibly better way to do this without the loops. So far unable to make other solutions work appropriately.

            XmlDocument doc = new XmlDocument();
            doc.Load(tuneReportFilePath);
            // Console.WriteLine(doc.DocumentElement.OuterXml);
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
                            // Console.WriteLine(QQQTuneInfo.ChildNodes[x].Name);
                            switch (QQQTuneInfo.ChildNodes[x].Name)
                            {
                                // Get the results for each polarity. If the polarity has been performed then set the flag to try after it's been
                                // successfully parsed.
                                case "PositiveResults":
                                    report.positive = readPolarityResults(QQQTuneInfo.ChildNodes[x].FirstChild);
                                    report.positive.polarityPerformed = true;
                                    break;
                                case "NegativeResults":
                                    report.negative = readPolarityResults(QQQTuneInfo.ChildNodes[x].FirstChild);
                                    report.negative.polarityPerformed = true;
                                    break;
                                default: break;
                            }
                        }
                    }
                }
            }

            this.report = report;

        }

        // Method to find nodes associated with each polarity

        private polarity readPolarityResults(XmlNode polarityNode)
        {
            polarity results = new polarity();
            for (int i = 0; i < polarityNode.ChildNodes.Count; i++)
            {
                switch (polarityNode.ChildNodes[i].Name)
                {
                    case "Standard":
                        results.standard = readResolutionResults(polarityNode.ChildNodes[i]);
                        break;
                    case "Wide":
                        results.wide = readResolutionResults(polarityNode.ChildNodes[i]);
                        break;
                    case "Widest":
                        results.widest = readResolutionResults(polarityNode.ChildNodes[i]);
                        break;
                    default: break;
                }
            }

            return results;
        }
        
        // Method to find nodes associated with each Mass investigated

        private resolution readResolutionResults(XmlNode resolutionNode)
        {
            resolution results = new resolution();
            int ms1masses = 0;
            int ms2masses = 0;
            for (int i = 0; i < resolutionNode.ChildNodes.Count; i++)
            {
                // Console.WriteLine(resolutionNode.ChildNodes[i].Name);
                switch (resolutionNode.ChildNodes[i].Name)
                {
                    case "MS1ScanMassList":
                        switch (ms1masses)
                        {
                            case 0: results.ms1mass1 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 1: results.ms1mass2 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 2: results.ms1mass3 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 3: results.ms1mass4 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 4: results.ms1mass5 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 5: results.ms1mass6 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            default: break;
                        }
                        ms1masses++;
                        break;
                    case "MS2ScanMassList":
                        switch (ms2masses)
                        {
                            case 0: results.ms2mass1 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 1: results.ms2mass2 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 2: results.ms2mass3 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 3: results.ms2mass4 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 4: results.ms2mass5 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            case 5: results.ms2mass6 = readMassResults(resolutionNode.ChildNodes[i]); break;
                            default: break;
                        }
                        ms2masses++;
                        break;
                    default: break;
                }
            }

            return results;
        }

        // Method to read mass/abundance pairs from individual mass node

        private mzAb readMassResults(XmlNode massNode)
        {
            mzAb results = new mzAb();

            for (int i = 0; i < massNode.ChildNodes.Count; i++)
            {
                switch (massNode.ChildNodes[i].Name)
                {
                    case "MZExpected":
                        results.mzExpected = float.Parse(massNode.ChildNodes[i].InnerText);
                        break;
                    case "AbundanceObserved":
                        results.abundance = float.Parse(massNode.ChildNodes[i].InnerText);
                        break;
                    default:
                        break;
                }
            }

            // Console.WriteLine(results.mzExpected);
            // Console.WriteLine(results.abundance);
            return results;
        }

        // Method to print all results to the console.

        public void printStandardTuneReport()
        {

            // Console.WriteLine(this.tuneType);
            // Console.WriteLine(this.tuneDateTime.ToString("dd MMMM yyyy hh:mm:ss tt"));
            // Console.WriteLine(this.tuneReportFilePath);
            if (this.passStatus)
            {
                if (this.report.positive.polarityPerformed)
                {
                    Console.Write(this.report.positive.standard.ms1mass1.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms1mass1.abundance);
                    Console.Write(this.report.positive.standard.ms1mass2.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms1mass2.abundance);
                    Console.Write(this.report.positive.standard.ms1mass3.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms1mass3.abundance);
                    Console.Write(this.report.positive.standard.ms1mass4.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms1mass4.abundance);
                    Console.Write(this.report.positive.standard.ms1mass5.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms1mass5.abundance);
                    Console.Write(this.report.positive.standard.ms1mass6.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms1mass6.abundance);

                    Console.Write(this.report.positive.standard.ms2mass1.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms2mass1.abundance);
                    Console.Write(this.report.positive.standard.ms2mass2.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms2mass2.abundance);
                    Console.Write(this.report.positive.standard.ms2mass3.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms2mass3.abundance);
                    Console.Write(this.report.positive.standard.ms2mass4.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms2mass4.abundance);
                    Console.Write(this.report.positive.standard.ms2mass5.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms2mass5.abundance);
                    Console.Write(this.report.positive.standard.ms2mass6.mzExpected + ": ");
                    Console.WriteLine(this.report.positive.standard.ms2mass6.abundance);
                }

                if (this.report.negative.polarityPerformed)
                {
                    Console.Write(this.report.negative.standard.ms1mass1.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms1mass1.abundance);
                    Console.Write(this.report.negative.standard.ms1mass2.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms1mass2.abundance);
                    Console.Write(this.report.negative.standard.ms1mass3.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms1mass3.abundance);
                    Console.Write(this.report.negative.standard.ms1mass4.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms1mass4.abundance);
                    Console.Write(this.report.negative.standard.ms1mass5.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms1mass5.abundance);
                    Console.Write(this.report.negative.standard.ms1mass6.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms1mass6.abundance);

                    Console.Write(this.report.negative.standard.ms2mass1.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms2mass1.abundance);
                    Console.Write(this.report.negative.standard.ms2mass2.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms2mass2.abundance);
                    Console.Write(this.report.negative.standard.ms2mass3.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms2mass3.abundance);
                    Console.Write(this.report.negative.standard.ms2mass4.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms2mass4.abundance);
                    Console.Write(this.report.negative.standard.ms2mass5.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms2mass5.abundance);
                    Console.Write(this.report.negative.standard.ms2mass6.mzExpected + ": ");
                    Console.WriteLine(this.report.negative.standard.ms2mass6.abundance);
                }
            }

            

        }

        // Instance Constructor.
        public QQQTuneReport(string folderPath)
        {
            this.folderPath = folderPath;
            this.tuneReportFilePath = folderPath + "\\QQQTuneReport.xml";
            // Console.WriteLine(this.tuneReportFilePath);
        }

    }

}
