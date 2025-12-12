CREATE OR REPLACE PROCEDURE usuarios.sp_registrar_usuario(
    IN p_nombre character varying,
    IN p_telefono character varying,
    IN p_pais_id integer,
    IN p_departamento_id integer,
    IN p_municipio_id integer,
    IN p_direccion character varying,
    OUT p_id_usuario integer
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO usuarios.usuario (
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
    )
    RETURNING id_usuario INTO p_id_usuario;
END;
$$;