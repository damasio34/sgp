(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('ContrachequeController', ContrachequeController);

    ContrachequeController.$inject = ['TrabalhoService'];
    function ContrachequeController(TrabalhoService) {
        var vm = this;

        // -------------------------------------------------------------

        _init();

        // -------------------------------------------------------------------

        function _init() {
            TrabalhoService.getContraCheque().success(function(contraCheque) {
                vm.ContraCheque = contraCheque;
            });
        }
    };

})(angular);
