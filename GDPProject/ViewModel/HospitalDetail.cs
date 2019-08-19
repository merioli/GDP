using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDPProject.ViewModel
{
    public class HospitalDetail
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string mohandes { get; set; }
        public string ostan { get; set; }
        public string address { get; set; }
        public string sharestan { get; set; }
        public string phone { get; set; }
        public string mahaleazmoon { get; set; }
        public string  active { get; set; }
    }

    public class HospitalRoot
    {
        public List<HospitalDetail> hospitalDetail { get; set; }
    }
}