using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TuneReportViewer.Model;

namespace TuneReportViewer.View
{
    class DeNester
    {
        public class denestedQQQReport
        {

            #region denestedQQQReport Properties

            public bool passStatus;
            public DateTime tuneDateTime;
            public string tuneType;

            public bool postivePerformed;
            
            public float positivemass1;
            public float positivemass2;
            public float positivemass3;
            public float positivemass4;
            public float positivemass5;
            public float positivemass6;

            public float negativemass1;
            public float negativemass2;
            public float negativemass3;
            public float negativemass4;
            public float negativemass5;
            public float negativemass6;

            public float positivestandardms1mass1Ab;
            public float positivestandardms1mass2Ab;
            public float positivestandardms1mass3Ab;
            public float positivestandardms1mass4Ab;
            public float positivestandardms1mass5Ab;
            public float positivestandardms1mass6Ab;
            
            public float positivestandardms2mass1Ab;
            public float positivestandardms2mass2Ab;
            public float positivestandardms2mass3Ab;
            public float positivestandardms2mass4Ab;
            public float positivestandardms2mass5Ab;
            public float positivestandardms2mass6Ab;

            public float negativestandardms1mass1Ab;
            public float negativestandardms1mass2Ab;
            public float negativestandardms1mass3Ab;
            public float negativestandardms1mass4Ab;
            public float negativestandardms1mass5Ab;
            public float negativestandardms1mass6Ab;

            public float negativestandardms2mass1Ab;
            public float negativestandardms2mass2Ab;
            public float negativestandardms2mass3Ab;
            public float negativestandardms2mass4Ab;
            public float negativestandardms2mass5Ab;
            public float negativestandardms2mass6Ab;

            public float positivewidems1mass1Ab;
            public float positivewidems1mass2Ab;
            public float positivewidems1mass3Ab;
            public float positivewidems1mass4Ab;
            public float positivewidems1mass5Ab;
            public float positivewidems1mass6Ab;

            public float positivewidems2mass1Ab;
            public float positivewidems2mass2Ab;
            public float positivewidems2mass3Ab;
            public float positivewidems2mass4Ab;
            public float positivewidems2mass5Ab;
            public float positivewidems2mass6Ab;

            public float negativewidems1mass1Ab;
            public float negativewidems1mass2Ab;
            public float negativewidems1mass3Ab;
            public float negativewidems1mass4Ab;
            public float negativewidems1mass5Ab;
            public float negativewidems1mass6Ab;

            public float negativewidems2mass1Ab;
            public float negativewidems2mass2Ab;
            public float negativewidems2mass3Ab;
            public float negativewidems2mass4Ab;
            public float negativewidems2mass5Ab;
            public float negativewidems2mass6Ab;

            public float positivewidestms1mass1Ab;
            public float positivewidestms1mass2Ab;
            public float positivewidestms1mass3Ab;
            public float positivewidestms1mass4Ab;
            public float positivewidestms1mass5Ab;
            public float positivewidestms1mass6Ab;

            public float positivewidestms2mass1Ab;
            public float positivewidestms2mass2Ab;
            public float positivewidestms2mass3Ab;
            public float positivewidestms2mass4Ab;
            public float positivewidestms2mass5Ab;
            public float positivewidestms2mass6Ab;

            public float negativewidestms1mass1Ab;
            public float negativewidestms1mass2Ab;
            public float negativewidestms1mass3Ab;
            public float negativewidestms1mass4Ab;
            public float negativewidestms1mass5Ab;
            public float negativewidestms1mass6Ab;

            public float negativewidestms2mass1Ab;
            public float negativewidestms2mass2Ab;
            public float negativewidestms2mass3Ab;
            public float negativewidestms2mass4Ab;
            public float negativewidestms2mass5Ab;
            public float negativewidestms2mass6Ab;

            #endregion

        }

