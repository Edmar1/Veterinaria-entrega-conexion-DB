CREATE DATABASE db_veterinaria
GO

USE db_veterinaria
GO

CREATE TABLE Sedes
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Direccion] NVARCHAR(100) NOT NULL,
	[Barrio] NVARCHAR(50)NOT NULL,
	[Parqueadero] BIT,
	[Telefono] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Salas
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Numero] INT NOT NULL,
	[Tipo] NVARCHAR(50) NOT NULL,
	[Area] DECIMAL(18,2),
	[Id_Sede] INT NOT NULL,

	CONSTRAINT FK_IdSede_Salas FOREIGN KEY (Id_Sede) REFERENCES Sedes(Id)
);
GO

CREATE TABLE Equipos
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Marca] NVARCHAR(50) NOT NULL,
	[Modelo] NVARCHAR(50) NOT NULL,
	[Fecha_Fabricacion] DATE,
	[Fecha_Adquisicion] DATE NOT NULL,
	[Ultima_Revision] DATE NOT NULL,
	[Id_Sala] INT NOT NULL,

	CONSTRAINT FK_IdSala_Equipos FOREIGN KEY (Id_Sala) REFERENCES Salas(Id)
);
GO

CREATE TABLE Insumos
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Proveedor] NVARCHAR(50) NOT NULL,
	[Precio_Unidad] INT NOT NULL,
	[Toxico] BIT NOT NULL
);
GO

CREATE TABLE Medicamentos
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Proveedor] NVARCHAR(50) NOT NULL,
	[Precio_Unidad] INT NOT NULL,
	[Via_Administracion] NVARCHAR(50) NOT NULL,
	[Refrigeracion] BIT NOT NULL
);
GO

CREATE TABLE Empleados
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Documento] NVARCHAR(50) NOT NULL,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Cargo] NVARCHAR(50) NOT NULL,
	[Fecha_Ingreso] DATE NOT NULL,
	[Horario] NVARCHAR(50) NOT NULL,
	[Telefono] NVARCHAR(50) NOT NULL,
	[Id_Sede] INT NOT NULL,

	CONSTRAINT FK_IdSede_Empleados FOREIGN KEY (Id_Sede) REFERENCES Sedes(Id)
);
GO

CREATE TABLE Veterinarios
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Documento] NVARCHAR(50) NOT NULL,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Edad] INT NOT NULL,
	[Especialidad] NVARCHAR(50),
	[Fecha_Ingreso] DATE NOT NULL,
	[Horario] NVARCHAR(50) NOT NULL,
	[Telefono] NVARCHAR(50) NOT NULL,
	[Id_Sede] INT NOT NULL,

	CONSTRAINT FK_IdSede_Veterinarios FOREIGN KEY (Id_Sede) REFERENCES Sedes(Id)
);
GO

CREATE TABLE Propietarios
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Documento] NVARCHAR(50) NOT NULL,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Edad] INT NOT NULL,
	[Direccion] NVARCHAR(50) NOT NULL,
	[Correo] NVARCHAR(50)NOT NULL,
	[Genero] NVARCHAR(50) NOT NULL,
	[Estrato] INT NOT NULL,
	[Fecha_Registro] DATE NOT NULL
);
GO

CREATE TABLE Tel_Propietarios
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Telefono] NVARCHAR(50) NOT NULL,
	[Id_Propietario] INT NOT NULL,

	CONSTRAINT FK_IdPropietario_Tel FOREIGN KEY (Id_Propietario) REFERENCES Propietarios(Id)
);
GO

CREATE TABLE Mascotas
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Especie] NVARCHAR(50) NOT NULL,
	[Genero] NVARCHAR(50),
	[Peso] DECIMAL(18,2) NOT NULL,
	[Esterilizado] BIT,
	[Fecha_Adquisicion] DATE,
	[Fecha_Registro] DATE NOT NULL,
	[Id_propietario] INT NOT NULL,

	CONSTRAINT FK_IdPropietario_Mascotas FOREIGN KEY (Id_Propietario) REFERENCES Propietarios(Id)
);
GO

