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