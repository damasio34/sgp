(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('PontoListarController', PontoListarController);

    PontoListarController.$inject = ['TrabalhoService'];
    function PontoListarController(TrabalhoService) {
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
