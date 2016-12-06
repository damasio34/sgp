(function (angular) {
    'use strict';

    angular
        .module('sgp.services')
        .factory('PessoaService', PessoaService);

    PessoaService.$inject = ['RestBaseService'];
	function PessoaService(RestBaseService) {

		var _service = function() { };
		var base = RestBaseService;
		base.setMainRoute('pessoa');
		// Herdando a implementação de RestServiceBase
		_service.prototype = base;

		return new _service();

	};

})(angular);
