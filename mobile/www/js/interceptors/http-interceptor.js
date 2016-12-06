(function(angular) {
    'use strict';

    angular
        .module('sgp.interceptors')
        .factory('HttpInterceptor', HttpInterceptor);

    HttpInterceptor.$inject = ['$q', '$rootElement', '$rootScope', '$location', 'WebStorageService'];
    function HttpInterceptor($q, $rootElement, $rootScope, $location, WebStorageService) {

        var loginInterceptorFactory = {};
        var appName = $rootElement.attr('ng-app');

        var _request = function(config) {
            $rootScope.$broadcast('loading:show');
            config.headers = config.headers || { 'Content-Type': 'application/json' };;
            var token = WebStorageService.getStorage(appName + '_$token');
            if (token) config.headers.Authorization = 'Bearer ' + token;

            return config;
        };
        var _response = function(response) {
            $rootScope.$broadcast('loading:hide');
            return response;
        };
        var _responseError = function(rejection) {
            if (rejection.status === 401) {
                WebStorageService.clear();
                $location.path('/');
            }
            $rootScope.$broadcast('loading:hide');

            return $q.reject(rejection);
        };

        loginInterceptorFactory.request = _request;
        loginInterceptorFactory.response = _response;
        loginInterceptorFactory.responseError = _responseError;

        return loginInterceptorFactory;
    };

})(angular);
