using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace divar.ViewModel.getcode
{
    public class Data
    {
        public string result { get; set; }
        public int smsCode { get; set; }
    }

    public class Response
    {
        public int status { get; set; }
        public bool show_dialog { get; set; }
        public object message { get; set; }
        public string positive_btn { get; set; }
        public string negative_btn { get; set; }
        public string positive_url { get; set; }
        public bool can_dismiss { get; set; }
    }

    public class getCodeVM
    {
        public Data data { get; set; }
        public Response response { get; set; }
    }
   
}