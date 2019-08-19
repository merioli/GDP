using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDPProject.ViewModel
{
    public class PDFModel
    {
        public PDFModel() {
            ID = 0;
            PDFTitle = "";
            hospitalName = "";
            OperatorID = "";
            DuraionTime = "";
            timestamp = "";
            PDFPath = "";
            catID = "";
            mark = "";
            model = "";
            position = "";
            status = "";
            serial = "";
            amval = "";
            referer = "";

        }
        public int ID { get; set; }
        public string referer{ get; set; }
        public string PDFTitle { get; set; }
        public string hospitalName { get; set; }
        public string  OperatorID { get; set; }
        public string DuraionTime { get; set; }
        public string timestamp { get; set; }
        public string PDFPath { get; set; }
        public string catID { get; set; }
        public string mark { get; set; }
        public string model { get; set; }
        public string position { get; set; }
        public string status { get; set; }
        public string serial { get; set; }
        public string amval { get; set; }
    }
}