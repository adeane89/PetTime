using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PetTime.Services
{
    public class DataScraper
    {
        private string _apiKey;

        public DataScraper(string apiKey)
        {
            this._apiKey = apiKey;
        }

        public DogApiDog[] Scrape()
        {
            string serviceUrl = "https://api.thedogapi.com/v1/images/search?size=med&mime_types=jpg&format=json&has_breeds=true&order=ASC&page=0&limit=100";
            
            System.Net.WebRequest req = System.Net.WebRequest.Create(serviceUrl);
            req.Headers.Add("content-type", "application/json");
            req.Headers.Add("x-api-key", _apiKey);
            System.Net.WebResponse response = req.GetResponse();
            System.IO.Stream s = response.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(s);
            Newtonsoft.Json.JsonTextReader jsonTextReader = new Newtonsoft.Json.JsonTextReader(sr);
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

            return serializer.Deserialize<DogApiDog[]>(jsonTextReader);
        }

        public class DogApiDog
        {
            public string id { get; set; }
            public string url { get; set; }
            public DogApiBreed[] breeds { get; set; }
            public object categories { get; set; }
        }

        public class DogApiBreed
        {
            public int id { get; set; }
            public string name { get; set; }
            public string life_span { get; set; }
        }
    }
}
