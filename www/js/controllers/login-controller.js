(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location'];

    function LoginController($location) {
        var vm = this;
    };

})(angular);
