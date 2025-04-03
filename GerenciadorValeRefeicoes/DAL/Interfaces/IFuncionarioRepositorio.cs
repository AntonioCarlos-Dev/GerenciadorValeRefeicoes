﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorValeRefeicoes.Models;

/* Esta interface define os métodos que devem ser implementados por qualquer repositório de funcionários.
 Ela inclui métodos para obter todos os funcionários, obter um funcionário por ID, adicionar um novo funcionário e atualizar um funcionário existente.
 Esses métodos são utilizados para manipular os dados dos funcionários na aplicação.
 A interface é utilizada para garantir que as classes que implementam essa interface
 forneçam implementações para os métodos definidos, permitindo uma separação clara entre a lógica de negócios e a lógica de acesso a dados.*/

namespace GerenciadorValeRefeicoes.DAL.Interfaces
{
    public interface IFuncionarioRepositorio
    {
        List<Funcionario> ObterTodos();
        Funcionario ObterPorId(int id);
        void Adicionar(Funcionario funcionario);
        void Atualizar(Funcionario funcionario);

    }
}
