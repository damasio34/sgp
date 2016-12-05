(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$state', '$ionicPopup', 'WebStorageService', 'LoginService', 'TrabalhoService'];

    function LoginController($state, $ionicPopup, WebStorageService, LoginService, TrabalhoService) {
        var vm = this;

        vm.Usuario = { Login: 'damasio34', Senha: 1235 };
        vm.Login = Login;

        _init();

        // -----------------------------------------------------------------------------

        function _init() {
            if (LoginService.usuarioAutenticado()) {
                $ionicPopup.alert({
                    title: 'Clube Palace Diz!',
                    cssClass: 'custom-popup',
                    content: '<div class="text-center">Você já está logado, para sair faça logout.</div>',
                    buttons: [
                        {
                            text: '<b>Ok</b>',
                            type: 'btn-amarelo',
                        },
                    ]
                });

                $state.go('app.dashboard');
            };
        };
        function Login(usuario) {
            LoginService.login({ username: usuario.Login, password: usuario.Senha }, usuario.LembrarSenha)
                .then(function(result, status) {
                    if (result.access_token) {
                        // ToDo: Colocar toda a lógica dentro do serviço
                        TrabalhoService.setIdTrabalhoPadrao().success(function(idTrabalhoPadrao) {
                            if (usuario.LembrarSenha) WebStorageService.setLocalStorage('IdTrabalhoPadrao', idTrabalhoPadrao);
                            else WebStorageService.setSessionStorage('IdTrabalhoPadrao', idTrabalhoPadrao);

                            $state.go('app.dashboard');
                        });
                    };
                });
        };
    };

})(angular);
