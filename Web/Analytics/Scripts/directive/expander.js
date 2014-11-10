/**
 * @author Bieberbau
 */
tgan.directive("myExpander", function () {
   return {
      restrict: "AEC",
      scope: {
         title: "@" // take the title from HTML attribute
      },
      transclude: true,
      templateUrl: "tganPages/directiveTemplates/myExpanderTemplate.html"
   }
});

