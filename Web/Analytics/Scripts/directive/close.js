/**
 * @author Bieberbau
 */



tgan.directive('myClose', function() {
  return {
      restrict: 'AE',
      replace: 'true',
      transclude: true,
      templateUrl: "tganPages/directiveTemplates/myClose.html"
  };
});