CREATE TABLE Citas
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Motivo] NVARCHAR(50) NOT NULL,
	[Fecha] DATE NOT NULL,
	[Hora] NVARCHAR(50) NOT NULL,
	[Sede] NVARCHAR(50) NOT NULL,
	[Id_Propietario] INT NOT NULL,

	CONSTRAINT FK_IdPropietario_Citas FOREIGN KEY (Id_Propietario) REFERENCES Propietarios(Id)
);
GO

CREATE TABLE Pagos
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Fecha] DATE NOT NULL,
	[Metodo] NVARCHAR(50),
	[Valor] INT NOT NULL,
	[Estado] NVARCHAR(50) NOT NULL,
	[Id_Propietario] INT NOT NULL,

	CONSTRAINT FK_IdPropietario_Pagos FOREIGN KEY (Id_Propietario) REFERENCES Propietarios(Id)
);
GO

CREATE TABLE Salas_Insumos
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Id_Sala] INT NOT NULL,
	[Id_Insumo] INT NOT NULL,

	CONSTRAINT FK_IdSala_Salin FOREIGN KEY (Id_Sala) REFERENCES Salas(Id),
	CONSTRAINT FK_IdInsumo_Salin FOREIGN KEY (Id_Insumo) REFERENCES Insumos(Id)
);
GO

CREATE TABLE Salas_Medicamentos
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Id_Sala] INT NOT NULL,
	[Id_Medicamento] INT NOT NULL,

	CONSTRAINT FK_IdSala_Salmed FOREIGN KEY (Id_Sala) REFERENCES Salas(Id),
	CONSTRAINT FK_IdMedicamento_Salmed FOREIGN KEY (Id_Medicamento) REFERENCES Medicamentos(Id)
);
GO

CREATE TABLE Veterinarios_Mascotas
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Fecha] DATE NOT NULL,
	[Id_Veterinario] INT NOT NULL,
	[Id_Mascota] INT NOT NULL,

	CONSTRAINT FK_IdVeterinario_Vetmas FOREIGN KEY (Id_Veterinario) REFERENCES Veterinarios(Id),
	CONSTRAINT FK_IdMascota_Vetmas FOREIGN KEY (Id_Mascota) REFERENCES Mascotas(Id)
);
GO



INSERT INTO Sedes (Nombre, Direccion, Barrio, Parqueadero, Telefono)
VALUES 
    ('Veterinaria Centro', 'Carrera 50 #45-20', 'El Centro', 1, '3001234567'),
    ('Veterinaria Norte', 'Calle 80 #30-15', 'Laureles', 0, '3009876543');
GO

INSERT INTO Salas (Numero, Tipo, Area, Id_Sede)
VALUES 
    (101, 'Consulta General', 25.50, 1),
    (201, 'Cirugía', 40.75, 1),
    (102, 'Consulta General', 30.00, 2),
    (202, 'Urgencias', 35.25, 2);
GO

INSERT INTO Equipos (Nombre, Marca, Modelo, Fecha_Fabricacion, Fecha_Adquisicion, Ultima_Revision, Id_Sala)
VALUES 
    ('Estetoscopio', 'Littmann', 'Classic III', '2022-03-15', '2022-06-10', '2024-01-15', 1),
    ('Mesa Quirúrgica', 'Medtek', 'MQ-500', '2021-11-20', '2022-01-25', '2024-02-10', 2),
    ('Monitor Signos Vitales', 'Philips', 'IntelliVue', '2023-05-10', '2023-07-15', '2024-01-20', 3),
    ('Lámpara Quirúrgica', 'Steris', 'Harmony', '2022-08-05', '2022-10-12', '2024-02-05', 4);
GO

INSERT INTO Insumos (Nombre, Proveedor, Precio_Unidad, Toxico)
VALUES 
    ('Jeringa 5ml', 'MedSupply', 1500, 0),
    ('Guantes Látex', 'SafeHands', 800, 0),
    ('Desinfectante', 'CleanVet', 12000, 1),
    ('Vendas', 'MedCare', 3500, 0);
GO

