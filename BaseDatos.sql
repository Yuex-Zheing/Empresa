use Empresa;
/*
drop table Movimientos;
drop table Cuentas;
drop table Clientes;
drop table Personas;
*/

/*
Select * from Personas;
select * from Clientes;
select * from Cuentas;
select * from Movimientos;
*/

create table Personas(
	IdPersona int not null PRIMARY KEY IDENTITY(1,1) ,
	Nombre nvarchar(100) not null,
	Genero nvarchar(1) not null, -- M masculino,  F femenino
	Edad int not null,
	Identificacion nvarchar(10) not null unique,
	Direccion nvarchar(200) not null,
	Telefono nvarchar(10) not null
);

create table Clientes(
	IdCliente int not null PRIMARY KEY IDENTITY(1,1) ,
	Persona int not null FOREIGN KEY REFERENCES Personas(IdPersona),
	Clave nvarchar(20) not null,
	Estado nvarchar(1) not null
);

create table Cuentas(
	IdCuenta int not null PRIMARY KEY IDENTITY(1,1) ,
	Cliente int not null FOREIGN KEY REFERENCES Clientes(IdCliente),
	NumeroCuenta  nvarchar(10) not null unique,
	TipoCuenta nvarchar(1) not null, --A Ahorro, C Corriente
	SaldoInicial decimal(12,3) not null,
	Estado nvarchar(1) not null -- A Activo , I Inactivo
);

create table Movimientos(
	IdMovimiento bigint not null PRIMARY KEY IDENTITY(1,1),
	Cuenta int not null FOREIGN KEY REFERENCES Cuentas(IdCuenta),
	Fecha datetime not null,
	TipoMovimiento nvarchar(1) not null,-- D debito , C Credito
	Valor decimal(12,3) not null,
	Saldo decimal(12,3) not null,
	Estado nvarchar(1) not null, -- A Aprobado, R Rechazado
	MovDescripcion nvarchar(200) null
);
