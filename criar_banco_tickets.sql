-- Criação do Banco de Dados
CREATE DATABASE IF NOT EXISTS GerenciadorValeRefeicoes;

-- Usa o Banco de Dados
USE GerenciadorValeRefeicoes;

-- Cria a Tabela de Funcionários
CREATE TABLE IF NOT EXISTS Funcionarios (
    id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(255) NOT NULL,
    CPF VARCHAR(11) NOT NULL UNIQUE,
    Situacao CHAR(1) NOT NULL CHECK (Situacao IN ('A', 'I')),
    DataAlteracao DATETIME NOT NULL
);

-- Cria a Tabela de ValeRefeicao
CREATE TABLE IF NOT EXISTS ValeRefeicao (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    FuncionarioId INT NOT NULL,
    Quantidade INT NOT NULL,
    Situacao CHAR(1) NOT NULL CHECK (Situacao IN ('A', 'I')),
    DataModificacao DATETIME NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionarios(Id)
);
