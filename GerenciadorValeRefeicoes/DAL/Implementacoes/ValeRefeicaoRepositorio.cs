using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GerenciadorValeRefeicoes.DAL.Interfaces;
using GerenciadorValeRefeicoes.Models;
using MySql.Data.MySqlClient;

/* Esta classe implementa a interface IValeRefeicaoRepositorio, que define métodos para manipulação de dados de vale-refeição.
 Ela utiliza a biblioteca Dapper para facilitar a execução de consultas SQL e o mapeamento de resultados para objetos C#.
 O repositório é responsável por operações como obter todos os vales-refeição, obter um vale-refeição por ID, adicionar e atualizar um vale-refeição.
 O construtor recebe uma string de conexão que é usada para se conectar ao banco de dados MySQL.
 A classe também inclui tratamento de exceções para capturar e registrar erros durante as operações de banco de dados.*/


namespace GerenciadorValeRefeicoes.DAL.Implementacoes
{
    public class ValeRefeicaoRepositorio : IValeRefeicaoRepositorio
    {

        private readonly string _connectionString;

        public ValeRefeicaoRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ValeRefeicao> ObterTodos()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.Query<ValeRefeicao>("SELECT * FROM ValeRefeicao").ToList();
            }
        }

        public ValeRefeicao ObterPorId(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<ValeRefeicao>("SELECT * FROM ValeRefeicao WHERE Id = @Id",
                    new { Id = id });
            }
        }

        public void Adicionar(ValeRefeicao valeRefeicao)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = @"INSERT INTO ValeRefeicao (FuncionarioId, Quantidade, Situacao, DataModificacao) VALUES (@FuncionarioId, @Quantidade, @Situacao, @DataModificacao)";

                    connection.Execute(sql, valeRefeicao);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar vale-refeição: {ex.Message}\n{ex.StackTrace}");
                throw;
            }

        }

        public void Atualizar(ValeRefeicao valeRefeicao)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE ValeRefeicao SET FuncionarioId = @FuncionarioId, Quantidade = @Quantidade, Situacao = @Situacao, DataModificacao = @DataModificacao WHERE Id = @Id";
                    connection.Execute(sql, valeRefeicao);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar vale-refeição: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
