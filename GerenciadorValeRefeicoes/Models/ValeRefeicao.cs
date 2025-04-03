using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* A Classe ValeRefeicao representa um vale-refeição associado a um funcionário.
 Ela contém propriedades como Id, FuncionarioId, Quantidade, Situacao e DataModificacao.
 A propriedade FuncionarioId é uma chave estrangeira que referencia o funcionário associado ao vale-refeição.
 A classe também inclui validações para garantir que os dados sejam inseridos corretamente.*/

namespace GerenciadorValeRefeicoes.Models
{
    public class ValeRefeicao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O funcionário é obtigatório.")]
        public int FuncionarioId { get; set; } // Chave estrangeira para o funcionário
        public Funcionario Funcionario { get; set; } // Propriedade de navegação

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [RegularExpression("^[AI]$", ErrorMessage = "A situação deve ser 'A' para ativo ou 'I' para inativo.")]
        public char Situacao { get; set; } = 'A'; // Valor padrão 'A' para ativo

        public DateTime DataModificacao { get; set; } = DateTime.Now; // Valor padrão data/hora atual
    }
}
