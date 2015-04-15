using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI.Entities
{
    public class ScoreCard
    {
        public int ChampionId { get; set; }
        public string ChampionName { get; set; }
        public long Kills { get; set; }
        public long Deaths { get; set; }
        public long Assists { get; set; }
    }
}
