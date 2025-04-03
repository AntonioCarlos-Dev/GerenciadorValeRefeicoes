using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorValeRefeicoes.BLL.Interfaces;
using GerenciadorValeRefeicoes.DAL.Interfaces; 
using GerenciadorValeRefeicoes.Models;

/* Repositório de funcionários
 Este repositório é responsável por gerenciar as operações relacionadas aos funcionários
 como adicionar, atualizar e obter informações dos funcionários.
 O repositório utiliza a interface IFuncionarioRepositorio para definir as operações  
 que podem ser realizadas com os funcionários.*/
namespace GerenciadorValeRefeicoes.BLL
{
    public class FuncionarioServico : IFuncionarioServico // Implementa a interface IFuncionarioServico
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        public FuncionarioServico(IFuncionarioRepositorio funcionarioRepositorio)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public List<Funcionario> ObterTodosFuncionarios()
        {
            return _funcionarioRepositorio.ObterTodos();
        }
        public Funcionario ObterFuncionarioPorId(int id)
        {
            return _funcionarioRepositorio.ObterPorId(id);
        }

        // Adiciona um novo funcionário e verifica se o CPF já existe e se é válido
        public void AdicionarFuncionario(Funcionario funcionario)
        {
            if (String.IsNullOrEmpty(funcionario.Nome))
            {
                throw new ArgumentException("O nome é obrigatório.");
            }

            if (String.IsNullOrEmpty(funcionario.CPF))
            {
                throw new ArgumentException("O CPF é obrigatório.");
            }

            if (!ValidarCPF(funcionario.CPF))
            {
                throw new ArgumentException("O CPF deve conter 11 dígitos e ser composto apenas por números.");
            }

            funcionario.DataAlteracao = DateTime.Now;
            _funcionarioRepositorio.Adicionar(funcionario);

        }
        // Atualiza um funcionário existente e verifica se o CPF já existe e se é válido
        public void AtualizarFuncionario(Funcionario funcionario)
        {
            if (String.IsNullOrEmpty(funcionario.Nome))
            {
                throw new ArgumentException("O nome é obrigatório.");
            }

            if (String.IsNullOrEmpty(funcionario.CPF))
            {
                throw new ArgumentException("O CPF é obrigatório.");
            }

            if (!ValidarCPF(funcionario.CPF))
            {
                throw new ArgumentException("O CPF deve conter 11 dígitos e ser composto apenas por números.");
            }

            if (CPFjaExiste(funcionario.CPF))
            {
                throw new ArgumentException("O CPF já existe.");
            }

            funcionario.DataAlteracao = DateTime.Now;
            _funcionarioRepositorio.Atualizar(funcionario);
        }

        // Verifica se o CPF já existe
        private bool CPFjaExiste(String cpf)
        {
            return _funcionarioRepositorio.ObterTodos().Any(f => f.CPF == cpf);
        }

        // Verifica se o CPF é válido
        // O CPF deve ter 11 dígitos e ser composto apenas por números
        // Se o CPF não for válido, lança uma exceção
        private bool ValidarCPF(String cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
            {
                return false;
            }

            return cpf.All(char.IsDigit);
        }

    }
}
