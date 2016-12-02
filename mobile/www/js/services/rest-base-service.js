(function (angular) {
    'use strict';

    angular
        .module('sgp.services')
        .service('RestBaseService', RestBaseService);

    RestBaseService.$inject = ['$http', 'WebStorageService', 'LoginService', 'UrlDefault'];

    function RestBaseService($http, WebStorageService, LoginService, UrlDefault) {
        let self = this;
        this.mainRoute = undefined;
        this.urlBase = UrlDefault.Uri;
        this.headers = { 'Content-Type': 'application/json' };

        var _service = {
            editar: editar,
            excluir: excluir,
            getAll: getAll,
            getById: getById,
            incluir: incluir,
            setMainRoute: setMainRoute
        };

        return _service;

        // Implementação
        function setMainRoute(mainRoute) {
            self.mainRoute = mainRoute;
        }

        function getAll() {
            if (!self.mainRoute) throw "mainRoute não configurada.";
            // self.headers['Authorization'] = 'bearer ' + LoginService.getToken();
            return $http.get(self.urlBase + self.mainRoute, { headers: self.headers });
        }

        function getById(id) {
            if (!id) throw "id não informado";
            if (!self.mainRoute) throw "mainRoute não configurada.";
            return $http.get(self.urlBase + self.mainRoute + '/' + id, { headers: self.headers });
        }

        function incluir(model) {
            if (!self.mainRoute) throw "mainRoute não configurada.";
            return $http.post(self.urlBase + self.mainRoute, model, { headers: self.headers });
        }

        function editar(model) {
            if (!self.mainRoute) throw "mainRoute não configurada.";
            return $http.put(self.urlBase + self.mainRoute, model, { headers: self.headers });
        }

        function excluir(id) {
            if (!self.mainRoute) throw "mainRoute não configurada.";
            return $http.delete(self.urlBase + self.mainRoute + '/' + id, { headers: self.headers });
        }
    }

})(angular);
