using GerenciadorValeRefeicoes.DAL.Implementacoes;
using GerenciadorValeRefeicoes.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GerenciadorValeRefeicoes.BLL.Interfaces;
using GerenciadorValeRefeicoes.Models;
using static GerenciadorValeRefeicoes.Models.Funcionario;
using GerenciadorValeRefeicoes.BLL;

/* Esta classe representa o formulário de cadastro de funcionário, ela coentém métodos para adicionar, atualizar e limpar os campos do formulário.
 Também contém métodos para validar o CPF e verificar se o CPF já existe no banco de dados.
 O formulário é carregado com os dados do funcionário selecionado para edição, se houver.
 A classe utiliza a interface IFuncionarioServico para realizar operações relacionadas aos funcionários.
 O formulário é inicializado com os valores do enum SituacaoFuncionario para o campo de situação do funcionário.
 O enum SituacaoFuncionario define os possíveis estados do funcionário, como Ativo e Inativo.*/

namespace GerenciadorValeRefeicoes.Views
{
    public partial class FormCadastroFuncionario : Form
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IFuncionarioServico _funcionarioServico;
        private Funcionario _funcionarioAtual;
        public FormCadastroFuncionario(IFuncionarioServico funcionarioServico)
        {
            _funcionarioServico = funcionarioServico;
            InitializeComponent();

            cboSituacao.Items.Add('A'); // Ativo
            cboSituacao.Items.Add('I'); // Inativo
            cboSituacao.SelectedItem = 'A'; // Valor padrão: Ativo

            dtpDataAlteracao.Enabled = false;
        }

        private void FormCadastroFuncionario_Load(object sender, EventArgs e)
        {
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            _funcionarioAtual = null;
            LimparCampos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_funcionarioAtual == null)
                {
                    _funcionarioAtual = new Funcionario();
                }

                _funcionarioAtual.Nome = txtNome.Text;
                _funcionarioAtual.CPF = System.Text.RegularExpressions.Regex.Replace(mtbCPF.Text, "[^0-9]", "");

                if (_funcionarioAtual.CPF.Length != 11)
                {
                    MessageBox.Show("CPF inválido. O CPF deve conter 11 dígitos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cboSituacao.SelectedItem != null)
                {
                    _funcionarioAtual.Situacao = (char)cboSituacao.SelectedItem; // Cast para char
                }
                else
                {
                    
                    MessageBox.Show("Selecione a situação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _funcionarioAtual.DataAlteracao = DateTime.Now;

                if (_funcionarioAtual.Id == 0)
                {
                    _funcionarioServico.AdicionarFuncionario(_funcionarioAtual);
                    MessageBox.Show("Funcionário cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _funcionarioServico.AtualizarFuncionario(_funcionarioAtual);
                    MessageBox.Show("Funcionário atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar funcionário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            mtbCPF.Clear();
            cboSituacao.SelectedItem = SituacaoFuncionario.Ativo; // Define o valor padrão
            dtpDataAlteracao.Value = DateTime.Now;
        }

        public void CarregarFuncionario(Funcionario funcionario)
        {
            _funcionarioAtual = funcionario; // Define o funcionário atual para edição
            txtNome.Text = funcionario.Nome;
            mtbCPF.Text = funcionario.CPF;
            cboSituacao.SelectedItem = funcionario.Situacao;
            dtpDataAlteracao.Value = funcionario.DataAlteracao;
        }

        private bool ValidarCPF(string cpf)
        {
            // Remove caracteres não numéricos
            return cpf.All(char.IsDigit) && cpf.Length == 11;

        }

        private bool CPFjaExiste(string cpf)
        {
            // Consulta o banco de dados para verificar se o CPF já existe.
            var funcionario = _funcionarioRepositorio.ObterTodos().FirstOrDefault(f => f.CPF == cpf);
            return funcionario != null;
        }
    }
}
