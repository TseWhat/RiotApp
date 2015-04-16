using RiotAPI.Entities;
using RiotSharp.MatchEndpoint;
using RiotSharp.StaticDataEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI
{
    public class DataExtractor
    {
        public ScoreCard AddScore()
        {
            return new ScoreCard();
        }

        public AverageStats CalculateAverageStats(List<MatchDetail> matches)
        {
            AverageStats stats = new AverageStats();


            foreach (var match in matches)
            {
                if (match != null)
                    foreach (Participant summoner in match.Participants)
                    {
                        stats.TotalKills += summoner.Stats.Kills;
                        stats.TotalDeaths += summoner.Stats.Deaths;
                        stats.PentaKills += summoner.Stats.PentaKills;
                        stats.DoubleKills += summoner.Stats.DoubleKills;
                        stats.TripleKills += summoner.Stats.TripleKills;
                        stats.QuadraKills += summoner.Stats.QuadraKills;
                        stats.BiggestCrit = 
                            summoner.Stats.LargestCriticalStrike > stats.BiggestCrit ? 
                                summoner.Stats.LargestCriticalStrike : 
                                stats.BiggestCrit;
                    }
            }

            return stats;
        }

        public List<ScoreCard> CalculateChampionScores(List<MatchDetail> matches, ChampionListStatic champions)
        {
            Dictionary<long, ScoreCard> championScores = new Dictionary<long,ScoreCard>();

            foreach (MatchDetail match in matches)
                if (match != null)
                    foreach (Participant summoner in match.Participants)
                    {
                        int championId = summoner.ChampionId;
                        ParticipantStats stats = summoner.Stats;

                        ScoreCard score = new ScoreCard
                        {
                            ChampionId = championId,
                            ChampionName = champions.Champions.Values.Single(id => id.Id == championId).Name,
                            TimesPlayed = 1,
                            Kills = stats.Kills,
                            Deaths = stats.Deaths,
                            Assists = stats.Assists
                        };

                        if (championScores.ContainsKey(championId))
                        {
                            championScores[championId].Kills += score.Kills;
                            championScores[championId].Deaths += score.Deaths;
                            championScores[championId].Assists += score.Assists;
                            championScores[championId].TimesPlayed += score.TimesPlayed;
                        }
                        else
                            championScores.Add(championId, score);
                    }

            return championScores.Values.ToList();
        }
    }
}
