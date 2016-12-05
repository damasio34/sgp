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
            TrabalhoService.getPontosDoDia().success(function(pontosDoDia) {
                vm.Relogio = "00:00:00";
                vm.PontosDoDia = pontosDoDia;
                _exibeHorasTrabalhadas();
            });
        };

        function _calcularHorasTrabalhadas(pontosDoDia) {
            if (!pontosDoDia) return;
            var horasTrabalhadas;
            var almoco;

            var horarioDeEntrada = pontosDoDia.HorarioDeEntrada ? new Date(pontosDoDia.HorarioDeEntrada) : null;
            var horarioDeSaida = pontosDoDia.HorarioDeSaida ? new Date(pontosDoDia.HorarioDeSaida) : null;
            var horarioDeEntradaDoAlmoco = pontosDoDia.HorarioDeEntradaDoAlmoco ? new Date(pontosDoDia.HorarioDeEntradaDoAlmoco) : null;
            var horarioDeSaidaDoAlmoco = pontosDoDia.HorarioDeSaidaDoAlmoco ? new Date(pontosDoDia.HorarioDeSaidaDoAlmoco) : null;

            if (horarioDeEntrada && (!pontosDoDia.ControlaAlmoco || !horarioDeEntradaDoAlmoco) && !horarioDeSaida)
            {
                horasTrabalhadas = new Date() - horarioDeEntrada;
            }
            else if (horarioDeEntrada && pontosDoDia.ControlaAlmoco && horarioDeEntradaDoAlmoco && !horarioDeSaidaDoAlmoco)
            {
                horasTrabalhadas = horarioDeEntradaDoAlmoco - horarioDeEntrada;
            }
            else if (horarioDeEntrada && pontosDoDia.ControlaAlmoco && horarioDeEntradaDoAlmoco && horarioDeSaidaDoAlmoco)
            {
                almoco = horarioDeSaidaDoAlmoco - horarioDeEntradaDoAlmoco;
                horasTrabalhadas = new Date() - horarioDeEntrada - almoco;
            }
            else if ((horarioDeEntrada && pontosDoDia.ControlaAlmoco &&
                horarioDeEntradaDoAlmoco && horarioDeSaidaDoAlmoco && horarioDeSaida) ||
                (horarioDeEntrada && !pontosDoDia.ControlaAlmoco && horarioDeSaida))
            {
                horasTrabalhadas = horarioDeSaida - horarioDeEntrada;
            }

            return horasTrabalhadas;
        }
        function _exibeHorasTrabalhadas() {
            var tickInterval = 1000;
            var tick = function() {
                var horasTrabalhadas = _calcularHorasTrabalhadas(vm.PontosDoDia);
                vm.Relogio = $filter('date')(horasTrabalhadas, 'HH:mm:ss') || "00:00:00"; // get the current time

                if (vm.PontosDoDia.HorarioDeSaida ||
                    (vm.PontosDoDia.HorarioDeEntradaDoAlmoco && !vm.PontosDoDia.HorarioDeSaidaDoAlmoco))
                {
                    $timeout.cancel(tick); // Stop the timer
                    return;
                }
                else $timeout(tick, tickInterval); // restart the timer
            };
            $timeout(tick, tickInterval); // Start the timer
        }

        function MarcarPonto() {
            TrabalhoService.postMarcarPonto().success(function(pontosDoDia) {
                vm.PontosDoDia = pontosDoDia;
                _exibeHorasTrabalhadas();
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
