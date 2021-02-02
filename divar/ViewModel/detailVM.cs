using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace divar.ViewModel.detail
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    public class Objcity
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Objregion
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Objstate
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Objcompany
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class State
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class City
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Region
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Detail
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string desc_ads { get; set; }
        public string total_amount { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string companyname { get; set; }
        public string account_type { get; set; }
        public Objcity objcity { get; set; }
        public Objregion objregion { get; set; }
        public Objstate objstate { get; set; }
        public Objcompany objcompany { get; set; }
        public string initial_deposit_amount { get; set; }
        public string premium_amount { get; set; }
        public string project_process_percentage { get; set; }
        public string metrics_based_price { get; set; }
        public string is_metrics_based_price { get; set; }
        public State state { get; set; }
        public City city { get; set; }
        public Region region { get; set; }
        public string start_date { get; set; }
        public string floors_count { get; set; }
        public string end_date { get; set; }
        public string number_of_units { get; set; }
        public string unit_area { get; set; }
        public string amount_per_installment { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string image1 { get; set; }
        public string image2 { get; set; }
        public string image3 { get; set; }
        public string image4 { get; set; }
        public string image5 { get; set; }
        public string time { get; set; }
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

    public class detailVM
    {
        public List<Listcomment> listcomment { get; set; }
        public UserComment user_comment { get; set; }
        public Detail detail { get; set; }
        public Response response { get; set; }
    }



    public class UserComment
    {
        public string ID { get; set; }
        public string comment { get; set; }
        public string fullname { get; set; }
        public object rate { get; set; }
        public string time { get; set; }
    }

    
    public class Listcomment
    {
        public string ID { get; set; }
        public string comment { get; set; }
        public string fullname { get; set; }
        public string time { get; set; }
    }
   

}