create database callaopets
go

use callaopets
go

/*CREANDO TABLAS*/
CREATE TABLE animal (
    IdAnimal INT PRIMARY KEY,
    Descripcion VARCHAR(20) NULL
)
go

CREATE TABLE area (
    IdTipoArea INT PRIMARY KEY,
    Descripcion VARCHAR(20) NOT NULL
)
go

CREATE TABLE cargo (
    IdCargo INT PRIMARY KEY,
    Descripcion VARCHAR(20) NOT NULL
)
go
   
CREATE TABLE cliente (
    IdCliente INT PRIMARY KEY,
    Nombres VARCHAR(30) NOT NULL,
    Apellidos VARCHAR(30) NOT NULL,
    Telefono VARCHAR(11) NOT NULL,
    Direccion VARCHAR(80) NOT NULL,
    Correo VARCHAR(60) NOT NULL,
    DNI VARCHAR(8) NOT NULL,
    password VARCHAR(250) NULL
)
go

CREATE TABLE empresa_delivery (
  IdEmpresaDelivery INT NOT NULL,
  Nombre VARCHAR(20) NULL,
  Telefono VARCHAR(11) NULL,
  RUC VARCHAR(11) NULL,
  PRIMARY KEY (IdEmpresaDelivery)
)
go    

CREATE TABLE trabajador (
    IdTrabajador INT PRIMARY KEY,
    Nombres VARCHAR(30) NOT NULL,
    Apellidos VARCHAR(30) NOT NULL,
    DNI VARCHAR(8) NOT NULL,
    Telefono VARCHAR(11) NOT NULL,
    Correo VARCHAR(60) NOT NULL,
    Direccion VARCHAR(60) NOT NULL,
    IdCargo INT NOT NULL,
    IdTipoArea INT NOT NULL,
    Password VARCHAR(250) NOT NULL,
    FOREIGN KEY (IdTipoArea) REFERENCES area(IdTipoArea),
    FOREIGN KEY (IdCargo) REFERENCES Cargo(IdCargo)
)
go

CREATE TABLE proveedor (
  IdProveedor int NOT NULL,
  Telefono varchar(11) NOT NULL,
  Direccion varchar(80) NOT NULL,
  Empresa varchar(20) NOT NULL,
  RUC varchar(11) NOT NULL,
  Correo varchar(60) NOT NULL,
  Representante varchar(60) DEFAULT NULL,
  PRIMARY KEY (IdProveedor)
)
go

CREATE TABLE ingreso_producto (
  CodIngresoPro INT NOT NULL,
  FechaPedido DATETIME NULL,
  IdProveedor INT NOT NULL,
  MontoTotal DECIMAL(10,2) NOT NULL,
  IdTrabajador INT NOT NULL,
  PRIMARY KEY (CodIngresoPro),
  CONSTRAINT FK_ip_IdTrabajador FOREIGN KEY (IdTrabajador) REFERENCES trabajador (IdTrabajador),
  CONSTRAINT FK_ip_IdProveedor FOREIGN KEY (IdProveedor) REFERENCES proveedor (IdProveedor)
)
go

CREATE TABLE tipoproducto (
  IdTipoPro int NOT NULL,
  Descripcion varchar(20) NOT NULL,
  PRIMARY KEY (IdTipoPro)
)
go

CREATE TABLE producto (
  IdProducto INT NOT NULL,
  IdTipoPro INT NOT NULL,
  IdProveedor INT NOT NULL,
  Nombre VARCHAR(120) NOT NULL,
  Cantidad SMALLINT NOT NULL,
  Preciopublico DECIMAL(10,2) NOT NULL,
  StockMinimo SMALLINT NOT NULL,
  StockMaximo SMALLINT NOT NULL,
  estado TINYINT NOT NULL,
  IdAnimal INT NOT NULL,
  PrecioProveedor DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (IdProducto),
  CONSTRAINT FK_Producto_IdTipoPro FOREIGN KEY (IdTipoPro) REFERENCES tipoproducto (IdTipoPro),
  CONSTRAINT FK_Producto_IdProveedor FOREIGN KEY (IdProveedor) REFERENCES proveedor (IdProveedor),
  CONSTRAINT FK_Producto_IdAnimal FOREIGN KEY (IdAnimal) REFERENCES animal (IdAnimal)
)
go

