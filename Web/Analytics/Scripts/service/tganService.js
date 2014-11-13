/**
 * @author Bieberbau
 */

(function() {
	"use strict";

    

    /**
	 * @constructor
	 */
	function tganService($http) {

	    this.serviceApiUrl = "http://localhost:49512/api";

	    this.getGameHistoryAggregated = function (roundId) {
	        return $http.get(this.buildSvcUrl(roundId, "RoundResult")).then(function (response) {
	            return response.data;
	        });
	    };

	    this.getTendenzData = function (groupId) {
	        return $http.get(this.buildSvcUrl(groupId,"tendency")).then(function (response) {
				return response.data;
			});
		};

	    this.getEchteBankData = function (groupId) {
		    return $http.get(this.buildSvcUrl(groupId, "echte")).then(function (response) {
				return response.data;
			});
		};
		
	    this.getUnechteBankData = function (groupId) {
		    return $http.get(this.buildSvcUrl(groupId, "unechte")).then(function (response) {
				return response.data;
			});
		};
		
		this.getNeunerTippDataOfGroup = function (groupId) {
		    return $http.get(this.buildSvcUrl(groupId, "neuner")).then(function (response) {
		        return response.data;
		    });
		};

		this.getUserGroups = function () {
		    return $http.get(this.serviceApiUrl + "/group").then(function (response) {
		        return response.data;
		    });
		};

		this.getRoundsOfActualSeason = function () {
		    return $http.get(this.serviceApiUrl + "/round").then(function (response) {
		        return response.data;
		    });
		};

		this.buildSvcUrl = function(parameter, methode){

		    return this.serviceApiUrl + "/" + methode + "/" + parameter;
		}
	}

	angular.module("tganStatistics").service("tganService", tganService);

})(); 