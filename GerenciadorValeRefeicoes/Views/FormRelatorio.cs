using GerenciadorValeRefeicoes.BLL.Interfaces;
using GerenciadorValeRefeicoes.DAL.Implementacoes;
using GerenciadorValeRefeicoes.DAL.Interfaces;
using GerenciadorValeRefeicoes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

 /* Esta classe representa o formulário de relatório, onde o usuário pode visualizar e filtrar os vales-refeição.
  * ela contém campos para selecionar o funcionário, data inicial e data final para filtrar os vales-refeição.
  * a classe utiliza os serviços IValeRefeicaoServico e IFuncionarioServico para realizar operações relacionadas a vale-refeição e funcionários.
  * ela também contém métodos para carregar o relatório com base nos filtros selecionados e exibir os dados em um DataGridView.
  * a classe é responsável por gerenciar a interação do usuário com o formulário de relatório.*/

namespace GerenciadorValeRefeicoes.Views
{
    public partial class FormRelatorio : Form
    {
        private readonly IFuncionarioServico _funcionarioServico;
        private readonly IValeRefeicaoServico _valeRefeicaoServico;
        public FormRelatorio(IFuncionarioServico funcionarioServico, IValeRefeicaoServico valeRefeicaoServico)
        {
            InitializeComponent();
            _funcionarioServico = funcionarioServico;
            _valeRefeicaoServico = valeRefeicaoServico;
        }

        private void FormRelatorio_Load(object sender, EventArgs e)
        {
            cboFuncionario.DataSource = _funcionarioServico.ObterTodosFuncionarios();
            cboFuncionario.DisplayMember = "Nome";
            cboFuncionario.ValueMember = "Id";

            CarregarRelatorio();


        }
        // Método para carregar o relatório com base nos filtros selecionados
        private void CarregarRelatorio(int? funcionarioId = null, DateTime? dataInicial = null, DateTime? dataFinal = null, char? situacao = null)
        {
            try
            {
                var vales = _valeRefeicaoServico.ObterTodosValeRefeicoes();

                if (funcionarioId.HasValue)
                {
                    vales = vales.Where(v => v.FuncionarioId == funcionarioId.Value).ToList();
                }

                if (dataInicial.HasValue)
                {
                    vales = vales.Where(v => v.DataModificacao.Date >= dataInicial.Value.Date).ToList();
                }

                if (dataFinal.HasValue)
                {
                    vales = vales.Where(v => v.DataModificacao.Date <= dataFinal.Value.Date).ToList();
                }

                if (situacao != null)
                {
                    vales = vales.Where(v => v.Situacao == situacao).ToList();
                }
                
                var valesParaExibir = vales.Select(vale => new  // Seleciona os dados que serão exibidos no DataGridView
                {
                    vale.Id,
                    FuncionarioNome = _funcionarioServico.ObterFuncionarioPorId(vale.FuncionarioId)?.Nome,
                    vale.Quantidade,
                    vale.Situacao,
                    vale.DataModificacao
                }).ToList();



                dgvRelatorio.DataSource = valesParaExibir;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar relatório: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
        // Método para filtrar os vales-refeição com base nos critérios selecionados
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            int? funcionarioId = (int?)cboFuncionario.SelectedValue;

            DateTime? dataInicial = null;
            if (dtpDataInicial.Checked)
            {
                dataInicial = dtpDataInicial.Value;
            }


            DateTime? dataFinal = null;
            if (dtpDataFinal.Checked)
            {
                dataFinal = dtpDataFinal.Value;
            }

            CarregarRelatorio(funcionarioId, dataInicial, dataFinal);

        }

        // Método para exibir todos os vales-refeição
        private void btnExibirTodos_Click(object sender, EventArgs e)
        {
            CarregarRelatorio();
        }
    }
}
