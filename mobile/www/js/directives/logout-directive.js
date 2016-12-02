(function(angular) {
    'use strict'

    angular
        .module('sgp.directives')
        .directive('logOut', logout);

        logout.$inject = ['$state', 'LoginService'];
        function logout($state, LoginService) {
            var directive = {
                link: link,
                // restrict: 'EA'
            };
            return directive;

            function link(scope, element, attrs) {
                element.on('click', function() {
                    LoginService.logout();
                    $state.go('login');
                });
            }
        };

})(angular);
