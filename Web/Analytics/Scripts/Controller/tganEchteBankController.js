/**
 * @author Bieberbau
 */


tgan.controller("tganEchteBankController", function tganEchteBankController($scope,tganService) {
   "use strict";

   $scope.loadData = function (userGroup) {
       tganService.getEchteBankData(userGroup.ID).then(function (analyticsData) {
           $scope.names = analyticsData;
       });
   }
});