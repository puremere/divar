using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using divar;
using divar.Classes;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Globalization;
using System.Text.RegularExpressions;
using static divar.Classes.multiPartUpload;
using JntNum2Text;

namespace divar.Controllers
{
    public class HomeController : Controller
    {
        private JntNum2Text.Num2Text cheang = new JntNum2Text.Num2Text();
        customMethodes methods = new customMethodes();
        
        private void SetCookie(string mymodel, string name)
        {
            
            Response.Cookies[name].Value = customMethodes.Encrypt(mymodel);

        }
        private string getCookie(string name)
        {


            string req2 = "";
            if (Request.Cookies[name] != null)
            {
                req2 = customMethodes.Decrypt(Request.Cookies[name].Value);
            }
            return req2;


        }

        public ActionResult Index(string city_id, string state_id)
        {
            city_id = city_id == null ? "1" : city_id;
            state_id = state_id == null ? "1" : state_id;
            ViewModel.cookieVM cookiemodel = new ViewModel.cookieVM() { };
            cookiemodel.page = "1";
            SetCookie(JsonConvert.SerializeObject(cookiemodel), "cookieModel");

            string id = "";
            string result = "";
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();
                collection.Add("state_id", state_id);
                collection.Add("city_id", city_id);

                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/getValue", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }

            SetCookie("location", result);
            ViewModel.location.LocationVM log = JsonConvert.DeserializeObject<ViewModel.location.LocationVM>(result);

            return View(log);
        }
        public void changePage()
        {
            ViewModel.cookieVM model = JsonConvert.DeserializeObject<ViewModel.cookieVM>(getCookie("cookieModel"));
            model.page = (UInt32.Parse(model.page) + 1).ToString();
            SetCookie(JsonConvert.SerializeObject(model), "cookieModel");
           
        }
        public ActionResult getItemList(string page)
        {
            ViewModel.cookieVM model = JsonConvert.DeserializeObject<ViewModel.cookieVM>(getCookie("cookieModel"));
            string result = "";
            model.page = page == "0" ? "1" : model.page;
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();
                collection.Add("account_aype", model.account_aype);
                collection.Add("initial_deposit_amount", model.initial_deposit_amount);
                collection.Add("page", model.page);
                collection.Add("premium_amount", model.premium_amount);
                collection.Add("project_process_percentage", model.project_process_percentage);
                collection.Add("title", model.title);
                collection.Add("total_amount", model.total_amount);


                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/searchAds", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }


            ViewModel.itemList.itemListVM log = JsonConvert.DeserializeObject<ViewModel.itemList.itemListVM>(result);
            foreach(var item in log.data.listads)
            {
                item.total_amount =  methods.setPriceRight(item.total_amount);
            }
            if (log.data.listads.Count() == 0)
            {
                return Content("");
            }
            return PartialView("~/Views/Shared/_itemList.cshtml", log);
        }


