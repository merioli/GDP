﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.ViewModel
{
    public class CatPageViewModel
    {
        public RootObjectFilter log { get; set; }
        public string layer { get; set; }
        public string selectedfilters { get; set; }
        public string levelfinder { get; set; }
        public productinfoviewdetail productmodel { get; set; }
        public string SelectedCat { get; set; }
        public IEnumerable<SelectListItem> Cats { get; set; }

        public string SelectedlistProduct { get; set; }
        public IEnumerable<SelectListItem> types { get; set; }

        public string SelectedaddProduct { get; set; }
        public IEnumerable<SelectListItem> brands { get; set; }

        public string SelectedColor { get; set; }
        public IEnumerable<SelectListItem> Colores { get; set; }
       


   }
}