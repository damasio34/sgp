using System;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloPessoa.Factories;
using Damasio34.SGP.Dominio.ModuloPessoa.Interfaces;

namespace Damasio34.SGP.Aplicacao.Services
{
    public class PessoaAppService : IPessoaAppService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PessoaAppService(IPessoaRepository pessoaRepository, IUsuarioRepository usuarioRepository)
        {
            this._pessoaRepository = pessoaRepository;
            this._usuarioRepository = usuarioRepository;
        }

        public void Incluir(PessoaDto pessoaDto)
        {
            try
            {
                var pessoa = PessoaFactory.Criar(pessoaDto.Nome, pessoaDto.Cpf, pessoaDto.Email);
                var usuario = UsuarioFactory.Criar(pessoa, pessoaDto.Login, pessoaDto.Senha);

                _usuarioRepository.Incluir(usuario);
                _pessoaRepository.Incluir(pessoa);
                _pessoaRepository.Commit();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
