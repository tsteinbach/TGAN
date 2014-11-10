/**
 * @author Bieberbau
 */

(function() {
	"use strict";

	/**
	 * @constructor
	 */
	function tganService($http) {
		this.getTendenzData = function() {
		    return $http.get("Analytics/Scripts/service/data/tendenz.json").then(function (response) {
				return response.data;
			});
		};

		this.getEchteBankData = function() {
		    return $http.get("Analytics/Scripts/service/data/echteBankCount.json").then(function (response) {
				return response.data;
			});
		};
		
		this.getUnechteBankData = function() {
			return $http.get("Analytics/Scripts/service/data/unechteBankCount.json").then(function(response) {
				return response.data;
			});
		};
		
		this.getNeunerTippDataOfGroup = function (groupId) {
		    return $http.get("http://localhost:49512/api/neuner/"+groupId).then(function (response) {
		        return response.data;
		    });
		};

		this.getNeunerTippData = function() {
		    return $http.get("http://localhost:49512/api/neuner/475A6D64-7306-4FDA-8DA6-CDC733BA0A71").then(function (response) {
		        return response.data;
		    });
		};

		this.getUserGroups = function () {
		    return $http.get("http://localhost:49512/api/group").then(function (response) {
		        return response.data;
		    });
		};
	}


	angular.module("tganStatistics").service("tganService", tganService);

})(); 