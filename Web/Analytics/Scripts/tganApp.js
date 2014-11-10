/**
 * @author Bieberbau
 */
var tgan = angular.module("tganStatistics", ['ngRoute']);

// configure our routes
	tgan.config(function($routeProvider,$locationProvider) {
		$routeProvider

			// route for the start page
			.when('/', {
				templateUrl : 'tganPages/start.html',
				controller  : 'tganStartController'
			})
			
			// route for the tendency page
			.when('/tendency', {
				templateUrl : 'tganPages/tendency.html',
				controller  : 'tganTendencyController'
			})
			
			// route for the unechte Bank page
			.when('/unechteBank', {
				templateUrl : 'tganPages/unechteBank.html',
				controller  : 'tganUnechteBankController'
			})
			
			// route for the neuner Tipp page
			.when('/neunerTipp', {
				templateUrl : 'tganPages/neunerTipp.html',
				controller  : 'tganNeunerTippController'
			})
			
			// route for the echte Bank page
			.when('/echteBank', {
				templateUrl : 'tganPages/echteBank.html',
				controller  : 'tganEchteBankController'
			});
			
	// use the HTML5 History API
		$locationProvider.html5Mode(true);
			
	});