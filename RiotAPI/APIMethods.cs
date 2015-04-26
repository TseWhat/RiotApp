using RiotAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI
{
    public static class APIMethods
    {
        /// <summary>
        /// Base URL for all REST calls.
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string GetBaseUrl(Region region)
        {
            return String.Format(BaseURL, region.ToString());
        }

        /// <summary>
        /// Retrieve Champion by ID. 
        /// </summary>
        /// <param name="championId"></param>
        /// <returns></returns>
        public static string GetIndividialChampionUrl(long championId)
        {
            return String.Format(IndividualChampionUrl, championId);
        }

        private static string BaseURL
        {
            get { return "https://{0}.api.pvp.net/api/lol/"; }
        }

        /// <summary>
        /// Retrieve all Champions.
        /// </summary>
        public static string ChampionURL
        {
            get { return "/v1.2/champion"; }
        }

        private static string IndividualChampionUrl
        {
            get { return "/v1.2/champion/{0}"; }
        }

        /// <summary>
        /// Get current game information by Summoner iD.
        /// </summary>
        /// <param name="platformId"></param>
        /// <param name="summonerId"></param>
        /// <returns></returns>
        public static string GetCurrentGameUrl(string platformId, long summonerId)
        {
            return String.Format(CurrentGameUrl, platformId, summonerId);
        }

        private static string CurrentGameUrl
        {
            get { return "/observer-mode/rest/consumer/getSpectatorGameInfo/{0}/{1}"; }
        }

        /// <summary>
        /// Get list of featured games.
        /// </summary>
        public static string FeaturedGamesUrl
        {
            get { return "/observer-mode/rest/featured"; }
        }

        /// <summary>
        /// Get recent games of Summoner by Summoner ID.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="summonerId"></param>
        /// <returns></returns>
        public static string GetGamesBySummonerIdUrl(Region region, long summonerId)
        {
            return String.Format(GamesBySummonerIdURL, region, summonerId);
        }

        private static string GamesBySummonerIdURL
        {
            get { return "/api/lol/{0}/v1.3/game/by-summoner/{1}/recent"; }
        }

        private static string GetBaseLeagueUrl(Region region)
        {
            return String.Format("/api/lol/{0}/v2.5/league/", region.ToString()); 
        }

        /// <summary>
        /// Get leagues mapped by summoner ID for a given list of summoner IDs.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="summonerIds"></param>
        /// <returns></returns>
        public static string GetLeaguesBySummonerIdsUrl(Region region, long[] summonerIds)
        {
            return GetBaseLeagueUrl(region) + String.Format("by-summoner/", summonerIds.ToString());
        }

        /// <summary>
        /// Get league entries mapped by summoner ID for a given list of summoner IDs.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="summonerIds"></param>
        /// <returns></returns>
        public static string GetLeagueEntriesBySummonerIdsUrl(Region region, long[] summonerIds)
        {
            return GetBaseLeagueUrl(region) + String.Format("by-summoner/", summonerIds.ToString() + "/entry");
        }

    }
}
