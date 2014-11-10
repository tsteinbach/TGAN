/**
 * @author Bieberbau
 */


tgan.controller("tganEchteBankController", function tganEchteBankController($scope,tganService) {
   "use strict";

	tganService.getEchteBankData().then(function (userData) {
         $scope.names = userData;
      });
				
});