(function (angular) {
    'use strict';

    angular
        .module('sgp.services')
        .factory('DashboardService', DashboardService);

    DashboardService.$inject = ['$http', 'UrlDefault'];
	function DashboardService($http, UrlDefault) {
		var _service = function() {
            var self = this;
            this.mainRoute = 'dashboard';
            this.urlBase = UrlDefault.Uri;
            this.headers = { 'Content-Type': 'application/json' };

            this.getAll = function() {
                return $http.get(self.urlBase + self.mainRoute, { headers: self.headers });
            };
        };
		return new _service();
	};

})(angular);
