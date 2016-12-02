(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$scope', '$timeout', '$filter', '$http', 'ClockService'];

    function DashboardController($scope, $timeout, $filter, $http, ClockService) {

        $scope.MarcarPonto = MarcarPonto;

        GetConfiguracoes();

        // -------------------------------------------------------------

        var tickInterval = 1000;
        var tick = function() {
            $scope.clock = $filter('date')(Date.now(), 'HH:mm:ss'); // get the current time
            $timeout(tick, tickInterval); // reset the timer
        }

        // Start the timer
        $timeout(tick, tickInterval);
        
        DashaboardService.Listar(function(pontos) {
            Pontos = pontos;
        });

        function GetConfiguracoes() {
            console.log(localStorage.getItem('_token'));
            $http.get('http://localhost:1151/api/dashboard', { headers: { Authorization: 'Bearer ' + localStorage.getItem('_token') } })
                .then(function(response) {
                    console.log(response.data.IdTrabalho);
                    localStorage.setItem('_IdTrabalho', response.data.IdTrabalho);
                })
                .catch(function(response) {
                    console.error(response);
                });
        };

        function MarcarPonto() {
            var data = localStorage.getItem('_IdTrabalho');
            console.log(data);
            $http.post('http://localhost:1151/api/dashboard/' + data, null,
                    { headers: { Authorization: 'Bearer ' + localStorage.getItem('_token') }
                })
                .then(function(response) {
                    console.log(response.data);
                })
                .catch(function(response) {
                    console.error(response.data);
                });
        };
    };

})(angular);
