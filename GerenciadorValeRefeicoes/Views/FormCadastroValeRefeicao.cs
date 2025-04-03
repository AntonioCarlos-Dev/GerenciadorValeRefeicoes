using GerenciadorValeRefeicoes.BLL.Interfaces;
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

 /* Esta classe representa o formulário de cadastro de vale-refeição, onde o usuário pode adicionar ou atualizar informações sobre os vales-refeição.
  * ela contém campos para selecionar o funcionário, quantidade de vale-refeição, situação (ativo/inativo) e data de modificação.
  * a classe utiliza os serviços IValeRefeicaoServico e IFuncionarioServico para realizar operações relacionadas a vale-refeição e funcionários.
  * ela também contém métodos para limpar os campos, carregar os dados do vale-refeição selecionado e salvar as informações no banco de dados.
  * a classe é responsável por gerenciar a interação do usuário com o formulário de cadastro de vale-refeição.*/

namespace GerenciadorValeRefeicoes.Views
{

    public partial class FormCadastroValeRefeicao : Form
    {

        private readonly IValeRefeicaoServico _valeRefeicaoServico;
        private readonly IFuncionarioServico _funcionarioServico;
        private ValeRefeicao _valeRefeicaoAtual;

        public FormCadastroValeRefeicao(IValeRefeicaoServico valeRefeicaoServico, IFuncionarioServico funcionarioServico)
        {
            InitializeComponent();
            _valeRefeicaoServico = valeRefeicaoServico;
            _funcionarioServico = funcionarioServico;

            cboFuncionario.DataSource = _funcionarioServico.ObterTodosFuncionarios();
            cboFuncionario.DisplayMember = "Nome";
            cboFuncionario.ValueMember = "Id";

            cboSituacaoVeleRefeicao.Items.Add("A");
            cboSituacaoVeleRefeicao.Items.Add("I");
            cboSituacaoVeleRefeicao.SelectedItem = "A";

            dtpDataModificacao.Enabled = false;
        }

        private void FormCadastroValeRefeicao_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                _valeRefeicaoAtual = _valeRefeicaoAtual ?? new ValeRefeicao();

                _valeRefeicaoAtual.FuncionarioId = (int)cboFuncionario.SelectedValue;
                _valeRefeicaoAtual.Quantidade = (int)nudQuantidade.Value;
                _valeRefeicaoAtual.Situacao = char.Parse(cboSituacaoVeleRefeicao.SelectedItem.ToString());

                _valeRefeicaoAtual.DataModificacao = DateTime.Now;

                if (_valeRefeicaoAtual.Id == 0)
                {
                    _valeRefeicaoServico.AdicionarValeRefeicao(_valeRefeicaoAtual);
                    MessageBox.Show("Vale-Refeição adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _valeRefeicaoServico.AtualizarValeRefeicao(_valeRefeicaoAtual);
                    MessageBox.Show("Vale-Refeição atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparCampos();
                cboFuncionario.DataSource = _funcionarioServico.ObterTodosFuncionarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar o vale-refeição: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            _valeRefeicaoAtual = null;
            LimparCampos();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha o formulário

        }

        private void LimparCampos()
        {
            cboFuncionario.SelectedIndex = -1; // Limpa a seleção do funcionário
            nudQuantidade.Value = 0;
            cboSituacaoVeleRefeicao.SelectedItem = 'A';
            dtpDataModificacao.Value = DateTime.Now;
        }

        private void CarregarValeRefeicao(ValeRefeicao valeRefeicao)
        {
            _valeRefeicaoAtual = valeRefeicao;
            cboFuncionario.SelectedValue = valeRefeicao.FuncionarioId;
            nudQuantidade.Value = valeRefeicao.Quantidade;

            
            cboSituacaoVeleRefeicao.SelectedItem = valeRefeicao.Situacao.ToString();

            dtpDataModificacao.Value = valeRefeicao.DataModificacao;
        }
    }
}
