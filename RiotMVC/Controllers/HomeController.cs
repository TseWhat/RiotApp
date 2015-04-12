using RiotAPI;
using RiotAPI.Entities;
using RiotMVC.Models;
using RiotSharp;
using RiotSharp.MatchEndpoint;
using RiotSharp.SummonerEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RiotMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SummonerModel model = new SummonerModel();
            
            return View(model);
        }

        public JsonResult GetMatchInformation(int hours, int minutes, string date )
        {
            SummonerModel model = new SummonerModel();

            API riotApi = new API();


            DateTime parsedDate = DateTime.Parse(date);
            parsedDate = parsedDate.AddHours(hours);
            parsedDate = parsedDate.AddMinutes(minutes);

            List<long> gameIds = riotApi.GetUrfGames(parsedDate);

            if (gameIds.Count > 0)
            {
                var api = RiotApi.GetInstance(riotApi.KeyOnly);

                List<MatchDetail> matchDetails = new List<MatchDetail>();


                for (int i = 0; i < gameIds.Count; i++)
                {
                    matchDetails.Add(api.GetMatch(Region.euw, gameIds[i]));
                }

                ScoreCardCalculator scoreCalculator = new ScoreCardCalculator();

                model.ChampionScoreCards = scoreCalculator.CalculateChampionScores(matchDetails, riotApi.GetChampions());


            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }    }
}