using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDPProject.ViewModel
{
    public class PDFReportVM
    {
        public string activeHospital { get; set; }
        public List<PDFmodelForReport> list { get; set; }
        public IEnumerable<SelectListItem> DeviceItems { get; set; }
        public IEnumerable<SelectListItem> PositionItems { get; set; }
        public IEnumerable<SelectListItem> HospitalItems { get; set; }
        
    }
}