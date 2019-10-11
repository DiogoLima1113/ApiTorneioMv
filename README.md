# ApiTorneioMv
Necessario instalar Node.js na versão mínima 6.4.1
Necessario instalar Dot Net Core na versão mínima 2.2.402

Necessária a criação de um banco de dados no SQLServer, segue query modelo:
    CREATE DATABASE Torneio
    GO

    USE Torneio

    CREATE TABLE Time(
    id INT IDENTITY(1,1) PRIMARY KEY,
    nome VARCHAR(100))

    CREATE TABLE Jogador(
    id INT IDENTITY(1,1) PRIMARY KEY,
    nome VARCHAR(100))

    CREATE TABLE RelacionamentoTimeJogador(
    id INT IDENTITY(1,1) PRIMARY KEY,
    idTime INT,
    idJogador INT)

    CREATE TABLE Jogo(
    id INT IDENTITY(1,1) PRIMARY KEY,
    idTimeCasa INT,
    idTimeVisitante INT,
    pontuacaoTimeCasa INT,
    pontuacaoTimeVisitante INT);

Após a criacao do mesmo deve-se atualizar a connectionString no arquivo appsettings.json

Ao abrir o projeto deve realizar os comando:
    dotnet restore
    dotnet run