CREATE TABLE tipobaja (
  IdTipoBaja int NOT NULL PRIMARY KEY,
  Descripcion varchar(20) NOT NULL
)
go

CREATE TABLE tipopago (
  idtipopago int NOT NULL PRIMARY KEY,
  descripcion varchar(45) NULL
)
go

CREATE TABLE detalle_tipopago (
  iddetallepago INT NOT NULL,
  idcliente INT NULL,
  idtipopago INT NULL,
  numerocuenta VARCHAR(45) NULL,
  fechavencimiento DATE NULL,
  PRIMARY KEY (iddetallepago),
  CONSTRAINT fk_dpidcliente FOREIGN KEY (idcliente) REFERENCES cliente (IdCliente),
  CONSTRAINT fk_dpidtipopago FOREIGN KEY (idtipopago) REFERENCES tipopago (idtipopago)
)
go

CREATE TABLE venta_producto (
  CodVentaPro int NOT NULL,
  IdCliente int NOT NULL,
  IdEmpresaDelivery int NOT NULL,
  Fecha datetime DEFAULT NULL,
  Direccion varchar(80) DEFAULT NULL,
  MontoTotal decimal(10,2) NOT NULL,
  IdPago int DEFAULT NULL,
  PRIMARY KEY (CodVentaPro),
  CONSTRAINT fk_vpidpago FOREIGN KEY (IdPago) REFERENCES detalle_tipopago (iddetallepago),
  CONSTRAINT fk_vpidcliente FOREIGN KEY (IdCliente) REFERENCES cliente (IdCliente),
  CONSTRAINT fk_vpidempresa FOREIGN KEY (IdEmpresaDelivery) REFERENCES empresa_delivery (IdEmpresaDelivery)
)
GO
-------------------Carrito------------

CREATE TABLE baja_producto (
    CodBajaPro INT PRIMARY KEY,
    IdTrabajador INT NOT NULL,
    FechaBaja DATETIME NOT NULL,
    CONSTRAINT FK_baja_producto_trabajador FOREIGN KEY (IdTrabajador) REFERENCES trabajador (IdTrabajador)
)
go

CREATE TABLE detalle_baja (
  IdProducto INT NOT NULL,
  CodBajaPro INT NOT NULL,
  Cantidad SMALLINT NOT NULL,
  IdTipoBaja INT NULL,
  PRIMARY KEY (IdProducto, CodBajaPro),
  CONSTRAINT FK_Producto_DetalleBaja FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto),
  CONSTRAINT FK_BajaProducto_DetalleBaja FOREIGN KEY (CodBajaPro) REFERENCES baja_producto (CodBajaPro),
  CONSTRAINT FK_TipoBaja_DetalleBaja FOREIGN KEY (IdTipoBaja) REFERENCES tipobaja (IdTipoBaja)
)
go
CREATE TABLE detalle_ingreso (
  CodIngresoPro INT NOT NULL,
  IdProducto INT NOT NULL,
  PrecioCompra INT NOT NULL,
  Cantidad SMALLINT NULL,
  Monto DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (CodIngresoPro, IdProducto),
  CONSTRAINT FK_IngresoProducto_DetalleIngreso FOREIGN KEY (CodIngresoPro) REFERENCES ingreso_producto (CodIngresoPro),
  CONSTRAINT FK_Producto_DetalleIngreso FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto)
)
go


