(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('PontoListarController', PontoListarController);

    PontoListarController.$inject = ['$ionicPopup', 'TrabalhoService'];
    function PontoListarController($ionicPopup, TrabalhoService) {
        var vm = this;

        vm.ExcluirPonto = ExcluirPonto;

        _init();

        // -------------------------------------------------------------------

        function _init() { _getPontos(); };
        function _getPontos() {
            TrabalhoService.getPontos().success(function(pontos) {
                vm.Pontos = pontos;
            });
        }

        function ExcluirPonto(idPonto) {
            TrabalhoService.deletePonto(idPonto).success(function() {
                _getPontos();
                $ionicPopup.alert({
                    title: 'Alerta',
                    cssClass: 'custom-popup',
                    content: '<div class="text-center">Ponto excluido com sucesso.</div>',
                    buttons: [
                        {
                            text: '<b>Ok</b>',
                            type: 'btn-amarelo',
                        },
                    ]
                });
            });
        };
    };

})(angular);
