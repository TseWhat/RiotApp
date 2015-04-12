using RiotAPI.Entities;
using RiotSharp.MatchEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI
{
    public class ScoreCardCalculator
    {
        public ScoreCard AddScore()
        {
            return new ScoreCard();
        }

        public List<ScoreCard> CalculateChampionScores(List<MatchDetail> matches, List<Champion> champions)
        {
            Dictionary<long, ScoreCard> championScores = new Dictionary<long,ScoreCard>();

            foreach (Champion champion in champions)
                championScores.Add(champion.Id, new ScoreCard { ChampionId = (int)champion.Id});

            foreach (MatchDetail match in matches)
                if (match != null)
                    foreach (Participant summoner in match.Participants)
                    {
                        int championId = summoner.ChampionId;
                        ParticipantStats stats = summoner.Stats;

                        ScoreCard score = new ScoreCard
                        {
                            ChampionId = championId,
                            Kills = stats.Kills,
                            Deaths = stats.Deaths,
                            Assists = stats.Assists,
                            DoubleKills = stats.DoubleKills,
                            TripleKills = stats.TripleKills,
                            QuadraKills = stats.QuadraKills,
                            PentaKills = stats.PentaKills
                        };

                        championScores[championId] = score;                                            
                    }

            return championScores.Values.ToList();
        }
    }
}
