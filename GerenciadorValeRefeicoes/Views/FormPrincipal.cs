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
using GerenciadorValeRefeicoes.DAL.Interfaces;
using GerenciadorValeRefeicoes.Views;

/* Esta classe representa o formulário principal da aplicação, onde o usuário pode acessar diferentes funcionalidades do sistema.
 * ela contém métodos para abrir formulários filhos dentro de um painel, permitindo que o usuário navegue entre diferentes seções do sistema.
 * a classe utiliza os serviços IFuncionarioServico e IValeRefeicaoServico para realizar operações relacionadas a funcionários e vale-refeição.
 * ela é responsável por carregar o formulário principal e gerenciar a navegação entre os formulários filhos.
 * ela também contém eventos para os botões de cadastro, vale-refeição e relatório, que abrem os respectivos formulários filhos.
 */

namespace GerenciadorValeRefeicoes.Views
{
    public partial class FormPrincipal : Form
    {

        private readonly IFuncionarioServico _funcionarioServico; // Serviços
        private readonly IValeRefeicaoServico _valeRefeicaoServico;
        public FormPrincipal(IFuncionarioServico funcionarioServico, IValeRefeicaoServico valeRefeicaoServico)
        {
            _funcionarioServico = funcionarioServico;
            _valeRefeicaoServico = valeRefeicaoServico;

            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void AbrirFormNoPainel(Form formularioFilho)
        {
            panelDesktop.Controls.Clear();

            formularioFilho.TopLevel = false;
            formularioFilho.FormBorderStyle = FormBorderStyle.None;
            formularioFilho.Dock = DockStyle.Fill;

            panelDesktop.Controls.Add(formularioFilho);

            formularioFilho.Show();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            AbrirFormNoPainel(new FormCadastroFuncionario(_funcionarioServico));
        }

        private void btnValeRefeicao_Click(object sender, EventArgs e)
        {
            AbrirFormNoPainel(new FormCadastroValeRefeicao(_valeRefeicaoServico, _funcionarioServico));
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            AbrirFormNoPainel(new FormRelatorio(_funcionarioServico, _valeRefeicaoServico));
        }
    }
}
