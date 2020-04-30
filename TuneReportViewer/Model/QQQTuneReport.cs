using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuneReportViewer.Model
{
    class QQQTuneReport
    {
        public string fileName;

        struct massAbundance
        {
            public float mass;
            public int abundance;
        }

        struct tuneValuesPerResolution
        {
            public massAbundance mass1;
            public massAbundance mass2;
            public massAbundance mass3;
            public massAbundance mass4;
            public massAbundance mass5;
            public massAbundance mass6;
        }

        struct instrParms
        {
            
        }

        struct fullReport
        {
            public tuneValuesPerResolution standard;
            public tuneValuesPerResolution wide;
            public tuneValuesPerResolution widest;
        }

        public void ReadQQQTuneReport()
        {

        }


    }
}
