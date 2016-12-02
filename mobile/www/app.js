(function(angular) {
    'use strict'

    angular.module('sgp', [
        'ionic',
        'ui.utils.masks',

        'sgp.interceptors',
        'sgp.services',
        'sgp.controllers'
    ]);

})(angular);
