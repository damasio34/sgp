
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

            this.getConfiguracoes = function() {
                return $http.get(self.urlBase + self.mainRoute + '/configuracoes', { headers: self.headers });
            };
            this.getPontos = function() {
                return $http.get(self.urlBase + self.mainRoute + '/ponto', { headers: self.headers });
            };
            this.getPontosDoDia = function() {
                return $http.get(self.urlBase + self.mainRoute + '/ponto/dodia', { headers: self.headers });
            };
            this.postMarcarPonto = function() {
                return $http.post(self.urlBase + self.mainRoute + '/ponto/marcar', { headers: self.headers });
            };
            this.putConfiguracoes = function(model) {
                return $http.put(self.urlBase + self.mainRoute + '/configuracoes', model, { headers: self.headers });
            };
            this.deletePonto = function(idPonto) {
                return $http.delete(self.urlBase + self.mainRoute + '/ponto/' + idPonto, { headers: self.headers });
            };
        };
		return new _service();
	};

})(angular);
