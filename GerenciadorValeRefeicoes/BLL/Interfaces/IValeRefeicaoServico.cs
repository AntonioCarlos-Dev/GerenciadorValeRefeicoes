using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorValeRefeicoes.Models;

 /*Esta interface define os métodos que devem ser implementados pelo serviço de gerenciamento de vale-refeição. 
 Ela inclui métodos para obter todos os vales-refeição, obter um vale-refeição por ID, adicionar, atualizar e remover um vale-refeição.   
 Esses métodos são utilizados para manipular os dados de vale-refeição na aplicação.
 A interface é utilizada para garantir que as classes que implementam essa interface
 forneçam implementações para os métodos definidos, permitindo uma separação clara entre a lógica de negócios e a lógica de acesso a dados.*/

namespace GerenciadorValeRefeicoes.BLL.Interfaces
{
    public interface IValeRefeicaoServico
    {
        List<ValeRefeicao> ObterTodosValeRefeicoes();
        ValeRefeicao ObterValeRefeicaoPorId(int id);
        void AdicionarValeRefeicao(ValeRefeicao valeRefeicao);
        void AtualizarValeRefeicao(ValeRefeicao valeRefeicao);
        
    }
}
