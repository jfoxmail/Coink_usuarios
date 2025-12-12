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