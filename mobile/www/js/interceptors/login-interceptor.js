(function(angular) {
    'use strict';

    angular
        .module('sgp.interceptors')
        .factory('LoginInterceptor', LoginInterceptor);

    LoginInterceptor.$inject = ['$q', '$rootElement', '$location', 'WebStorageService'];

    function LoginInterceptor($q, $rootElement, $location, WebStorageService) {

        var loginInterceptorFactory = {};
        var appName = $rootElement.attr('ng-app');

        var _request = function(config) {
            config.headers = config.headers || { 'Content-Type': 'application/json' };;
            var token = WebStorageService.getStorage(appName + '_$token');
            if (token) config.headers.Authorization = 'Bearer ' + token;

            return config;
        }
        var _responseError = function(rejection) {
            if (rejection.status === 401) {
                $location.path('/login');
            }
            return $q.reject(rejection);
        }

        loginInterceptorFactory.request = _request;
        loginInterceptorFactory.responseError = _responseError;

        return loginInterceptorFactory;
    };

})(angular);
