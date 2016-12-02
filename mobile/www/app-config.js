(function(angular) {
    'use strict';

    angular.module('sgp')
        .config(function($ionicConfigProvider, $httpProvider) {
            // note that you can also chain configs
            $ionicConfigProvider.backButton.text('').icon('ion-chevron-left');
            $ionicConfigProvider.views.maxCache(0);

            $httpProvider.interceptors.push('LoginInterceptor');
            $httpProvider.interceptors.push('LoaderInterceptor');
        });

})(angular);
