(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$ionicPopup', '$timeout', 'DateTimeService', 'TrabalhoService'];
    function DashboardController($ionicPopup, $timeout, DateTimeService, TrabalhoService) {
        var vm = this;

        vm.MarcarPonto = MarcarPonto;

        // -------------------------------------------------------------

        _init();

        // -------------------------------------------------------------

        function _init() {
            TrabalhoService.getPontosDoDia().success(function(pontosDoDia) {
                vm.Relogio = "00:00:00";
                _exibeHorasTrabalhadas(pontosDoDia);
            });
        };
        //ToDo: Colocar método no DateTimeService
        function _exibeHorasTrabalhadas(pontosDoDia) {
            var tickInterval = 1000;
            vm.PontosDoDia = pontosDoDia;
            if (pontosDoDia.HorarioDeSaida) vm.bloqueiaBotao = true;
            else vm.bloqueiaBotao = false;

            var tick = function() {
                var horasTrabalhadas = DateTimeService.calcularHorasTrabalhadas(pontosDoDia);
                vm.Relogio = horasTrabalhadas.duracao;

                if (!pontosDoDia.HorarioDeEntrada || pontosDoDia.HorarioDeSaida ||
                    (pontosDoDia.HorarioDeEntradaDoAlmoco && !pontosDoDia.HorarioDeSaidaDoAlmoco))
                {
                    $timeout.cancel(tick); // Stop the timer
                    return;
                }
                else $timeout(tick, tickInterval); // restart the timer
            };
            $timeout(tick, tickInterval); // Start the timer
        };

        function MarcarPonto() {
            vm.bloqueiaBotao = true;
            
            TrabalhoService.postMarcarPonto().success(function(pontosDoDia) {
                _exibeHorasTrabalhadas(pontosDoDia);
                $ionicPopup.alert({
                    title: 'Mensagem',
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
