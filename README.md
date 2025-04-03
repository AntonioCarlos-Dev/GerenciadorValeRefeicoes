# Gerenciador de Tickets de Refeição

## Descrição

Aplicação Windows Forms para gerenciamento de tickets de refeição de funcionários. Permite o cadastro e edição de funcionários, o lançamento de vales refeição e a geração de relatórios.

## Tecnologias Utilizadas

* C#
* Windows Forms
* MySQL
* Dapper (ORM micro framework))

## Arquitetura da Solução

A solução foi desenvolvida utilizando uma arquitetura em camadas para promover a separação de responsabilidades e facilitar a manutenção e testabilidade do código. As principais camadas são:

* **BLL (Business Logic Layer):** Camada responsável pela lógica de negócios da aplicação.
* **DAL (Data Access Layer):** Camada responsável pelo acesso aos dados no banco de dados MySQL.
* **Models:** Camada que define as classes de modelo (entidades) utilizadas na aplicação.
* **Views:** Camada responsável pela interface do usuário (Windows Forms).

### BLL (Business Logic Layer)

A camada BLL contém as interfaces de serviço e suas implementações concretas. As interfaces definem os contratos de serviço, permitindo a injeção de dependência e facilitando a criação de testes unitários.

*   `IFuncionarioServico`: Define as operações para gerenciar funcionários.
*   `IValeRefeicaoServico`: Define as operações para gerenciar vales refeição.
*   `FuncionarioServico`: Implementação concreta de `IFuncionarioServico`. Implementa a lógica de negócio para gerenciar funcionários, como adicionar, atualizar e validar dados.
*   `ValeRefeicaoServico`: Implementação concreta de `IValeRefeicaoServico`. Implementa a lógica de negócio para gerenciar vales refeição, como adicionar, atualizar e validar dados.
	
### DAL (Data Access Layer)

A camada DAL contém as interfaces de repositório e suas implementações concretas. Os repositórios são responsáveis por abstrair o acesso aos dados, permitindo que a camada BLL não precise conhecer os detalhes de implementação do banco de dados.

*   `IFuncionarioRepositorio`: Define as operações para acessar os dados de funcionários.
*   `IValeRefeicaoRepositorio`: Define as operações para acessar os dados de vales refeição.
*   `FuncionarioRepositorio`: Implementação concreta de `IFuncionarioRepositorio`. Utiliza Dapper para acessar o banco de dados e realizar operações CRUD na tabela `Funcionarios`.
*   `ValeRefeicaoRepositorio`: Implementação concreta de `IValeRefeicaoRepositorio`. Utiliza Dapper para acessar o banco de dados e realizar operações CRUD na tabela `ValeRefeicao`.
	
### Models

A camada Models define as classes de modelo (entidades) utilizadas na aplicação.

*   `Funcionario`: Representa um funcionário. Inclui validações de dados utilizando Data Annotations.
*   `ValeRefeicao`: Representa um vale refeição. Inclui validações de dados utilizando Data Annotations.
	
### Views

A camada Views contém os formulários da aplicação (interface do usuário).

*   `FormCadastroFuncionario`: Formulário para cadastrar e editar funcionários. Valida os dados de entrada e interage com a camada BLL para salvar as informações.
*   `FormCadastroValeRefeicao`: Formulário para cadastrar vales refeição. Permite selecionar o funcionário e a quantidade de vales a serem atribuídos.
*   `FormRelatorio`: Formulário para gerar relatórios. Permite filtrar os vales refeição por funcionário, data e situação.
*   `FormPrincipal`: Formulário principal da aplicação. Gerencia a navegação entre os outros formulários.
	
## Como Executar o Projeto

1.  Clone o repositório do GitHub.
2.  Abra a solução `GerenciadorValeRefeicoes.sln` no Visual Studio.
3.  Restaure os pacotes NuGet (se necessário).
4.  Crie o banco de dados MySQL utilizando o script `criar_banco_tickets.sql`.
5.  Configure a string de conexão no arquivo `App.config`:
	
    ```xml
    <connectionStrings>
        <add name="MySqlConnection" connectionString="Server=localhost;Database=GerenciadorValeRefeicoes;Uid=root;Pwd=sua_senha;"/>
    </connectionStrings>
	```
	Substitua `Uid` e `Pwd` pelas suas credenciais do MySQL.

6. Execute o projeto.

## Decisões de Design e Padrões de Projeto

*   **Arquitetura em Camadas:** A arquitetura em camadas foi escolhida para promover a separação de responsabilidades e facilitar a manutenção e testabilidade do código.
*   **Repository Pattern:** O Repository Pattern foi utilizado na camada DAL para abstrair o acesso aos dados e permitir a troca do banco de dados sem afetar a camada BLL.
*   **Dependency Injection:** A aplicação utiliza Dependency Injection (DI) para gerenciar as dependências entre as camadas. O `Program.cs` configura os serviços e os injeta nos formulários.
*   **Dapper:** Dapper foi utilizado como micro ORM para simplificar o acesso ao banco de dados.
*   **Windows Forms:** Windows Forms foi escolhido por ser uma tecnologia rápida para o desenvolvimento de aplicações desktop e atender aos requisitos do projeto.
	
## Áreas para Melhoria

*   **Testes Unitários:** A implementação de testes unitários seria fundamental para garantir a qualidade e a robustez do código.
*   **Validação de Dados:** A validação de dados poderia ser aprimorada para fornecer feedback mais detalhado ao usuário.
*   **Tratamento de Exceções:** O tratamento de exceções poderia ser aprimorado para registrar os erros em um arquivo de log.
*   **Interface do Usuário:** A interface do usuário poderia ser modernizada para melhorar a experiência do usuário.
	
## Informações Adicionais

*   **NuGet Packages:**
    *   `MySql.Data`: Utilizado para conectar a aplicação ao banco de dados MySQL.
    *   `Dapper`: Utilizado como micro ORM para facilitar o acesso aos dados.
		 
## Considerações sobre o Código

*   A validação de CPF na classe `FuncionarioServico` verifica se o CPF contém 11 dígitos e é composto apenas por números.
*   A classe `FormCadastroFuncionario` utiliza Data Annotations para validar os dados de entrada do usuário.
*   A classe `FormRelatorio` permite filtrar os vales refeição por funcionário, data e situação.
*   O script SQL `criar_banco_tickets.sql` cria as tabelas `Funcionarios` e `ValeRefeicao` no banco de dados MySQL.
