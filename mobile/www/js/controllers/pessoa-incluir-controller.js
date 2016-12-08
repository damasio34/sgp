(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('PessoaIncluirController', PessoaIncluirController);

    PessoaIncluirController.$inject = ['$state', 'WebStorageService', 'CryptSha1Service', 'PessoaService', 'LoginService'];
    function PessoaIncluirController($state, WebStorageService, CryptSha1Service, PessoaService, LoginService) {
        var vm = this;

        vm.Cadastrar = cadastrar;

        // -------------------------------------------------------------

        _init();

        // -------------------------------------------------------------------

        function _init() { }

        function cadastrar(pessoa) {
            var _pessoa = angular.copy(pessoa, _pessoa);
            _pessoa.Senha = CryptSha1Service.hash(_pessoa.Senha);

            PessoaService.incluir(_pessoa).success(function() {
                LoginService.login(pessoa.Login, pessoa.Senha, false).then(function(result, status) {
                    if (result.access_token) $state.go('app.dashboard');
                });
            });
        }
    };

})(angular);
