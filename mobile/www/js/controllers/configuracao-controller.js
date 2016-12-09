(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('ConfiguracaoController', ConfiguracaoController);

    ConfiguracaoController.$inject = ['TrabalhoService'];
    function ConfiguracaoController(TrabalhoService) {
        var vm = this;

        vm.Salvar = Salvar;

        // -------------------------------------------------------------------

        _init();

        // -------------------------------------------------------------------

        function _init() {
            TrabalhoService.getConfiguracao().success(function(configuracao) {
                vm.Configuracao = configuracao;
            });
        }

        function Salvar() {
            
        }
    };

})(angular);
