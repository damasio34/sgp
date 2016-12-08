(function(angular) {
    'use strict';

    angular.module('sgp')
        .constant('UrlDefault', {
            Uri: 'http://api20161202112557.azurewebsites.net/api/',
            Uri_: 'http://localhost:30809/api/'
        })
        .constant('$ionicLoadingConfig', {
            template: '<ion-spinner icon="android"></ion-spinner>',
            animation: 'fade-in',
            showBackdrop: true,
            maxWidth: 200,
            showDelay: 0
        })
        .constant('TipoDoEvento', {
            Entrada: 1,
            Saida: 2,
            EntradaDoAlmoco: 3,
            SaidaDoAlmoco: 4
        });

})(angular);
