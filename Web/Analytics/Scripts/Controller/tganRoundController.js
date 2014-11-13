/**
 * @author Bieberbau
 */


tgan.controller("tganRoundController", function tganRoundController($scope, tganService) {
   "use strict";


   tganService.getRoundsOfActualSeason().then(function (roundData) {
       $scope.roundList = roundData;
   });
});