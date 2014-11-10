/**
 * @author Bieberbau
 */


tgan.controller("tganTendencyController", function tganTendencyController($scope,tganService) {
   "use strict";

   $scope.loadData = function (userGroup) {
       tganService.getTendenzData(userGroup.ID).then(function (analyticsData) {
           $scope.names = analyticsData;
       });
   }

	//tganService.getTendenzData().then(function (userData) {
    //     $scope.names = userData;
    //  });
				
});