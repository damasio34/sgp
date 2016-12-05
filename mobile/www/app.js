(function(angular) {
    'use strict'

    angular.module('sgp', [
        'ionic',
        'angular.filter',
        'ui.utils.masks',

        'sgp.interceptors',
        'sgp.services',
        'sgp.directives',
        'sgp.controllers'
    ]);

})(angular);
