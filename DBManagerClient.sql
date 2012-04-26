create table Cliente (
	codigo int not null primary key,
	nome nvarchar(50) not null,
	dataCadastro nvarchar(100),
	telefone nvarchar(20),
	Bairro nvarchar(100) not null,
	Logradouro nvarchar(100),
	Numero nvarchar(15),
	cidade int
)

create table Endereco (
	Id int not null primary key identity,
	IdCliente int not null,
	Bairro nvarchar(100) not null,
	Logradouro nvarchar(100),
	Numero nvarchar(15),
	cidade int
)

create table Cidades (
	Id int not null primary key identity,
	Descricao nvarchar(100) not null,
)