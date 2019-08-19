using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDPProject.ViewModel
{
    public class FormModel
    {
        public string device { get; set; }
        public string code { get; set; }
        public string phone { get; set; }
        public string token { get; set; }
        public string formfillingtime { get; set; }
        public string model { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string timestamp { get; set; }
        public string isSync { get; set; }
        public string catID { get; set; }
       
    }
    public class model
    {
        public int id { get; set; }
        public int catID { get; set; }
        public int formID { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public int type { get; set; }
        public string page { get; set; }
        public string font { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public int masterID { get; set; }

    }
}