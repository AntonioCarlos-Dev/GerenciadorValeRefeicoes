using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorValeRefeicoes.Models;

/* Esta interface define os métodos que devem ser implementados por qualquer repositório de Vale Refeição.
 Ela inclui métodos para obter todos os vales-refeição, obter um vale-refeição por ID, adicionar um novo vale-refeição e atualizar um vale-refeição existente.
 Esses métodos são utilizados para manipular os dados de vale-refeição na aplicação.
 A interface é utilizada para garantir que as classes que implementam essa interface
 forneçam implementações para os métodos definidos, permitindo uma separação clara entre a lógica de negócios e a lógica de acesso a dados.*/


namespace GerenciadorValeRefeicoes.DAL.Interfaces
{
    public interface IValeRefeicaoRepositorio
    { 
        List<ValeRefeicao> ObterTodos();
        ValeRefeicao ObterPorId(int id);
        void Adicionar(ValeRefeicao valeRefeicao);
        void Atualizar(ValeRefeicao valeRefeicao);

    }
}
