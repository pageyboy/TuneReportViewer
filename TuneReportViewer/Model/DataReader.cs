using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuneReportViewer.Model
{
    class DataReader
    {
        public void MainProgram(string selectedDirectory)
        {
            // Create List of tune reports
            List<QQQTuneReport> trList = new List<QQQTuneReport>();
            string[] foldersInDirectory = Directory.GetDirectories(selectedDirectory);
            // Loop through all folders in folder that's specified
            foreach (string subDir in foldersInDirectory)
            {
                Console.WriteLine(subDir);
                string[] subDirComps = subDir.Split('\\');
                // Check that each folder is a tune folder
                if (subDirComps[subDirComps.Length - 1].Contains("tune") == true)
                {
                    QQQTuneReport tReport = new QQQTuneReport(subDir);
                    tReport.ReadQQQReport();
                    tReport.printStandardTuneReport();
                    trList.Add(tReport);
                }
            }           
        }
    }
}
