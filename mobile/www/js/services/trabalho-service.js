
(function (angular) {
    'use strict';

    angular
        .module('sgp.services')
        .factory('TrabalhoService', TrabalhoService);

    TrabalhoService.$inject = ['$http', 'UrlDefault', 'WebStorageService'];
	function TrabalhoService($http, UrlDefault, WebStorageService) {
		var _service = function() {
            var self = this;
            this.mainRoute = 'trabalho';
            this.urlBase = UrlDefault.Uri;

            this.setIdTrabalhoPadrao = function() {
                return $http.get(self.urlBase + self.mainRoute + '/padrao', { headers: self.headers });
            };

            this.getPontos = function() {
                var idTrabalho = WebStorageService.getStorage('IdTrabalhoPadrao');
                return $http.get(self.urlBase + self.mainRoute + '/' + idTrabalho + '/ponto', { headers: self.headers });
            };
            this.getPontosDoDia = function() {
                var idTrabalho = WebStorageService.getStorage('IdTrabalhoPadrao');
                return $http.get(self.urlBase + self.mainRoute + '/' + idTrabalho + '/ponto/dodia', { headers: self.headers });
            };
            this.postMarcarPonto = function() {
                var idTrabalho = WebStorageService.getStorage('IdTrabalhoPadrao');
                return $http.post(self.urlBase + self.mainRoute + '/' + idTrabalho + '/ponto/marcar', { headers: self.headers });
            };
        };
		return new _service();
	};

})(angular);
