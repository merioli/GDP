using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using GDPProject.Models;
//using GDPProject.ViewModel;
//using GDPProject.Classes;
//using PagedList;
using System.Web.Helpers;
using System.Drawing;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using GDPProject.ViewModel;
using Spire.Pdf.General.Find;
using Spire.Pdf;

namespace GDPProject.Controllers
{
    public class AdminController : Controller
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        public ActionResult CustomerLogin(string pass, string ischecked, string phone)
        {

            string json;
            using (var client2 = new WebClient())
            {
                json = client2.DownloadString("http://supectco.com/webs/liabello/Admin/getuserid.php?pass=" + pass + "&mobile=" + phone);
            }
            var log = JsonConvert.DeserializeObject<List<userdata>>(json);
            if (log != null)
            {
                userdata user = log[0];
                if (user.ID != "")
                {
                    Session["LogedInUser2"] = user;
                    if (Request.Cookies["productcookiie"] != null)
                    {
                        HttpCookie currentUserCookie = Request.Cookies["productcookiie"];
                        Response.Cookies.Remove("productcookiie");
                        currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                        currentUserCookie.Value = null;
                        Response.SetCookie(currentUserCookie);
                    }




                    if (ischecked == "checked")
                    {
                        HttpCookie Username = new HttpCookie("Username");
                        HttpCookie Password = new HttpCookie("Password");
                        DateTime now = DateTime.Now;
                        Username.Value = phone;
                        Username.Expires = now.AddMonths(1);
                        Password.Value = pass;
                        Password.Expires = now.AddMonths(1);
                        Response.Cookies.Add(Username);
                        Response.Cookies.Add(Password);
                    }

                }
                return Content("1/Admin/product");
            }
            else
            {
                return Content("2/Admin/product");
            }



        }
        public ActionResult Index()
        {





            BaseViewModel basemodel = new BaseViewModel();
            if (Session["LogedInUser2"] != null)
            {

                userdata user = Session["LogedInUser2"] as userdata;

            }

            else
            {
                HttpCookie Username = new HttpCookie("Username");

                Username = Request.Cookies["Username"];
                HttpCookie Password = new HttpCookie("Password");
                Password = Request.Cookies["Password"];
                if (Username != null)
                {
                    basemodel.username = Username.Value;
                }
                if (Password != null)
                {
                    basemodel.pass = Password.Value;
                }

            }
            return View(basemodel);
        }


        public void updatePostion(string deviceID)
        {
            string json;
            deviceID = "1";
            using (var client = new WebClient())
            {

                json = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/getListOfFeatures.php?CatID=" + deviceID);

            }

            FeatureModel log = JsonConvert.DeserializeObject<FeatureModel>(json);

            var testFile = "";
            testFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF/pump.pdf");
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(testFile);
            PdfTextFind results = null;
            //setting position for every title
            PdfPageBase page;
            foreach (var item in log.featureData.First().subFeatures)
            {
                if(item.title == "keyfidalilyek")
                {

                }
                int PAGE = Convert.ToInt32(item.page) - 1;
                page = doc.Pages[PAGE];
                string xposition = "";
                string yposition = "0";
                if (item.value != "master")
                {
                    results = page.FindText(item.title).Finds.First();
                    float width = results.Size.Width;
                    xposition = (results.Position.X + width).ToString();
                    yposition = (results.Position.Y).ToString();
                }
                string json2;
                using (var client = new WebClient())
                {

                    json2 = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/setPositionForFeatures.php?ID=" + item.ID + "&xpos=" + xposition + "&ypos=" + yposition);

                }

            }
        }
        public ActionResult setDetail()
        {
            string json = "";
            using (var client = new WebClient())
            {

                json = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/getHospitalDetail.php?");

                HospitalRoot log = JsonConvert.DeserializeObject<HospitalRoot>(json);

                return View(log.hospitalDetail);
            }
        }
        public ActionResult setDetailPost(HospitalDetail model)
        {





            string json = "";
            string result = "";
            using (WebClient client = new WebClient())
            {

                var collection = new NameValueCollection();
                collection.Add("name", model.name);
                collection.Add("ostan", model.ostan);
                collection.Add("phone", model.phone);
                collection.Add("sharestan", model.sharestan);
                collection.Add("mohandes", model.mohandes);
                collection.Add("mahaleazmoon", model.mahaleazmoon);
                collection.Add("address", model.address);


                byte[] response =
                client.UploadValues("http://www.supectco.com/webs/GDP/Admin/setHospitalDetail.php?", collection);

                result = System.Text.Encoding.UTF8.GetString(response);
            }

            if (json.Contains("success"))
            {
                return Content("success");
            }
            else
            {
                return Content("error");

            }

        }

