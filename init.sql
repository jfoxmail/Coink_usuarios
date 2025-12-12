CREATE SCHEMA IF NOT EXISTS Parametros;
CREATE SCHEMA IF NOT EXISTS Usuarios;

CREATE TABLE Parametros.Pais (
    id_pais SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Parametros.Departamento (
    id_departamento SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    id_pais INT NOT NULL,
    CONSTRAINT fk_departamento_pais FOREIGN KEY (id_pais)
        REFERENCES Parametros.Pais(id_pais)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    UNIQUE (nombre, id_pais)
);

CREATE TABLE Parametros.Municipio (
    id_municipio SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    id_departamento INT NOT NULL,
    CONSTRAINT fk_municipio_departamento FOREIGN KEY (id_departamento)
        REFERENCES Parametros.Departamento(id_departamento)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    UNIQUE (nombre, id_departamento)
);

CREATE TABLE Usuarios.Usuario (
    id_usuario SERIAL PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    telefono VARCHAR(30),
    direccion VARCHAR(200),
    id_pais INT NOT NULL,
    id_departamento INT NOT NULL,
    id_municipio INT NOT NULL,
    CONSTRAINT fk_usuario_pais FOREIGN KEY (id_pais)
        REFERENCES Parametros.Pais(id_pais),
    CONSTRAINT fk_usuario_departamento FOREIGN KEY (id_departamento)
        REFERENCES Parametros.Departamento(id_departamento),
    CONSTRAINT fk_usuario_municipio FOREIGN KEY (id_municipio)
        REFERENCES Parametros.Municipio(id_municipio)
);


CREATE OR REPLACE PROCEDURE usuarios.sp_registrar_usuario(
	IN p_nombre character varying,
	IN p_telefono character varying,
	IN p_pais_id integer,
	IN p_departamento_id integer,
	IN p_municipio_id integer,
	IN p_direccion character varying)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
    INSERT INTO Usuarios.Usuario (
        nombre,
        telefono,
        id_pais,
        id_departamento,
        id_municipio,
        direccion
    )
    VALUES (
        p_nombre,
        p_telefono,
        p_pais_id,
        p_departamento_id,
        p_municipio_id,
        p_direccion
    );
END;
$BODY$;
ALTER PROCEDURE usuarios.sp_registrar_usuario(character varying, character varying, integer, integer, integer, character varying)
    OWNER TO postgres;

    INSERT INTO Parametros.Pais (nombre)
VALUES ('Colombia')
ON CONFLICT (nombre) DO NOTHING;

-- Obtener id del país
WITH pais AS (
    SELECT id_pais FROM Parametros.Pais WHERE nombre = 'Colombia'
)

INSERT INTO Parametros.Departamento (nombre, id_pais)
SELECT d.nombre, p.id_pais
FROM pais p,
     (VALUES
        ('Amazonas'),
        ('Antioquia'),
        ('Arauca'),
        ('Atlántico'),
        ('Bogotá D.C.'),
        ('Bolívar'),
        ('Boyacá'),
        ('Caldas'),
        ('Caquetá'),
        ('Casanare'),
        ('Cauca'),
        ('Cesar'),
        ('Chocó'),
        ('Córdoba'),
        ('Cundinamarca'),
        ('Guainía'),
        ('Guaviare'),
        ('Huila'),
        ('La Guajira'),
        ('Magdalena'),
        ('Meta'),
        ('Nariño'),
        ('Norte de Santander'),
        ('Putumayo'),
        ('Quindío'),
        ('Risaralda'),
        ('San Andrés y Providencia'),
        ('Santander'),
        ('Sucre'),
        ('Tolima'),
        ('Valle del Cauca'),
        ('Vaupés'),
        ('Vichada')
     ) AS d(nombre)
ON CONFLICT (nombre, id_pais) DO NOTHING;

INSERT INTO Parametros.Municipio (nombre, id_departamento) VALUES
-- Amazonas (ID: 1)
('Leticia', 1),
('Puerto Nariño', 1),

-- Antioquia (ID: 2)
('Medellín', 2),
('Bello', 2),
('Envigado', 2),
('Itagüí', 2),
('Apartadó', 2),

-- Arauca (ID: 3)
('Arauca', 3),
('Saravena', 3),

-- Atlántico (ID: 4)
('Barranquilla', 4),
('Soledad', 4),
('Malambo', 4),

-- Bogotá D.C. (ID: 5)
-- Bogotá D.C. es un distrito capital y, a efectos prácticos, actúa como un único municipio.
('Bogotá', 5),

-- Bolívar (ID: 6)
('Cartagena de Indias', 6),
('Magangué', 6),
('El Carmen de Bolívar', 6),

-- Boyacá (ID: 7)
('Tunja', 7),
('Duitama', 7),
('Sogamoso', 7),

-- Caldas (ID: 8)
('Manizales', 8),
('Villamaría', 8),
('La Dorada', 8),

-- Caquetá (ID: 9)
('Florencia', 9),
('San Vicente del Caguán', 9),

-- Casanare (ID: 10)
('Yopal', 10),
('Aguazul', 10),

-- Cauca (ID: 11)
('Popayán', 11),
('Santander de Quilichao', 11),

-- Cesar (ID: 12)
('Valledupar', 12),
('Aguachica', 12),

-- Chocó (ID: 13)
('Quibdó', 13),
('Istmina', 13),

-- Córdoba (ID: 14)
('Montería', 14),
('Lorica', 14),

-- Cundinamarca (ID: 15)
('Soacha', 15),
('Fusagasugá', 15),
('Chía', 15),
('Zipaquirá', 15),

-- Guainía (ID: 16)
('Inírida', 16),

-- Guaviare (ID: 17)
('San José del Guaviare', 17),

-- Huila (ID: 18)
('Neiva', 18),
('Pitalito', 18),

-- La Guajira (ID: 19)
('Riohacha', 19),
('Maicao', 19),

-- Magdalena (ID: 20)
('Santa Marta', 20),
('Ciénaga', 20),

-- Meta (ID: 21)
('Villavicencio', 21),
('Acacías', 21),

-- Nariño (ID: 22)
('Pasto', 22),
('Tumaco', 22),
('Ipiales', 22),

-- Norte de Santander (ID: 23)
('Cúcuta', 23),
('Ocaña', 23),

-- Putumayo (ID: 24)
('Mocoa', 24),
('Puerto Asís', 24),

-- Quindío (ID: 25)
('Armenia', 25),
('Montenegro', 25),

-- Risaralda (ID: 26)
('Pereira', 26),
('Dosquebradas', 26),

-- San Andrés y Providencia (ID: 27)
('San Andrés', 27),

-- Santander (ID: 28)
('Bucaramanga', 28),
('Floridablanca', 28),
('Barrancabermeja', 28),

-- Sucre (ID: 29)
('Sincelejo', 29),
('Corozal', 29),

-- Tolima (ID: 30)
('Ibagué', 30),
('Espinal', 30),

-- Valle del Cauca (ID: 31)
('Cali', 31),
('Palmira', 31),
('Buenaventura', 31),
('Tuluá', 31),

-- Vaupés (ID: 32)
('Mitú', 32),

-- Vichada (ID: 33)
('Puerto Carreño', 33);
