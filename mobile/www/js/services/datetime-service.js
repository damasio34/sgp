(function (angular) {
    'use strict';

    angular
        .module('sgp.services')
        .factory('DateTimeService', DateTimeService);

    DateTimeService.$inject = [];
    function DateTimeService() {
        var self = this;

        var _service = {
            calcularHorasTrabalhadas: _calcularHorasTrabalhadas,
        };

        return _service;

        function _getDateTime(datahora) {
            return datahora ? new Date(datahora) : null;
        };
        function _somaHoras(hora1, hora2) {
            var obj = new Object();

            //  Calcula soma
            //  -------------------------------------------------------------------  //
            obj.hora = hora1.hora + hora2.hora;
            obj.minutos = hora1.minutos + hora2.minutos;
            obj.segundos = hora1.segundos + hora2.segundos;

            if (obj.minutos > 60) {
                var tmpMin = Math.round(obj.minutos / 60);
                obj.minutos = obj.minutos % 60;
                obj.hora += parseInt(tmpMin);
            }

            if (obj.segundos > 60){
                var tmpSec = Math.round(obj.segundos / 60);
                obj.segundos = obj.segundos % 60;
                obj.minutos += parseInt(tmpSec);
            }
            //  -------------------------------------------------------------------  //

            //  Format Duration
            //  -------------------------------------------------------------------  //
            //  Format Hours
            var hourtext = '00';
            if (obj.hora > 0){ hourtext = String(obj.hora);}
            if (hourtext.length == 1){hourtext = '0' + hourtext};

            //  Format Minutes
            var mintext = '00';
            if (obj.minutos > 0){ mintext = String(obj.minutos);}
            if (mintext.length == 1) { mintext = '0' + mintext };

            //  Format Seconds
            var sectext = '00';
            if (obj.segundos > 0) { sectext = String(obj.segundos); }
            if (sectext.length == 1) { sectext = '0' + sectext };

            //  Set Duration
            var sDuration = hourtext + ':' + mintext + ':' + sectext;
            obj.duracao = sDuration;
            //  -------------------------------------------------------------------  //

            // console.log(obj);
            return obj;
        }
        function _calculaDiferencaEntreHoras(menorHora, maiorHora) {
            var obj = new Object();

            //  Calculate diferença
            //  -------------------------------------------------------------------  //
            var nTotalDiff = maiorHora.getTime() - menorHora.getTime();

            obj.days = Math.floor(nTotalDiff / 1000 / 60 / 60 / 24);
            nTotalDiff -= obj.days * 1000 * 60 * 60 * 24;

            obj.hora = Math.floor(nTotalDiff / 1000 / 60 / 60);
            nTotalDiff -= obj.hora * 1000 * 60 * 60;

            obj.minutos = Math.floor(nTotalDiff / 1000 / 60);
            nTotalDiff -= obj.minutos * 1000 * 60;

            obj.segundos = Math.floor(nTotalDiff / 1000);
            //  -------------------------------------------------------------------  //

            //  Format Duration
            //  -------------------------------------------------------------------  //
            //  Format Hours
            var hourtext = '00';
            if (obj.hora > 0){ hourtext = String(obj.hora);}
            if (hourtext.length == 1){hourtext = '0' + hourtext};

            //  Format Minutes
            var mintext = '00';
            if (obj.minutos > 0){ mintext = String(obj.minutos);}
            if (mintext.length == 1) { mintext = '0' + mintext };

            //  Format Seconds
            var sectext = '00';
            if (obj.segundos > 0) { sectext = String(obj.segundos); }
            if (sectext.length == 1) { sectext = '0' + sectext };

            //  Set Duration
            var sDuration = hourtext + ':' + mintext + ':' + sectext;
            obj.duracao = sDuration;
            //  -------------------------------------------------------------------  //

            // console.log(obj);
            return obj;
        }

        function _calcularHorasTrabalhadas(pontosDoDia) {
            if (!pontosDoDia) return;

            var horasTrabalhadas;
            var horarioDeEntrada = _getDateTime(pontosDoDia.HorarioDeEntrada);
            var horarioDeEntradaDoAlmoco = _getDateTime(pontosDoDia.HorarioDeEntradaDoAlmoco);
            var horarioDeSaida = _getDateTime(pontosDoDia.HorarioDeSaida);
            var horarioDeSaidaDoAlmoco = _getDateTime(pontosDoDia.HorarioDeSaidaDoAlmoco);

            if (horarioDeEntrada && (!pontosDoDia.ControlaAlmoco || !horarioDeEntradaDoAlmoco) && !horarioDeSaida)
            {
                horasTrabalhadas = _calculaDiferencaEntreHoras(horarioDeEntrada, _getDateTime(moment().format("YYYY-MM-DDTHH:mm:ss")));
            }
            else if (horarioDeEntrada && pontosDoDia.ControlaAlmoco && horarioDeEntradaDoAlmoco && !horarioDeSaidaDoAlmoco)
            {
                horasTrabalhadas = _calculaDiferencaEntreHoras(horarioDeEntrada, horarioDeEntradaDoAlmoco);
            }
            else if (horarioDeEntrada && pontosDoDia.ControlaAlmoco && horarioDeEntradaDoAlmoco && horarioDeSaidaDoAlmoco && !horarioDeSaida)
            {
                var parte1 = _calculaDiferencaEntreHoras(horarioDeEntrada, horarioDeEntradaDoAlmoco);
                var parte2 = _calculaDiferencaEntreHoras(horarioDeSaidaDoAlmoco, _getDateTime(moment().format("YYYY-MM-DDTHH:mm:ss")));

                horasTrabalhadas = _somaHoras(parte1, parte2);
            }
            else if (horarioDeEntrada && pontosDoDia.ControlaAlmoco && horarioDeEntradaDoAlmoco && horarioDeSaidaDoAlmoco && horarioDeSaida)
            {
                var parte1 = _calculaDiferencaEntreHoras(horarioDeEntrada, horarioDeEntradaDoAlmoco);
                var parte2 = _calculaDiferencaEntreHoras(horarioDeSaidaDoAlmoco, horarioDeSaida);

                horasTrabalhadas = _somaHoras(parte1, parte2);
            }
            else if (horarioDeEntrada && !pontosDoDia.ControlaAlmoco && horarioDeSaida)
            {
                horasTrabalhadas = _calculaDiferencaEntreHoras(horarioDeEntrada, horarioDeSaida);
            }

            return horasTrabalhadas;
        }
    }

})(angular);
