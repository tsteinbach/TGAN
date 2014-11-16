/**
 * @author Bieberbau
 */


tgan.controller("tganTendencyController", function tganTendencyController($scope,tganService) {
   "use strict";

   $scope.loadData = function (userGroup) {
       tganService.getTendenzData(userGroup.ID).then(function (analyticsData) {
           $scope.tganData = analyticsData;
       });
   };

   $scope.tableRowExpanded = false;
   $scope.tableRowIndexCurrExpanded = "";
   $scope.tableRowIndexPrevExpanded = "";
   $scope.tendenyExpanded = "";
   $scope.seasonDataCollapse = [true, true, true, true, true, true];


   $scope.transactionShow = 0;

   $scope.seasonDataCollapseFn = function () {
       for (var i = 0; $scope.tganData.length - 1; i += 1) {
           $scope.seasonDataCollapse.append('true');
       }
   };

   $scope.selectTableRow = function (index, tendency) {
       if ($scope.seasonDataCollapse === 'undefined') {
           $scope.seasonDataCollapse = $scope.seasonDataCollapseFn();
       } else {

           if ($scope.tableRowExpanded === false && $scope.tableRowIndexCurrExpanded === "" && $scope.tendenyExpanded === "") {
               $scope.tableRowIndexPrevExpanded = "";
               $scope.tableRowExpanded = true;
               $scope.tableRowIndexCurrExpanded = index;
               $scope.tendenyExpanded = tendency;
               $scope.seasonDataCollapse[index] = false;
           } else if ($scope.tableRowExpanded === true) {
               if ($scope.tableRowIndexCurrExpanded === index && $scope.tendenyExpanded === tendency) {
                   $scope.tableRowExpanded = false;
                   $scope.tableRowIndexCurrExpanded = "";
                   $scope.tendenyExpanded = "";
                   $scope.seasonDataCollapse[index] = true;
               } else {
                   $scope.tableRowIndexPrevExpanded = $scope.tableRowIndexCurrExpanded;
                   $scope.tableRowIndexCurrExpanded = index;
                   $scope.tendenyExpanded = tendency;
                   $scope.seasonDataCollapse[$scope.tableRowIndexPrevExpanded] = true;
                   $scope.seasonDataCollapse[$scope.tableRowIndexCurrExpanded] = false;
               }
           }
       }
   };
	
				
});