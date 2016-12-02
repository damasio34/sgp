(function (angular) {
    'use strict';

    angular
        .module('sgp.services')
        .factory('LoginService', LoginService);

    LoginService.$inject = ['$q', '$rootElement', '$http', 'WebStorageService', 'UrlDefault'];

    function LoginService($q, $rootElement, $http, WebStorageService, UrlDefault) {
        var self = this;
        var appName = $rootElement.attr('ng-app');
        this.urlBase = UrlDefault.Uri + 'security/token';
        this.headers = { 'Content-Type': 'application/json' };

        var _service = {
            getToken: _getToken,
            setToken: _setToken,
            incluir: incluir,
            login: login,
            logout: logout,
            recuperarSenha: recuperarSenha,
            usuarioAutenticado: usuarioAutenticado
        };

        return _service;

        function _getToken() {
            return WebStorageService.getLocalStorage(appName + '_$token') || WebStorageService.getSessionStorage(appName + '_$token');
        }
        function _setToken(token, temporario) {
            if (temporario) WebStorageService.setLocalStorage(appName + '_$token', token);
            else WebStorageService.setSessionStorage(appName + '_$token', token);
        }

        function incluir(model) {
            var user = {
                username: model.usuario,
                password: CryptSha1Service.hash(model.senha),
                email: model.email
            };

            return $http.post('https://api.parse.com/1/users', user, { headers: _headers })
            .success(function(data, status) {
                if (status == 201 && !!data.sessionToken) _setToken(data.sessionToken, model.salvarSenha)
            }).error(function (data, status) {
                if (status === 400 && status === 202) console.warn('O nome de usuário ' + model.usuario + ' já está cadastrado.');
            });
        }
        function login(model, lembrarSenha) {
            var deferred = $q.defer();
            var data = "grant_type=password&username=" + model.username + "&password=" + model.password;
            var _headers = angular.copy(self.headers, _headers);
            _headers['Content-Type'] = 'application/x-www-form-urlencoded';

            if (usuarioAutenticado()) logout();

            $http.post(self.urlBase, data, { headers:_headers })
            .success(function(data, status) {
                if (status == 200 && !!data.access_token) _setToken(data.access_token, lembrarSenha)
                deferred.resolve(data);
            })
            .error(function (data, status) {
                deferred.reject(data);
            });

            return deferred.promise;
        }
        function logout() {
            var token = _getToken();
            if (token) {
                var _headers = angular.copy(self.headers, _headers);
                _headers['Authorization'] = 'Bearer ' + token;
                $http.post(self.urlBase + '/logout', null, { headers: _headers });
            }

            sessionStorage.clear();
            localStorage.clear();
        };
        function recuperarSenha(model) { return; }
        function usuarioAutenticado() {
            var token = _getToken();
            if (!token || token === null) return false;
            else return true;
        }
    }

})(angular);
