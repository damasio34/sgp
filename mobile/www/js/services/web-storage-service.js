(function (angular) {
	'use strict';

	angular
		.module('sgp.services')
		.factory('WebStorageService', WebStorageService);

	function WebStorageService() {

		var _service = {
			setLocalStorage: setLocalStorage,
			setSessionStorage: setSessionStorage,
			getLocalStorage: getLocalStorage,
			getSessionStorage: getSessionStorage,

			getStorage: getStorage
		};
		return _service;

		function setLocalStorage(key, value){
			if (!!value) localStorage[key] = JSON.stringify(value);
		}
		function getLocalStorage(key){
			if (!!localStorage[key]) return JSON.parse(localStorage[key]);
			else return null;
		}
		function setSessionStorage(key, value){
			if (!!value) sessionStorage[key] = JSON.stringify(value);
		}
		function getSessionStorage(key){
			if (!!sessionStorage[key]) return JSON.parse(sessionStorage[key]);
			else return null;
		}
		function getStorage(key) {
			return getSessionStorage(key) || getLocalStorage(key);
		}
	}

})(angular);
