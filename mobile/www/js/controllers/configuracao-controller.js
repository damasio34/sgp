(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('ConfiguracaoController', ConfiguracaoController);

    ConfiguracaoController.$inject = ['$ionicPopup', 'TrabalhoService'];
    function ConfiguracaoController($ionicPopup, TrabalhoService) {
        var vm = this;

        vm.Salvar = Salvar;

        // -------------------------------------------------------------------

        _init();

        // -------------------------------------------------------------------

        function _init() {
            TrabalhoService.getConfiguracoes().success(function(configuracoes) {
                vm.Configuracoes = configuracoes;
            });
        }

        function Salvar(configuracoes) {
            TrabalhoService.putConfiguracoes(configuracoes).success(function() {
                $ionicPopup.alert({
                    title: 'Alerta',
                    cssClass: 'custom-popup',
                    content: '<div class="text-center">Configurações atualizadas com sucesso.</div>',
                    buttons: [
                        {
                            text: '<b>Ok</b>',
                            type: 'btn-amarelo',
                        },
                    ]
                });
            });
        }
    };

})(angular);
