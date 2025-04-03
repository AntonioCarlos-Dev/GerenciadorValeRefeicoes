using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorValeRefeicoes.DAL.Interfaces;
using GerenciadorValeRefeicoes.Models;
using GerenciadorValeRefeicoes.BLL.Interfaces;


 /* Esta classe implementa a interface IValeRefeicaoServico, que define os métodos para gerenciar as operações relacionadas aos vales-refeição.
  Ela inclui métodos para obter todos os vales-refeição, obter um vale-refeição por ID, adicionar e atualizar um vale-refeição.
  Esses métodos são utilizados para manipular os dados de vale-refeição na aplicação. A classe utiliza um repositório de vale-refeição e um repositório de funcionário
  para realizar as operações necessárias. A implementação dos métodos garante que as regras de negócio sejam seguidas,
  como verificar se o funcionário existe e se está ativo antes de adicionar ou atualizar um vale-refeição.*/

namespace GerenciadorValeRefeicoes.BLL
{
    public class ValeRefeicaoServico : IValeRefeicaoServico
    {
        private readonly IValeRefeicaoRepositorio _valeRefeicaoRepositorio;
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public ValeRefeicaoServico(IValeRefeicaoRepositorio valeRefeicaoRepositorio, IFuncionarioRepositorio funcionarioRepositorio)
        {
            _valeRefeicaoRepositorio = valeRefeicaoRepositorio;
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public List<ValeRefeicao> ObterTodosValeRefeicoes()
        {
            return _valeRefeicaoRepositorio.ObterTodos();
        }

        public ValeRefeicao ObterValeRefeicaoPorId(int id)
        {
            return _valeRefeicaoRepositorio.ObterPorId(id);
        }

        /* Método para adicionar um vale-refeição
         Este método verifica se o funcionário existe e se está ativo antes de adicionar o vale-refeição.
         Se o funcionário não existir ou estiver inativo, uma exceção é lançada.*/
        public void AdicionarValeRefeicao(ValeRefeicao valeRefeicao)
        {
            if (valeRefeicao.FuncionarioId <= 0)
            {
                throw new ArgumentException("O Funcionário é obrigatório.");
            }

            if (valeRefeicao.Quantidade <= 0)
            {
                throw new ArgumentException("A quantidade de vales é obrigatória.");
            }

            var funcionario = _funcionarioRepositorio.ObterPorId(valeRefeicao.FuncionarioId);

            if (funcionario == null)
            {
                throw new ArgumentException("Funcionário não encontrado.");
            }

            if (funcionario.Situacao != 'A')
            {
                throw new ArgumentException("Não é possível adicionar vales-refeição para um funcionário inativo.");
            }
            
            valeRefeicao.DataModificacao = DateTime.Now; // Atualiza a data de modificação 

            _valeRefeicaoRepositorio.Adicionar(valeRefeicao); // Adiciona o vale-refeição ao repositório

        }

        public void AtualizarValeRefeicao(ValeRefeicao valeRefeicao)
        {
            if (valeRefeicao.FuncionarioId <= 0)
            {
                throw new ArgumentException("O Funcionário é obrigatório.");
            }

            if (valeRefeicao.Quantidade <= 0)
            {
                throw new ArgumentException("A quantidade de vales é obrigatória.");
            }

            var funcionario = _funcionarioRepositorio.ObterPorId(valeRefeicao.FuncionarioId);

            if (funcionario == null)
            {
                throw new ArgumentException("Funcionário não encontrado.");
            }

            if (funcionario.Situacao != 'A')
            {
                throw new ArgumentException("Não é possível atualizar vales-refeição para um funcionário inativo.");
            }

            valeRefeicao.DataModificacao = DateTime.Now; // Atualiza a data de modificação
            _valeRefeicaoRepositorio.Atualizar(valeRefeicao); // Atualiza o vale-refeição no repositório


        }





    }
}
