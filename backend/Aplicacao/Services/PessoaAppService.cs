using System;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloPessoa.Factories;
using Damasio34.SGP.Dominio.ModuloPessoa.Interfaces;
using Damasio34.SGP.Dominio.ModuloTrabalho.Factories;
using Damasio34.SGP.Dominio.ModuloTrabalho.Interfaces;

namespace Damasio34.SGP.Aplicacao.Services
{
    public class PessoaAppService : IPessoaAppService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITrabalhoRepository _trabalhoRepository;

        public PessoaAppService(IPessoaRepository pessoaRepository, 
            IUsuarioRepository usuarioRepository, ITrabalhoRepository trabalhoRepository)
        {
            this._pessoaRepository = pessoaRepository;
            this._usuarioRepository = usuarioRepository;
            this._trabalhoRepository = trabalhoRepository;
        }

        public void Incluir(PessoaDto pessoaDto)
        {
            try
            {
                var pessoa = PessoaFactory.Criar(pessoaDto.Nome, pessoaDto.Cpf, pessoaDto.Email);
                var usuario = UsuarioFactory.Criar(pessoa, pessoaDto.Login, pessoaDto.Senha);
                var trabalho = TrabalhoFactory.Criar(pessoa);

                this._usuarioRepository.Incluir(usuario);
                this._pessoaRepository.Incluir(pessoa);
                this._trabalhoRepository.Incluir(trabalho);
                _pessoaRepository.Commit();
            }
            catch (Exception ex)
            {               
                throw ex;
            }
        }
    }
}
