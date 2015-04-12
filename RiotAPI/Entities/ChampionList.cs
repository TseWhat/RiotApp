using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Entities
{
    class ChampionList
    {
        [JsonProperty("champions")]
        public List<Champion> Champions { get; set; }
    }
}
