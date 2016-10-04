(function(angular) {
    'use strict'

    angular
        .module('sgp')
        .config(function($stateProvider, $urlRouterProvider) {
            $stateProvider

                .state('login', {
                    url: '/login',
                    templateUrl: 'views/login-view.html',
                    controller: 'LoginController'
                })

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
                });

           $urlRouterProvider.otherwise('/login');

        });

})(angular);
