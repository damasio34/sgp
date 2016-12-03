
(function (angular) {
    'use strict';

    angular
        .module('sgp.services')
        .factory('TrabalhoService', TrabalhoService);

    TrabalhoService.$inject = ['$http', 'UrlDefault'];
	function TrabalhoService($http, UrlDefault) {
		var _service = function() {
            var self = this;
            this.mainRoute = 'trabalho';
            this.urlBase = UrlDefault.Uri;

            this.getTrabalhoPadrao = function() {
                return $http.get(self.urlBase + self.mainRoute + '/padrao', { headers: self.headers });
            };

            this.getPontosDoDia = function(idTrabalho) {
                return $http.get(self.urlBase + self.mainRoute + '/' + idTrabalho + '/ponto/dodia', { headers: self.headers });
            };

            this.postMarcarPonto = function(idTrabalho) {
                return $http.post(self.urlBase + self.mainRoute + '/' + idTrabalho + '/ponto/marcar', { headers: self.headers });
            };
        };
		return new _service();
	};

})(angular);