INSERT INTO Medicamentos (Nombre, Proveedor, Precio_Unidad, Via_Administracion, Refrigeracion)
VALUES 
    ('Amoxicilina', 'VetPharma', 25000, 'Oral', 0),
    ('Vacuna Antirrábica', 'BioVet', 45000, 'Subcutánea', 1),
    ('Meloxicam', 'AnimalHealth', 18000, 'Oral', 0),
    ('Suero Fisiológico', 'MedVet', 8000, 'Intravenosa', 1);
GO

INSERT INTO Empleados (Documento, Nombre, Cargo, Fecha_Ingreso, Horario, Telefono, Id_Sede)
VALUES 
    ('12345678', 'Ana García', 'Recepcionista', '2023-03-15', '8:00 AM - 5:00 PM', '3101234567', 1),
    ('87654321', 'Carlos López', 'Auxiliar Veterinario', '2023-01-10', '7:00 AM - 4:00 PM', '3109876543', 1),
    ('11223344', 'María Rodríguez', 'Recepcionista', '2023-05-20', '2:00 PM - 10:00 PM', '3105556677', 2),
    ('44332211', 'Luis Martínez', 'Auxiliar Veterinario', '2023-02-28', '8:00 AM - 5:00 PM', '3107778899', 2);
GO

INSERT INTO Veterinarios (Documento, Nombre, Edad, Especialidad, Fecha_Ingreso, Horario, Telefono, Id_Sede)
VALUES 
    ('98765432', 'Dr. Roberto Pérez', 45, 'Cirugía', '2022-08-15', '9:00 AM - 6:00 PM', '3201234567', 1),
    ('56789012', 'Dra. Elena Vargas', 38, 'Medicina Interna', '2023-01-20', '8:00 AM - 5:00 PM', '3209876543', 1),
    ('34567890', 'Dr. Miguel Torres', 42, 'Dermatología', '2022-11-10', '10:00 AM - 7:00 PM', '3205556677', 2),
    ('78901234', 'Dra. Carmen Silva', 36, 'Cardiología', '2023-03-05', '7:00 AM - 4:00 PM', '3207778899', 2);
GO

INSERT INTO Propietarios (Documento, Nombre, Edad, Direccion, Correo, Genero, Estrato, Fecha_Registro)
VALUES 
    ('1020304050', 'Juan Ramírez', 35, 'Calle 45 #20-30', 'juan.ramirez@email.com', 'Masculino', 3, '2023-06-15'),
    ('2030405060', 'Laura Gómez', 28, 'Carrera 70 #35-45', 'laura.gomez@email.com', 'Femenino', 4, '2023-07-20'),
    ('3040506070', 'Pedro Morales', 52, 'Calle 60 #25-40', 'pedro.morales@email.com', 'Masculino', 2, '2023-08-10'),
    ('4050607080', 'Sandra Castro', 31, 'Carrera 80 #50-25', 'sandra.castro@email.com', 'Femenino', 5, '2023-09-05');
GO

INSERT INTO Tel_Propietarios (Telefono, Id_Propietario)
VALUES 
    ('3001112233', 1),
    ('3002223344', 1),
    ('3003334455', 2),
    ('3004445566', 2),
    ('3005556677', 3),
    ('3006667788', 3),
    ('3007778899', 4),
    ('3008889900', 4);
GO

INSERT INTO Mascotas (Nombre, Especie, Genero, Peso, Esterilizado, Fecha_Adquisicion, Fecha_Registro, Id_Propietario)
VALUES 
    ('Max', 'Perro', 'Macho', 28.50, 1, '2022-01-15', '2023-06-20', 1),
    ('Luna', 'Gato', 'Hembra', 4.20, 1, '2021-08-10', '2023-06-25', 1),
    ('Rocky', 'Perro', 'Macho', 32.00, 0, '2023-03-05', '2023-07-25', 2),
    ('Mia', 'Gato', 'Hembra', 3.80, 1, '2022-12-20', '2023-08-01', 2),
    ('Toby', 'Perro', 'Macho', 25.75, 1, '2021-05-18', '2023-08-15', 3),
    ('Nala', 'Gato', 'Hembra', 3.50, 0, '2023-01-12', '2023-08-20', 3),
    ('Bruno', 'Perro', 'Macho', 22.30, 1, '2022-09-08', '2023-09-10', 4),
    ('Chloe', 'Gato', 'Hembra', 5.20, 1, '2021-11-25', '2023-09-15', 4);
