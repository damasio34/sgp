(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('PontoEditarController', PontoEditarController);

    PontoEditarController.$inject = ['$ionicPopup', '$state', '$filter', '$stateParams', 'TrabalhoService'];
    function PontoEditarController($ionicPopup, $state, $filter, $stateParams, TrabalhoService) {
        var vm = this;
        var idPonto = $stateParams.idPonto;

        vm.Salvar = Salvar;

        // -------------------------------------------------------------

        _init(idPonto);

        // -------------------------------------------------------------------

        function _init(idPonto) {
            TrabalhoService.getPontoById(idPonto).success(function(ponto) {
                ponto.DataHora = new Date($filter('date')(ponto.DataHora, 'MM/dd/yyyy HH:mm'));
                vm.Ponto = ponto;
            });
        }

        function Salvar(pontoForm, ponto) {
            if (pontoForm.$valid) {
                TrabalhoService.putPonto(ponto).success(function() {
                    var alerta = $ionicPopup.alert({
                        title: 'Alerta',
                        cssClass: 'custom-popup',
                        content: '<div class="text-center">Ponto atualizado com sucesso.</div>',
                        buttons: [
                            {
                                text: '<b>Ok</b>',
                                type: 'btn-amarelo',
                            },
                        ]
                    });

                    alerta.then(function(){
                        $state.go('app.pontos');
                    })
                });
            };
        }
    };

})(angular);
