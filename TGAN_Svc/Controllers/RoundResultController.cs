using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TGAN_Svc.Models;
using TGAN_Svc.Models.DTO;
using BusinessLayerLogic;
using BusinessLayerLogic.Typemethods;
using DataLayerLogic.Types;


namespace TGAN_Svc.Controllers
{
    public class RoundResultController : ApiController
    {

        // GET api/RoundResult/GetDetail/{id}/{id2}
        public IEnumerable<AnalyticsResultHistoryDetailDTO> GetDetail(string id, string id2)
        {
            var teamHome = new Guid(id);
            var teamAway = new Guid(id2);

            List<AnalyticsResultHistoryDetailDTO> result = new List<AnalyticsResultHistoryDetailDTO>();

            using (var analytics = new Models.TGANAnalyticsEntities())
            {
                // Alle Hinspiele
                var history = analytics.AnalyticsResultHistory(teamHome, teamAway).ToList();

                result = history.Select(x => new AnalyticsResultHistoryDetailDTO
                {
                    Season = x.Season,
                    ResultFirstHalfSeason = FormatGameResult(x.result)
                }).ToList();

                //Alle Rückspiele
                var historyViseVersa = analytics.AnalyticsResultHistory(teamAway, teamHome).ToList();

                foreach (var item in result)
                {
                    var seasonItem = historyViseVersa.SingleOrDefault(x => x.Season == item.Season);
                    item.ResultSecondHalfSeason = seasonItem == null ? "" : FormatGameResult(seasonItem.result);
                }
            }

            return result;
        }

        // GET api/RoundResult/Get/EFD495F8-DAF3-4FD0-AFAF-3E278327EAD2
        public IEnumerable<AnalyticsResultHistoryDTO> Get(string id)
        {
            Guid roundId;
            if (!Guid.TryParse(id, out roundId))
                return new List<AnalyticsResultHistoryDTO>();

            List<AnalyticsResultHistoryDTO> result = new List<AnalyticsResultHistoryDTO>();

            using (var analytics = new Models.TGANAnalyticsEntities())
            {
                var games = analytics.GetGamesWithTeamName(roundId);

                foreach (var game in games)
                {
                    var history = analytics.AnalyticsResultHistory(game.TeamID_home, game.TeamID_away).ToList();

                    AnalyticsResultHistoryDTO historyResult = new AnalyticsResultHistoryDTO
                    {
                        Team1ID = game.TeamID_home.HasValue ? game.TeamID_home.Value : Guid.Empty,
                        Team2ID = game.TeamID_away.HasValue ? game.TeamID_away.Value : Guid.Empty ,
                        Team1 = game.HomeTeam,
                        Team2 = game.AwayTeam,
                        Draw = history.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Draw),
                        Team1Win = history.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Home),
                        Team2Win = history.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Away),
                    };

                    //Alle Rückspiele
                    var historyViseVersa = analytics.AnalyticsResultHistory(game.TeamID_away, game.TeamID_home).ToList();

                    historyResult.Draw += historyViseVersa.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Draw);
                    historyResult.Team2Win += historyViseVersa.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Home);
                    historyResult.Team1Win += historyViseVersa.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Away);

                    result.Add(historyResult);
                }
            }
 
            return result;
        }

        private static string FormatGameResult(string unformatted)
        {
            var result = TippBL.GetFinalResultArray(unformatted);

            return String.Format(@"{0}:{1}", result[0], result[1]);
        }
    }
}