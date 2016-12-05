(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$ionicPopup', '$timeout', '$filter', 'ClockService', 'TrabalhoService'];
    function DashboardController($ionicPopup, $timeout, $filter, ClockService, TrabalhoService) {
        var vm = this;

        vm.MarcarPonto = MarcarPonto;

        _init();

        // -------------------------------------------------------------

        function _init() {
            var tickInterval = 1000;
            var tick = function() {
                vm.Relogio = $filter('date')(Date.now(), 'HH:mm:ss'); // get the current time
                $timeout(tick, tickInterval); // reset the timer
            };

            // Start the timer
            $timeout(tick, tickInterval);

            TrabalhoService.getPontosDoDia().success(function(pontosDoDia) {
                vm.PontosDoDia = pontosDoDia;
            });
        };

        function MarcarPonto() {
            TrabalhoService.postMarcarPonto().success(function(pontosDoDia) {
                vm.PontosDoDia = pontosDoDia;
                $ionicPopup.alert({
                    title: 'Parabéns :)',
                    cssClass: 'custom-popup',
                    content: '<div class="text-center">Ponto marcado com sucesso.</div>',
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
