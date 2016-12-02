(function(angular) {
    'use strict';

    angular
        .module('sgp')
        .config(function($stateProvider, $urlRouterProvider) {
            $stateProvider

                .state('login', {
                    url: '/',
                    templateUrl: 'views/login-view.html',
                    controller: 'LoginController as vm'
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
                            controller: 'DashboardController as vm'
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
                });

           $urlRouterProvider.otherwise('/app/dashboard');

        });

})(angular);
