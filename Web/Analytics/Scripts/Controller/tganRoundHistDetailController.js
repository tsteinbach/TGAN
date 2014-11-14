/**
 * @author Bieberbau
 */


tgan.controller("tganRoundHistDetailController", function tganRoundHistDetailController($scope, tganService) {
   "use strict";

   $scope.showButton = true;

   $scope.loadGameHistoryDetails = function (game) {
       tganService.getGameHistoryResultDetails(game.Team1ID, game.Team2ID).then(function (analyticsData) {
           $scope.showButton = false;
           $scope.gamesDetails = analyticsData;
       });
   }
});