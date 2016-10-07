(function(angular) {
    'use strict'

    angular
        .module('sgp')
        .config(function($stateProvider, $urlRouterProvider) {
            $stateProvider

                .state('login', {
                    url: '/login',
                    templateUrl: 'views/login-view.html',
                    controller: 'LoginController',
                    // controllerAs: 'login'
                })

                // --------------------------------------------

                .state('app', {
                    url: '/app',
                    abstract: true,
                    templateUrl: 'views/app-view.html',
                    controller: 'AppController'
                })

                .state('app.dashboard', {
                    url: '/dashboard',
                    views: {
                        'MainContent': {
                            templateUrl: 'views/dashboard-view.html',
                            controller: 'DashboardController'
                        }
                    }
                })

                .state('app.configuracao', {
                    url: '/configuracao',
                    views: {
                        'MainContent': {
                            templateUrl: 'views/configuracao-view.html',
                            controller: 'ConfiguracaoController'
                        }
                    }
                })

                ;

           $urlRouterProvider.otherwise('/login');

        });

})(angular);
