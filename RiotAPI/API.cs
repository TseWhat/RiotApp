using RiotAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI
{
    public class API
    {
        private const long _requestLimit = 10;
        private TimeSpan _requestResetTime = TimeSpan.FromSeconds(10);
        private const string _url = "https://euw.api.pvp.net/api/lol/";
        private Region _region;

        private const string _urfGames = "/v4.1/game/ids";
        private const string _champions = "/v1.2/champion";

        private const string _urfGamesParameters = "?beginDate={0}&";
        private const string _championParameters = "?freeToPlay={0}&";
        private string _apiKey = "api_key=8bd0939d-15b0-48b6-b14b-64e1280afe5d";
        public string KeyOnly = "8bd0939d-15b0-48b6-b14b-64e1280afe5d";
        private long _epoch;
        private string _baseCallUrl = _url + Region.euw.ToString(); // + urfGames;

        public API(Region region, string key = "api_key=8bd0939d-15b0-48b6-b14b-64e1280afe5d")
        {
            _apiKey = key;
            _region = region;
        }

        public List<long> GetUrfGames(DateTime date)
        {            
            
            string callUrl = _baseCallUrl + _urfGames;
            SetEpoch(date);
            string parameters = _urfGamesParameters.Replace("{0}", _epoch.ToString()) + _apiKey;

            HttpResponseMessage response = CallRiotApi(callUrl, parameters);

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<List<long>>().Result;
            else
                return new List<long>();
        }

        public List<Champion> GetChampions(bool freeToPlay = false)
        {
            string baseUrl =  APIMethods.GetBaseUrl(_region);
            string callUrl = _baseCallUrl + _champions;
            string parameters = _championParameters.Replace("{0}", freeToPlay.ToString()) + _apiKey; 

            HttpResponseMessage response = CallRiotApi(callUrl, parameters);

            if (response.IsSuccessStatusCode)
            {
               return response.Content.ReadAsAsync<ChampionList>().Result.Champions;
            }
            else
                return new List<Champion>();
        }
        
        public void SetEpoch(DateTime date)
        {
             _epoch = ToUnixTime(RoundDown(date, TimeSpan.FromMinutes(5))); 
        }

        private HttpResponseMessage CallRiotApi(string url, string parameters)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(parameters).Result;

            return response;
        }
 
        private DateTime RoundUp(DateTime dt, TimeSpan period)
        {
            return new DateTime(((dt.Ticks + period.Ticks - 1) / period.Ticks) * period.Ticks);
        }

        private DateTime RoundDown(DateTime dt, TimeSpan period)
        {
            return new DateTime(((dt.Ticks + period.Ticks) / period.Ticks) * period.Ticks);
        }

        private DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        private long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }


}
