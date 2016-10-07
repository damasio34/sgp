(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$scope', '$timeout', '$filter', '$http', 'ClockService'];

    function DashboardController($scope, $timeout, $filter, $http, ClockService) {

        $scope.MarcarPonto = MarcarPonto;


        var tickInterval = 1000;
        var tick = function() {
            $scope.clock = $filter('date')(Date.now(), 'dd/MM/yyyy - HH:mm:ss'); // get the current time
            $timeout(tick, tickInterval); // reset the timer
        }

        // Start the timer
        $timeout(tick, tickInterval);

        var data = {
            TipoDoEvento: 0,
            IdTrabalho: '614544E1-1CC8-4887-9B7A-3BBC8F0264F8'
        }

        function MarcarPonto() {
            $http.post('http://localhost:1151/api/ponto', data)
                .then(function(response) {
                    console.log(response.data);
                })
                .catch(function(response) {
                    console.error(response.data);
                });
        };
    };

})(angular);
