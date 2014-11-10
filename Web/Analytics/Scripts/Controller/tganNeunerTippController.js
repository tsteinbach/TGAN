/**
 * @author Bieberbau
 */


tgan.controller("tganNeunerTippController", function tganNeunerTippController($scope,tganService) {
   "use strict";

   tganService.getUserGroups().then(function (userData) {
       $scope.groupList = userData;
   });

	$scope.loadData = function (x) {
	    tganService.getNeunerTippDataOfGroup(x.ID).then(function (analyticsData) {
	        $scope.names = analyticsData;
	    });
	}
				
});