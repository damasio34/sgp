(function(angular) {
    'use strict'

    angular
        .module('sgp.controllers')
        .controller('AppController', AppController);

    AppController.$inject = ['$ionicModal', '$ionicPopover', '$timeout', 'LoginService'];
    function AppController($ionicModal, $ionicPopover, $timeout, LoginService) {
        var vm = this;

        vm.Logout = Logout;

        // -------------------------------------------------------------

        // Form data for the login modal
        vm.loginData = {};
        vm.isExpanded = false;
        vm.hasHeaderFabLeft = false;
        vm.hasHeaderFabRight = false;

        vm.hideNavBar = hideNavBar;
        vm.showNavBar = showNavBar;
        vm.noHeader = noHeader;
        vm.setExpanded = setExpanded;
        vm.setHeaderFab = setHeaderFab;
        vm.hasHeader = hasHeader;
        vm.hideHeader = hideHeader;
        vm.showHeader = showHeader;
        vm.clearFabs = clearFabs;

        _init();

        // -------------------------------------------------------------

        function _init() {
            var navIcons = document.getElementsByClassName('ion-navicon');
            for (var i = 0; i < navIcons.length; i++) {
                navIcons.addEventListener('click', function() {
                    this.classList.toggle('active');
                });
            }
        }

        ////////////////////////////////////////
        // Layout Methods
        ////////////////////////////////////////

        function Logout() {
            LoginService.logout();
            $state.go('login');
        };

        function hideNavBar() {
            document.getElementsByTagName('ion-nav-bar')[0].style.display = 'none';
        };
        function showNavBar() {
            document.getElementsByTagName('ion-nav-bar')[0].style.display = 'block';
        };
        function noHeader() {
            var content = document.getElementsByTagName('ion-content');
            for (var i = 0; i < content.length; i++) {
                if (content[i].classList.contains('has-header')) {
                    content[i].classList.toggle('has-header');
                }
            }
        };
        function setExpanded(bool) {
            vm.isExpanded = bool;
        };
        function setHeaderFab(location) {
            var hasHeaderFabLeft = false;
            var hasHeaderFabRight = false;

            switch (location) {
                case 'left':
                    hasHeaderFabLeft = true;
                    break;
                case 'right':
                    hasHeaderFabRight = true;
                    break;
            }

            vm.hasHeaderFabLeft = hasHeaderFabLeft;
            vm.hasHeaderFabRight = hasHeaderFabRight;
        };
        function hasHeader() {
            var content = document.getElementsByTagName('ion-content');
            for (var i = 0; i < content.length; i++) {
                if (!content[i].classList.contains('has-header')) {
                    content[i].classList.toggle('has-header');
                }
            }

        };
        function hideHeader() {
            vm.hideNavBar();
            vm.noHeader();
        };
        function showHeader() {
            vm.showNavBar();
            vm.hasHeader();
        };
        function clearFabs() {
            var fabs = document.getElementsByClassName('button-fab');
            if (fabs.length && fabs.length > 1) {
                fabs[0].remove();
            }
        };
    };

})(angular);
