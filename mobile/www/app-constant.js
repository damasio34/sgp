(function(angular) {
    'use strict';

    angular.module('sgp')
        .constant('UrlDefault', {
            Uri_: 'http://api20161202112557.azurewebsites.net/api/',
            Uri: 'http://localhost:30809/api/'
        })
        .constant('$ionicLoadingConfig', {
            template: '<ion-spinner icon="android"></ion-spinner>',
            animation: 'fade-in',
            showBackdrop: true,
            maxWidth: 200,
            showDelay: 0
        });

})(angular);
