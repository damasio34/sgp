(function(angular) {
  'use strict'

  angular
    .module('sgp.services')
    .factory('ClockService', function($interval) {

        var service = {
            clock: addClock,
            cancelClock: removeClock
        };

        var clockElts = [];
        var clockTimer = null;
        var cpt = 0;

        function addClock(fn) {
            var elt = {
                id: cpt++,
                fn: fn
            };
            clockElts.push(elt);
            if (clockElts.length === 1) {
                startClock();
            }
            return elt.id;
        };

        function removeClock(id) {
            for (var i in clockElts) {
                if (clockElts[i].id === id) {
                    clockElts.splice(i, 1);
                }
            }
            if (clockElts.length === 0) {
                stopClock();
            }
        };

        function startClock() {
            if (clockTimer === null) {
                clockTimer = $interval(function() {
                    for (var i in clockElts) {
                        clockElts[i].fn();
                    }
                }, 1000);
            }
        }

        function stopClock() {
            if (clockTimer !== null) {
                $interval.cancel(clockTimer);
                clockTimer = null;
            }
        }

        return service;
    });

})(angular);
