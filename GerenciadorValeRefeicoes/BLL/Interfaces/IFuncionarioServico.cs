using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorValeRefeicoes.Models;


/* Esta interface define os métodos que serão implementados na classe FuncionarioServico para gerenciar as operações relacionadas aos funcionários.
 Ela inclui métodos para obter todos os funcionários, obter um funcionário por ID, adicionar e atualizar um funcionário.
 Esses métodos são utilizados para manipular os dados dos funcionários na aplicação. A interface é utilizada para garantir que as classes que implementam essa interface
 forneçam implementações para os métodos definidos, permitindo uma separação clara entre a lógica de negócios e a lógica de acesso a dados.*/

namespace GerenciadorValeRefeicoes.BLL.Interfaces
{
    public interface IFuncionarioServico
    {
        List<Funcionario> ObterTodosFuncionarios();
        Funcionario ObterFuncionarioPorId(int id);
        void AdicionarFuncionario(Funcionario funcionario);
        void AtualizarFuncionario(Funcionario funcionario);
    }
}