        public denestedQQQReport createDenestedTuneReport(QQQTuneReport nestedTuneReport)
        {
            denestedQQQReport denestedQQQTuneReport = new denestedQQQReport();

            denestedQQQTuneReport.passStatus = nestedTuneReport.passStatus;
            denestedQQQTuneReport.tuneDateTime = nestedTuneReport.tuneDateTime;
            denestedQQQTuneReport.tuneType = nestedTuneReport.tuneType;

            if(nestedTuneReport.report.positive.polarityPerformed)
            {
                denestedQQQTuneReport.positivestandardms1mass1Ab = nestedTuneReport.report.positive.standard.ms1mass1.abundance;
                denestedQQQTuneReport.positivestandardms1mass2Ab = nestedTuneReport.report.positive.standard.ms1mass2.abundance;
                denestedQQQTuneReport.positivestandardms1mass3Ab = nestedTuneReport.report.positive.standard.ms1mass3.abundance;
                denestedQQQTuneReport.positivestandardms1mass4Ab = nestedTuneReport.report.positive.standard.ms1mass4.abundance;
                denestedQQQTuneReport.positivestandardms1mass5Ab = nestedTuneReport.report.positive.standard.ms1mass5.abundance;
                denestedQQQTuneReport.positivestandardms1mass6Ab = nestedTuneReport.report.positive.standard.ms1mass6.abundance;

                denestedQQQTuneReport.positivestandardms2mass1Ab = nestedTuneReport.report.positive.standard.ms2mass1.abundance;
                denestedQQQTuneReport.positivestandardms2mass2Ab = nestedTuneReport.report.positive.standard.ms2mass2.abundance;
                denestedQQQTuneReport.positivestandardms2mass3Ab = nestedTuneReport.report.positive.standard.ms2mass3.abundance;
                denestedQQQTuneReport.positivestandardms2mass4Ab = nestedTuneReport.report.positive.standard.ms2mass4.abundance;
                denestedQQQTuneReport.positivestandardms2mass5Ab = nestedTuneReport.report.positive.standard.ms2mass5.abundance;
                denestedQQQTuneReport.positivestandardms2mass6Ab = nestedTuneReport.report.positive.standard.ms2mass6.abundance;
            }

            if (nestedTuneReport.report.negative.polarityPerformed)
            {
                denestedQQQTuneReport.negativestandardms1mass1Ab = nestedTuneReport.report.negative.standard.ms1mass1.abundance;
                denestedQQQTuneReport.negativestandardms1mass2Ab = nestedTuneReport.report.negative.standard.ms1mass2.abundance;
                denestedQQQTuneReport.negativestandardms1mass3Ab = nestedTuneReport.report.negative.standard.ms1mass3.abundance;
                denestedQQQTuneReport.negativestandardms1mass4Ab = nestedTuneReport.report.negative.standard.ms1mass4.abundance;
                denestedQQQTuneReport.negativestandardms1mass5Ab = nestedTuneReport.report.negative.standard.ms1mass5.abundance;
                denestedQQQTuneReport.negativestandardms1mass6Ab = nestedTuneReport.report.negative.standard.ms1mass6.abundance;

                denestedQQQTuneReport.negativestandardms2mass1Ab = nestedTuneReport.report.negative.standard.ms2mass1.abundance;
                denestedQQQTuneReport.negativestandardms2mass2Ab = nestedTuneReport.report.negative.standard.ms2mass2.abundance;
                denestedQQQTuneReport.negativestandardms2mass3Ab = nestedTuneReport.report.negative.standard.ms2mass3.abundance;
                denestedQQQTuneReport.negativestandardms2mass4Ab = nestedTuneReport.report.negative.standard.ms2mass4.abundance;
                denestedQQQTuneReport.negativestandardms2mass5Ab = nestedTuneReport.report.negative.standard.ms2mass5.abundance;
                denestedQQQTuneReport.negativestandardms2mass6Ab = nestedTuneReport.report.negative.standard.ms2mass6.abundance;
            }

            return denestedQQQTuneReport;
        }
    }
}
