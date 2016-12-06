(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('PessoaIncluirController', PessoaIncluirController);

    PessoaIncluirController.$inject = ['$state', 'WebStorageService', 'CryptSha1Service', 'PessoaService', 'LoginService', 'TrabalhoService'];
    function PessoaIncluirController($state, WebStorageService, CryptSha1Service, PessoaService, LoginService, TrabalhoService) {
        var vm = this;

        vm.Cadastrar = cadastrar;

        _init();

        // -------------------------------------------------------------------

        function _init() { }

        function cadastrar(pessoa) {
            var _pessoa = angular.copy(pessoa, _pessoa);
            _pessoa.Senha = CryptSha1Service.hash(_pessoa.Senha);

            PessoaService.incluir(_pessoa).success(function() {
                LoginService.login(pessoa.Login, pessoa.Senha, false).then(function(result, status) {
                    if (result.access_token) {
                        // ToDo: Colocar toda a lógica dentro do serviço
                        TrabalhoService.setIdTrabalhoPadrao().success(function(idTrabalhoPadrao) {
                            WebStorageService.setSessionStorage('IdTrabalhoPadrao', idTrabalhoPadrao);
                            $state.go('app.dashboard');
                        });
                    };
                });
            });
        }

        // model.Senha = CryptSha1Service.hash(model.senha);
    };

})(angular);
