using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spire.License;
using Spire.Pdf.General.Find;
using Spire.Pdf;
using GDPProject.Models;
using Spire.Pdf.Graphics;
using GDPProject.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace GDPProject.Controllers
{

    public class HomeController : Controller
    {



        string txtpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.txt");


        public ActionResult Index()
        {
            PrivateFontCollection private_fonts2 = new PrivateFontCollection();
            string fontpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fonts\\BNAZANIN.ttf");
            private_fonts2.AddFontFile(fontpath);



            string emptyNamePDF = "";
            emptyNamePDF = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF/pumpEmpty.pdf");
            var testFile2 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), emptyNamePDF);
            string newFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ResultPDF/result/title.pdf");
            string finalFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ResultPDF/final.pdf");

            Spire.Pdf.PdfDocument doc2 = new Spire.Pdf.PdfDocument();
            doc2.LoadFromFile(testFile2);
            float pageheight = 0;

            foreach (PdfPageBase spipage in doc2.Pages)
            {

               // string imagepath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "checked.png");
                string imagepath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "unchecked.png");

                Spire.Pdf.Graphics.PdfImage image = Spire.Pdf.Graphics.PdfImage.FromFile(imagepath);
                float width = image.Width * 0.75f;
                float height = image.Height * 0.75f;
                spipage.Canvas.DrawImage(image, float.Parse("100"), float.Parse("100"), 20, 20);

                //PdfTrueTypeFont iransanse = new PdfTrueTypeFont(fontpath, 11);
                //PdfTrueTypeFont Nazanin = new PdfTrueTypeFont(new System.Drawing.Font(private_fonts2.Families[0], 22), true);
                //PdfTrueTypeFont Arial = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 9f), true);
                //Spire.Pdf.Graphics.PdfFont font1 = new Spire.Pdf.Graphics.PdfFont(PdfFontFamily.TimesRoman, 9);
                //Spire.Pdf.Graphics.PdfBrush brush = PdfBrushes.Black;
                //PdfStringFormat format = new PdfStringFormat();
                //SizeF size = Arial.MeasureString("aaaa", format);
                //float Xposition = float.Parse("100") - size.Width;
                //PointF position = new PointF(Xposition, float.Parse("100"));
                //spipage.Canvas.DrawString("سلام حالت چطوره ", Nazanin, brush, position, new PdfStringFormat() { RightToLeft = true });


            }
            doc2.SaveToFile(newFile, FileFormat.PDF);
            doc2.Close();
            string txtpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.txt");
            string mymodel = System.IO.File.ReadAllText(txtpath);

            ////  mymodel = "{\"id\":1927,\"catID\":1,\"formID\":25,\"title\":\"مشخصات متقاضی\",\"value\":\"master\",\"min\":-1,\"max\":-1,\"type\":0,\"page\":\"1\",\"font\":\"\",\"x\":\"0\",\"y\":\"0\",\"masterID\":143},{\"id\":1928,\"catID\":1,\"formID\":25,\"title\":\"hospital name\",\"value\":\"15\",\"min\":-1,\"max\":-1,\"type\":0,\"page\":\"1\",\"font\":\"P\",\"x\":\"0\",\"y\":\"0\",\"masterID\":143},{\"id\":1929,\"catID\":1,\"formID\":25,\"title\":\"mohandes\",\"value\":\"\",\"min\":-1,\"max\":-1,\"type\":0,\"page\":\"1\",\"font\":\"P\",\"x\":\"0\",\"y\":\"0\",\"masterID\":143}";

            // List<model> lst2 = JsonConvert.DeserializeObject<List<model>>(mymodel);



            //  FormModel model = JsonConvert.DeserializeObject<FormModel>("");

            //  List<model> lst = JsonConvert.DeserializeObject<List<model>>(model.model);
            //  getModel(model);
            return View();
        }

        public void setpdfindatabase(PDFModel model)
        {
            string json = "";
            string result = "";
            string txtpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.txt");
            System.IO.File.AppendAllText(txtpath, "set pdf detail");
            using (WebClient client = new WebClient())
            {

                var collection = new NameValueCollection();
                collection.Add("DeviceID", model.catID);
                collection.Add("hospitalName",GlobalVariables.name );
                collection.Add("ostan",GlobalVariables.ostan );
                collection.Add("OperatorID", model.OperatorID);
                collection.Add("DuraionTime", model.DuraionTime);
                collection.Add("PDFPath", model.PDFPath);
                collection.Add("PDFTitle", model.PDFTitle);
                collection.Add("timestamp", model.timestamp);
                collection.Add("model", model.model);
                collection.Add("mark", model.mark);
                collection.Add("serial", model.serial);
                collection.Add("amvaal", model.amval);
                collection.Add("status", model.status);
                collection.Add("esteghrar", model.position);
                collection.Add("referer", model.referer);



                byte[] response =
                client.UploadValues("http://www.supectco.com/webs/GDP/Admin/setPDFDetail.php?", collection);

                result = System.Text.Encoding.UTF8.GetString(response);
            }
            try
            {
                string delpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ResultPDF\\Result" + model.PDFTitle + ".pdf");

                 //System.IO.File.OpenRead(delpath);
                 System.IO.File.Delete(delpath);
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText(txtpath, e.Message);
            }
            
            //System.IO.File.WriteAllText(txtpath, result);

        }
        public ActionResult getModel(FormModel postedModel)
        {
           
            PDFModel PDFmdoel = new PDFModel()
            {
                DuraionTime = postedModel.formfillingtime,
                OperatorID = postedModel.phone,
                hospitalName = GlobalVariables.name,
                timestamp = postedModel.timestamp,
                PDFTitle = postedModel.title,
                catID = postedModel.catID,


            };
            if (GlobalVariables.address == "" || GlobalVariables.address == null)
            {
                string json = "";
                using (var client = new WebClient())
                {

                    json = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/getHospitalDetail.php?active=true");

                    HospitalRoot log = JsonConvert.DeserializeObject<HospitalRoot>(json);
                    GlobalVariables.address = log.hospitalDetail.First().address;
                     GlobalVariables.name = log.hospitalDetail.First().name;
                    GlobalVariables.mohandes = log.hospitalDetail.First().mohandes;
                    GlobalVariables.ostan = log.hospitalDetail.First().ostan;
                    GlobalVariables.sharestan = log.hospitalDetail.First().sharestan;
                    GlobalVariables.phone = log.hospitalDetail.First().phone;

                }
            }
            try
            {
                string json3 = "";
                using (var client = new WebClient())
                {

                    json3 = client.DownloadString("http://www.supectco.com/webs/GDP/Admin/getLastPDFName.php?name=" + postedModel.title);


                }
                if (json3.Length > 1)
                {

                    List<string> firstList = json3.Split(',').ToList();
                    List<string> TagIds = firstList[0].Split('_').ToList();
                    if (TagIds.Count() > 1)
                    {
                        postedModel.title = postedModel.title + "_" + (int.Parse(TagIds[1]) + 1).ToString();
                    }
                    else
                    {
                        postedModel.title = postedModel.title + "_1";
                    }


                    PDFmdoel.referer = firstList[1];
                    PDFmdoel.PDFTitle = postedModel.title;
                }
            }
            catch (Exception e)
            {

                System.IO.File.WriteAllText(e.Message, postedModel.title);
            }
            
            //string txtpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.txt");
            //System.IO.File.AppendAllText(txtpath, String.Empty);
            System.IO.File.WriteAllText(txtpath, postedModel.title);

            
            string id = "";
            try
            {
                string srt = postedModel.model;


                //  string txtpath2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample2.txt");
                System.IO.File.AppendAllText(txtpath, String.Empty);
                System.IO.File.AppendAllText(txtpath, srt);



                List<model> mymodel = JsonConvert.DeserializeObject<List<model>>(srt);
                string count = mymodel.Count().ToString();
                System.IO.File.AppendAllText(txtpath, String.Empty);
                System.IO.File.AppendAllText(txtpath, count);

                var testFile = "";
                testFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF/pump.pdf");
                string emptyNamePDF = "";
                emptyNamePDF = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF/pumpEmpty.pdf");

                //set pdf in database ;


                //create pdf proccess

                //Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
                //doc.LoadFromFile(testFile);
                //PdfTextFind results = null;
                ////setting position for every title
                //PdfPageBase page;
              
                //foreach (var item in mymodel)
                //{
                    
                //    int PAGE = Convert.ToInt32(item.page) - 1;
                //    page = doc.Pages[PAGE];
                //    if (item.value != "master")
                //    {
                //        results = page.FindText(item.title).Finds.First();
                //        float width = results.Size.Width;
                //        item.x = (results.Position.X + width).ToString();
                //        item.y = (results.Position.Y).ToString();
                //    }
                   


                //}

                
                var testFile2 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), emptyNamePDF);
                string newFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ResultPDF/result" + postedModel.title + ".pdf");
                string finalFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ResultPDF/" + postedModel.title + ".pdf");

                Spire.Pdf.PdfDocument doc2 = new Spire.Pdf.PdfDocument();
                doc2.LoadFromFile(testFile2);
                float pageheight = 0;
              
                foreach (PdfPageBase spipage in doc2.Pages)
                {
                    int index = doc2.Pages.IndexOf(spipage);
                    if (index == 0)
                    {
                        
                        mymodel.Where(x => x.page == "1" && x.title == "address").FirstOrDefault().value = GlobalVariables.address;
                        mymodel.Where(x => x.page == "1" && x.title == "hospital name").FirstOrDefault().value = GlobalVariables.name;
                        mymodel.Where(x => x.page == "1" && x.title == "mohandes").FirstOrDefault().value = GlobalVariables.mohandes;
                        mymodel.Where(x => x.page == "1" && x.title == "ostan").FirstOrDefault().value = GlobalVariables.ostan;
                        mymodel.Where(x => x.page == "1" && x.title == "shahrestan").FirstOrDefault().value = GlobalVariables.sharestan;
                        mymodel.Where(x => x.page == "1" && x.title == "phone").FirstOrDefault().value = GlobalVariables.phone;



                        foreach (var item in mymodel.Where(x => x.page == "1" && x.title == "mark"))
                        {
                            PDFmdoel.mark = PDFmdoel.mark + item.value;
                        }
                        foreach (var item in mymodel.Where(x => x.page == "1" && x.title == "model"))
                        {
                            PDFmdoel.model = PDFmdoel.model + item.value;
                        }
                        foreach(var item in mymodel.Where(x => x.page == "1" && x.title == "esteghrar"))
                        {
                            PDFmdoel.position = PDFmdoel.position + item.value;
                        }
                        PDFmdoel.serial = mymodel.Where(x => x.page == "1" && x.title == "serial").FirstOrDefault().value;


                        PDFmdoel.amval = mymodel.Where(x => x.page == "1" && x.title == "amvaal").FirstOrDefault().value;
                        
                        if (mymodel.Where(x => x.page == "1" && x.title == "Passed").FirstOrDefault() != null)
                        {
                            if (mymodel.Where(x => x.page == "1" && x.title == "Passed").FirstOrDefault().value == "true")
                            {
                                PDFmdoel.status = "Passed";
                            }
                        }
                        if (mymodel.Where(x => x.page == "1" && x.title == "Limited").FirstOrDefault() != null)
                        {
                            if (mymodel.Where(x => x.page == "1" && x.title == "Limited").FirstOrDefault().value == "true")
                            {
                                PDFmdoel.status = "Limited";
                            }
                        }
                        if (mymodel.Where(x => x.page == "1" && x.title == "Failed").FirstOrDefault() != null)
                        {
                            if (mymodel.Where(x => x.page == "1" && x.title == "Failed").FirstOrDefault().value == "true")
                            {
                                PDFmdoel.status = "Failed";
                            }
                        }
                    }
                    List<model> pageFieldes = mymodel.Where(x => x.page == (index + 1).ToString()).ToList();
                    pageheight = spipage.Size.Height;
                  
                   
                    foreach (var item in pageFieldes)
                    {


                        PdfTrueTypeFont Arial = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 10f), true);
                        PdfTrueTypeFont Arial9 = new PdfTrueTypeFont(new System.Drawing.Font("Arial", 9f), true);
                        Spire.Pdf.Graphics.PdfFont font1 = new Spire.Pdf.Graphics.PdfFont(PdfFontFamily.TimesRoman, 9);
                        Spire.Pdf.Graphics.PdfBrush brush = PdfBrushes.Black;
                        PdfStringFormat format = new PdfStringFormat();
                        SizeF size = Arial9.MeasureString(item.value, format);
                        float Xposition = float.Parse(item.x) - size.Width;
                        float yposition = float.Parse(item.y)-1;
                        PointF position = new PointF(Xposition, float.Parse(item.y));

                        if (item.type == 1)
                        {
                            if(item.value == "true")
                            {
                                string imagepath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "checked.png");
                                Spire.Pdf.Graphics.PdfImage image = Spire.Pdf.Graphics.PdfImage.FromFile(imagepath);
                                float width = image.Width * 0.75f;
                                float height = image.Height * 0.75f;
                                spipage.Canvas.DrawImage(image, Xposition, yposition, 10, 10);
                            }
                            else
                            {

                                string imagepath2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "unchecked.png");
                                Spire.Pdf.Graphics.PdfImage image2 = Spire.Pdf.Graphics.PdfImage.FromFile(imagepath2);
                                spipage.Canvas.DrawImage(image2, Xposition , yposition, 10, 10);
                            }
                           

                        }
                        else
                        {
                            if (item.font == "E")
                            {
                                spipage.Canvas.DrawString(item.value, Arial, brush, position);
                            }
                            else
                            {
                                position.X = position.X ;
                                spipage.Canvas.DrawString(item.value, Arial, brush, position, new PdfStringFormat() { RightToLeft = true });
                            }
                        }
                        

                    };

                }
                doc2.SaveToFile(newFile, FileFormat.PDF);
                doc2.Close();
                string srt2 = "";
                using (PdfReader pdfReader = new PdfReader(newFile))
                {
                    float widthTo_Trim = pageheight - 15;// iTextSharp.text.Utilities.MillimetersToPoints(450);


                    using (FileStream output = new FileStream(finalFile, FileMode.Create, FileAccess.Write))
                    {
                        using (PdfStamper pdfStamper = new PdfStamper(pdfReader, output))
                        {

                            iTextSharp.text.Rectangle cropBox = pdfReader.GetCropBox(1);
                            cropBox.Top = widthTo_Trim;
                            pdfReader.GetPageN(1).Put(PdfName.CROPBOX, new PdfRectangle(cropBox));
                            //for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                            //{

                            //}
                        }

                      

                        PDFmdoel.PDFPath = finalFile;
                       

                        returnModel m2 = new returnModel()
                        {
                            status = 200,
                            message = "http://gdp.sup-ect.ir/ResultPDF/" + postedModel.title + ".pdf",
                        };
                        srt2 = JsonConvert.SerializeObject(m2);
                      
                      
                    }


                    pdfReader.Close();
                    setpdfindatabase(PDFmdoel);
                }

                return Content(srt2);

            }
            catch (Exception e)
            {
                string txtpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.txt");
                System.IO.File.AppendAllText(txtpath, e.Message);
                returnModel m = new returnModel()
                {
                    status = 400,
                    message = e.Message + id,
                };
                string srt = JsonConvert.SerializeObject(m);
                return Content(srt);
            }

        }


    }
}





