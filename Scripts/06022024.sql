

// Tabla Categoria
 Create table Categoria (
   CategoriaId int not null IDENTITY,
   Nombre  nvarchar(120) not null
  );
  
  Alter table Categoria
  Add Constraint Pk_Categoria PRIMARY KEY (CategoriaId);

// Tabla Marca
 Create table Marca (
   MarcaId int not null IDENTITY,
   Nombre  nvarchar(120) not null
  );
  
  Alter table Marca
  Add Constraint Pk_Marca PRIMARY KEY (MarcaId);

// Tabla Producto
  Create table Producto (
    ProductoId int not null IDENTITY,
	NombreProducto nvarchar(120) not null,
	Precio numeric(7,2) not null,
	Costo numeric(7,2) not null,
    CategoriaId int not null,
	MarcaId int not null
  );
  
  Alter table Producto
  Add constraint Pk_Producto PRIMARY KEY (ProductoId);
  
  ALTER Table Producto
  Add Constraint Fk_Producto_Categoria_CategoriaId FOREIGN KEY(CategoriaId)
  REFERENCES Categoria (CategoriaId) ON DELETE CASCADE;

  ALTER Table Producto
  Add Constraint Fk_Producto_Marca_MarcaId FOREIGN KEY(MarcaId)
  REFERENCES Marca (MarcaId) ON DELETE CASCADE;

   Insert into Categoria (nombre)
   values ('Computadoras');

   Insert into Categoria (nombre)
   values ('Impresoras');

   Insert into Marca (nombre)
   values ('HP');

   Insert into Marca (nombre)
   values ('Apple');
