
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


            this.getConfiguracao = function() {
                var idTrabalho = WebStorageService.getStorage('IdTrabalhoPadrao');
                return $http.get(self.urlBase + self.mainRoute + '/' + idTrabalho + '/configuracao', { headers: self.headers });
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

            this.putConfiguracao = function(model) {
                var idTrabalho = WebStorageService.getStorage('IdTrabalhoPadrao');
                return $http.put(self.urlBase + self.mainRoute + '/' + idTrabalho + '/configuracao', model, { headers: self.headers });
            };

            this.deletePonto = function(idPonto) {
                var idTrabalho = WebStorageService.getStorage('IdTrabalhoPadrao');
                return $http.delete(self.urlBase + self.mainRoute + '/' + idTrabalho + '/ponto/' + idPonto, { headers: self.headers });
            };
        };
		return new _service();
	};

})(angular);
