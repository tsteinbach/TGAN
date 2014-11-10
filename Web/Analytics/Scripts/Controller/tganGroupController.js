/**
 * @author Bieberbau
 */


tgan.controller("tganGroupController", function tganGroupController($scope, tganService) {
   "use strict";

   tganService.getUserGroups().then(function (userData) {
       $scope.groupList = userData;
   });
});