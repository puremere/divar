using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace divar.ViewModel.location
{
    public class LocationVM
    {
        public Data data { get; set; }
        public Response response { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Listcity
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Liststate
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Listregion
    {
        public int ID { get; set; }
        public int title { get; set; }
    }

    public class Listcompany
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Data
    {
        public List<Listcity> listcity { get; set; }
        public List<Liststate> liststate { get; set; }
        public List<Listregion> listregion { get; set; }
        public List<Listcompany> listcompany { get; set; }
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

   



}