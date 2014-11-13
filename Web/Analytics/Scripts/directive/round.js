/**
 * @author Bieberbau
 */



tgan.directive('myRound', function() {
  return {
      restrict: 'AE',
      replace: 'true',
      transclude: true,
      controller: 'tganRoundController',
      templateUrl: "tganPages/directiveTemplates/myRound.html"
  };
});
