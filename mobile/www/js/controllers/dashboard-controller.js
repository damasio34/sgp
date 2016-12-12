(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$ionicPopup', '$timeout', 'DateTimeService', 'TrabalhoService', 'NfcService'];
    function DashboardController($ionicPopup, $timeout, DateTimeService, TrabalhoService, NfcService) {
        var vm = this;

        vm.MarcarPonto = MarcarPonto;
        // vm.tag = NfcService.tag;

        // -------------------------------------------------------------

        _init();

        // -------------------------------------------------------------

        function _init() {
            // vm.PontosDoDia = {};
            var datahora = new Date(moment().format("YYYY-MM-DDTHH:mm:ss"));
            TrabalhoService.getPontosDoDia(datahora).success(function(pontosDoDia) {
                vm.Relogio = "00:00:00";
                _exibeHorasTrabalhadas(pontosDoDia);
            });

            NfcService.setCallback(MarcarPontoPorNFC);
        };
        //ToDo: Colocar método no DateTimeService
        function _exibeHorasTrabalhadas(pontosDoDia) {
            if (!pontosDoDia.HorarioDeEntrada) return;

            var tickInterval = 1000;
            vm.PontosDoDia = pontosDoDia;
            if (pontosDoDia.HorarioDeSaida) vm.bloqueiaBotao = true;
            else vm.bloqueiaBotao = false;

            var tick = function() {
                var horasTrabalhadas = DateTimeService.calcularHorasTrabalhadas(vm.PontosDoDia);
                vm.Relogio = horasTrabalhadas.duracao;

                if (!vm.PontosDoDia.HorarioDeEntrada || vm.PontosDoDia.HorarioDeSaida ||
                    (vm.PontosDoDia.HorarioDeEntradaDoAlmoco && !vm.PontosDoDia.HorarioDeSaidaDoAlmoco))
                {
                    $timeout.cancel(tick); // Stop the timer
                    return;
                }
                else $timeout(tick, tickInterval); // restart the timer
            };
            $timeout(tick, tickInterval); // Start the timer
        };

        function MarcarPontoPorNFC(nfcEvent) {
            if (vm.PontosDoDia.IdNfc == nfcEvent.tag.id.toString()) {
                MarcarPonto();
            }
            else {
                $ionicPopup.alert({
                    title: 'Mensagem',
                    cssClass: 'custom-popup',
                    content: '<div class="text-center">NFC não cadastrado.</div>',
                    buttons: [
                        {
                            text: '<b>Ok</b>',
                            type: 'btn-amarelo',
                        },
                    ]
                });
            }
        }

        function MarcarPonto() {
            vm.bloqueiaBotao = true;

            var datahora = new Date(moment().format("YYYY-MM-DDTHH:mm:ss"));
            TrabalhoService.postMarcarPonto(datahora).success(function() {
                TrabalhoService.getPontosDoDia(datahora).success(function(pontosDoDia) {
                    // vm.PontosDoDia = {};
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
            });
        };
    };

})(angular);