        public ActionResult features()
        {

            //if (Session["LogedInUser2"] == null)
            //{

            //    return RedirectToAction("Index", "Admin");

            //}
            //userdata user = Session["LogedInUser2"] as userdata;
            string json = "";
            using (var client = new WebClient())
            {

                json = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/getDevices.php?");

            }
            RootObjectFilter log = JsonConvert.DeserializeObject<RootObjectFilter>(json);
            IEnumerable<SelectListItem> items = log.filtersModel.Select(c => new SelectListItem
            {
                Value = c.ID,
                Text = c.title

            });
            ViewBag.list = items;
            return View(log);



        }
        public ActionResult createFeature(string level1title, string catID, string mainF, string min, string max, string type, string page, string value, string lan)
        {
            string json = "";
            //using (var client = new WebClient())
            //{
            //    json = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/createFeature.php?title=" + level1title + "&catID=" + catID + "&mainF=" + mainF);
            //}
            string result = "";
            using (WebClient client = new WebClient())
            {

                var collection = new NameValueCollection();
                collection.Add("title", level1title);
                collection.Add("catID", catID);
                collection.Add("mainF", mainF);
                collection.Add("min", min);
                collection.Add("max", max);
                collection.Add("type", type);
                collection.Add("page", page);
                collection.Add("lan", lan);
                collection.Add("value", value);

                byte[] response =
                client.UploadValues("http://www.supectco.com/webs/GDP/Admin/createFeature.php?", collection);

                result = System.Text.Encoding.UTF8.GetString(response);
            }

            if (json.Contains("success"))
            {
                return Content("success");
            }

            else
            {
                return Content("error");
            }

        }
        public ActionResult deleteFeature(string subf, string mainf)
        {
            string json = "";
            using (var client = new WebClient())
            {
                json = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/deleteFeature.php?subf=" + subf + "&mainf=" + mainf);
            }

            if (json.Contains("success"))
            {
                return Content("success");
            }
            else if (json.Contains("subexist"))
            {
                return Content("subexist");
            }
            else
            {
                return Content("error");
            }

        }
        public ActionResult getfeature(string catID)
        {
            string json = "";
            using (var client = new WebClient())
            {

                json = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/getListOfFeatures.php?CatID=" + catID);

            }

            FeatureModel log = JsonConvert.DeserializeObject<FeatureModel>(json);

            return PartialView("/Views/Shared/AdminShared/_feature.cshtml", log);
        }
        public ActionResult getfeaturedetail(string detail)
        {
            FeatureModel log = JsonConvert.DeserializeObject<FeatureModel>(detail);
            return PartialView("/Views/Shared/AdminShared/_feature.cshtml", log);
        }
        public ActionResult PDFReport()
        {

            string HospitalList = "";
            using (var client = new WebClient())
            {
                HospitalList = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/getHospitalDetail.php?");
            }
            HospitalRoot log4 = JsonConvert.DeserializeObject<HospitalRoot>(HospitalList);
            HospitalDetail detail = log4.hospitalDetail.Where(x => x.active == "true").FirstOrDefault();
            string selectdid = detail.ID;
            List<SelectListItem> HospitalItems = new List<SelectListItem>();
            if (log4.hospitalDetail != null)
            {
                HospitalItems = log4.hospitalDetail.Select(c => new SelectListItem
                {
                    Value = c.ID,
                    Text = c.name,
                    Selected = false,

                }).ToList();
                HospitalItems.Where(x => x.Value == selectdid).SingleOrDefault().Selected = true;

            }
            string PositionList = "";
            using (var client = new WebClient())
            {
                PositionList = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/getPosition.php?");
            }
            RootObjectFilter log3 = JsonConvert.DeserializeObject<RootObjectFilter>(PositionList);

            List<SelectListItem> PositionItems = new List<SelectListItem>();
            if (log3.filtersModel != null)
            {
                PositionItems = log3.filtersModel.Select(c => new SelectListItem
                {
                    Value = c.title,
                    Text = c.title,


                }).ToList();
            }


            string json = "";
            using (var client = new WebClient())
            {

                json = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/getDevices.php?");

            }
            RootObjectFilter log2 = JsonConvert.DeserializeObject<RootObjectFilter>(json);
            List<SelectListItem> DeviceItems = new List<SelectListItem>();
            if (log2.filtersModel != null)
            {
                DeviceItems = log2.filtersModel.Select(c => new SelectListItem
                {
                    Value = c.ID,
                    Text = c.title

                }).ToList();
            }


            PDFModel model = new PDFModel();
            model.hospitalName = selectdid;
            string srt = getListOfPDF(model);
            PDFModelForReportList log = JsonConvert.DeserializeObject<PDFModelForReportList>(srt);
            PDFReportVM VM = new PDFReportVM();

            VM.list = log.PDFmodelForReport;
            VM.DeviceItems = DeviceItems;
            VM.PositionItems = PositionItems;
            VM.HospitalItems = HospitalItems;
            return View(VM);

        }
        public string getListOfPDF(PDFModel model)
        {

            string result = "";
            using (WebClient client = new WebClient())
            {

                var collection = new NameValueCollection();
                collection.Add("DeviceID", model.catID);
                collection.Add("hospitalName", model.hospitalName);
                collection.Add("timestamp", model.timestamp);
                collection.Add("model", model.model);
                collection.Add("mark", model.mark);
                collection.Add("serial", model.serial);
                collection.Add("amvaal", model.amval);
                collection.Add("status", model.status);
                collection.Add("esteghrar", model.position);


                byte[] response =
                client.UploadValues("http://www.supectco.com/webs/GDP/Admin/getPDFList.php?", collection);

                result = System.Text.Encoding.UTF8.GetString(response);
            }
            return result;
        }
        public PartialViewResult UpdatePdfList(string hospitalName, string catID, string position, string status, string model, string mark, string serial, string amval)
        {
            PDFModel mymodel = new PDFModel()
            {
                amval = amval,
                catID = catID,
                hospitalName = hospitalName,
                mark = mark,
                model = model,
                position = position,
                serial = serial,
                status = status
            };
            string srt = getListOfPDF(mymodel);
            PDFModelForReportList log = JsonConvert.DeserializeObject<PDFModelForReportList>(srt);
            return PartialView("~/Views/Shared/AdminShared/_PDFList.cshtml", log);
        }


        public class ImageResult : ActionResult
        {
            public ImageResult(Stream imageStream, string contentType)
            {
                if (imageStream == null)

                    throw new ArgumentNullException("imageStream");

                if (contentType == null)

                    throw new ArgumentNullException("contentType");
                this.ImageStream = imageStream;

                this.ContentType = contentType;

            }
            public Stream ImageStream { get; private set; }
            public string ContentType { get; private set; }
            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                    throw new ArgumentNullException("context");
                HttpResponseBase response = context.HttpContext.Response;
                response.ContentType = this.ContentType;
                byte[] buffer = new byte[4096];
                while (true)
                {
                    int read = this.ImageStream.Read(buffer, 0, buffer.Length);
                    if (read == 0)
                        break;
                    response.OutputStream.Write(buffer, 0, read);
                }
                response.End();
            }
        }
    }


}
