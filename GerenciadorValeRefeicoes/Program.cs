using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using GerenciadorValeRefeicoes.DAL.Implementacoes;
using GerenciadorValeRefeicoes.DAL.Interfaces;
using GerenciadorValeRefeicoes.Views;
using Microsoft.Extensions.DependencyInjection;
using GerenciadorValeRefeicoes.BLL.Interfaces;
using GerenciadorValeRefeicoes.BLL;

/* O program.cs é o ponto de entrada do aplicativo. Ele configura os serviços e inicia a aplicação.
 O código usa o padrão de injeção de dependência para gerenciar as instâncias dos repositórios e serviços.
 O método Main é o ponto de entrada principal para o aplicativo. Ele configura os serviços, cria um provedor de serviços e inicia o formulário principal.
 O código também lê a string de conexão do banco de dados a partir do arquivo de configuração (App.config).*/

namespace GerenciadorValeRefeicoes
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            var services = new ServiceCollection();

            services.AddTransient<IFuncionarioRepositorio>(sp => new FuncionarioRepositorio(connectionString));
            services.AddTransient<IValeRefeicaoRepositorio>(sp => new ValeRefeicaoRepositorio(connectionString));
            services.AddTransient<IFuncionarioServico, FuncionarioServico>(); // Registra os serviços
            services.AddTransient<IValeRefeicaoServico, ValeRefeicaoServico>(); // Registra os serviços


            var serviceProvider = services.BuildServiceProvider();

            var funcionarioServico = serviceProvider.GetRequiredService<IFuncionarioServico>();
            var valeRefeicaoServico = serviceProvider.GetRequiredService<IValeRefeicaoServico>();

            Application.Run(new FormPrincipal(funcionarioServico, valeRefeicaoServico)); // Injeta os serviços no FormPrincipal            


        }
    }
}
