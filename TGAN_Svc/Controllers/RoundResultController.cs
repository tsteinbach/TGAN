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

        // GET api/RoundResult/EFD495F8-DAF3-4FD0-AFAF-3E278327EAD2
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
                        Team1 = game.HomeTeam,
                        Team2 = game.AwayTeam,
                        Draw = history.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Draw),
                        Team1Win = history.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Home),
                        Team2Win = history.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Away),
                        Details = history.Select(x => new AnalyticsResultHistoryDetailDTO
                        {
                            Season = x.Season,
                            ResultFirstHalfSeason = FormatGameResult(x.result)
                        }).ToList()
                    };

                    //Alle Rückspiele
                    var historyViseVersa = analytics.AnalyticsResultHistory(game.TeamID_away, game.TeamID_home).ToList();

                    historyResult.Draw += historyViseVersa.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Draw);
                    historyResult.Team2Win += historyViseVersa.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Home);
                    historyResult.Team1Win += historyViseVersa.Count(x => TippBL.DetermineResultOfGame(x.result) == TippValue.Away);

                    foreach (var item in historyResult.Details)
                    {
                        var seasonItem = historyViseVersa.SingleOrDefault(x => x.Season == item.Season);
                        item.ResultSecondHalfSeason = seasonItem == null ? "" : FormatGameResult(seasonItem.result);
                    }

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