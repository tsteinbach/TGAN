/**
 * @author Bieberbau
 */


tgan.controller("tganResultHistoryController", function tganResultHistoryController($scope, tganService) {
   "use strict";

   $scope.loadData = function (roundId) {
       tganService.getGameHistoryAggregated(roundId).then(function (analyticsData) {
           $scope.games = analyticsData;
       });
   }
});