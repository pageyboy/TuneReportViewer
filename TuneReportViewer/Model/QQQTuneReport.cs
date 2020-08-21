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

        public int posemv { get; set; }
        public int negemv { get; set; }

        public float positivemass1 { get; set; }
        public float positivemass2 { get; set; }
        public float positivemass3 { get; set; }
        public float positivemass4 { get; set; }
        public float positivemass5 { get; set; }
        public float positivemass6 { get; set; }

        public float positivestandardms1mass1Ab { get; set; }
        public float positivestandardms1mass2Ab { get; set; }
        public float positivestandardms1mass3Ab { get; set; }
        public float positivestandardms1mass4Ab { get; set; }
        public float positivestandardms1mass5Ab { get; set; }
        public float positivestandardms1mass6Ab { get; set; }

        public float positivestandardms2mass1Ab { get; set; }
        public float positivestandardms2mass2Ab { get; set; }
        public float positivestandardms2mass3Ab { get; set; }
        public float positivestandardms2mass4Ab { get; set; }
        public float positivestandardms2mass5Ab { get; set; }
        public float positivestandardms2mass6Ab { get; set; }

        public float positivewidems1mass1Ab { get; set; }
        public float positivewidems1mass2Ab { get; set; }
        public float positivewidems1mass3Ab { get; set; }
        public float positivewidems1mass4Ab { get; set; }
        public float positivewidems1mass5Ab { get; set; }
        public float positivewidems1mass6Ab { get; set; }

        public float positivewidems2mass1Ab { get; set; }
        public float positivewidems2mass2Ab { get; set; }
        public float positivewidems2mass3Ab { get; set; }
        public float positivewidems2mass4Ab { get; set; }
        public float positivewidems2mass5Ab { get; set; }
        public float positivewidems2mass6Ab { get; set; }

        public float positivewidestms1mass1Ab { get; set; }
        public float positivewidestms1mass2Ab { get; set; }
        public float positivewidestms1mass3Ab { get; set; }
        public float positivewidestms1mass4Ab { get; set; }
        public float positivewidestms1mass5Ab { get; set; }
        public float positivewidestms1mass6Ab { get; set; }

        public float positivewidestms2mass1Ab { get; set; }
        public float positivewidestms2mass2Ab { get; set; }
        public float positivewidestms2mass3Ab { get; set; }
        public float positivewidestms2mass4Ab { get; set; }
        public float positivewidestms2mass5Ab { get; set; }
        public float positivewidestms2mass6Ab { get; set; }

        public float negativemass1 { get; set; }
        public float negativemass2 { get; set; }
        public float negativemass3 { get; set; }
        public float negativemass4 { get; set; }
        public float negativemass5 { get; set; }
        public float negativemass6 { get; set; }

        public float negativestandardms1mass1Ab { get; set; }
        public float negativestandardms1mass2Ab { get; set; }
        public float negativestandardms1mass3Ab { get; set; }
        public float negativestandardms1mass4Ab { get; set; }
        public float negativestandardms1mass5Ab { get; set; }
        public float negativestandardms1mass6Ab { get; set; }

        public float negativestandardms2mass1Ab { get; set; }
        public float negativestandardms2mass2Ab { get; set; }
        public float negativestandardms2mass3Ab { get; set; }
        public float negativestandardms2mass4Ab { get; set; }
        public float negativestandardms2mass5Ab { get; set; }
        public float negativestandardms2mass6Ab { get; set; }

        public float negativewidems1mass1Ab { get; set; }
        public float negativewidems1mass2Ab { get; set; }
        public float negativewidems1mass3Ab { get; set; }
        public float negativewidems1mass4Ab { get; set; }
        public float negativewidems1mass5Ab { get; set; }
        public float negativewidems1mass6Ab { get; set; }

        public float negativewidems2mass1Ab { get; set; }
        public float negativewidems2mass2Ab { get; set; }
        public float negativewidems2mass3Ab { get; set; }
        public float negativewidems2mass4Ab { get; set; }
        public float negativewidems2mass5Ab { get; set; }
        public float negativewidems2mass6Ab { get; set; }

        public float negativewidestms1mass1Ab { get; set; }
        public float negativewidestms1mass2Ab { get; set; }
        public float negativewidestms1mass3Ab { get; set; }
        public float negativewidestms1mass4Ab { get; set; }
        public float negativewidestms1mass5Ab { get; set; }
        public float negativewidestms1mass6Ab { get; set; }

        public float negativewidestms2mass1Ab { get; set; }
        public float negativewidestms2mass2Ab { get; set; }
        public float negativewidestms2mass3Ab { get; set; }
        public float negativewidestms2mass4Ab { get; set; }
        public float negativewidestms2mass5Ab { get; set; }
        public float negativewidestms2mass6Ab { get; set; }

        #region Other QQQReport Subclasses
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
            public int emv { get; set; }
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

        #endregion

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
                    if (root.ChildNodes[i].Name == "QQQTuneFile")
                    {
                        XmlNodeList nodes = root.ChildNodes[i].SelectNodes("//positiveIonParameters//tuneParameters");
                        foreach (XmlNode node in nodes)
                        {
                            Console.WriteLine(node["id"].InnerText);
                            if (node["id"].InnerText == "EMV")
                            {
                                posemv = int.Parse(node["setting"].InnerText);
                            }
                        }
                        nodes = root.ChildNodes[i].SelectNodes("//negativeIonParameters//tuneParameters");
                        foreach (XmlNode node in nodes)
                        {
                            Console.WriteLine(node["id"].InnerText);
                            if (node["id"].InnerText == "EMV")
                            {
                                negemv = int.Parse(node["setting"].InnerText);
                            }
                        }
                    }
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
                                    this.positivemass1 = report.positive.standard.ms1mass1.mzExpected;
                                    this.positivemass2 = report.positive.standard.ms1mass2.mzExpected;
                                    this.positivemass3 = report.positive.standard.ms1mass3.mzExpected;
                                    this.positivemass4 = report.positive.standard.ms1mass4.mzExpected;
                                    this.positivemass5 = report.positive.standard.ms1mass5.mzExpected;
                                    this.positivemass6 = report.positive.standard.ms1mass6.mzExpected;

                                    this.positivestandardms1mass1Ab = report.positive.standard.ms1mass1.abundance;
                                    this.positivestandardms1mass2Ab = report.positive.standard.ms1mass2.abundance;
                                    this.positivestandardms1mass3Ab = report.positive.standard.ms1mass3.abundance;
                                    this.positivestandardms1mass4Ab = report.positive.standard.ms1mass4.abundance;
                                    this.positivestandardms1mass5Ab = report.positive.standard.ms1mass5.abundance;
                                    this.positivestandardms1mass6Ab = report.positive.standard.ms1mass6.abundance;

                                    this.positivestandardms2mass1Ab = report.positive.standard.ms2mass1.abundance;
                                    this.positivestandardms2mass2Ab = report.positive.standard.ms2mass2.abundance;
                                    this.positivestandardms2mass3Ab = report.positive.standard.ms2mass3.abundance;
                                    this.positivestandardms2mass4Ab = report.positive.standard.ms2mass4.abundance;
                                    this.positivestandardms2mass5Ab = report.positive.standard.ms2mass5.abundance;
                                    this.positivestandardms2mass6Ab = report.positive.standard.ms2mass6.abundance;

                                    this.positivewidems1mass1Ab = report.positive.wide.ms1mass1.abundance;
                                    this.positivewidems1mass2Ab = report.positive.wide.ms1mass2.abundance;
                                    this.positivewidems1mass3Ab = report.positive.wide.ms1mass3.abundance;
                                    this.positivewidems1mass4Ab = report.positive.wide.ms1mass4.abundance;
                                    this.positivewidems1mass5Ab = report.positive.wide.ms1mass5.abundance;
                                    this.positivewidems1mass6Ab = report.positive.wide.ms1mass6.abundance;

                                    this.positivewidems2mass1Ab = report.positive.wide.ms2mass1.abundance;
                                    this.positivewidems2mass2Ab = report.positive.wide.ms2mass2.abundance;
                                    this.positivewidems2mass3Ab = report.positive.wide.ms2mass3.abundance;
                                    this.positivewidems2mass4Ab = report.positive.wide.ms2mass4.abundance;
                                    this.positivewidems2mass5Ab = report.positive.wide.ms2mass5.abundance;
                                    this.positivewidems2mass6Ab = report.positive.wide.ms2mass6.abundance;

                                    this.positivewidestms1mass1Ab = report.positive.widest.ms1mass1.abundance;
                                    this.positivewidestms1mass2Ab = report.positive.widest.ms1mass2.abundance;
                                    this.positivewidestms1mass3Ab = report.positive.widest.ms1mass3.abundance;
                                    this.positivewidestms1mass4Ab = report.positive.widest.ms1mass4.abundance;
                                    this.positivewidestms1mass5Ab = report.positive.widest.ms1mass5.abundance;
                                    this.positivewidestms1mass6Ab = report.positive.widest.ms1mass6.abundance;

                                    this.positivewidestms2mass1Ab = report.positive.widest.ms2mass1.abundance;
                                    this.positivewidestms2mass2Ab = report.positive.widest.ms2mass2.abundance;
                                    this.positivewidestms2mass3Ab = report.positive.widest.ms2mass3.abundance;
                                    this.positivewidestms2mass4Ab = report.positive.widest.ms2mass4.abundance;
                                    this.positivewidestms2mass5Ab = report.positive.widest.ms2mass5.abundance;
                                    this.positivewidestms2mass6Ab = report.positive.widest.ms2mass6.abundance;

                                    break;
                                case "NegativeResults":
                                    report.negative = readPolarityResults(QQQTuneInfo.ChildNodes[x].FirstChild);
                                    report.negative.polarityPerformed = true;

                                    this.negativemass1 = report.negative.standard.ms1mass1.mzExpected;
                                    this.negativemass2 = report.negative.standard.ms1mass2.mzExpected;
                                    this.negativemass3 = report.negative.standard.ms1mass3.mzExpected;
                                    this.negativemass4 = report.negative.standard.ms1mass4.mzExpected;
                                    this.negativemass5 = report.negative.standard.ms1mass5.mzExpected;
                                    this.negativemass6 = report.negative.standard.ms1mass6.mzExpected;

                                    this.negativestandardms1mass1Ab = report.negative.standard.ms1mass1.abundance;
                                    this.negativestandardms1mass2Ab = report.negative.standard.ms1mass2.abundance;
                                    this.negativestandardms1mass3Ab = report.negative.standard.ms1mass3.abundance;
                                    this.negativestandardms1mass4Ab = report.negative.standard.ms1mass4.abundance;
                                    this.negativestandardms1mass5Ab = report.negative.standard.ms1mass5.abundance;
                                    this.negativestandardms1mass6Ab = report.negative.standard.ms1mass6.abundance;

                                    this.negativestandardms2mass1Ab = report.negative.standard.ms2mass1.abundance;
                                    this.negativestandardms2mass2Ab = report.negative.standard.ms2mass2.abundance;
                                    this.negativestandardms2mass3Ab = report.negative.standard.ms2mass3.abundance;
                                    this.negativestandardms2mass4Ab = report.negative.standard.ms2mass4.abundance;
                                    this.negativestandardms2mass5Ab = report.negative.standard.ms2mass5.abundance;
                                    this.negativestandardms2mass6Ab = report.negative.standard.ms2mass6.abundance;

                                    this.negativewidems1mass1Ab = report.negative.wide.ms1mass1.abundance;
                                    this.negativewidems1mass2Ab = report.negative.wide.ms1mass2.abundance;
                                    this.negativewidems1mass3Ab = report.negative.wide.ms1mass3.abundance;
                                    this.negativewidems1mass4Ab = report.negative.wide.ms1mass4.abundance;
                                    this.negativewidems1mass5Ab = report.negative.wide.ms1mass5.abundance;
                                    this.negativewidems1mass6Ab = report.negative.wide.ms1mass6.abundance;

                                    this.negativewidems2mass1Ab = report.negative.wide.ms2mass1.abundance;
                                    this.negativewidems2mass2Ab = report.negative.wide.ms2mass2.abundance;
                                    this.negativewidems2mass3Ab = report.negative.wide.ms2mass3.abundance;
                                    this.negativewidems2mass4Ab = report.negative.wide.ms2mass4.abundance;
                                    this.negativewidems2mass5Ab = report.negative.wide.ms2mass5.abundance;
                                    this.negativewidems2mass6Ab = report.negative.wide.ms2mass6.abundance;

                                    this.negativewidestms1mass1Ab = report.negative.widest.ms1mass1.abundance;
                                    this.negativewidestms1mass2Ab = report.negative.widest.ms1mass2.abundance;
                                    this.negativewidestms1mass3Ab = report.negative.widest.ms1mass3.abundance;
                                    this.negativewidestms1mass4Ab = report.negative.widest.ms1mass4.abundance;
                                    this.negativewidestms1mass5Ab = report.negative.widest.ms1mass5.abundance;
                                    this.negativewidestms1mass6Ab = report.negative.widest.ms1mass6.abundance;

                                    this.negativewidestms2mass1Ab = report.negative.widest.ms2mass1.abundance;
                                    this.negativewidestms2mass2Ab = report.negative.widest.ms2mass2.abundance;
                                    this.negativewidestms2mass3Ab = report.negative.widest.ms2mass3.abundance;
                                    this.negativewidestms2mass4Ab = report.negative.widest.ms2mass4.abundance;
                                    this.negativewidestms2mass5Ab = report.negative.widest.ms2mass5.abundance;
                                    this.negativewidestms2mass6Ab = report.negative.widest.ms2mass6.abundance;

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
