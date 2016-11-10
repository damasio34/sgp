(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$scope', '$state', '$http'];

    function LoginController($scope, $state, $http) {
        // var login = this;
        var authUrl = "http://localhost:1151/api/security/token";

        $scope.usuario = { username: 'damasio34', password: '1235' };
        $scope.entrar = entrar;

        function entrar(usuario) {
            var data = "grant_type=password&username=" + usuario.username + "&password=" + usuario.password + "";
            $http.post(authUrl, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function(response) {
                    console.log(response.data.access_token);
                    localStorage.setItem('_token', response.data.access_token);
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + response.data.access_token;
                    $state.go('app.dashboard');
                })
                .catch(function(response) {
                    console.error(response.data.error_description, response.status);
                });
        };
    };

})(angular);
