using System;
using System.Data.Entity;
using System.Linq;
using Damasio34.Seedwork.Domain;
using Damasio34.Seedwork.Extensions;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloPessoa;
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

        private readonly Pessoa _pessoa;

        public PessoaAppService(IAutenticacao autenticacao, IPessoaRepository pessoaRepository, 
            IUsuarioRepository usuarioRepository, ITrabalhoRepository trabalhoRepository)
        {
            this._pessoaRepository = pessoaRepository;
            this._usuarioRepository = usuarioRepository;
            this._trabalhoRepository = trabalhoRepository;

            if (autenticacao.IdUsuario.EhValido())
            {
                this._pessoa = _pessoaRepository.BaseQuery
                    .Include("Usuario")
                    .SingleOrDefault(p => p.Id.Equals(autenticacao.IdUsuario));
                if (_pessoa.IsNull()) throw new Exception("Pessoa não encontrada.");
            }
        }

        private PessoaDto EntidadeToDto(Pessoa pessoa)
        {
            var pessoaDto = new PessoaDto
            {
                Nome = pessoa.Nome,
                Email = pessoa.Email,
                Cpf = pessoa.Cpf,
                Login = pessoa.Usuario.Login,
                Senha = pessoa.Usuario.Senha
            };

            return pessoaDto;
        }
        private void DtoToEntidade(PessoaDto pessoaDto, Pessoa pessoa)
        {
            pessoa.Nome = pessoaDto.Nome;
            pessoa.Email = pessoaDto.Email;
            pessoa.Cpf = pessoaDto.Cpf;
            pessoa.Usuario.Login = pessoaDto.Login;
            pessoa.Usuario.Senha = pessoaDto.Senha;
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

        public PessoaDto Selecionar()
        {
            try
            {
                var pessoaDto = EntidadeToDto(this._pessoa);
                return pessoaDto;
            }
            catch (Exception ex) { throw ex; }
        }
        public PessoaDto Alterar(PessoaDto pessoaDto)
        {
            try
            {
                DtoToEntidade(pessoaDto, _pessoa);
                _pessoaRepository.Alterar(_pessoa);
                _usuarioRepository.Alterar(_pessoa.Usuario);
                _pessoaRepository.Commit();
                return pessoaDto;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
