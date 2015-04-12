using RiotSharp.StatsEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotMVC.Entities
{
    public class Summoner
    {
        public string Name { get; set; }
        public long SummonerId { get; set; }
        public int ProfileIconId { get; set; }
        public List<ChampionStats> ChampionStats { get; set; }
        public List<PlayerStatsSummary> StatSummaries { get; set; }
        public long Level { get; set; }
    }
}
