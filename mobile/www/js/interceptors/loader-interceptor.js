(function(angular) {
    'use strict';

    angular
        .module('sgp.interceptors')
        .factory('LoaderInterceptor', LoaderInterceptor);

    LoaderInterceptor.$inject = ['$rootScope'];

    function LoaderInterceptor($rootScope) {
        var loaderInterceptorFactory = {};
        var _request = function(config) {
            $rootScope.$broadcast('loading:show');
            return config;
        }
        var _response = function(response) {
            $rootScope.$broadcast('loading:hide');
            return response;
        }
        var _responseError = function(rejection) {
            $rootScope.$broadcast('loading:hide');
            return rejection;
        }

        loaderInterceptorFactory.request = _request;
        loaderInterceptorFactory.response = _response;
        loaderInterceptorFactory.responseError = _responseError;

        return loaderInterceptorFactory;
    };

})(angular);