CREATE TABLE detalle_venta (
  CodVentaPro INT NOT NULL,
  IdProducto INT NOT NULL,
  Cantidad SMALLINT NOT NULL,
  PrecioUnitario DECIMAL(10,2) NOT NULL,
  Monto DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (CodVentaPro, IdProducto),
  CONSTRAINT FK_detalle_venta_CodVentaPro FOREIGN KEY (CodVentaPro) REFERENCES venta_producto (CodVentaPro),
  CONSTRAINT FK_detalle_venta_IdProducto FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto)
)
go


/*INSERTS A LAS TABLAS*/
INSERT INTO animal (IdAnimal, Descripcion) VALUES (1, 'gato'), (2, 'perro')
go

INSERT INTO area (IdTipoArea, Descripcion) VALUES
    (1, 'logística'),
    (2, 'compras'),
    (3, 'almacén'),
    (4, 'ventas'),
    (5, 'dirección')
go

INSERT INTO cargo (IdCargo, Descripcion)
VALUES (1, 'gerente'),
       (2, 'asistente'),
       (3, 'auxiliar'),
       (4, 'técnico')
 go  

INSERT INTO cliente (IdCliente, Nombres, Apellidos, Telefono, Direccion, Correo, DNI, password)
VALUES
(1, 'Sabrina', 'Mendoza Paredes', '912561329', 'Av Venezuela', 'sabrinamendoza@gmai.com', '09876543', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(4, 'Ariana', 'Milla Leon', '912561329', 'Jr Napo 123', 'arianamillaleon@gmail.com', '12345678', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(5, 'Sofia', 'Vera', '912561329', 'Tamarugal D34', 'sofiavera@gmail.com', '09876543', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(6, 'Valeria Patricia', 'Mendoza Paredes', '956712345', 'Tamarugal D34', 'valeriapatricia@gmail.com', '71234567', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3')
go

INSERT INTO trabajador (IdTrabajador, Nombres, Apellidos, DNI, Telefono, Correo, Direccion, IdCargo, IdTipoArea, password)
VALUES
(1, 'Gibeth Andrea', 'Peña Alarcon', '74854123', '912561329', 'gibethandrea@gmail.com', 'Tamarugal D34', 1, 1, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(2, 'Alfredo', 'Castillo', '79876543', '998765432', 'alfredocastillo@gmail.com', 'Av Tomas Valle 1120', 3, 2, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(3, 'Sabrina', 'Garay Mendoza', '20984567', '912561329', 'sabrinagaray@gmai.com', 'Islas Aleutianas', 3, 5, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(4, 'Akem', 'Del Rio', '76781234', '912987321', 'akemidelrio@gmail.com', 'Av. Tomás Valle', 1, 1, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(5, 'Milagros', 'Calderón', '77897654', '955677675', 'milagroscalderon@gmail.com', 'Av La Marina 1021', 2, 1, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(6, 'Joshua', 'Perez Martinez', '74854122', '945612389', 'joshuaperez@gmail.com', 'Tamarugal D34', 1, 1, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(7, 'Zadith', 'Vera', '09876543', '956712345', 'zadithvera@gmail.com', 'Av Venezuela', 1, 1, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(9, 'Sabrina', 'Vera', '09876543', '912234897', 'sabrinavera@gmail.com', 'Islas Aleutianas', 1, 3, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
(10, 'Akemi', 'Alarcon Gutierrez', '79876543', '912345678', 'akemialarcon@gmail.com', 'Tamarugal D34', 3, 4, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3')
go

INSERT INTO empresa_delivery (IdEmpresaDelivery, Nombre, Telefono, RUC)
VALUES (1, 'Rappi', '01 4254126', '20602985971'),
       (2, 'PedidosYa', '01 4789876', '20556082708')
 go  

INSERT INTO proveedor (IdProveedor, Telefono, Direccion, Empresa, RUC, Correo, Representante)
VALUES (1, '359-1406', 'Av. José Pardo 434', 'Rintisa', '20100617332', 'consultas@ricocan.com', 'José Perez')
go

INSERT INTO proveedor (IdProveedor, Telefono, Direccion, Empresa, RUC, Correo, Representante)
VALUES (2, '0800-10210', 'Fabrica Nestle, Alberto Reyes 1808, Lima 15081', 'Nestlé', '20100166578', 'elviracano@gmail.com', 'Elvira Cano')
go

INSERT INTO tipoproducto (IdTipoPro, Descripcion)
VALUES
(1, 'comidas'),
(2, 'juguetes'),
(3, 'ropa y accesorios'),
(4, 'limpieza'),
(5, 'medicamentos')
go

INSERT INTO producto (IdProducto, IdTipoPro, IdProveedor, Nombre, Cantidad, Preciopublico, StockMinimo, StockMaximo, estado, IdAnimal, PrecioProveedor)
VALUES
(1, 1, 1, 'CANBO DOG CON CORDERO RZ.MYG AD 3KG', 20, 51.60, 5, 30, 0, 2, 38.17),
(2, 1, 1, 'CANBO DOG CON CORDERO RZ.MYG AD 15KG', 10, 221.80, 5, 20, 1, 2, 164.14),
(3, 1, 1, 'CANBO DOG CON CORDERO RZ.PQ AD 3KG', 6, 51.60, 5, 30, 1, 2, 38.17),
(5, 1, 1, 'CANBO DOG HIGH BALANCE T.RZ AD 15KG', 17, 221.80, 5, 20, 1, 2, 164.14),
(6, 1, 1, 'CANBO DOG CON CORDERO RZ.MYG CACH 3KG', 18, 56.20, 5, 30, 1, 2, 41.61),
(7, 1, 1, 'CANBO DOG CON CORDERO RZ.MYG CACH 15KG', 10, 232.10, 5, 20, 1, 2, 171.78),
(8, 1, 1, 'CANBO DOG CON CORDERO RZ.PQ CACH 1KG', 26, 20.60, 5, 30, 1, 2, 15.27),
(9, 1, 1, 'CANBO DOG CON CORDERO RZ.PQ CACH 7KG', 15, 117.10, 5, 25, 1, 2, 86.65),
(10, 1, 1, 'CANBO DOG CON POLLO ARROZ RZ.PQ AD 3KG', 10, 59.00, 5, 30, 1, 2, 43.64),
(11, 1, 1, 'CANBO DOG CON POLLO ARROZ RZ.PQ AD 7KG', 15, 129.00, 5, 25, 1, 2, 95.20),
(12, 1, 1, 'CANBO DOG CON POLLO ARROZ RZ.MYG AD 3KG', 10, 59.00, 5, 30, 1, 2, 43.64),
(13, 1, 1, 'CANBO DOG CON POLLO ARROZ RZ.MYG AD 15KG', 10, 253.00, 5, 20, 1, 2, 186.44),
(14, 1, 1, 'CANBO DOG CON POLLO ARROZ T.RZ SENIOR 3KG', 15, 59.00, 5, 30, 1, 2, 43.64),
(15, 1, 1, 'CANBO DOG CON POLLO ARROZ T.RZ SENIOR 15KG', 15, 253.00, 5, 20, 1, 2, 186.44),
(16, 1, 1, 'CANBO DOG CON POLLO ARROZ RZ.PQ CACH 1KG', 10, 23.60, 5, 30, 1, 2, 17.48),
(17, 1, 1, 'CANBO DOG CON POLLO ARROZ RZ.PQ CACH 7KG', 15, 134.20, 5, 25, 1, 2, 99.32),
(18, 1, 1, 'CANBO DOG CON POLLO ARROZ RZ.MYG CACH 3KG', 25, 64.40, 5, 30, 1, 2, 47.67),
(19, 1, 1, 'CANBO DOG CON POLLO ARROZ RZ.MYG CACH 15KG', 15, 269.00, 5, 20, 1, 2, 198.63),
(20, 1, 1, 'CANBO DOG SKIN PROTECTION CON SALMON T.RZ AD 3KG', 10, 64.40, 5, 30, 1, 2, 47.67),
(21, 1, 1, 'CANBO DOG SKIN PROTECTION CON SALMON T.RZ AD 15KG', 12, 269.00, 5, 20, 1, 2, 198.63),
(22, 1, 1, 'CANBO DOG WEIGHT CONTROL CON POLLO T.RZ AD 3KG', 10, 64.40, 5, 30, 1, 2, 47.67),
(23, 1, 1, 'CANBO DOG WEIGHT CONTROL CON POLLO T.RZ AD 15KG', 18, 269.00, 5, 20, 1, 2, 198.63)
go

INSERT INTO tipobaja (IdTipoBaja, Descripcion)
VALUES
  (1, 'pérdida'),
  (2, 'deterioro')
 go 

INSERT INTO tipopago (idtipopago, descripcion) VALUES (1, 'VISA')
INSERT INTO tipopago (idtipopago, descripcion) VALUES (2, 'MASTERCARD')
go




/*PROCEDURES*/

create or alter procedure usp_boleta
as
	select*from ingreso_producto  
go

create or alter procedure usp_boleta_deta
as
	select*from detalle_ingreso
go
create or alter proc usp_animal_list
as
select *from animal
go
create or alter proc usp_tipoproducto_list
as
select *from tipoproducto
go
 create or alter proc usp_como_proveedor
 as
 select IdProveedor,Empresa from proveedor
 go


create or alter proc usp_listar_cliente
as
begin 
    select *from cliente
end
go

CREATE or alter PROCEDURE listar_cliente_nombre
    @nom varchar(30)
AS
BEGIN
    SELECT * FROM cliente WHERE Nombres LIKE @nom + '%'
END
go

create or alter proc agregar_clientes
    @idCliente int,
    @Nombres varchar(30),
    @Apellidos varchar(30),
    @Telefono varchar(11),
    @Direccion varchar(80),
    @Correo varchar(60),
    @DNI varchar(8),
    @password varchar(250)
as
begin
    insert into cliente values (@idCliente, @Nombres, @Apellidos, @Telefono, @Direccion, @Correo, @DNI, @password)
end
go

create or alter proc actualizar_clientes
    @idCliente int,
    @Nombres varchar(30),
    @Apellidos varchar(30),
    @Telefono varchar(11),
    @Direccion varchar(80),
    @Correo varchar(60),
    @DNI varchar(8),
    @password varchar(250)
as
begin
    update cliente set Nombres = @Nombres, Apellidos = @Apellidos, Telefono = @Telefono, Direccion = @Direccion, Correo = @Correo, DNI = @DNI, password = @password
    where idCliente = @idCliente
end
go

create or alter proc eliminar_clientes
    @idCliente int
as
begin
    delete from cliente
    where idCliente = @idCliente
end
go


CREATE or alter PROCEDURE listar_trabajador
AS
BEGIN
    SELECT *
    FROM trabajador
END
go
 
CREATE or alter PROCEDURE listar_trabajador_nombre
    @nom NVARCHAR(30)
AS
BEGIN
    SELECT t.IdTrabajador, CONCAT(t.Nombres, ' ', t.Apellidos) AS Nombres, t.DNI, t.Telefono,
           t.Correo, t.Direccion, c.Descripcion AS Cargo, a.Descripcion AS Area, t.Password
    FROM trabajador t
    INNER JOIN cargo c ON c.IdCargo = t.IdCargo
    INNER JOIN area a ON a.IdTipoArea = t.IdTipoArea
    WHERE t.Nombres LIKE CONCAT(@nom, '%')
    ORDER BY 1
END
go

create or alter proc agregar_trabajador
    @IdTrabajador int,
    @Nombres varchar(30),
    @Apellidos varchar(30),
    @DNI varchar(8),
    @Telefono varchar(11),
    @Correo varchar(60),
    @Direccion varchar(60),
    @IdCargo int,
    @IdTipoArea int,
    @Password varchar(250)
as
begin
    insert into trabajador values (@IdTrabajador, @Nombres, @Apellidos,  @DNI, @Telefono, @Correo, @Direccion, @IdCargo, @IdTipoArea, @Password)
end
go

create or alter proc actualizar_trabajador
    @IdTrabajador int,
    @Nombres varchar(30),
    @Apellidos varchar(30),
    @DNI varchar(8),
    @Telefono varchar(11),
    @Correo varchar(60),
    @Direccion varchar(60),
    @IdCargo int,
    @IdTipoArea int,
    @Password varchar(250)
as
begin
    update trabajador set Nombres = @Nombres, Apellidos = @Apellidos, DNI = @DNI, Telefono = @Telefono, Correo = @Correo, Direccion = @Direccion, IdCargo = @IdCargo, IdTipoArea = @IdTipoArea, Password = @Password
    where IdTrabajador = @IdTrabajador
end
go

create or alter proc eliminar_trabajador
    @IdTrabajador int
as
begin
    delete from trabajador
    where IdTrabajador = @IdTrabajador
end
go

CREATE or alter PROCEDURE buscar_productos_nombre
    @nom NVARCHAR(50)
AS
BEGIN
    SELECT p.IdProducto, t.Descripcion, pr.IdProveedor, p.Nombre, p.Cantidad, p.PrecioPublico, 
           p.StockMinimo, p.StockMaximo, p.Estado, a.Descripcion AS Animal, p.PrecioProveedor
    FROM producto AS p
    INNER JOIN proveedor AS pr ON p.IdProveedor = pr.IdProveedor
    INNER JOIN tipoproducto AS t ON t.IdTipoPro = p.IdTipoPro
    INNER JOIN animal AS a ON a.IdAnimal = p.IdAnimal
    WHERE p.Nombre LIKE CONCAT(@nom, '%')
END
go

CREATE or alter PROCEDURE lista_productos
AS
BEGIN
    SELECT p.IdProducto, t.Descripcion, pr.IdProveedor, p.Nombre, p.Cantidad, p.PrecioPublico, 
           p.StockMinimo, p.StockMaximo, p.Estado, a.Descripcion AS Animal, p.PrecioProveedor
    FROM producto AS p
    INNER JOIN proveedor AS pr ON p.IdProveedor = pr.IdProveedor
    INNER JOIN tipoproducto AS t ON t.IdTipoPro = p.IdTipoPro
    INNER JOIN animal AS a ON a.IdAnimal = p.IdAnimal
END
go
exec lista_productos
go

create or alter proc agregar_productos
    @IdProducto int,
    @IdTipoPro int,
    @IdProveedor int,
    @Nombre varchar(120),
    @Cantidad smallint,
    @Preciopublico decimal(10, 2),
    @StockMinimo smallint,
    @StockMaximo smallint,
    @estado TINYINT,
    @IdAnimal int,
    @PrecioProveedor decimal(10, 2)
as
begin
    insert into producto values (@IdProducto, @IdTipoPro, @IdProveedor,  @Nombre, @Cantidad, @PrecioPublico, @StockMinimo, @StockMaximo, @estado, @IdAnimal, @PrecioProveedor)
end
go

create or alter proc actualizar_productos
    @IdProducto int,
    @IdTipoPro int,
    @IdProveedor int,
    @Nombre varchar(120),
    @Cantidad smallint,
    @Preciopublico decimal(10, 2),
    @StockMinimo smallint,
    @StockMaximo smallint,
    @estado TINYINT,
    @IdAnimal int,
    @PrecioProveedor decimal(10, 2)
as
begin
    update producto set IdTipoPro = @IdTipoPro, IdProveedor = @IdProveedor, Nombre = @Nombre, Cantidad = @Cantidad, PrecioPublico = @PrecioPublico, StockMinimo = @StockMinimo, StockMaximo = @StockMaximo, estado = @estado, IdAnimal = @IdAnimal, PrecioProveedor = @PrecioProveedor
    where IdProducto = @IdProducto
end
go

create or alter proc eliminar_productos
    @IdProducto int
as
begin
    delete from producto
    where IdProducto = @IdProducto
end
go

CREATE or alter PROCEDURE usp_ingresosistemacliente
    @correo varchar(60),
    @clave varchar(250)
AS
BEGIN
    SELECT *
    FROM cliente
    WHERE Correo = @correo AND [password] = @clave
END
go

CREATE or alter PROCEDURE usp_ingresosistematrabajador
    @correo varchar(60),
    @clave varchar(250)
AS
BEGIN
    SELECT *
    FROM trabajador
    WHERE Correo = @correo AND [password] = @clave
END
go
exec usp_ingresosistematrabajador 'gibethandrea@gmail.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'
go

create or alter proc listar_proveedores
as
BEGIN
    select * from proveedor
end
go
exec listar_proveedores
go

create or alter proc listar_proveedores_empresa
    @empresa varchar(20)
AS
BEGIN
    select * from proveedor
    where Empresa like @empresa + '%'
end


GO

create or alter proc agregar_proveedores
    @idProveedor int,
    @Telefono varchar(11),
    @Direccion varchar(80),
    @Empresa varchar(20),
    @RUC varchar(11),
    @Correo varchar(60),
    @Representante varchar(60)
as
begin
    insert into proveedor values (@idProveedor, @Telefono, @Direccion, @Empresa, @RUC, @Correo, @Representante)
end
go

create or alter proc actualizar_proveedores
    @idProveedor int,
    @Telefono varchar(11),
    @Direccion varchar(80),
    @Empresa varchar(20),
    @RUC varchar(11),
    @Correo varchar(60),
    @Representante varchar(60)
as
begin
    update proveedor set Telefono = @Telefono, Direccion = @Direccion, Empresa = @Empresa, RUC = @RUC, Correo = @Correo, Representante = @Representante
    where @idProveedor = IdProveedor
end
go

create or alter proc eliminar_proveedores
    @idProveedor int
as
BEGIN
    delete from proveedor
    where IdProveedor = @idProveedor
end
go


/*AutoGenerados*/
create or alter proc usp_autogenera_idproveedor
@idproveedor int output
as
begin
	Declare @aux int = (Select top 1 IdProveedor from proveedor order by 1 desc)
	if(@aux is null)
		set @idproveedor = 1 
	else
		set @idproveedor = @aux + 1 
end
go


create or alter proc usp_autogenera_idtrabajador
@idtrabajador int output
as
begin
	Declare @aux int = (Select top 1 IdTrabajador from trabajador order by 1 desc)
	if(@aux is null)
		set @idtrabajador = 1 
	else
		set @idtrabajador = @aux + 1 
end
go

create or alter proc usp_autogenera_idcliente
@idcliente int output
as
begin
	Declare @aux int = (Select top 1 Idcliente from cliente order by 1 desc)
	if(@aux is null)
		set @idcliente = 1 
	else
		set @idcliente = @aux + 1 
end
go

create or alter proc usp_autogenera_idproducto
@idprod int output
as
begin
	Declare @aux int = (Select top 1 IdProducto from producto order by 1 desc)
	if(@aux is null)
		set @idprod = 1 
	else
		set @idprod = @aux + 1 
end
go

--baja
create or alter procedure sp_agregar_baja
@id int,
@fecha datetime,
@idtrabajador int
as
insert into baja_producto values(@id,@idtrabajador,@fecha)
go


create or alter procedure sp_detalle_baja
@id int,
@idproducto int,
@cant int,
@idtipobaja int
as
insert into detalle_baja values(@idproducto,@id,@cant,@idtipobaja)
go

/*PROBANDO PROC Y SELECTS*/
select*from animal
select*from area
select*from cargo
select*from cliente
select*from empresa_delivery
select*from producto
select*from proveedor
select*from tipobaja
select*from tipopago
select*from trabajador
go