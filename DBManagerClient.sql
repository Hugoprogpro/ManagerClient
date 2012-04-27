create table Cliente (
	codigo int not null primary key,
	nome nvarchar(50) not null,
	dataCadastro nvarchar(100),
	telefone nvarchar(20),	
)

create table Endereco (
	Id int not null primary key identity,
	IdCliente int not null,
	Bairro nvarchar(100) not null,
	Logradouro nvarchar(100),
	Numero nvarchar(15),
	int cidade,
)

create table Cidade (
	Id int not null primary key identity,
	Descricao nvarchar(100) not null,
)