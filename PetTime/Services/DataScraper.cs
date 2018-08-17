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



        /*
         {[
  {
    "id": "BkrJjgcV7",
    "url": "https://cdn2.thedogapi.com/images/BkrJjgcV7_640.jpg",
    "breeds": [
      {
        "id": 223,
        "name": "Shih Tzu",
        "life_span": "10 - 18 years",
        "bred_for": "Lapdog",
        "breed_group": "Toy",
        "temperament": "Clever, Spunky, Outgoing, Friendly, Affectionate, Lively, Alert, Loyal, Independent, Playful, Gentle, Intelligent, Happy, Active, Courageous",
        "weight": {
          "imperial": "9 - 16 lbs",
          "metric": "4 - 7 kgs"
        },
        "height": {
          "imperial": "8 - 11 inches at the withers",
          "metric": "20 - 28 cm at the withers"
        }
      }
    ],
    "categories": []
  }
]}
         */

    }
}
