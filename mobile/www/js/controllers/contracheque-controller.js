(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('ContrachequeController', ContrachequeController);

    ContrachequeController.$inject = ['TrabalhoService'];
    function ContrachequeController(TrabalhoService) {
        var vm = this;

        _init();

        // -------------------------------------------------------------------

        function _init() {
            TrabalhoService.getPontos().success(function(pontos) {
                vm.Pontos = pontos;
            });
        }
    };

})(angular);
