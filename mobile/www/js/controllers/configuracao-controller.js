(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('ConfiguracaoController', ConfiguracaoController);

    ConfiguracaoController.$inject = ['$ionicPopup', 'TrabalhoService', 'NfcService'];
    function ConfiguracaoController($ionicPopup, TrabalhoService, NfcService) {
        var vm = this;

        vm.Salvar = Salvar;

        // -------------------------------------------------------------------

        _init();

        // -------------------------------------------------------------------

        function _init() {
            TrabalhoService.getConfiguracoes().success(function(configuracoes) {
                vm.Configuracoes = configuracoes;
            });

            NfcService.setCallback(_setNfc);
        }

        function _setNfc(nfcEvent) {
            vm.Configuracoes.IdNfc = nfcEvent.tag.id.toString();
        };

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
