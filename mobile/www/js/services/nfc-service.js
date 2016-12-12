(function (angular) {
    'use strict';

    angular
        .module('sgp.services')
        .factory('NfcService', NfcService);

    NfcService.$inject = ['$rootScope', '$ionicPlatform'];
	function NfcService($rootScope, $ionicPlatform) {
        var self = this;
        var tag = {};
        this.callback = null;

        $ionicPlatform.ready(function() {
            if (window.cordova) {

                nfc.addNdefListener(function (nfcEvent) {
                    self.callback(nfcEvent);
                    preencheTag(nfcEvent);
                }, function () {
                    // console.log("Listening for NDEF Tags.");
                }, function (reason) {
                    console.log("Error adding NFC Listener " + reason);
                });

            }
        });

        function clearTag() { angular.copy({}, this.tag); }
        function preencheTag() {
            // console.log(JSON.stringify(nfcEvent.tag, null, 4));
            $rootScope.$apply(function(nfcEvent) {
                angular.copy(nfcEvent.tag, tag);
            });
        };
        function setCallback(callback) {
            self.callback = callback;
        };

        return {
            tag: tag,
            clearTag: clearTag,
            setCallback: setCallback
        };
	};

})(angular);
