using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GerenciadorValeRefeicoes.DAL.Interfaces;
using GerenciadorValeRefeicoes.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections;

/* Esta classe implementa a interface IFuncionarioRepositorio e fornece métodos para interagir com a tabela de Funcionarios no banco de dados.
 Ela utiliza Dapper para executar consultas SQL e mapear os resultados para objetos Funcionario.
 O repositório é responsável por operações como obter todos os funcionários, obter um funcionário por ID, adicionar um novo funcionário e atualizar um funcionário existente.
 O construtor recebe uma string de conexão que é usada para se conectar ao banco de dados MySQL.*/

namespace GerenciadorValeRefeicoes.DAL.Implementacoes
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly string _connectionString;

        public FuncionarioRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Funcionario> ObterTodos()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.Query<Funcionario>("SELECT * FROM Funcionarios").ToList();
            }
        }

        public Funcionario ObterPorId(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Funcionario>("SELECT * FROM Funcionarios WHERE Id = @Id", 
                    new { Id = id });
            }
        }


        public void Adicionar(Funcionario funcionario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Funcionarios (Nome, CPF, Situacao, DataAlteracao) VALUES (@Nome, @CPF, @Situacao, @DataAlteracao)"; // Define a query primeiro

                Console.WriteLine(query); 
                connection.Execute(query, funcionario);
            }
        }


        public void Atualizar(Funcionario funcionario)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = @"UPDATE Funcionarios SET Nome = @Nome, CPF = @CPF, Situacao = @Situacao, DataAlteracao = @DataAlteracao WHERE Id = @Id";

                    connection.Execute(sql, funcionario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar funcionário: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

    }
}