GO

INSERT INTO Citas (Motivo, Fecha, Hora, Sede, Id_Propietario)
VALUES 
    ('Consulta General', '2024-03-15', '9:00 AM', 'Veterinaria Centro', 1),
    ('Vacunación', '2024-03-20', '2:00 PM', 'Veterinaria Centro', 1),
    ('Control Post-operatorio', '2024-03-18', '10:30 AM', 'Veterinaria Norte', 2),
    ('Consulta Dermatológica', '2024-03-22', '4:00 PM', 'Veterinaria Norte', 2),
    ('Chequeo General', '2024-03-25', '11:00 AM', 'Veterinaria Centro', 3),
    ('Emergencia', '2024-03-16', '6:00 PM', 'Veterinaria Norte', 3),
    ('Esterilización', '2024-03-28', '8:00 AM', 'Veterinaria Centro', 4),
    ('Control Cardiológico', '2024-03-30', '3:30 PM', 'Veterinaria Norte', 4);
GO

INSERT INTO Pagos (Fecha, Metodo, Valor, Estado, Id_Propietario)
VALUES 
    ('2024-03-15', 'Efectivo', 80000, 'Pagado', 1),
    ('2024-03-20', 'Tarjeta', 120000, 'Pagado', 1),
    ('2024-03-18', 'Transferencia', 150000, 'Pendiente', 2),
    ('2024-03-22', 'Efectivo', 95000, 'Pagado', 2),
    ('2024-03-25', 'Tarjeta', 75000, 'Pagado', 3),
    ('2024-03-16', 'Efectivo', 200000, 'Pagado', 3),
    ('2024-03-28', 'Transferencia', 300000, 'Pendiente', 4),
    ('2024-03-30', 'Tarjeta', 180000, 'Pagado', 4);
GO

INSERT INTO Salas_Insumos (Id_Sala, Id_Insumo)
VALUES 
    (1, 1), -- Sala 101 tiene Jeringa 5ml
    (1, 2), -- Sala 101 tiene Guantes Látex
    (2, 1), -- Sala 201 tiene Jeringa 5ml
    (2, 3), -- Sala 201 tiene Desinfectante
    (3, 2), -- Sala 102 tiene Guantes Látex
    (3, 4), -- Sala 102 tiene Vendas
    (4, 1), -- Sala 202 tiene Jeringa 5ml
    (4, 3); -- Sala 202 tiene Desinfectante
GO

INSERT INTO Salas_Medicamentos (Id_Sala, Id_Medicamento)
VALUES 
    (1, 1), -- Sala 101 tiene Amoxicilina
    (1, 3), -- Sala 101 tiene Meloxicam
    (2, 2), -- Sala 201 tiene Vacuna Antirrábica
    (2, 4), -- Sala 201 tiene Suero Fisiológico
    (3, 1), -- Sala 102 tiene Amoxicilina
    (3, 2), -- Sala 102 tiene Vacuna Antirrábica
    (4, 3), -- Sala 202 tiene Meloxicam
    (4, 4); -- Sala 202 tiene Suero Fisiológico
GO

INSERT INTO Veterinarios_Mascotas (Fecha, Id_Veterinario, Id_Mascota)
VALUES 
    ('2024-03-15', 1, 1), -- Dr. Roberto Pérez atendió a Max
    ('2024-03-15', 1, 2), -- Dr. Roberto Pérez atendió a Luna
    ('2024-03-20', 2, 1), -- Dra. Elena Vargas atendió a Max
    ('2024-03-18', 2, 3), -- Dra. Elena Vargas atendió a Rocky
    ('2024-03-22', 3, 4), -- Dr. Miguel Torres atendió a Mia
    ('2024-03-25', 3, 5), -- Dr. Miguel Torres atendió a Toby
    ('2024-03-28', 4, 7), -- Dra. Carmen Silva atendió a Bruno
    ('2024-03-30', 4, 8); -- Dra. Carmen Silva atendió a Chloe
GO
