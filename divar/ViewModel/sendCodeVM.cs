using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace divar.ViewModel.sendcode
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class User
    {
        public string ID { get; set; }
        public string last_sms_send { get; set; }
        public string isBlocked { get; set; }
        public string account_id { get; set; }
        public object block_reason { get; set; }
        public string verification_code { get; set; }
        public string fullname { get; set; }
        public string mobile { get; set; }
    }

    public class Auth
    {
        public bool is_login_valid { get; set; }
        public bool is_mobile_verfied { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }

    public class Data
    {
        public Auth auth { get; set; }
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

    public class sendCodeVM
    {
        public Data data { get; set; }
        public Response response { get; set; }
    }


}