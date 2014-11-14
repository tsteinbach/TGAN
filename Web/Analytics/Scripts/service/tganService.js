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

	    this.getGameHistoryResultDetails = function (team1, team2) {
	        return $http.get(this.buildSvcUrlWithTwoParameters(team1, team2, "RoundResult", "GetDetail")).then(function (response) {
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
		    return $http.get(this.serviceApiUrl + "/group/Get").then(function (response) {
		        return response.data;
		    });
		};

		this.getRoundsOfActualSeason = function () {
		    return $http.get(this.serviceApiUrl + "/round/Get").then(function (response) {
		        return response.data;
		    });
		};

		this.buildSvcUrl = function(parameter, controller){

		    return this.serviceApiUrl + "/" + controller + "/Get/" + parameter;
		}

		this.buildSvcUrlWithTwoParameters = function (parameter1, parameter2, controller ,methode) {

		    return this.serviceApiUrl + "/" + controller + "/"+ methode +"/" + parameter1 +"/" + parameter2;
		}
	}

	angular.module("tganStatistics").service("tganService", tganService);

})(); 