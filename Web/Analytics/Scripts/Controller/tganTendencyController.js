/**
 * @author Bieberbau
 */


tgan.controller("tganTendencyController", function tganTendencyController($scope,tganService) {
   "use strict";

	tganService.getTendenzData().then(function (userData) {
         $scope.names = userData;
      });
				
});