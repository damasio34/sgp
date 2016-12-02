(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$timeout', '$filter', 'ClockService', 'TrabalhoService'];
    function DashboardController($timeout, $filter, ClockService, TrabalhoService) {
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

            TrabalhoService.getPontosDoDia(function(pontosDoDia) {
                vm.PontosDoDia = pontosDoDia;
            });

            // console.log(localStorage.getItem('_token'));

            // $http.get('http://localhost:1151/api/dashboard', { headers: { Authorization: 'Bearer ' + localStorage.getItem('_token') } })
            //     .then(function(response) {
            //         console.log(response.data.IdTrabalho);
            //         localStorage.setItem('_IdTrabalho', response.data.IdTrabalho);
            //     })
            //     .catch(function(response) {
            //         console.error(response);
            //     });
        };
        function MarcarPonto() {
            TrabalhoService.postMarcarPonto(function(pontosDoDia) {
                vm.PontosDoDia = pontosDoDia;
            });
            // var data = localStorage.getItem('_IdTrabalho');
            // console.log(data);
            // $http.post('http://localhost:1151/api/dashboard/' + data, null,
            //         { headers: { Authorization: 'Bearer ' + localStorage.getItem('_token') }
            //     })
            //     .then(function(response) {
            //         console.log(response.data);
            //     })
            //     .catch(function(response) {
            //         console.error(response.data);
            //     });
        };
    };

})(angular);
