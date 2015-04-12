using RiotAPI.Entities;
using RiotMVC.Entities;
using RiotSharp.ChampionEndpoint;
using RiotSharp.StatsEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiotMVC.Models
{
    public class SummonerModel
    {
        public List<ScoreCard> ChampionScoreCards { get; set; }

        public SummonerModel ()
        {
            ChampionScoreCards = new List<ScoreCard>();
        }
    }
}