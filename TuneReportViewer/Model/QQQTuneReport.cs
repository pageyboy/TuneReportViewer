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
    public class QQQTuneReport
    {
        // Class members.
        //
        // Property.
        public string fName;
        public fullReport report;

        public struct fullReport
        {
            public string tuneType;
            public DateTime tuneDateTime;
            public bool failed;
            public polarity positive;
            public polarity negative;
            public polarity blank;
        }

        public struct polarity
        {
            public bool status;
            public resolution standard;
            public resolution wide;
            public resolution widest;
            public resolution blank;
        }

        public struct resolution
        {
            public mzAb ms1mass1;
            public mzAb ms1mass2;
            public mzAb ms1mass3;
            public mzAb ms1mass4;
            public mzAb ms1mass5;
            public mzAb ms1mass6;
            public mzAb ms2mass1;
            public mzAb ms2mass2;
            public mzAb ms2mass3;
            public mzAb ms2mass4;
            public mzAb ms2mass5;
            public mzAb ms2mass6;
            public mzAb blank;
        }

        public struct mzAb
        {
            public float mzExpected;
            public float abundance;
            public bool blank;
        }

        // Method
        public void ReadQQQReport()
        {
            fullReport report = new fullReport();
            report.negative.status = false;
            report.positive.status = false;


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
                                case "TuneReportType": report.tuneType = QQQTuneInfo.ChildNodes[x].InnerText; break;
                                //case "TuneDateTime": report.tuneDateTime = DateTime.Parse(QQQTuneInfo.ChildNodes[x].InnerText); break;
                                case "PositiveResults":
                                    report.positive = readPolarityResults(QQQTuneInfo.ChildNodes[x].FirstChild);
                                    break;
                                case "NegativeResults":
                                    report.negative = readPolarityResults(QQQTuneInfo.ChildNodes[x].FirstChild);
                                    break;
                                default: break;
                            }
                        }
                    }
                }
            }

            this.report = report;

        }

        private polarity readPolarityResults(XmlNode polarityNode)
        {
            polarity results = this.report.blank;
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
        
        private resolution readResolutionResults(XmlNode resolutionNode)
        {
            resolution results = this.report.blank.blank;
            int ms1masses = 0;
            int ms2masses = 0;
            for (int i = 0; i < resolutionNode.ChildNodes.Count; i++)
            {
                Console.WriteLine(resolutionNode.ChildNodes[i].Name);
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

        private mzAb readMassResults(XmlNode massNode)
        {
            mzAb results = this.report.blank.blank.blank;

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

            Console.WriteLine(results.mzExpected);
            Console.WriteLine(results.abundance);
            return results;
        }

        public void printStandardTuneReport()
        {
            Console.WriteLine(this.report.positive.standard.ms1mass1.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms1mass1.abundance);
            Console.WriteLine(this.report.positive.standard.ms1mass2.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms1mass2.abundance);
            Console.WriteLine(this.report.positive.standard.ms1mass3.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms1mass3.abundance);
            Console.WriteLine(this.report.positive.standard.ms1mass4.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms1mass4.abundance);
            Console.WriteLine(this.report.positive.standard.ms1mass5.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms1mass5.abundance);
            Console.WriteLine(this.report.positive.standard.ms1mass6.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms1mass6.abundance);

            Console.WriteLine(this.report.positive.standard.ms2mass1.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms2mass1.abundance);
            Console.WriteLine(this.report.positive.standard.ms2mass2.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms2mass2.abundance);
            Console.WriteLine(this.report.positive.standard.ms2mass3.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms2mass3.abundance);
            Console.WriteLine(this.report.positive.standard.ms2mass4.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms2mass4.abundance);
            Console.WriteLine(this.report.positive.standard.ms2mass5.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms2mass5.abundance);
            Console.WriteLine(this.report.positive.standard.ms2mass6.mzExpected);
            Console.WriteLine(this.report.positive.standard.ms2mass6.abundance);

            Console.WriteLine(this.report.negative.standard.ms1mass1.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms1mass1.abundance);
            Console.WriteLine(this.report.negative.standard.ms1mass2.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms1mass2.abundance);
            Console.WriteLine(this.report.negative.standard.ms1mass3.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms1mass3.abundance);
            Console.WriteLine(this.report.negative.standard.ms1mass4.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms1mass4.abundance);
            Console.WriteLine(this.report.negative.standard.ms1mass5.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms1mass5.abundance);
            Console.WriteLine(this.report.negative.standard.ms1mass6.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms1mass6.abundance);

            Console.WriteLine(this.report.negative.standard.ms2mass1.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms2mass1.abundance);
            Console.WriteLine(this.report.negative.standard.ms2mass2.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms2mass2.abundance);
            Console.WriteLine(this.report.negative.standard.ms2mass3.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms2mass3.abundance);
            Console.WriteLine(this.report.negative.standard.ms2mass4.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms2mass4.abundance);
            Console.WriteLine(this.report.negative.standard.ms2mass5.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms2mass5.abundance);
            Console.WriteLine(this.report.negative.standard.ms2mass6.mzExpected);
            Console.WriteLine(this.report.negative.standard.ms2mass6.abundance);

        }

        // Instance Constructor.
        public QQQTuneReport(string fName)
        {
            this.fName = fName;
        }

    }

}
