/**
 * @author Bieberbau
 */


tgan.controller("tganUnechteBankController", function tganUnechteBankController($scope,tganService) {
   "use strict";

	tganService.getUnechteBankData().then(function (userData) {
         $scope.names = userData;
      });
				
});