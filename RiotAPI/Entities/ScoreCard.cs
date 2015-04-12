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
        public long Kills { get; set; }
        public long Deaths { get; set; }
        public long Assists { get; set; }
        public long DoubleKills { get; set; }
        public long TripleKills { get; set; }
        public long QuadraKills { get; set; }
        public long PentaKills { get; set; }
    }
}
