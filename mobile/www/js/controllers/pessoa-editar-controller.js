(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('PessoaEditarController', PessoaEditarController);

    PessoaEditarController.$inject = ['CryptSha1Service', 'PessoaService'];
    function PessoaEditarController(CryptSha1Service, PessoaService) {
        var vm = this;

        vm.Salvar = Salvar;

        // -------------------------------------------------------------

        _init();

        // -------------------------------------------------------------------

        function _init() {
            PessoaService.getAll().success(function(pessoa) {
                vm.Pessoa = pessoa;
            });
        }

        function Salvar(pessoa) {
            var _pessoa = angular.copy(pessoa, _pessoa);
            _pessoa.Senha = CryptSha1Service.hash(_pessoa.Senha);
            PessoaService.alterar(_pessoa).success(function() {
                $ionicPopup.alert({
                    title: 'Alerta',
                    cssClass: 'custom-popup',
                    content: '<div class="text-center">Perfil atualizados com sucesso.</div>',
                    buttons: [
                        {
                            text: '<b>Ok</b>',
                            type: 'btn-amarelo',
                        },
                    ]
                });
            });
        }
    };

})(angular);
