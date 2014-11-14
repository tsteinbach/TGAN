/**
 * @author Bieberbau
 */



tgan.directive('myRoundhistdetail', function() {
  return {
      restrict: 'AE',
      replace: 'true',
      transclude: true,
      controller: 'tganRoundHistDetailController',
      templateUrl: "tganPages/directiveTemplates/myRoundhistdetail.html"
  };
});
