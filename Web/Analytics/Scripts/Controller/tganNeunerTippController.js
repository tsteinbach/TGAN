/**
 * @author Bieberbau
 */


tgan.controller("tganNeunerTippController", function tganNeunerTippController($scope,tganService) {
   "use strict";

	$scope.loadData = function (userGroup) {
	    tganService.getNeunerTippDataOfGroup(userGroup.ID).then(function (analyticsData) {
	        $scope.names = analyticsData;
	    });
	}
				
});