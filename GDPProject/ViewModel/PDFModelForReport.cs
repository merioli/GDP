using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDPProject.ViewModel
{
    public class PDFmodelForReport
    {
        public string active { get; set; }
        public string ID { get; set; }
        public string hospitalName { get; set; }
        public string timestamp { get; set; }
        public string catID { get; set; }
        public object mark { get; set; }
        public string model { get; set; }
        public string serial { get; set; }
        public string position { get; set; }
        public object status { get; set; }
        public string amval { get; set; }
        public string PDFTitle { get; set; }
    }

    public class PDFModelForReportList
    {
        public List<PDFmodelForReport> PDFmodelForReport { get; set; }
    }
    
    
    
}