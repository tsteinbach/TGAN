/**
 * @author Bieberbau
 */



tgan.directive('myGroup', function() {
  return {
      restrict: 'AE',
      replace: 'true',
      transclude: true,
      controller: 'tganGroupController',
      templateUrl: "tganPages/directiveTemplates/myGroup.html"
  };
});
