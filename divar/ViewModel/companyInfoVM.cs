using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace divar.ViewModel.companyInfo
{
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Company
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string phone { get; set; }
        public string image { get; set; }
        public string desc_company { get; set; }
    }

    public class Data
    {
        public Company company { get; set; }
    }

    public class Response
    {
        public int status { get; set; }
        public bool show_dialog { get; set; }
        public string message { get; set; }
        public string positive_btn { get; set; }
        public string negative_btn { get; set; }
        public string positive_url { get; set; }
        public bool can_dismiss { get; set; }
    }

    public class companyInfoVM
   
    {
        public Data data { get; set; }
        public Response response { get; set; }
    }


}