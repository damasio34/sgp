(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$scope', '$timeout', '$filter', 'ClockService'];

    function DashboardController($scope, $timeout, $filter, ClockService) {

        var tickInterval = 1000;
        var tick = function() {
            $scope.clock = $filter('date')(Date.now(), 'dd/MM/yyyy - HH:mm:ss'); // get the current time            
            $timeout(tick, tickInterval); // reset the timer
        }

        // Start the timer
        $timeout(tick, tickInterval);
    };

})(angular);
