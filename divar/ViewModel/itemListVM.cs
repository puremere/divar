using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace divar.ViewModel.itemList
{
   
    public class Listad
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string desc_ads { get; set; }
        public string total_amount { get; set; }
        public string address { get; set; }
        public string companyname { get; set; }
        public string account_type { get; set; }
        public string end_date { get; set; }
        public string project_process_percentage { get; set; }
        public string time { get; set; }
        public string saved { get; set; }
        public string image { get; set; }
    }

    public class Data
    {
        public int count { get; set; }
        public List<Listad> listads { get; set; }
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

    public class itemListVM
    {
        public Data data { get; set; }
        public Response response { get; set; }
    }
}