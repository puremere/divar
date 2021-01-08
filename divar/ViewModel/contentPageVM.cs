using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace divar.ViewModel.pageVM
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Data
    {
        public string data_content { get; set; }
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

    public class contentPageVM
    {
        public Data data { get; set; }
        public Response response { get; set; }
    }


    
}