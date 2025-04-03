using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

/* A classe Funcionario representa um funcionário no sistema de gerenciamento de vales-refeição.
 Ela contém propriedades como Id, Nome, CPF, Situacao e DataAlteracao.
 A classe também implementa o método ToString() para retornar o nome do funcionário como uma representação de string.
 A validação de dados é aplicada nas propriedades Nome e CPF para garantir que os dados sejam inseridos corretamente.*/

namespace GerenciadorValeRefeicoes.Models
{

    
    public class Funcionario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, ErrorMessage = "O CPF deev ter 11 dígitos.")]
        public string CPF { get; set; }

        [RegularExpression("^[AI]$", ErrorMessage = "A situação deve ser 'A' para ativo ou 'I' para inativo.")]
        public char Situacao { get; set; } = 'A'; // Valor padrão 'A' para ativo

        public DateTime DataAlteracao { get; set; } = DateTime.Now; // Valor padrão data/hora atual

        public override string ToString()
        {
            return Nome;
        }

    }
}
