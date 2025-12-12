
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