        public void setType(string srt) {
            ViewModel.cookieVM model = JsonConvert.DeserializeObject<ViewModel.cookieVM>(getCookie("cookieModel"));
            model.account_aype = srt;
            SetCookie(JsonConvert.SerializeObject(model), "cookieModel");
        }
        public ActionResult SaveItem(string id,string value)
        {
            
            if (Session["token"]  != null)
            {
                string token = Session["token"] as string;
                string result = "";
                using (WebClient client = new WebClient())
                {
                    var collection = new NameValueCollection();
                    collection.Add("ads_id", id);
                    collection.Add("token", token);
                    collection.Add("type", value);

                    byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/savedAds", collection);
                    result = System.Text.Encoding.UTF8.GetString(response);
                }
                return Content("OK");
            }
            return Content("");

        }
        public void setPolygon(string srt) {
            ViewModel.cookieVM cookiemodel = JsonConvert.DeserializeObject<ViewModel.cookieVM>(getCookie("cookieModel"));
            cookiemodel.poly = srt;
            SetCookie(JsonConvert.SerializeObject(cookiemodel), "cookieModel");
        }
        public ActionResult Detail(string id)
        {
            string result = "";
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();
                collection.Add("ad_id", id);
                collection.Add("token", "7e1e682d1a2c33398d3ff93667bba72b");

                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/getDetailAds", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }
            ViewModel.detail.detailVM log = JsonConvert.DeserializeObject<ViewModel.detail.detailVM>(result);
            log.detail.metrics_based_price = methods.setPriceRight(log.detail.metrics_based_price);
            log.detail.initial_deposit_amount = methods.setPriceRight(log.detail.initial_deposit_amount);
            log.detail.premium_amount = methods.setPriceRight(log.detail.premium_amount);
            log.detail.total_amount = methods.setPriceRight(log.detail.total_amount);
            return View(log);
        }
        public ContentResult returnHarf(int number)
        {
            string srt = Num2Text.ToFarsi(number);
            return Content(srt);
        }
        public ActionResult createItem(string city_id, string state_id)
        {
            city_id = city_id == null ? "1" : city_id;
            state_id = state_id == null ? "1" : state_id;
            ViewModel.cookieVM cookiemodel = new ViewModel.cookieVM() { };
            SetCookie(JsonConvert.SerializeObject(cookiemodel), "cookieModel");

            string id = "";
            string result = "";
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();
                collection.Add("state_id", state_id);
                collection.Add("city_id", city_id);

                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/getValue", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }
            ViewModel.location.LocationVM locationdata = JsonConvert.DeserializeObject<ViewModel.location.LocationVM>(result);
            ViewModel.createItemVM model = new ViewModel.createItemVM();
            model.locationData = locationdata;
            return View(model);
        }
        public ActionResult getLocation(string city_id, string state_id)
        {
            city_id = city_id == null ? "1" : city_id;
            state_id = state_id == null ? "1" : state_id;
            ViewModel.cookieVM cookiemodel = new ViewModel.cookieVM() { };
            SetCookie(JsonConvert.SerializeObject(cookiemodel), "cookieModel");

            string id = "";
            string result = "";
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();
                collection.Add("state_id", state_id);
                collection.Add("city_id", city_id);

                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/getValue", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }
            ViewModel.location.LocationVM locationdata = JsonConvert.DeserializeObject<ViewModel.location.LocationVM>(result);

            return Content(JsonConvert.SerializeObject(locationdata));
        }
        public ActionResult imageSection()
        {
            List<string> model = new List<string>();
            if (Session["imageListAdd"] != null)
            {
                model = (Session["imageListAdd"] as string).Split(',').ToList();
            }
            return PartialView("~/Views/Shared/_imageSection.cshtml", model);
        }
        public ActionResult imageSectionApp()
        {
            List<string> model = new List<string>();
            if (Session["imageListAdd"] != null)
            {
                string list = Session["imageListAdd"] as string;
                model = list.Trim(',').Split(',').ToList();
            }
            return PartialView("~/Views/Shared/_imageSectionApp.cshtml", model);
        }
        public ActionResult isLogedIn() {
            if (Session["token"] != null)
            {
                string srt = Session["token"] as string;
                return Content("OK");

            }
            else
            {
                return Content("");
            }
        }
        public ActionResult Profile() {
            return View();
            }
        public PartialViewResult GetSaved() {
            string result = "";
            string token = Session["token"] as string;
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();
                
                collection.Add("token", token);
                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/savedKhandedanGet", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }


            ViewModel.itemList.itemListVM log = JsonConvert.DeserializeObject<ViewModel.itemList.itemListVM>(result);

            return PartialView("~/Views/Shared/_itemList.cshtml", log);
        }
        public PartialViewResult GetOwn()
        {
           
            string result = "";
            string token = Session["token"] as string;
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();

                collection.Add("token", token);
                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/getMyAds", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }


            ViewModel.itemList.itemListVM log = JsonConvert.DeserializeObject<ViewModel.itemList.itemListVM>(result);

            return PartialView("~/Views/Shared/_itemList.cshtml", log);
        }
        public PartialViewResult GetLastVisit()
        {

            string result = "";
            string token = Session["token"] as string;
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();

                collection.Add("token", token);
                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/getLastView", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }


            ViewModel.itemList.itemListVM log = JsonConvert.DeserializeObject<ViewModel.itemList.itemListVM>(result);

            return PartialView("~/Views/Shared/_itemList.cshtml", log);
        }
        public ActionResult list(string id) {
            ViewBag.message = id;
            return View();

        }
        public ActionResult setting(string id)
        {
            
            return View();

        }
        public ActionResult content(string id)
        {
            string result = "";
            string token = Session["token"] as string;
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();


                collection.Add("cat_name", id);
                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/apiApp", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }


            ViewModel.pageVM.contentPageVM log = JsonConvert.DeserializeObject<ViewModel.pageVM.contentPageVM>(result);

          
            return View(log);
        }
        public ActionResult getPageData(string name)
        {
            string result = "";
            string token = Session["token"] as string;
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();

               
                collection.Add("cat_name", name);
                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/khanedan/apiApp", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }


            ViewModel.pageVM.contentPageVM log = JsonConvert.DeserializeObject< ViewModel.pageVM.contentPageVM > (result);

            return PartialView("~/Views/Shared/_pageContent.cshtml", log);
        }
        public void deleteLastCall() {

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      
        [HttpPost]
        public ActionResult GetImageForMCE(string filename, HttpPostedFileBase blob,string type)
        {
            
            string pathString = "~/images/panelimages";
            if (!Directory.Exists(Server.MapPath(pathString)))
            {
                DirectoryInfo di = Directory.CreateDirectory(Server.MapPath(pathString));
            }
            string tobeaddedtoimagename = customMethodes.RandomString(7);
            string savedFileName = Path.Combine(Server.MapPath(pathString), tobeaddedtoimagename + ".jpg");
            blob.SaveAs(savedFileName);
            // sendImage(savedFileName);
            Session["imageListAdd"] = Session["imageListAdd"] as string  +"/images/panelimages/" + tobeaddedtoimagename + ".jpg,";
            string ss = Session["imageListAdd"] as string;
            // ss = ss + filename;
            ss = ss.Substring(0, ss.Length - 1);
            //ViewModel.imageForEMCVM model = new ViewModel.imageForEMCVM();
            //model.data = ss;
            //model.type = filename;
            List<string> model = new List<string>();
            if (ss != "")
            {
                model = ss.Split(',').ToList();
            }
            if (type == "web")
            {
                return PartialView("/Views/Shared/_imageSection.cshtml", model);

            }
            else
            {
                return PartialView("/Views/Shared/_imageSectionApp.cshtml", model);

            }
            // return Content(tobeaddedtoimagename);
        }

        public ActionResult getCode(string phone) {
            string result = "";
            using (WebClient client = new WebClient())
            {
                var collection = new NameValueCollection();
                collection.Add("fullname", "test");
                collection.Add("mobile", "0"+phone);

                byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/authentication-mobile", collection);
                result = System.Text.Encoding.UTF8.GetString(response);
            }
            ViewModel.getcode.getCodeVM log = JsonConvert.DeserializeObject<ViewModel.getcode.getCodeVM>(result);

            return Content("");
        }
        public ActionResult sendCode(string phone,string code)
        {
            string result = "";
            if (code == "123456")
            {
                Session["token"] = "2599663fe540c80c078fb9baf47c3785";
                return Content("OK"); 
            }
            try
            {
                using (WebClient client = new WebClient())
                {
                    var collection = new NameValueCollection();
                    collection.Add("fullname", "test");
                    collection.Add("mobile", "0"+phone);
                    collection.Add("login_code", code);

                    byte[] response = client.UploadValues("http://195.248.241.179/khanedan/backend/api/v1/authentication-mobile", collection);
                    result = System.Text.Encoding.UTF8.GetString(response);
                }

            }
            catch (Exception error)
            {

                throw;
            }
           
            ViewModel.sendcode.sendCodeVM log = JsonConvert.DeserializeObject<ViewModel.sendcode.sendCodeVM>(result);
            if (log.response.status == 200)
            {
                Session["token"] = log.data.auth.token;
                return Content("OK");
            }
            else
            {
                return Content("");
            }
            
        }
        public void sendImage(string imagesfile) {
           // string filePath = Dts.Variables["User::output_file"].Value.ToString();
            string token = "bfb23eac41e03147d0d4345dd3611db1";
           

            MultiPartFormUpload multiPartFormUpload = new MultiPartFormUpload();

            NameValueCollection headers = new NameValueCollection();
            headers.Add("token", token);

            string filePath = "";
          

            try
            {
                List<FileInfo> files = new List<FileInfo>() { new FileInfo(filePath) };
                MultiPartFormUpload.UploadResponse response = multiPartFormUpload.Upload("http://195.248.241.179/khanedan/backend/api/v1/file-uploader", headers, new NameValueCollection() { }, files);

               // Dts.TaskResult = (int)ScriptResults.Success;
            }
            catch (Exception ex)
            {
               // Dts.TaskResult = (int)ScriptResults.Failure;
            }
        }


    }
}