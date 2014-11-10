/**
 * @author Bieberbau
 */


tgan.controller("tganUnechteBankController", function tganUnechteBankController($scope,tganService) {
   "use strict";

   $scope.loadData = function (userGroup) {
       tganService.getUnechteBankData(userGroup.ID).then(function (analyticsData) {
           $scope.names = analyticsData;
       });
   }
});